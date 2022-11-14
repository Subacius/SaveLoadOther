using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SaveLoadSystemNaujas;
using SaveLoadSystemBuildingName;
using System.IO;

[RequireComponent(typeof(SaveableEntityBuilding))]

public class ResourcesUI : MonoBehaviour, ISaveableBuilding {

    private ResourceTypeListSO resourceTypeList;
    private Dictionary<ResourceTypeSO, Transform> resourceTypeTransformDictionary;

    private GameObject gameObjectText;

    public List<GameObject> wariorCountingRes = new List<GameObject>();
    

    private GameObject [] addToList;

    // [SerializeField] private GameObject pfWarior1;
    

    // [SerializeField] private Transform resourceTemplate;


    // private static ResourcesUI myself = null;
    private void Awake()
    {

        string path = Application.persistentDataPath + "/saves/Building.save";

        if ( File.Exists(path)) {
                Debug.Log("yra failas " + path);
                SaveLoadSystemBuilding.saveName = "Building.save";
                SaveLoadSystemBuilding.Load();

        } else {
        
        resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        resourceTypeTransformDictionary = new Dictionary<ResourceTypeSO, Transform>();
        Transform resourceTemplate = transform.Find("resourceTemplate");
        resourceTemplate.gameObject.SetActive(false);


        int index = 0;
        foreach (ResourceTypeSO resourceType in resourceTypeList.list){
            Transform resourceTransform = Instantiate(resourceTemplate,transform);
            resourceTransform.gameObject.SetActive(true);
            float offsetAmount = -160f;
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
            resourceTransform.Find("image").GetComponent<Image>().sprite = resourceType.sprite;

            resourceTypeTransformDictionary[resourceType] = resourceTransform;
            index ++;

            }

        addToList = GameObject.FindGameObjectsWithTag("resourceTemplate");
            foreach( GameObject go in addToList) {
            wariorCountingRes.Add(go);
            }
        }


    }

    private void Start(){
        // DontDestroyOnLoad(gameObject);
        ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
        string path = Application.persistentDataPath + "/saves/Building.save";

            if ( File.Exists(path)) {
                Debug.Log("yra failas " + path);
                // SaveLoadSystemBuilding.saveName = "Building.save";
                // SaveLoadSystemBuilding.Load();

            } else {
                UpdateResourceAmount();
            }

        // DontDestroyOnLoad(gameObject);
    }

    private void ResourceManager_OnResourceAmountChanged(object sender, System.EventArgs e){
        UpdateResourceAmount();
    }

