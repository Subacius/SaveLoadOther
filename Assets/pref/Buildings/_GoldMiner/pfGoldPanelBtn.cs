using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class pfGoldPanelBtn : MonoBehaviour
{

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

    [SerializeField] private PlacedObjectTypeSO placedObjectTypeSO;
    
    private void Awake(){
        // DontDestroyOnLoad(gameObject);
        transform.Find("openPanelBtn").GetComponent<Button>().onClick.AddListener(() => {
            kazkasIsCanvas = GameObject.Find("Canvas/GoldMinerPanelBandom");
            Image image = kazkasIsCanvas.GetComponent<Image>();
            image.enabled = true;

            kazkasIsCanvasGiliau = GameObject.Find("Canvas/GoldMinerPanelBandom/bottomPicture");
            Image image2 = kazkasIsCanvasGiliau.GetComponent<Image>();
            image2.enabled = true;

            // kazkasIsCanvasGiliau2 = GameObject.Find("Canvas/BarrackPanel/Unit2Panel");
            // Image image3 = kazkasIsCanvasGiliau2.GetComponent<Image>();
            // image3.enabled = true;

            // kazkasIsCanvasGiliauDar = GameObject.Find("Canvas/BarrackPanel/Unit1Panel/unit1PanelImage");
            // Image image4 = kazkasIsCanvasGiliauDar.GetComponent<Image>();
            // image4.enabled = true;

            // kazkasIsCanvasGiliauDar2 = GameObject.Find("Canvas/BarrackPanel/Unit2Panel/unit2PanelImage");
            // Image image5 = kazkasIsCanvasGiliauDar2.GetComponent<Image>();
            // image5.enabled = true;

            kazkasIsCanvasGiliauDar3 = GameObject.Find("Canvas/GoldMinerPanelBandom/closeBTN");
            Image image6 = kazkasIsCanvasGiliauDar3.GetComponent<Image>();
            image6.enabled = true;

            kazkasIsCanvasGiliauDar4 = GameObject.Find("Canvas/GoldMinerPanelBandom/GoldMinerBTNText");
            metalMelterBTNText =  kazkasIsCanvasGiliauDar4.GetComponent<TextMeshProUGUI>();
            metalMelterBTNText.enabled = (true);
            metalMelterBTNText.SetText(placedObjectTypeSO.nameString);

            // checkIf1 = GameObject.Find("pfBarrack(Clone)/pfPanelis_Barrack/openPanelBtn");
            // Button button = checkIf1.GetComponent<Button>();
            // button.enabled = false;

            // checkIf2 = GameObject.Find("pfMetal Melter(Clone)/pfPanelis_MetalMelter/openPanelBtn");
            // Button button2 = checkIf2.GetComponent<Button>();
            // button2.enabled = false;

            kazkasIsCanvasGiliau2 = GameObject.Find("Canvas/GoldMinerPanelBandom/destroyBtn");
            Image image7 = kazkasIsCanvasGiliau2.GetComponent<Image>();
            image7.enabled = true;

            kazkasIsCanvasGiliauDar = GameObject.Find("Canvas/GoldMinerPanelBandom/destroyBtn/destroyText");
            destroyBTNText =  kazkasIsCanvasGiliauDar.GetComponent<TextMeshProUGUI>();
            destroyBTNText.enabled = true;


            checkIf1 = GameObject.Find("pfMetal Melter(Clone)/pfPanelis_MetalMelter/openPanelBtn");
            if(checkIf1 != null) {
                Button button = checkIf1.GetComponent<Button>();
                button.enabled = false;
            }

            checkIf2 = GameObject.Find("pfSolarPanel(Clone)/pfPanelis_SolarPanel/openPanelBtn");
            if(checkIf2 != null) {
                Button button2 = checkIf2.GetComponent<Button>();
                button2.enabled = false;
            }

            checkIf3 = GameObject.Find("pfBarrack(Clone)/pfPanelis_Barrack/openPanelBtn");
            if (checkIf3 != null) {
                Button button3 = checkIf3.GetComponent<Button>();
                button3.enabled = false;
            }
            


        });
        
    }
}
