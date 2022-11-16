using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using SaveLoadSystem;


//[RequireComponent(typeof(SaveableEntity))]

public class ResourcesUI : MonoBehaviour//, ISaveable 
{

    // >> 
    // >> This is the correct way to get a template object (Prefab)
    // >> You don't add it as a child in the editor, you add it as a serialized field like this, 
    // >> so you can see the slot in the unity editor. Drag and drop the prefab you want to use. (already done)
    // >> 
    [SerializeField] ResouceUIElement resourceTemplate;
    [SerializeField] float offsetAmount = -160;

    public static ResourcesUI Instance { get; private set; }


    private ResourceTypeListSO resourceTypeList;
    private Dictionary<ResourceTypeSO, ResouceUIElement> uiRousourceDictionary;
    // private Dictionary<ResourceTypeSO, Transform> resourceTypeTransformDictionary;

    // >> 
    // >> What is the gameObejctText needed for?
    // >> 
    //private GameObject gameObjectText;

    // >> 
    // >> What are these objects? If you try to store the resource amount using these, don't. 
    // >> The saving of the resources is part of your ResourceManager -> see there, I added the part, to save the amounts =)
    // >> 
    //public List<GameObject> wariorCountingRes = new List<GameObject>();


    // private GameObject [] addToList;

    // [SerializeField] private GameObject pfWarior1;
    

    // [SerializeField] private Transform resourceTemplate;


    // private static ResourcesUI myself = null;
    private void Awake()
    {
        Instance = this;
        //string path = Application.persistentDataPath + "/saves/Building.save";

        // >> 
        // >> Do not load any save in the awake of a object. At this point of time it is not garanteed
        // >> that all objects got instantiated by the engine.
        // >> 
        /* if ( File.Exists(path)) {
                 Debug.Log("yra failas " + path);
             SaveLoadSystem.SaveLoadSystem.saveName = "Building.save";
             SaveLoadSystem.SaveLoadSystem.Load();

         } else */
        {

            resourceTypeList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

            //resourceTypeTransformDictionary = new Dictionary<ResourceTypeSO, Transform>();
            uiRousourceDictionary = new Dictionary<ResourceTypeSO, ResouceUIElement>();
            //Transform resourceTemplate = transform.Find("resourceTemplate");
            //resourceTemplate.gameObject.SetActive(false);


            int index = 0;
            foreach (ResourceTypeSO resourceType in resourceTypeList.list)
            {
                //Transform resourceTransform = Instantiate(resourceTemplate,transform);
                ResouceUIElement resourceTransform = Instantiate(resourceTemplate, transform);
                resourceTransform.gameObject.SetActive(true);
                float offsetAmount = -160f;
                //resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetAmount * index, 0);
                //resourceTransform.Find("image").GetComponent<Image>().sprite = resourceType.sprite;
                resourceTransform.SetPosition(new Vector2(offsetAmount * index, 0));
                resourceTransform.SetImage(resourceType.sprite);


                uiRousourceDictionary[resourceType] = resourceTransform;
                index ++;

            }

            /*addToList = GameObject.FindGameObjectsWithTag("resourceTemplate");
            foreach( GameObject go in addToList) 
            {
            wariorCountingRes.Add(go);
            }*/
        }


    }

    private void Start(){
        // DontDestroyOnLoad(gameObject);
        //ResourceManager.Instance.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged;
        //string path = Application.persistentDataPath + "/saves/Building.save";

        // >> 
        // >> Loading any save file may cause problems, depending on how you have setup the objects, but 
        // >> at the time I tested it, it causes no problems.
        // >> I have seen, that you may load at different code parts, here and in GridBuildinSystem
        // >> The loading of the save shuld not be the part of the Resource UI
        // >> If the GridBuildingSystem is your main gameObject of the game, then the task of saving and loading would be a part of the GridBuildingSystem
        // >> Because I thought it is the main object, i moved this part to that.
        // >>
        /*if ( File.Exists(path)) {
                Debug.Log("yra failas " + path);
            SaveLoadSystem.SaveLoadSystem.saveName = "Building.save";
            SaveLoadSystem.SaveLoadSystem.Load();

        } else {
                UpdateResourceAmount();
            }*/

        // DontDestroyOnLoad(gameObject);
    }

