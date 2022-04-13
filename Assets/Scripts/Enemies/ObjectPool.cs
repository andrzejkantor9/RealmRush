using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    //CACHE
    [Header("CACHE")]
    [SerializeField]
    GameObject m_enemyPrefab = null;

    //PROPERTIES
    [Space(10f)] [Header("PROPERTIES")]
    [SerializeField] [Range(0, 50)]
    [Tooltip("number of enemy instantiations in pool")]
    int m_poolSize = 5;
    [SerializeField][Range(.1f, 30f)]
    float m_enemySpawnTimer = 1f;

    //STATES
    GameObject[] m_pool;

    /////////////////////////////////////////

    private void Awake() 
    {
        AssertCache();

        PopulatePool();
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

#region Initialization

    private void AssertCache()
    {
        UnityEngine.Assertions.Assert.IsNotNull(m_enemyPrefab, $"Script: {GetType().ToString()} variable m_enemyPrefab is null");
    }

    private void PopulatePool()
    {
        m_pool = new GameObject[m_poolSize];

        for (int i = 0; i < m_poolSize; i++)
        {
            m_pool[i] = Instantiate(m_enemyPrefab, transform);
            m_pool[i].SetActive(false);
        }
    }

#endregion

#region EnemyManagement

    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            EnableObjectInPool();   
            yield return new WaitForSeconds(m_enemySpawnTimer);
        }
    }

    private void EnableObjectInPool()
    {
        foreach (GameObject enemy in m_pool)
        {
            if(!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                return;
            }
        }
    }

    #endregion
}
