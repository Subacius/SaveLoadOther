using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackPanelOpenerPlease : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject panelObject;
    void Start()
    {
        panelObject = GameObject.Find("Canvas/PanelGameObject/Panel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenPanelPlease() {
        
        panelObject.SetActive(true);
    }
}
