using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SaveLoadSystemNaujas;
using System.IO;
using SaveLoadSystemBuildingName;

public class ResourceManager : MonoBehaviour
{
    //singleton pattern  // static instante field  // in Awake nepamirstan Instance = this;
    public static ResourceManager Instance {get; private set;}
    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;


    public event EventHandler OnResourceAmountChanged;

    //adding starting resources
    [SerializeField] private List<ResourceAmount> startingResourceAmountList;
    //
  


    private void Awake()
    {

        // SaveLoadSystem.saveName = "Save_1.save";


        // SaveLoadSystem.Load();


        Instance = this;
        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();

        ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        foreach (ResourceTypeSO resourceType in resourceTypeList.list)
        {
            resourceAmountDictionary[resourceType] = 0;
        }

        foreach (ResourceAmount resourceAmount in startingResourceAmountList) {
            AddResource(resourceAmount.resourceType, resourceAmount.amount);

        } 

       //adding starting resources


        // string path = Application.persistentDataPath + "/saves/Building.save";
        // // Debug.Log(path + " path");
        // // Debug.Log(File.Exists(path) + " exits");

        //     if ( !File.Exists(path)) {
        //         // Debug.Log("yra failas " + path);
        //         Debug.Log("nera--------------");
        //         foreach (ResourceAmount resourceAmount in startingResourceAmountList) {
        //             AddResource(resourceAmount.resourceType, resourceAmount.amount);

        //         } 

        //     }


        // TestLogResourceAmountDictionary();

        
    }

    private void Start() {

    }

    //for testing
    private void Update ()
    {
        // if (Input.GetKeyDown(KeyCode.T))
        // {
        //     ResourceTypeListSO resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
        //     AddResource(resourceTypeList.list[0], 2);
        //     TestLogResourceAmountDictionary();

        // }
        // SpendResources();
    }
    private void TestLogResourceAmountDictionary()
    {
        foreach (ResourceTypeSO resourceType in resourceAmountDictionary.Keys)
        {
            Debug.Log(resourceType.nameString + " : " + resourceAmountDictionary[resourceType]);
        }
    }

    

    public void AddResource(ResourceTypeSO resourceType, int amount)
    {
        resourceAmountDictionary[resourceType] += amount;
        //?.Invoke patikrina ar ne null 
        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetResourceAmount(ResourceTypeSO resourceType) {
        return resourceAmountDictionary[resourceType];
    }

    public bool CanAfford(ResourceAmount[] resourceAmountArray) {
        foreach (ResourceAmount resourceAmount in resourceAmountArray) {
            if(GetResourceAmount(resourceAmount.resourceType) >= resourceAmount.amount) {
                //Can afford
            } else {
                //cannot
                return false;
            }
        }
        return true;
    }

      public void SpendResources(ResourceAmount[] resourceAmountArray) {
        foreach (ResourceAmount resourceAmount in resourceAmountArray) {
            resourceAmountDictionary[resourceAmount.resourceType] -= resourceAmount.amount;
        }
      }
}