    private void UpdateResourceAmount(){
        
        foreach (ResourceTypeSO resourceType in resourceTypeList.list){

            Transform resourceTransform = resourceTypeTransformDictionary[resourceType];
            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);
            // Debug.Log(resourceAmount + " resource amount tikrinam");
            resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
        }
    }

 //------------------------------------
    // ISaveable implementation...
    //------------------------------------

    // Create a Serializable struct which contains all sorable data:
    // You don't need to save the location, rotation and scale, this will be done behind the scenes ;)
    [System.Serializable]
    struct PlayerDataCounterOtherRes
    {
        // public int counteris;
        // public SaveableEntity.Vector3Data direction;
        public string gameObjectText;

        // public int resourceAmount;
        // public Dictionary<PlayerTypeSO, Transform> wariorTypeTransformDictionary;

        // public Dictionary<PlayerTypeSO, Transform> tempDictionary;

        public List<string> resCounting;

        public List<int> resIntCount;




        // public RectTransform rectTransform;
        

    }
    public object SaveState()
    {
        gameObjectText = GameObject.Find("resourceTemplate(Clone)/text");
        List<string> gameObjectsResources = new List<string>();
            foreach (GameObject go in wariorCountingRes) {
            gameObjectsResources.Add(go.GetComponent<SaveableEntityBuilding>().GetID());
            // Debug.Log(go.GetComponent<SaveableEntityBuilding>().GetID());
            // Debug.Log(gameObjectText.GetComponent<TMPro.TextMeshProUGUI>().text + "ressssssssss");
            }

            //
        List<int> intResCount = new List<int>();
            foreach (ResourceTypeSO resourceType in resourceTypeList.list){

            Transform resourceTransform = resourceTypeTransformDictionary[resourceType];
            int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);
            Debug.Log(resourceAmount + " resource amount tikrinam");
            resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
            }
        
        
        // Instantiate the struct which contains the data we want to save and return it as object
        return new PlayerDataCounterOtherRes() {
            // counteris = counteris,
            // tempDictionary = wariorTypeTransformDictionary,
            gameObjectText = gameObjectText.GetComponent<TMPro.TextMeshProUGUI>().text,
            resCounting = gameObjectsResources,
            resIntCount = intResCount,
            // resourceAmount = PlayerManagerAll.Instance.GetResourceAmount(pfPlayer1),
            // wariorTypeTransformDictionary = wariorTypeTransformDictionary,

            
            // rectTransform = rectTransform,

            // direction = new SaveableEntity.Vector3Data(transform.position),

        };
    }
    public void LoadState(object state)
    {

        PlayerDataCounterOtherRes data = (PlayerDataCounterOtherRes)state; // Receive a object which we need to
                                             // cast to extract our loaded data

        // this.counteris = data.counteris;
        gameObjectText = GameObject.Find("resourceTemplate(Clone)/text");
        gameObjectText.GetComponent<TMPro.TextMeshProUGUI>().text = data.gameObjectText;
        Debug.Log( gameObjectText.GetComponent<TMPro.TextMeshProUGUI>().text);
        // Debug.Log(gameObjectText.GetComponent<TMPro.TextMeshProUGUI>().text + " text mesh pro results ressssssssssss");
        // wariorTypeTransformDictionary = data.tempDictionary;

        // this.wariorTypeTransformDictionary = data.wariorTypeTransformDictionary;




        // this.rectTransform = data.rectTransform;

        // transform.position = data.direction.ToVector3();

    }
    public bool NeedsToBeSaved()
    {
        // If this GameObject has a parent which also inherits from ISaveable,
        // than any return value of of childs will be ignored, because the parent will decide if the whole Object structure
        // gets saved or not.

        // Otherwise:
        // If true gets returned, this GameObject will be saved
        // If false gets returned, this GameObject will be ignored when saving
        return true;
    }

    // Return true, if this object needs to be reinstantiated at load or false if the loading is enough
    public bool NeedsReinstantiation()
    {
        return true;
    }
    public void GotAddedAsChild(GameObject obj, GameObject hisParent)
    {
        // This function lets you know, that somewere deeper in the hirarchy of this GameObject a loaded GameObject got added to the structure
        // You may want to know that so you can setup the relations you may need


    }

    public void PostInstantiation(object state)
    {
        PlayerDataCounterOtherRes data = (PlayerDataCounterOtherRes)state; // Receive a object which we need to cast to extract our loaded data
        // Will be called after all Objects got loaded and Parents of each Objects are set.
        // You may need to do stuff like in GotAddedAsChild(...) but with objects which are not childs of this
        // Then you do this stuff here, because here it is guaranteed, that all objects got Instantiated
        // You can find your target objects using:
        // SaveableEntity.FindID(string id) -> this will search all objects by the ID
        // just grab the .gameObject of the returned Object and use it
        // You may want to store the targets ID, so you can load back to the right object.
        // Call: obj.id on the SaveableEntity component of your target to get the ID

        List<string> gameObjectsResources = data.resCounting;
        List<GameObject> foundEnemiesRes = new List<GameObject>();
        foreach (string go in gameObjectsResources)
        {
            // Search for Objects with a given ID
            SaveableEntityBuilding obj = SaveableEntityBuilding.FindID(go);
            if(obj)
            {
                // Found a anemy with the saved ID
                foundEnemiesRes.Add(obj.gameObject);
                Debug.Log("RES TEmplate with ID: " + go + " found.");
            }
            else
            {
                Debug.Log("No RES TEmplat with ID: " + go + " found.");
            }
        }
        wariorCountingRes = foundEnemiesRes;




        // List<int> gameObjectsResourcesInt = data.resIntCount;
        // List<GameObject> foundEnemiesResInt = new List<GameObject>();
        // foreach (int goInt in gameObjectsResourcesInt)
        // {
        //     // Search for Objects with a given ID
        //     SaveableEntityBuilding objInt = SaveableEntityBuilding.FindID(goInt);
        //     if(objInt)
        //     {
        //         // Found a anemy with the saved ID
        //         foundEnemiesResInt.Add(objInt.gameObject);
        //         Debug.Log("RES TEmplate with ID: " + goInt + " found.");
        //     }
        //     else
        //     {
        //         Debug.Log("No RES TEmplat with ID: " + goInt + " found.");
        //     }
        // }
        // wariorCountingRes = foundEnemiesResInt;
        // Debug.Log(wariorCounting + " wariorcounting");
    }


}
