using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResouceUIElement : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TMPro.TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string text)
    {
        this.text.SetText(text);
    }
    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
    }
    public void SetPosition(Vector2 pos)
    {
        GetComponent<RectTransform>().anchoredPosition = pos;
    }
}
