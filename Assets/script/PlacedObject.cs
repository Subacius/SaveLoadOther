using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObject : MonoBehaviour {

    public static PlacedObject Create(Vector3 worldPosition, PlacedObjectTypeSO placedObjectTypeSO) {
        Transform placedObjectTransform = Instantiate(placedObjectTypeSO.prefab, worldPosition, Quaternion.identity);

        PlacedObject placedObject = placedObjectTransform.GetComponent<PlacedObject>();
        placedObject.Setup(placedObjectTypeSO);
        // Debug.Log("paziurim cia 1");

        return placedObject;

        
    }

    private PlacedObjectTypeSO placedObjectTypeSO;
    private Vector2Int origin;
    private PlacedObjectTypeSO.Dir dir;

    private BuildingConstruction buildingConstruction;

    // private void Setup(PlacedObjectTypeSO placedObjectTypeSO, Vector2Int origin) {
    //     this.placedObjectTypeSO = placedObjectTypeSO;
    //     this.origin = origin;

        
    // }

      private void Setup(PlacedObjectTypeSO placedObjectTypeSO) {
        this.placedObjectTypeSO = placedObjectTypeSO;
        // this.origin = origin;
        
        
    }

    private void Update() {

        
    }

    // public List<Vector2Int> GetGridPositionList() {
    //     return placedObjectTypeSO.GetGridPositionList(origin);
    // }

      public List<Vector2Int> GetGridPositionList() {
        return placedObjectTypeSO.GetGridPositionList(origin, dir);
    }

    public void DestroySelf() {
        Destroy(gameObject);
    }

    public override string ToString() {
        return placedObjectTypeSO.nameString;
    }

}