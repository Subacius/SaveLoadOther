using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{

    public static TooltipUI Instance { get; private set;}
    [SerializeField] private RectTransform canvasRectTransform;
    //draginam canvas per inpestor
    private RectTransform rectTransform;
    private TextMeshProUGUI textMeshPro;
    private RectTransform backgroundRectTransform;

    private TooltipTimer tooltipTimer;

    private void Awake () {

        Instance = this;
        rectTransform = GetComponent<RectTransform>();
        textMeshPro = transform.Find("text").GetComponent<TextMeshProUGUI>();
        backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        
        Hide();
        // for testing 
        // SetText("hi there");
    }
    
    private void Update() {
        HandleFollowMouse();
        
       

        if (tooltipTimer !=null) {
            tooltipTimer.timer -= Time.deltaTime;
                if (tooltipTimer.timer <= 0) {
                    Hide();
                }
        }
    }

    private void HandleFollowMouse() {
         // update negalima naudoti Getcomponent, todel turim "cashint" // virsuje dedam ir i awake dedam
        // GetComponent<RectTransform>().anchoredPosition = Input.mousePosition; -- nenaudoti!!!!!!
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;
        // kai dadejom convasRectTransform.localscale.x kad tiksliai pele sektu

        // if jeigu tooltip iseina is ekrano lauko, kad ji vistiek rodytu.
        if ( anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width) {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }
         if ( anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height) {
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }

        rectTransform.anchoredPosition = anchoredPosition;
    }
    private void SetText(string tooltipText) {
        textMeshPro.SetText(tooltipText);
        //call textmesh pro
        textMeshPro.ForceMeshUpdate();

        //size of the text
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        //bakcground size according text size
        //pridedam padding, nes tekstas lygiai su background yra
        Vector2 padding = new Vector2 (8,8);
        backgroundRectTransform.sizeDelta = textSize + padding;

    }

    public void Show (string tooltipText, TooltipTimer tooltipTimer = null) {
        this.tooltipTimer = tooltipTimer;
        gameObject.SetActive(true);
        SetText(tooltipText);
        HandleFollowMouse();
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public class TooltipTimer {
        public float timer;
    }
}
