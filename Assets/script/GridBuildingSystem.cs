using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using CodeMonkey.Utils;


public class GridBuildingSystem : MonoBehaviour {

    public static GridBuildingSystem Instance { get; private set; }

    public event EventHandler<OnSelectedEventArgs> OnSelectedChanged;

    public class OnSelectedEventArgs : EventArgs {
        public PlacedObjectTypeSO activebuildingGhostui;
    }
    public event EventHandler OnObjectPlaced;





    private GridXZ<GridObject> grid;
    [SerializeField] private List<PlacedObjectTypeSO> placedObjectTypeSOList = null;
    private PlacedObjectTypeSO activeplacedObjectTypeSO;
    private PlacedObjectTypeSO.Dir dir;

    private BuildingConstruction buildingConstruction;

    [SerializeField] private LayerMask mm_layerMask;

    private MeshRenderer meshRenderer;

    

    private void Awake() {

        Instance = this;

        buildingConstruction = GetComponent<BuildingConstruction>();
        

        int gridWidth = 10;
        int gridHeight = 10;
        float cellSize = 10f;
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(0, 0, 0), (GridXZ<GridObject> g, int x, int y) => new GridObject(g, x, y));

        activeplacedObjectTypeSO = null; //placedObjectTypeSOList[0];

        // SaveLoadSystem.saveName = "Building.save";
        // Debug.Log(SaveLoadSystem.saveName + " saved name");

        // string path = Application.persistentDataPath + "/saves/Building.save";
        // // Debug.Log(path + " path");
        // // Debug.Log(File.Exists(path) + " exits");

        //     if ( File.Exists(path)) {
        //         // Debug.Log("yra failas " + path);
        //         SaveLoadSystem.saveName = "Building.save";
        //         SaveLoadSystem.Load();

        //     } else {

