using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [Header("CACHE")]
    [SerializeField] [Tooltip("add waypoints for enemy here in order")]
    List<Waypoint> m_pathList = new List<Waypoint>();

    [Space(10)] [Header("PROPERTIES")]
    [SerializeField] [Range(0f, 10f)] [Tooltip("How fast enemy moves")]
    float m_speed = 1f;

    //STATES
    ///////////////////////////////////////////////

    void Awake() 
    {
        AssertComponents();
    }

    void Start()
    {
        StartCoroutine(FollowPath());
    }

#region SetupScript
    void AssertComponents()
    {
        if(m_pathList.Count == 0)
            Debug.LogWarning("m_pathList has no entries!");
    }
#endregion

#region EnemyMovement
    IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in m_pathList)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while(travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * m_speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }            
        }
    }
#endregion

}
