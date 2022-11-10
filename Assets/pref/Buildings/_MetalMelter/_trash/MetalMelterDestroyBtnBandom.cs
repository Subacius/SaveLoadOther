using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MetalMelterDestroyBtnBandom : MonoBehaviour {

[SerializeField] private GameObject buildingTypeForOpenPanel;

private void Awake() {

    transform.Find("pfBuildingDemolishBtn").GetComponent<Button>().onClick.AddListener(() => {
        // buildingTypeForOpenPanel = GameObject.Find("pfMetal Melter(Clone)/pfBuildingDemolishBtn");


        Canvas canvas = buildingTypeForOpenPanel.GetComponent<Canvas>();
        canvas.enabled = true;
        // buildingTypeForOpenPanel.SetActive(true);

    });
}

}
