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

    TextMeshPro label = null;
    Vector2Int coordinates = new Vector2Int();

    Waypoint m_waypoint;

    void Awake() 
    {
#if UNITY_EDITOR
        label = GetComponent<TextMeshPro>();
        m_waypoint = GetComponentInParent<Waypoint>();
        
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
            label.enabled = true;
        }        

        SetLabelColor();
        ToggleLabels();
#endif
    }

    void SetLabelColor()
    {
        label.color = m_waypoint.IsPlacable ? m_defaultColor : m_blockedColor;
    }

    void DisplayCoordinates()
    {
#if UNITY_EDITOR
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        label.text = coordinates.x + ", " +coordinates.y;
        label.enabled = false;
#endif
    }

    void UpdateObjectName()
    {
#if UNITY_EDITOR
        transform.parent.name = coordinates.ToString();
        label.color = m_waypoint.IsPlacable ? m_defaultColor : m_blockedColor;
#endif
    }

    void ToggleLabels()
    {
#if UNITY_EDITOR
        if(UnityEngine.InputSystem.Keyboard.current.cKey.wasPressedThisFrame)
        {
            label.enabled = !label.IsActive();                        
        }
#endif
    }
}
