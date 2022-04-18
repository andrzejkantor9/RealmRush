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
    [SerializeField]
    float m_buildDelay = 1f;

    /////////////////////////////////////////////////////////////

    void Start()
    {
        StartCoroutine(Build());
    }

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

    IEnumerator Build()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(false);
            }
        }

        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(m_buildDelay);

            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(true);
            }
        }
    }

#endregion
}
