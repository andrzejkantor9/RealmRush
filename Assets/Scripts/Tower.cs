using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //CACHE
    Bank m_bank;

    //PROPERTIES
    [Header("PROPERTIES")]
    [SerializeField]
    int m_cost = 75;

    /////////////////////////////////////////////////////////////

#region TowerCreation
    public bool CreateTower(Tower tower, Vector3 position)
    {
        m_bank = FindObjectOfType<Bank>();

        if(!m_bank)
            return false;
        else if (!CanAffordTower())
            return false;

        Instantiate(tower.gameObject, position, Quaternion.identity);
        m_bank.Withdraw(m_cost);
        return true;
    }

    public bool CanAffordTower()
    {
        if(!m_bank)
            m_bank = FindObjectOfType<Bank>();

        return m_bank.currentBalance >= m_cost;
    }

#endregion
}
