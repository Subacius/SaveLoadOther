using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhost : MonoBehaviour {

    private Transform visual;

    private ResourceNearbyOverlay resourceNearbyOverlay;


    private void Awake() {

        resourceNearbyOverlay = transform.Find("pfResourceNearbyOverlay").GetComponent<ResourceNearbyOverlay>();
        
    }

    private void Start() {
        RefreshVisual();

        GridBuildingSystem.Instance.OnSelectedChanged += GridBuildingSystem_OnSelectedChanged;
        
      
    }

    private void GridBuildingSystem_OnSelectedChanged(object sender, GridBuildingSystem.OnSelectedEventArgs e) {
        
        RefreshVisual();
        if (e.activebuildingGhostui == null) {
            resourceNearbyOverlay.Hide();
        } else {
            resourceNearbyOverlay.Show(e.activebuildingGhostui.resourceGeneratorData);
        }

    }

    private void LateUpdate() {
        Vector3 targetPosition = GridBuildingSystem.Instance.GetMouseWorldSnappedPosition();
        targetPosition.y = 1f;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 15f);

        // transform.rotation = Quaternion.Lerp(transform.rotation, GridBuildingSystem.Instance.GetPlacedObjectRotation(), Time.deltaTime * 15f);
    }

    private void RefreshVisual() {
        if (visual != null) {
            Destroy(visual.gameObject);
            visual = null;
        }
       
     

        PlacedObjectTypeSO placedObjectTypeSO = GridBuildingSystem.Instance.GetPlacedObjectTypeSO();

        if (placedObjectTypeSO != null) {
            visual = Instantiate(placedObjectTypeSO.visual, Vector3.zero, Quaternion.identity);
            visual.parent = transform;
            visual.localPosition = Vector3.zero;
            visual.localEulerAngles = Vector3.zero;
            SetLayerRecursive(visual.gameObject, 9);
        }
    }

    private void SetLayerRecursive(GameObject targetGameObject, int layer) {
        targetGameObject.layer = layer;
        foreach (Transform child in targetGameObject.transform) {
            SetLayerRecursive(child.gameObject, layer);
        }
    }

    public void Hide() {
        gameObject.SetActive(false);
        
    }

}

