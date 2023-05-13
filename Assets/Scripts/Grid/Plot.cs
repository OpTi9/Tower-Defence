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
        if (UIManager.Instance.IsHoveringUI()) return;
        sr.sprite = hoverSprite;
    }

    private void OnMouseExit()
    {
        sr.sprite = defaultSprite;
    }
    
    public void RemoveTower()
    {
        tower = null;
    }
    
    public void SetTower(GameObject newTower)
    {
        tower = newTower;
    }
    
    private void OnMouseDown()
    {
        if (UIManager.Instance.IsHoveringUI()) return;
        
        if (GameManager.Instance.currentState == GameManager.GameState.Building)
        {
            if (tower == null)
            {
                // If there is no tower, build one
                tower = BuildManager.Instance.BuildTower(gridPosition, transform.position);
            }
            else
            {
                // If there is already a tower, open its upgrade UI
                Tower towerScript = tower.GetComponent<Tower>();
                if (towerScript != null)
                {
                    towerScript.OpenUpgradeUI();
                }
            }
        }
    }
}

