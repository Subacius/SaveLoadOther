using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SolarPanelClose : MonoBehaviour
{
    // public GameObject Panel;
    private GameObject kazkasIsCanvas;

    private GameObject kazkasIsCanvasGiliau;

    private GameObject kazkasIsCanvasGiliau2;

    private GameObject kazkasIsCanvasGiliauDar;

    // private GameObject kazkasIsCanvasGiliauDar2;

    private GameObject kazkasIsCanvasGiliauDar3;

    private GameObject kazkasIsCanvasGiliauDar4;

    private TMP_Text metalMelterBTNText;

    private TMP_Text destroyBTNText;

    private GameObject checkIf1;

    private GameObject checkIf2;

    private GameObject checkIf3;

        private void Awake(){
        // DontDestroyOnLoad(gameObject);
        transform.Find("closeBTN").GetComponent<Button>().onClick.AddListener(() => {
            kazkasIsCanvas = GameObject.Find("Canvas/SolarPanelBandom");
            Image image = kazkasIsCanvas.GetComponent<Image>();
            image.enabled = false;

            kazkasIsCanvasGiliau = GameObject.Find("Canvas/SolarPanelBandom/bottomPicture");
            Image image2 = kazkasIsCanvasGiliau.GetComponent<Image>();
            image2.enabled = false;

            // kazkasIsCanvasGiliau2 = GameObject.Find("Canvas/MetalMelterPanel/Unit2Panel");
            // Image image3 = kazkasIsCanvasGiliau2.GetComponent<Image>();
            // image3.enabled = false;

            // kazkasIsCanvasGiliauDar = GameObject.Find("Canvas/BarrackPanel/Unit1Panel/unit1PanelImage");
            // Image image4 = kazkasIsCanvasGiliauDar.GetComponent<Image>();
            // image4.enabled = false;

            // kazkasIsCanvasGiliauDar2 = GameObject.Find("Canvas/BarrackPanel/Unit2Panel/unit2PanelImage");
            // Image image5 = kazkasIsCanvasGiliauDar2.GetComponent<Image>();
            // image5.enabled = false;

            kazkasIsCanvasGiliauDar3 = GameObject.Find("Canvas/SolarPanelBandom/closeBTN");
            Image image6 = kazkasIsCanvasGiliauDar3.GetComponent<Image>();
            image6.enabled = false;

            kazkasIsCanvasGiliauDar4 = GameObject.Find("Canvas/SolarPanelBandom/SolarPanelBTNText");
            metalMelterBTNText =  kazkasIsCanvasGiliauDar4.GetComponent<TextMeshProUGUI>();
            metalMelterBTNText.enabled = (false);


            kazkasIsCanvasGiliau2 = GameObject.Find("Canvas/SolarPanelBandom/destroyBtn");
            Image image7 = kazkasIsCanvasGiliau2.GetComponent<Image>();
            image7.enabled = false;

            kazkasIsCanvasGiliauDar = GameObject.Find("Canvas/SolarPanelBandom/destroyBtn/destroyText");
            destroyBTNText =  kazkasIsCanvasGiliauDar.GetComponent<TextMeshProUGUI>();
            destroyBTNText.enabled = false;


            checkIf1 = GameObject.Find("pfMetal Melter(Clone)/pfPanelis_MetalMelter/openPanelBtn");
            if (checkIf1 != null) {
                Button button = checkIf1.GetComponent<Button>();
                button.enabled = true;
            }



            checkIf2 = GameObject.Find("pfBarrack(Clone)/pfPanelis_Barrack/openPanelBtn");
            if (checkIf2 != null) {
                Button button2 = checkIf2.GetComponent<Button>();
                button2.enabled = true;
            }

            checkIf3 = GameObject.Find("pfGoldMiner(Clone)/pfPanelis_GoldMiner/openPanelBtn");
            if(checkIf3 != null) {
                Button button3 = checkIf3.GetComponent<Button>();
                button3.enabled = true;
            }

            
            // if ( checkIf1 != null) {
            //     checkIf1 = GameObject.Find("pfBarrack(Clone)/pfPanelis_Barrack/openPanelBtn");
            //     Button button = checkIf1.GetComponent<Button>();
            //     button.enabled = true;
            // } else {
            //     return;
            // }

            // if ( checkIf2 != null) {
            //     checkIf2 = GameObject.Find("pfMetal Melter(Clone)/pfPanelis_MetalMelter/openPanelBtn");
            //     Button button2 = checkIf2.GetComponent<Button>();
            //     button2.enabled = true;
            // } else {
            //     return;
            // }

        });
        
    }

    // public void ClosePanel() {
    //     if (Panel != null) {

    //         // bool isActive = Panel.activeSelf;

    //         Panel.SetActive(false);
    //     }
    // }
}