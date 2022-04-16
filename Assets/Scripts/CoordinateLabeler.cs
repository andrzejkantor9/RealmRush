using UnityEngine;

using TMPro;
using System;
using UnityEngine.InputSystem;

[ExecuteAlways] [RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField]
    Color m_defaultColor = Color.white;
    [SerializeField]
    Color m_blockedColor = Color.grey;
    [SerializeField]
    Color m_exploredColor = Color.yellow;
    [SerializeField]
    Color m_pathColor = new Color(1f, .5f, 0f);

    TextMeshPro m_label = null;
    Vector2Int m_coordinates = new Vector2Int();

    GridManager m_gridManager = null;

    void Awake() 
    {
#if UNITY_EDITOR
        m_gridManager = FindObjectOfType<GridManager>();

        m_label = GetComponent<TextMeshPro>();
        
        DisplayCoordinates();
#endif
    }

    void Update()
    {
#if UNITY_EDITOR
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
            m_label.enabled = true;
        }        

        SetLabelColor();
        ToggleLabels();
#endif
    }

    void SetLabelColor()
    {
        if(m_gridManager == null) 
            return;

        Node node = m_gridManager.GetNode(m_coordinates);

        if(node == null)
            return;

        if(!node.isWalkable)
        {
            m_label.color = m_blockedColor;
        }
        else if(node.isPath)
        {
            m_label.color = m_pathColor;
        }
        else if(node.isExplored)
        {
            m_label.color = m_exploredColor;
        }
        else
        {
            m_label.color = m_defaultColor;
        }
    }

    void DisplayCoordinates()
    {
#if UNITY_EDITOR
        if(m_gridManager == null) return;

        m_coordinates.x = Mathf.RoundToInt(transform.parent.position.x / m_gridManager.UnityGridSize);
        m_coordinates.y = Mathf.RoundToInt(transform.parent.position.z / m_gridManager.UnityGridSize);

        m_label.text = m_coordinates.x + ", " +m_coordinates.y;
        m_label.enabled = false;
#endif
    }

    void UpdateObjectName()
    {
#if UNITY_EDITOR
        transform.parent.name = m_coordinates.ToString();
        // m_label.color = m_waypoint.IsPlacable ? m_defaultColor : m_blockedColor;
#endif
    }

    void ToggleLabels()
    {
#if UNITY_EDITOR
        if(UnityEngine.InputSystem.Keyboard.current.cKey.wasPressedThisFrame)
        {
            m_label.enabled = !m_label.IsActive();                        
        }
#endif
    }
}
