using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }
    
    public int currency;
    public int totalCurrencyEarned;

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

    private void Start()
    {
        totalCurrencyEarned += currency;
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
        totalCurrencyEarned += amount;
    }
    
    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            // buy
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("not enough");
            return false;
        }
    }
}
