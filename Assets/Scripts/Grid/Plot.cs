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
    private Vector2Int gridPosition;
    
    private void Start()
    {
        defaultSprite = sr.sprite;
        gridPosition = GridManager.Instance.GetGridPosition(transform.position);
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
        if (GameManager.Instance.currentState == GameManager.GameState.Building)
        {
            BuildManager.Instance.BuildTower(gridPosition, transform.position);
        }
    }
}

