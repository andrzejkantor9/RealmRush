using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    //CACHE
    [Header("CACHE")]
    Enemy m_enemy;

    //PROPERTIES
    [Space(10)] [Header("PROPERTIES")]
    [SerializeField]
    int m_maxHitPoints = 5;
    [SerializeField] [Tooltip("adds amount to maxHitPoints each time this enemy dies")]
    int m_difficultyRamp = 1;

    int m_currentHitPoints = 0;

    ///////////////////////////////////////////////

    void Awake() 
    {
        SetupCache();
        AssertCache();
    }

    void OnEnable()
    {
        ResetEnemy();
    }

    void OnParticleCollision(GameObject other) 
    {
        ProcessHit();
    }

#region  initialization
    void SetupCache()
    {
        m_enemy = GetComponent<Enemy>();
    }

    void AssertCache()
    {
        UnityEngine.Assertions.Assert.IsNotNull(m_enemy, $"Script: {GetType().ToString()} variable m_enemy is null");
    }

    void ResetEnemy()
    {
        m_currentHitPoints = m_maxHitPoints;
    }
#endregion

#region  HitPoints
    void ProcessHit()
    {
        --m_currentHitPoints        ;

        if(m_currentHitPoints <= 0)
        {
            gameObject.SetActive(false);
            m_maxHitPoints += m_difficultyRamp;
            m_enemy.RewardGold();
        }
    }
#endregion
}
