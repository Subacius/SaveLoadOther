using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
// 20 pamoka

public class ResourceGeneratorOverlay : MonoBehaviour
{

    //pridedam scripta prie pfwoodharvester / pfstoneharvester / pfgoldharvester

    [SerializeField] private ResourceGenerator resourceGenerator;
    //viska imam is resourceGenerator script
    private Transform barTransform;

    // private static ResourceGeneratorOverlay myself = null;
    private void Awake() {
        // DontDestroyOnLoad(gameObject);
    //         if (myself == null) {
    //   myself = this;
    //   DontDestroyOnLoad(myself.gameObject);
    // } else {
    //   Destroy(gameObject);
    // }
    }

    private void Start () {
        ResourceGeneratorData resourceGeneratorData = resourceGenerator.GetResourceGeneratorData();
        //parenka icona pagal pasirinkta pastata

        barTransform = transform.Find("bar");

        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;

        transform.Find("text").GetComponent<TextMeshPro>().SetText(resourceGenerator.GetAmountGeneratedPerSecond().ToString("F1"));
        // "F1" - vienas skaicius po kablelio
    }

    private void Update() {
        barTransform.localScale = new Vector3(1 - resourceGenerator.GetTimerNormalized(), 1 , 1);
    }
}
