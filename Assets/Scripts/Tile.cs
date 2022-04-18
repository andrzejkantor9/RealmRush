using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerDownHandler
{
    //CACHE
    [Header("CACHE")]
    [SerializeField]
    Tower m_towerPrefab;

    GridManager m_gridManager;
    Pathfinder m_pahtfinder;
    Vector2Int m_coordinates = new Vector2Int();

    //PROPERTIES
    [Space(10)] [Header("PROPERTIES")]
    [SerializeField] [Tooltip("can tower be placed here?")]
    bool m_isPlacable = false;
    ///public getter of above
    public bool IsPlacable{ get {return m_isPlacable;} }

    ///////////////////////////////////////////////

    void Awake() 
    {
        SetupCache();
        AssertComponents();

        Initialization();
    }

    void Start()
    {   
    }

    void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
            PlaceTower();
    }

#region Initialization

    void SetupCache()
    {
        m_gridManager = FindObjectOfType<GridManager>();
        m_pahtfinder = FindObjectOfType<Pathfinder>();
    }
    
    void AssertComponents()
    {
        UnityEngine.Assertions.Assert.IsNotNull(m_towerPrefab, $"Script: {GetType().ToString()} variable m_boxCollider is null");
        UnityEngine.Assertions.Assert.IsNotNull(m_gridManager, $"Script: {GetType().ToString()} variable m_gridManager is null");
        UnityEngine.Assertions.Assert.IsNotNull(m_pahtfinder, $"Script: {GetType().ToString()} variable m_pahtfinder is null");
    }

    private void Initialization()
    {
        if (m_gridManager)
        {
            m_coordinates = m_gridManager.GetCoordinatesFromPosition(transform.position);

            if (!m_isPlacable)
            {
                m_gridManager.BlockNode(m_coordinates);
            }
        }
    }

#endregion

#region Towers

    private void PlaceTower()
    {
        // CustomDebug.Log($"m_gridManager: {m_gridManager.ToString()}, m_pahtfinder: {m_pahtfinder.ToString()}" +
        // ", gridmanager get node: {m_gridManager.GetNode(m_coordinates).ToString()}");
        if (m_towerPrefab && m_towerPrefab.CanAffordTower() &&
             m_gridManager.GetNode(m_coordinates) != null &&
             m_gridManager.GetNode(m_coordinates).isWalkable && !m_pahtfinder.WillBlockPath(m_coordinates))
        {
            bool IsSuccessful = m_towerPrefab.CreateTower(m_towerPrefab, transform.position);
            // Instantiate(m_towerPrefab, transform.position, Quaternion.identity);
            if(IsSuccessful)
            {
                m_gridManager.BlockNode(m_coordinates);
                m_pahtfinder.NotifyReceivers();
            }                
        }
    }

#endregion
}
