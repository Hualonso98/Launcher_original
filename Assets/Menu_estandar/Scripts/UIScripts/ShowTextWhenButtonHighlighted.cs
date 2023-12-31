﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowTextWhenButtonHighlighted : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private GameObject childText = null; //  or make public and drag
 

    void Start()
    {
        Text text = GetComponentInChildren<Text>();
        if (text != null)
        {
            childText = text.gameObject;
            childText.SetActive(false);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        childText.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        childText.SetActive(false);
    }
}

