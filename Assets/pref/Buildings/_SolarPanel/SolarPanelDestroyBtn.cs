using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SolarPanelDestroyBtn : MonoBehaviour {

[SerializeField] private GameObject buildingTypeForOpenPanel;

private void Awake() {

    transform.Find("destroyBtn").GetComponent<Button>().onClick.AddListener(() => {
        // buildingTypeForOpenPanel = GameObject.Find("pfMetal Melter/MetalMelterPopUpCanvas");

        buildingTypeForOpenPanel = GameObject.Find("pfBuildingDemolishBtn");


        Canvas canvas = buildingTypeForOpenPanel.GetComponent<Canvas>();
        canvas.enabled = true;
        // buildingTypeForOpenPanel.SetActive(true);

    });
}

}