using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //CACHE
    [Header("CACHE")]
    Bank m_bank;

    //PROPERTIES
    [Space(10)] [Header("PROPERTIES")]
    [SerializeField]
    int m_goldReward = 25;
    [SerializeField]
    int m_goldPenalty = 25;

    /////////////////////////////////////////

    void Awake()
    {
        SetupCache();
        AssertCache();
    }

#region Initialization

    void SetupCache()
    {
        m_bank = FindObjectOfType<Bank>();
    }

    void AssertCache()
    {
        UnityEngine.Assertions.Assert.IsNotNull(m_bank, $"Script: {GetType().ToString()} variable m_bank is null");
    }
    
#endregion

#region Gold

    public void RewardGold()
    {
        if(m_bank)
            m_bank.Deposit(m_goldReward);
    }

    public void PenalizeGold()
    {
        if(m_bank)
            m_bank.Withdraw(m_goldPenalty);
    }

#endregion
}