    public static void AddResource(ResourceTypeSO resourceType, int amount)
    {
        if (!Instance) return;
        if (Instance.uiRousourceDictionary.ContainsKey(resourceType))
        {
            Debug.LogWarning("This resource already exists in the list");
            return;
        }
        ResouceUIElement resourceUI = Instantiate(Instance.resourceTemplate, Instance.transform);
        resourceUI.gameObject.SetActive(true);
        resourceUI.SetPosition(new Vector2(Instance.offsetAmount * Instance.uiRousourceDictionary.Count, 0));
        resourceUI.SetImage(resourceType.sprite);

        Instance.uiRousourceDictionary[resourceType] = resourceUI;
    }
    public static void RemoveResource(ResourceTypeSO resourceType, int amount)
    {
        if (!Instance) return;
        if (!Instance.uiRousourceDictionary.ContainsKey(resourceType))
            return;
        ResouceUIElement ui = Instance.uiRousourceDictionary[resourceType];
        Instance.uiRousourceDictionary.Remove(resourceType);
        Destroy(ui.gameObject);

        int index = 0;
        foreach (var elem in Instance.uiRousourceDictionary)
        {
            elem.Value.SetPosition(new Vector2(Instance.offsetAmount * index, 0));
            index++;
        }
    }

   /* private void ResourceManager_OnResourceAmountChanged(object sender, System.EventArgs e){
        UpdateResourceAmount();
    }*/

