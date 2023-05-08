using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] TextMeshProUGUI currencyUI;

    [SerializeField] private Animator anim;

    private bool isMenuOpen = true;

    private void OnGUI()
    {
        currencyUI.text = CurrencyManager.Instance.currency.ToString();
    }

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }

    public void SetSelected()
    {
        
    }
}
