using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowOutsideTextWhenButtonHighligted : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private GameObject GameObjectText = null; //  or make public and drag
    public Text text;

    void Start()
    {
        GameObjectText = text.gameObject;
        GameObjectText.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObjectText.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        GameObjectText.SetActive(false);
    }
}

