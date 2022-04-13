using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    //CACHE
    [Header("CACHE")]
    [SerializeField]
    Transform m_weapon = null;
    [SerializeField]
    ParticleSystem m_projectileParticles = null;
    
    Transform m_target = null;

    //PROPERTIES
    [Space(10)] [Header("PROPERTIES")]
    [SerializeField]
    float m_maxRange = 15f;

    ///////////////////////////////////////////////

    void Awake()
    {
        AssertCache();        
    }

    void Update()
    {
        UnityEngine.Profiling.Profiler.BeginSample($"{GetType().ToString()}: Update");

        FindClosestTarget();
        AimWeapon();

        UnityEngine.Profiling.Profiler.EndSample();   
    }

#region Initialization

    void AssertCache()
    {
        UnityEngine.Assertions.Assert.IsNotNull(m_weapon, $"Script: {GetType().ToString()} variable m_weapon is null");
        UnityEngine.Assertions.Assert.IsNotNull(m_projectileParticles, $"Script: {GetType().ToString()} variable m_wem_projectileParticlesapon is null");
    }

#endregion

#region Weapon

    private void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if(targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }

            m_target = closestTarget;
        }
    }

    void AimWeapon()
    {
        float TargetDistance = Vector3.Distance(transform.position, m_target.position);
        if(TargetDistance <= m_maxRange)
        {
            EnableAttacking(true);
            m_weapon.LookAt(m_target);
        }
        else
        {
            EnableAttacking(false);
        }       
    }

    void EnableAttacking(bool active)
    {
        ParticleSystem.EmissionModule emission = m_projectileParticles.emission;
        emission.enabled = active;  
    }

#endregion
}
