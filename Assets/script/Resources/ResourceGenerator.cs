using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{


    public static int GetNearbyResourceAmount(ResourceGeneratorData resourceGeneratorData, Vector3 position) {
          Collider[] hitColliders = Physics.OverlapSphere(position + new Vector3(5,0,5), resourceGeneratorData.resourceDetectionRadius);
         int nearbyResourceAmount = 0;
       foreach (Collider collider in hitColliders) {
           ResourceNode resourceNode = collider.GetComponent<ResourceNode>();
           if ( resourceNode != null) {

               if ( resourceNode.resourceType == resourceGeneratorData.resourceType){
                   //same type
                    nearbyResourceAmount++;
               }
               
           }
        // Debug.Log("esame pas collider viduje");
       }
        //uzfiksuojam minimuma ir maximuma
       nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);
        
       return nearbyResourceAmount;
       
    }
    private ResourceManager resourceManager;
    // private PlacedObjectTypeSO buildingType;

    private ResourceGeneratorData resourceGeneratorData;
    private float timer;
    private float timerMax;

 

    BuildingConstruction buildingConstruction;

    //

   
    // public LayerMask m_LayerMask;


    // private static ResourceGenerator myself = null;
    private void Awake()
    {

        // if (myself == null) {
        //     myself = this;
        //     DontDestroyOnLoad(myself.gameObject);
        // } else {
        // Destroy(gameObject);
        // }
        // DontDestroyOnLoad(transform.root.gameObject);
        //         transform.parent = null;
        // DontDestroyOnLoad(this);
        // DontDestroyOnLoad(transform.gameObject);
        resourceGeneratorData =  GetComponent<BuildingTypeHolder>().buildingType.resourceGeneratorData;
        timerMax = resourceGeneratorData.timerMax;
        buildingConstruction = GetComponent<BuildingConstruction>();
    }

    private void Start() {

        // DontDestroyOnLoad(gameObject);
        
        CollisionResourceNode();
        // CollisionResourceNodeBoxCollider();
        

       

      
        
    }

    //sitas veikia, tik sphera collider...
    public void CollisionResourceNode() {
        
        int nearbyResourceAmount = GetNearbyResourceAmount(resourceGeneratorData, transform.position);
       
        if (nearbyResourceAmount ==0) {
            //no reource nodes nearby
            //disable resource generator
            enabled = false;
        } else {
            timerMax = (resourceGeneratorData.timerMax / 2f) + 
            resourceGeneratorData.timerMax * (1 - (float)nearbyResourceAmount / resourceGeneratorData.maxResourceAmount);
        }

    //    Debug.Log("Amount  "+ nearbyResourceAmount + " ,  timerMax : " + timerMax);

    }
    
        
    private void Update()
    {

        

        timer -= Time.deltaTime;

       
             if(timer <=0f){
            timer +=timerMax;
            // Debug.Log("boom" + resourceGeneratorData.resourceType.nameString);
            ResourceManager.Instance.AddResource(resourceGeneratorData.resourceType, 1);
            }
    

       


}


    // void MyCollisions()
    // {
    //     //Use the OverlapBox to detect if there are any other colliders within this box area.
    //     //Use the GameObject's centre, half the size (as a radius) and rotation. This creates an invisible box around your GameObject.
    //     Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity, m_LayerMask);
    //   int nearbyResourceAmount = 0;
    //    foreach (Collider collider in hitColliders) {
    //        ResourceNode resourceNode = collider.GetComponent<ResourceNode>();
    //        if ( resourceNode != null) {

    //            if ( resourceNode.resourceType == resourceGeneratorData.resourceType){
    //                //same type
    //                 nearbyResourceAmount++;
    //            }
               
    //        }
        
    //    }
    //     Debug.Log("Amount  "+ nearbyResourceAmount + " ,  timerMax : " + timerMax);
    // }


        public void CollisionResourceNodeBoxCollider() {
        
         Collider[] hitColliders = Physics.OverlapBox(transform.position + new Vector3 (5,0,5) , (transform.localScale  ) / 2 , Quaternion.identity );
         int nearbyResourceAmount = 0;
       foreach (Collider collider in hitColliders) {
           ResourceNode resourceNode = collider.GetComponent<ResourceNode>();
           if ( resourceNode != null) {

               if ( resourceNode.resourceType == resourceGeneratorData.resourceType){
                   //same type
                    nearbyResourceAmount++;
               }
               
           }
        
       }
        //uzfiksuojam minimuma ir maximuma
       nearbyResourceAmount = Mathf.Clamp(nearbyResourceAmount, 0, resourceGeneratorData.maxResourceAmount);
        if (nearbyResourceAmount ==0) {
            //no reource nodes nearby
            //disable resource generator
            enabled = false;
        } else {
            timerMax = (resourceGeneratorData.timerMax / 2f) + 
            resourceGeneratorData.timerMax * (1 - (float)nearbyResourceAmount / resourceGeneratorData.maxResourceAmount);
        }

       Debug.Log("Amount  "+ nearbyResourceAmount + " ,  timerMax : " + timerMax);

    }

    public ResourceGeneratorData GetResourceGeneratorData() {
        return resourceGeneratorData;
    }

    public float GetTimerNormalized() {
        return timer/timerMax;
    }

    public float GetAmountGeneratedPerSecond() {
        return 1 / timerMax;
    }

   
 
}









