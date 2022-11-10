using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Building : MonoBehaviour
{
    private Transform buildingDemolishBtn;

    private void Awake() {

        buildingDemolishBtn = transform.Find("pfBuildingDemolishBtn");

        HideBuildingDemolishBtn();
    }

    void Start()
    {

    }
    private void OnMouseEnter() {
        ShowBuildingDemolishBtn();
    }

    private void OnMouseExit() {
        HideBuildingDemolishBtn();
    }

    private void ShowBuildingDemolishBtn() {
          if (buildingDemolishBtn !=null) {
            buildingDemolishBtn.gameObject.SetActive(true);
        }
    }

    private void HideBuildingDemolishBtn() {
          if (buildingDemolishBtn !=null) {
            buildingDemolishBtn.gameObject.SetActive(false);
        }
    }


  
}
