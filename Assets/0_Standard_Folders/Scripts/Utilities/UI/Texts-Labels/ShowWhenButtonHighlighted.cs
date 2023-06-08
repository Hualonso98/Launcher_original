using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowWhenButtonHighlighted : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject shownObject = null; //  or make public and drag
    
    void Start()
    {
        shownObject.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        shownObject.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        shownObject.SetActive(false);
    }
}