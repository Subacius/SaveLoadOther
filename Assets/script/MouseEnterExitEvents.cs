using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

//scripta dedam canvas / buildingtypeselectui/ btntemplate

// buildingtypeSelectUI naudoja sita scripta

public class MouseEnterExitEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{   
    public event EventHandler OnMouseEnter;
    public event EventHandler OnMouseExit;
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnMouseEnter?.Invoke(this, EventArgs.Empty);

        // OnMouseEnter?.Invoke(this, EventArgs.Empty);
        // Debug.Log("enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnMouseExit?.Invoke(this, EventArgs.Empty);
        // Debug.Log("exit");
    }
}
