using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isMouseOver = false;

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
        UIManager.Instance.SetHoveringState(true);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
        UIManager.Instance.SetHoveringState(false);
        gameObject.SetActive(false);
    }
}
