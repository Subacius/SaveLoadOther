using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MetalMelterDestroyBtn : MonoBehaviour {

[SerializeField] private GameObject buildingTypeForOpenPanel;
// private Transform buildingDemolishBtn;

private void Awake() {

    // buildingTypeForOpenPanel = GameObject.Find("pfBuildingDemolishBtn");

    // buildingTypeForOpenPanel.gameObject.SetActive(false);

    transform.Find("destroyBtn").GetComponent<Button>().onClick.AddListener(() => {
        // buildingTypeForOpenPanel = GameObject.Find("pfMetal Melter/MetalMelterPopUpCanvas");

        

        // buildingTypeForOpenPanel.SetActive(true);


        // Canvas canvas = buildingTypeForOpenPanel.GetComponent<Canvas>();
        // canvas.enabled = true;
        

    });

    }
    // private void OnMouseEnter() {
    //     buildingTypeForOpenPanel.gameObject.SetActive(true);
    // }

    // private void OnMouseExit() {
    //     buildingTypeForOpenPanel.gameObject.SetActive(false);
    // }
}