        //         // Debug.Log(path + " Dosnt exits ");
        //     }

    }

  

    public class GridObject {

        private GridXZ<GridObject> grid;
        private int x;
        private int y;
        public PlacedObject placedObject;

        public BuildingConstruction buildingConstruction;

        public GridObject(GridXZ<GridObject> grid, int x, int y) {
            this.grid = grid;
            this.x = x;
            this.y = y;
            placedObject = null;
        }

        // public override string ToString() {
        //     return x + ", " + y + "\n" + placedObject;
        // }

         public override string ToString() {
            return x + ", " + y + "\n" + buildingConstruction;
        }

        public void SetPlacedObject(PlacedObject placedObject) {
            this.placedObject = placedObject;
            grid.TriggerGridObjectChanged(x, y);
        }

         public void SetPlacedObjectBuildingShader(BuildingConstruction buildingConstruction) {
            this.buildingConstruction = buildingConstruction;
            grid.TriggerGridObjectChanged(x, y);
        }

        public void ClearPlacedObject() {
            placedObject = null;
            grid.TriggerGridObjectChanged(x, y);
        }

        public PlacedObject GetPlacedObject() {
            return placedObject;
        }

        public bool CanBuild() {
            return placedObject == null;
        }


    }

    private void OnApplicationQuit()
    {
        // Working

        // SaveLoadSystem.saveName = "Building.save";
        // Debug.Log(SaveLoadSystem.saveName + " save building");
        // SaveLoadSystem.SaveNew();


    }

    public void buttonCheck() {

        SaveLoadSystem.SaveLoadSystem.saveName = "Building.save";
        Debug.Log(SaveLoadSystem.SaveLoadSystem.saveName + " save building");
        SaveLoadSystem.SaveLoadSystem.SaveNew();
    }

    public void buttonCheckLoad() {
        SaveLoadSystem.SaveLoadSystem.saveName = "Building.save";

        SaveLoadSystem.SaveLoadSystem.Load();
    }



    private void Start() {

        // DontDestroyOnLoad(gameObject);






        //pagrindines bazes pastatas start metu

        // Vector3 placedObjectWorldPosition = grid.GetWorldPosition(0, 0);

        //    Vector2Int placedObjectOrigin22 = new Vector2Int(0, 0);
        //     placedObjectOrigin22 = grid.ValidateGridPosition(placedObjectOrigin22);

        //     // Test Can Build
        //     List<Vector2Int> gridPositionList = placedObjectTypeSOList[4].GetGridPositionList(placedObjectOrigin22, dir);
            
        

        // PlacedObject placedObject = PlacedObject.Create(placedObjectWorldPosition, placedObjectTypeSOList[3]);

        // grid.GetGridObject(1, 1).SetPlacedObject(placedObject);
        // grid.GetGridObject(0, 1).SetPlacedObject(placedObject);
        // grid.GetGridObject(0, 0).SetPlacedObject(placedObject);
        // grid.GetGridObject(1, 0).SetPlacedObject(placedObject);

    }

    private void Update() {
        if (Input.GetMouseButtonDown(0) && activeplacedObjectTypeSO != null && !EventSystem.current.IsPointerOverGameObject()) {
            

            
            Vector3 mousePosition = Mouse3D.GetMouseWorldPosition();
            
            grid.GetXZ(mousePosition, out int x, out int z);

            Vector2Int placedObjectOrigin = new Vector2Int(x, z);
            placedObjectOrigin = grid.ValidateGridPosition(placedObjectOrigin);

            // Test Can Build
            List<Vector2Int> gridPositionList = activeplacedObjectTypeSO.GetGridPositionList(placedObjectOrigin, dir);
            bool canBuild = true;
            foreach (Vector2Int gridPosition in gridPositionList) {
                if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild()) {
                    canBuild = false;
                    break;
                }

              
            }

            
            // Debug.Log("paziurim cia 2");
           


            if (canBuild && MyCollisions(activeplacedObjectTypeSO, Mouse3D.GetMouseWorldPosition())) {

                
                

                // Vector2Int rotationOffset = activeplacedObjectTypeSO.GetRotationOffset(dir);
                Vector3 placedObjectWorldPosition = grid.GetWorldPosition(placedObjectOrigin.x, placedObjectOrigin.y);
                
                

                if (ResourceManager.Instance.CanAfford(activeplacedObjectTypeSO.constructionResourceCostArray )) {
                ResourceManager.Instance.SpendResources(activeplacedObjectTypeSO.constructionResourceCostArray);
                //buildingconstruction darant panaikinam šitą
                // PlacedObject placedObject = PlacedObject.Create(placedObjectWorldPosition, placedObjectOrigin, dir, activeplacedObjectTypeSO);

                    BuildingConstruction buildingConstruction = BuildingConstruction.Create(placedObjectWorldPosition, placedObjectOrigin, activeplacedObjectTypeSO);
                   
                    foreach (Vector2Int gridPosition in gridPositionList) {
                    
                    // PlacedObject placedObject = PlacedObject.Create(placedObjectWorldPosition, activeplacedObjectTypeSO);
                    
                    
                    //
                    grid.GetGridObject(gridPosition.x, gridPosition.y).SetPlacedObjectBuildingShader(buildingConstruction);

                     OnObjectPlaced?.Invoke(this, EventArgs.Empty);

                    
                    }
                    
                      
                 
                   

               
            } else {
                UtilsClass.CreateWorldTextPopup("Not Enough Minerals!" + activeplacedObjectTypeSO.GetConstructionResourceCostString(), mousePosition);
            }

            } else {
                // Cannot build here
                UtilsClass.CreateWorldTextPopup("Cannot Build Here!", mousePosition);
                // DeselectObjectType();w
            }

            DeselectObjectType();
            // Debug.Log("paziurim cia 3");
        }
       

        if (Input.GetKeyDown(KeyCode.Escape)) { DeselectObjectType(); }

    }

    

    public void DeselectObjectType() {
        activeplacedObjectTypeSO = null; 
        RefreshSelectedObjectType();
    }

    public void RefreshSelectedObjectType() {
        OnSelectedChanged?.Invoke(this, new OnSelectedEventArgs { activebuildingGhostui = activeplacedObjectTypeSO });
        
    }


    public Vector2Int GetGridPosition(Vector3 worldPosition) {
        grid.GetXZ(worldPosition, out int x, out int z);
        return new Vector2Int(x, z);
    }

    public Vector3 GetMouseWorldSnappedPosition() {
        Vector3 mousePosition = Mouse3D.GetMouseWorldPosition();
        grid.GetXZ(mousePosition, out int x, out int z);

        if (activeplacedObjectTypeSO != null) {
            // Vector2Int rotationOffset = activeplacedObjectTypeSO.GetRotationOffset(dir);
            Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, z);
            return placedObjectWorldPosition;
            
        } else {
            return mousePosition;
        }
        
    }

    public void SetActiveBuildingType (PlacedObjectTypeSO buildingType) {
        
        activeplacedObjectTypeSO = buildingType;
        RefreshSelectedObjectType(); 
        }

    public PlacedObjectTypeSO GetPlacedObjectTypeSO() {
       
        return activeplacedObjectTypeSO;
        
       
    }

    private bool MyCollisions(PlacedObjectTypeSO placedObjectTypeSO, Vector3 position){

        BoxCollider boxCollider = placedObjectTypeSO.prefab.GetComponent<BoxCollider>();

        Collider[] hitColliders = Physics.OverlapBox(position, boxCollider.size / 2, Quaternion.identity, mm_layerMask);

        foreach (Collider collider in hitColliders) {

        }
        return hitColliders.Length ==0;
    }





   

}