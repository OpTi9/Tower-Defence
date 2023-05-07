using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    public SpriteRenderer sr;
    public Sprite hoverSprite;
    private Sprite defaultSprite;
    
    private GameObject tower;

    private void Start()
    {
        defaultSprite = sr.sprite;
    }

    private void OnMouseEnter()
    {
        sr.sprite = hoverSprite;
    }

    private void OnMouseExit()
    {
        sr.sprite = defaultSprite;
    }

    private void OnMouseDown()
    {
        Debug.Log("build here");
    }
}
