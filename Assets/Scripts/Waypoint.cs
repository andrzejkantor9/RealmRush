using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class Waypoint : MonoBehaviour, IPointerDownHandler
{
    [Header("CACHE")]
    [SerializeField]
    GameObject m_towerPrefab = null;

    [Space(10)] [Header("PROPERTIES")]
    [SerializeField] [Tooltip("can tower be placed here?")]
    bool m_isPlacable = false;

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
            Instantiate(m_towerPrefab, transform.position, Quaternion.identity);
            m_isPlacable = false;
            // Debug.Log("clicked: " + eventData.pointerCurrentRaycast.gameObject.name); 
        }
    }

#endregion
}
