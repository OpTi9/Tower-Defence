using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private bool isHoveringUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool IsHoveringUI()
    {
        return isHoveringUI;
    }

    public void SetHoveringState(bool state)
    {
        isHoveringUI = state;
    }

}
