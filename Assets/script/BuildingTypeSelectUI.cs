using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BuildingTypeSelectUI : MonoBehaviour
{
    //  public event EventHandler OnSelectedChanged;
    private PlacedObjectTypeSO placedObjectTypeSO;
    [SerializeField] private List<PlacedObjectTypeSO> placedObjectTypeSOList;
    // [SerializeField] private GridBuildingSystem gridBuildingSystem;
    // [SerializeField] private BuildingGhost buildingGhost;

    private Dictionary<PlacedObjectTypeSO, Transform> btnTransformDictionary;


    private void Awake() 
    {
        // DontDestroyOnLoad(gameObject);
        Transform buildingButtonTemplate = transform.Find("btnTemplate");
        buildingButtonTemplate.gameObject.SetActive(false);

        // PlacedObjectTypeSO placedObjectTypeSOlist = Resources.Load<PlacedObjectTypeSO> (typeof(PlacedObjectTypeSO).Name);
        btnTransformDictionary = new Dictionary<PlacedObjectTypeSO, Transform>();
        int index = 0;
        foreach (PlacedObjectTypeSO activeplacedObjectTypeSO in placedObjectTypeSOList)
        {
            Transform buildingBtnTransform = Instantiate(buildingButtonTemplate, transform);
            buildingBtnTransform.gameObject.SetActive(true);

            float offsetAmount = +120f;

            buildingBtnTransform.GetComponent<RectTransform>().anchoredPosition += new Vector2(index * offsetAmount, 0);

            buildingBtnTransform.Find("image").GetComponent<Image>().sprite = activeplacedObjectTypeSO.sprite;

            buildingBtnTransform.GetComponent<Button>().onClick.AddListener(() => 
            {
                 GridBuildingSystem.Instance.SetActiveBuildingType(activeplacedObjectTypeSO);
            });

            MouseEnterExitEvents mouseEnterExitEvents =  buildingBtnTransform.GetComponent<MouseEnterExitEvents>();
            mouseEnterExitEvents.OnMouseEnter += (object sender, EventArgs e) => { 
                TooltipUI.Instance.Show(activeplacedObjectTypeSO.nameString + " \n" + activeplacedObjectTypeSO.GetConstructionResourceCostString());
            };

               mouseEnterExitEvents.OnMouseExit += (object sender, EventArgs e) => { 
                TooltipUI.Instance.Hide();
            };
            

            btnTransformDictionary[activeplacedObjectTypeSO] = buildingBtnTransform;
            index++;
        }
    }

    private void Update() {
        UpdateActiveBuildingTypeButton();
    }

    private void UpdateActiveBuildingTypeButton() {
        foreach (PlacedObjectTypeSO placedObjectTypeSO in btnTransformDictionary.Keys) {
            Transform buildingBtnTransform = btnTransformDictionary[placedObjectTypeSO];
            buildingBtnTransform.Find("selected").gameObject.SetActive(false);
        }

        PlacedObjectTypeSO activeplacedObjectTypeSO = GridBuildingSystem.Instance.GetPlacedObjectTypeSO();
        if (  activeplacedObjectTypeSO !=null) {
            btnTransformDictionary[activeplacedObjectTypeSO].Find("selected").gameObject.SetActive(true);
        }

    }
}
