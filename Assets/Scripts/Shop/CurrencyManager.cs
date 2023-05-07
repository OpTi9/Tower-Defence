using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }
    
    public int currency;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currency = 10;
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;
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
