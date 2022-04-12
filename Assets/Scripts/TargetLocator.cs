using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    //CACHE
    [Header("CACHE")]
    [SerializeField]
    Transform weapon = null;
    
    Transform target = null;

    ///////////////////////////////////////////////

    void Start()
    {
        target = FindObjectOfType<EnemyMover>().transform;        
    }

    void Update()
    {
        UnityEngine.Profiling.Profiler.BeginSample($"{GetType().ToString()}: Update");

        AimWeapon();

        UnityEngine.Profiling.Profiler.EndSample();   
    }

#region Weapon
    private void AimWeapon()
    {
        weapon.LookAt(target);
    }
#endregion
}
