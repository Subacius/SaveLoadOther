using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceNearbyOverlay : MonoBehaviour
{
    private ResourceGeneratorData resourceGeneratorData;

    private void Awake() {
        Hide();
        

        // Show();
        
    }

    private void Update () {
        int nearbyResourceAmount = ResourceGenerator.GetNearbyResourceAmount (resourceGeneratorData, transform.position);
        float percent = Mathf.RoundToInt((float)nearbyResourceAmount / resourceGeneratorData.maxResourceAmount * 100f);
        transform.Find("text").GetComponent<TextMeshPro>().SetText(percent + "%");

      
    }
    public void Show(ResourceGeneratorData resourceGeneratorData) {

        if(resourceGeneratorData.resourceType == null)
        {
            return;
        } else {
            this.resourceGeneratorData = resourceGeneratorData;
            gameObject.SetActive(true);

            transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
        // Debug.Log("SHOWWWWWW");
        }
       

        

    }

    public void Hide() {
        gameObject.SetActive(false);
        // Debug.Log("pakviestas hide in resourcenearbyoverlay");
    }
}
