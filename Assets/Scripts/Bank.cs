using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    //CACHE
    [Header("CACHE")]
    [SerializeField]
    TextMeshProUGUI m_displayBalance = null;

    //PROPERTIES
    [Space(10)] [Header("PROPERTIES")]
    [SerializeField]
    int m_startingBalance = 150;

    //STATES
    int m_currentBalance;
    public int currentBalance {get {return m_currentBalance;}}

     //////////////////////////////////////////////////////////////////////////

    void Awake()
    {
        AssertCache();

        Initialize();
        UpdateDisplay();
    }

#region Initialization

    void AssertCache()
    {
        UnityEngine.Assertions.Assert.IsNotNull(m_displayBalance, $"Script: {GetType().ToString()} variable m_displayBalance is null");
    }

    void Initialize()
    {
        m_currentBalance = m_startingBalance;
    }

#endregion

#region MoneyTransfers
    public void Deposit(int amount)
    {
        m_currentBalance += Mathf.Abs(amount);
        UpdateDisplay();
        // CustomDebug.Log($"current balance: {m_currentBalance}");
    }

    public void Withdraw(int amount)
    {
        m_currentBalance -= Mathf.Abs(amount);
        UpdateDisplay();
        // CustomDebug.Log($"current balance: {m_currentBalance}");

        if(m_currentBalance < 0)
        {
            CustomDebug.Log("lost the game");
            ReloadScene();
        }            
    }
#endregion

#region LoseGame

    void ReloadScene()
    {
        CustomDebug.Log("LoseGame");
        string currentScenePath = SceneManager.GetActiveScene().path;
        SceneManager.LoadScene(currentScenePath);
    }

#endregion

#region UI
    
    void UpdateDisplay()
    {
        m_displayBalance.text = "Gold: " + m_currentBalance;
    }

#endregion
}
