using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class Waypoint : MonoBehaviour, IPointerDownHandler
{
    //CACHE
    [Header("CACHE")]
    [SerializeField]
    Tower m_towerPrefab = null;

    //PROPERTIES
    [Space(10)] [Header("PROPERTIES")]
    [SerializeField] [Tooltip("can tower be placed here?")]
    bool m_isPlacable = false;
    ///public getter of above
    public bool IsPlacable{ get {return m_isPlacable;} }

    ///////////////////////////////////////////////

    void Awake() 
    {
        AssertComponents();
    }

    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        PlaceTower();

    }

#region Initialization
    private void AssertComponents()
    {
        UnityEngine.Assertions.Assert.IsNotNull(m_towerPrefab, $"Script: {GetType().ToString()} variable m_boxCollider is null");
    }
#endregion

#region Towers

    private void PlaceTower()
    {
        if (m_isPlacable)
        {
            bool isPlaced = m_towerPrefab.CreateTower(m_towerPrefab, transform.position);
            // Instantiate(m_towerPrefab, transform.position, Quaternion.identity);
            m_isPlacable = !isPlaced;
        }
    }

#endregion
}
