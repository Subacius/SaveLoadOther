using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildingConstruction : MonoBehaviour
{

    
    public static BuildingConstruction Create(Vector3 worldPosition, Vector2Int origin, PlacedObjectTypeSO placedObjectTypeSO) {
        Transform pfBuildingConstruction = Resources.Load<Transform>("pfBuildingConstruction");
        Transform buildingConstructionTransform = Instantiate(pfBuildingConstruction, worldPosition, Quaternion.identity);
        BuildingConstruction buildingConstruction = buildingConstructionTransform.GetComponent<BuildingConstruction>();
        buildingConstruction.SetBuildingType2( placedObjectTypeSO, origin);
        return buildingConstruction;
    }

    //     public static PlacedObject Create(Vector3 worldPosition, Vector2Int origin, PlacedObjectTypeSO placedObjectTypeSO) {
    //     Transform placedObjectTransform = Instantiate(placedObjectTypeSO.prefab, worldPosition, Quaternion.identity);

    //     PlacedObject placedObject = placedObjectTransform.GetComponent<PlacedObject>();
    //     placedObject.Setup(placedObjectTypeSO, origin);
    //     // Debug.Log("paziurim cia 1");

    //     return placedObject;

        
    // }

    public float constructionTimer;
    private float constructionTimerMax;

    private PlacedObjectTypeSO placedObjectTypeSO;

    private PlacedObject placedObject;


    private BoxCollider boxCollider;

    private BuildingTypeHolder buildingTypeHolder;

    private Material constructionMaterial;

    private MeshRenderer meshRenderer;

    private Transform transformeris;

    private Vector2Int origin;

    // private PlacedObjectTypeSO.Dir dir;


    private void Awake() {
    //    boxCollider = GetComponent<BoxCollider>();
    boxCollider = GetComponent<BoxCollider>();
    transformeris = transform.Find("cube").GetComponent<Transform>();
    meshRenderer = transform.Find("cube").GetComponent<MeshRenderer>();
       
    buildingTypeHolder = GetComponent<BuildingTypeHolder>();
    //    Instantiate(placedObjectTypeSO.visual, transform.position, Quaternion.identity);
    constructionMaterial = meshRenderer.material;

    }

    


    private void Update() {
        constructionTimer -= Time.deltaTime;
        
        constructionMaterial.SetFloat("_Progress", GetconstructionTimerNormalized());

        

        // Instantiate(placedObjectTypeSO.exVisual, transform.position, Quaternion.identity);

        if (constructionTimer <= 0f) {
           
            // Instantiate(placedObjectTypeSO.prefab, transform.position,Quaternion.identity);
           

           

            Instantiate(placedObjectTypeSO.prefab, transform.position, Quaternion.identity);
            
           
            Destroy(gameObject);
            
            
        } 
    }

    private void SetBuildingType2( PlacedObjectTypeSO placedObjectTypeSO, Vector2Int origin) {
        this.placedObjectTypeSO = placedObjectTypeSO;
        this.origin = origin;
        
        constructionTimerMax = placedObjectTypeSO.constructionTimerMax;
        constructionTimer = constructionTimerMax;

        boxCollider.center = placedObjectTypeSO.exVisual.GetComponent<BoxCollider>().center;
        boxCollider.size = placedObjectTypeSO.exVisual.GetComponent<BoxCollider>().size;

        

        

      

       
        // Instantiate(placedObjectTypeSO.prefab, transform.position, Quaternion.identity);

       

        // transformeris = placedObjectTypeSO.exVisual;
        
        // transformeris = placedObjectTypeSO.exVisual;

        // boxCollider2D.offset = placedObjectTypeSO.prefab.GetComponent<BoxCollider2D>().offset;
        // boxCollider2D.size = placedObjectTypeSO.prefab.GetComponent<BoxCollider2D>().size;
        
        
         
    }

    public float GetconstructionTimerNormalized() {
        return 1 - constructionTimer / constructionTimerMax;
    }


    
    public void DestroySelfBuildingConstruction() {
        Destroy(gameObject, constructionTimer);
    }

     public void DestroySelf() {
        Destroy(gameObject);
    }


    public float TimerTimerTimer(){
        return constructionTimer;
    }


    

}
