using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //PROPERTIES
    [Header("PROPERTIES")]
    [SerializeField]
    int m_cost = 75;

    /////////////////////////////////////////////////////////////

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if(!bank)
            return false;
        else if (bank.currentBalance < m_cost)
            return false;

            
        CustomDebug.Log($"current balance: {bank.currentBalance}");
        Instantiate(tower.gameObject, position, Quaternion.identity);
        bank.Withdraw(m_cost);
        return true;
    }
}