    public static void UpdateResourceAmount()
    {
        if (!Instance) return;
        foreach (ResourceTypeSO resourceType in Instance.resourceTypeList.list){

            if(Instance.uiRousourceDictionary.ContainsKey(resourceType))
            {
                //Transform resourceTransform = resourceTypeTransformDictionary[resourceType];
                ResouceUIElement resourceTransform = Instance.uiRousourceDictionary[resourceType];
                int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);
                // Debug.Log(resourceAmount + " resource amount tikrinam");
                //if(resourceTransform)
                //    resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
                if (resourceTransform)
                    resourceTransform.SetText(resourceAmount.ToString());
            }
            else
            {
                Debug.LogWarning("Key: " + resourceType.name + " does not exist in the table: resourceTypeTransformDictionary");
            }
            
        }
    }

    public static void UpdateResourceAmount(ResourceTypeSO resourceType, int amount)
    {
        if (!Instance) return;
        if(Instance.uiRousourceDictionary.ContainsKey(resourceType))
        {
            ResouceUIElement uiElement = Instance.uiRousourceDictionary[resourceType];
            if (uiElement)
                uiElement.SetText(amount.ToString());
        }
    }

 //------------------------------------
    // ISaveable implementation...
    //------------------------------------

    // Create a Serializable struct which contains all sorable data:
    // You don't need to save the location, rotation and scale, this will be done behind the scenes ;)
   /* [System.Serializable]
    struct PlayerDataCounterOtherRes
    {
        // public int counteris;
        // public SaveableEntity.Vector3Data direction;
        public string gameObjectText;

        // public int resourceAmount;
        // public Dictionary<PlayerTypeSO, Transform> wariorTypeTransformDictionary;

        // public Dictionary<PlayerTypeSO, Transform> tempDictionary;

        //public List<string> resCounting;

        public List<int> resIntCount;




        // public RectTransform rectTransform;
        

    }
    public object SaveState()
    {
        gameObjectText = GameObject.Find("resourceTemplate(Clone)/text");
        List<string> gameObjectsResources = new List<string>();
            foreach (GameObject go in wariorCountingRes) {
            gameObjectsResources.Add(go.GetComponent<SaveableEntity>().GetID());
            // Debug.Log(go.GetComponent<SaveableEntity>().GetID());
            // Debug.Log(gameObjectText.GetComponent<TMPro.TextMeshProUGUI>().text + "ressssssssss");
            }

            //
        List<int> intResCount = new List<int>();
            foreach (ResourceTypeSO resourceType in resourceTypeList.list){

            //Transform resourceTransform = resourceTypeTransformDictionary[resourceType];
            if (resourceTypeTransformDictionary.ContainsKey(resourceType))
            {
                ResouceUIElement resourceTransform = resourceTypeTransformDictionary[resourceType];
                int resourceAmount = ResourceManager.Instance.GetResourceAmount(resourceType);
                Debug.Log(resourceAmount + " resource amount tikrinam");
                //resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
                if (resourceTransform)
                    resourceTransform.SetText(resourceAmount.ToString());
            }
            
            }
        
        
        // Instantiate the struct which contains the data we want to save and return it as object
        return new PlayerDataCounterOtherRes() {
            // counteris = counteris,
            // tempDictionary = wariorTypeTransformDictionary,
           // gameObjectText = gameObjectText.GetComponent<TMPro.TextMeshProUGUI>().text,
            //resCounting = gameObjectsResources,
          //  resIntCount = intResCount,
            // resourceAmount = PlayerManagerAll.Instance.GetResourceAmount(pfPlayer1),
            // wariorTypeTransformDictionary = wariorTypeTransformDictionary,

            
            // rectTransform = rectTransform,

            // direction = new SaveableEntity.Vector3Data(transform.position),

        };
    }
    public void LoadState(object state)
    {

        PlayerDataCounterOtherRes data = (PlayerDataCounterOtherRes)state; // Receive a object which we need to
                                             // cast to extract our loaded data

        // this.counteris = data.counteris;
        
        // Debug.Log(gameObjectText.GetComponent<TMPro.TextMeshProUGUI>().text + " text mesh pro results ressssssssssss");
        // wariorTypeTransformDictionary = data.tempDictionary;

        // this.wariorTypeTransformDictionary = data.wariorTypeTransformDictionary;




        // this.rectTransform = data.rectTransform;

        // transform.position = data.direction.ToVector3();

    }
    public bool NeedsToBeSaved()
    {
        // If this GameObject has a parent which also inherits from ISaveable,
        // than any return value of of childs will be ignored, because the parent will decide if the whole Object structure
        // gets saved or not.

        // Otherwise:
        // If true gets returned, this GameObject will be saved
        // If false gets returned, this GameObject will be ignored when saving
        return true;
    }

    // Return true, if this object needs to be reinstantiated at load or false if the loading is enough
    public bool NeedsReinstantiation()
    {
        return false;
    }
    public void GotAddedAsChild(GameObject obj, GameObject hisParent)
    {
        // This function lets you know, that somewere deeper in the hirarchy of this GameObject a loaded GameObject got added to the structure
        // You may want to know that so you can setup the relations you may need


    }

    public void PostInstantiation(object state)
    {
        PlayerDataCounterOtherRes data = (PlayerDataCounterOtherRes)state; // Receive a object which we need to cast to extract our loaded data
        // Will be called after all Objects got loaded and Parents of each Objects are set.
        // You may need to do stuff like in GotAddedAsChild(...) but with objects which are not childs of this
        // Then you do this stuff here, because here it is guaranteed, that all objects got Instantiated
        // You can find your target objects using:
        // SaveableEntity.FindID(string id) -> this will search all objects by the ID
        // just grab the .gameObject of the returned Object and use it
        // You may want to store the targets ID, so you can load back to the right object.
        // Call: obj.id on the SaveableEntity component of your target to get the ID

       // gameObjectText = GameObject.Find("resourceTemplate(Clone)/text");
      //  gameObjectText.GetComponent<TMPro.TextMeshProUGUI>().text = data.gameObjectText;
       // Debug.Log(gameObjectText.GetComponent<TMPro.TextMeshProUGUI>().text);

        List<string> gameObjectsResources = data.resCounting;
        List<GameObject> foundEnemiesRes = new List<GameObject>();
        foreach (string go in gameObjectsResources)
        {
            // Search for Objects with a given ID
            SaveableEntity obj = SaveableEntity.FindID(go);
            if(obj)
            {
                // Found a anemy with the saved ID
                foundEnemiesRes.Add(obj.gameObject);
                Debug.Log("RES TEmplate with ID: " + go + " found.");
            }
            else
            {
                Debug.Log("No RES TEmplat with ID: " + go + " found.");
            }
        }
        wariorCountingRes = foundEnemiesRes;




        // List<int> gameObjectsResourcesInt = data.resIntCount;
        // List<GameObject> foundEnemiesResInt = new List<GameObject>();
        // foreach (int goInt in gameObjectsResourcesInt)
        // {
        //     // Search for Objects with a given ID
        //     SaveableEntity objInt = SaveableEntity.FindID(goInt);
        //     if(objInt)
        //     {
        //         // Found a anemy with the saved ID
        //         foundEnemiesResInt.Add(objInt.gameObject);
        //         Debug.Log("RES TEmplate with ID: " + goInt + " found.");
        //     }
        //     else
        //     {
        //         Debug.Log("No RES TEmplat with ID: " + goInt + " found.");
        //     }
        // }
        // wariorCountingRes = foundEnemiesResInt;
        // Debug.Log(wariorCounting + " wariorcounting");
    }*/


}
