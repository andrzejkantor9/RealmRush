using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todo introduce delegates here
[RequireComponent(typeof(BoxCollider))]
public class ExampleScript : MonoBehaviour
{
    //CACHE
    [Header("CACHE")]
    [SerializeField][HideInInspector]
    BoxCollider m_boxCollider;

    const string FRIENDLY_TAG = "Friendly";
    
    //PROPERTIES
    [Space(10)] [Header("PROPERTIES")]
    [SerializeField] [Range(0,1)] [Tooltip("to display in inspector")]
    float m_speed = 1f; 
    
    //STATES
    bool m_isDead;

    ///////////////////////////////////////////////
    //only engine methods without regions
    //only methods inside engine methods
    //methods called must be below methods calling them
    //#if DEVELOPMENT_BUILD || UNITY_EDITOR

    void OnValidate()
    {
        SetupCache();
        AssertCache();
    }

    void Awake() 
    {
        AssertCache();
        Initialize();  
    }

    void Update() 
    {
        UnityEngine.Profiling.Profiler.BeginSample($"{GetType().ToString()}: Update");

        //update code here

        UnityEngine.Profiling.Profiler.EndSample();
    }

#region Initialization

    void SetupCache()
    {
        m_boxCollider = GetComponent<BoxCollider>();
    }

    void AssertCache()
    {
        UnityEngine.Assertions.Assert.IsNotNull(m_boxCollider, $"Script: {GetType().ToString()} variable m_boxCollider is null");
    }

    /// <summary>
    /// Setup on awake
    /// </summary>
    /// <param name="other">No params here</param>
    bool Initialize()
    {
        string exampleName;
        return true;
    }
    /// ?return? <>

#endregion

#region RegionsForSpecificPartOfFunctionality
#endregion

}