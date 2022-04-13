using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [Header("CACHE")]
    [SerializeField] [Tooltip("add waypoints for enemy here in order")]
    List<Waypoint> m_pathList = new List<Waypoint>();

    [SerializeField] [HideInInspector]
    Enemy m_enemy = null;

    [Space(10)] [Header("PROPERTIES")]
    [SerializeField] [Range(0f, 10f)] [Tooltip("How fast enemy moves")]
    float m_speed = 1f;
    ///////////////////////////////////////////////

    private void OnValidate() 
    {   
        SetupCacheOnValidate();     
    }

    void Awake() 
    {        
        AssertCache();
    }

    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        
        StartCoroutine(FollowPath());
    }

#region SetupScript
    void SetupCacheOnValidate()
    {
        m_enemy = GetComponent<Enemy>();
    }

    void AssertCache()
    {
        if(m_pathList.Count == 0)
            Debug.LogWarning("m_pathList has no entries!");

        UnityEngine.Assertions.Assert.IsNotNull(m_enemy, $"Script: {GetType().ToString()} variable m_enemy is null");
    }
#endregion

#region EnemyMovement
    void FindPath()
    {
        m_pathList.Clear();

        GameObject parent = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform child in parent.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();
            if(waypoint)
                m_pathList.Add(waypoint);
        }
    }

    void ReturnToStart()
    {
        transform.position = m_pathList[0].transform.position;
    }

    IEnumerator FollowPath()
    {
        foreach (Waypoint waypoint in m_pathList)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * m_speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
    }

    void FinishPath()
    {
        m_enemy.PenalizeGold();
        gameObject.SetActive(false);
    }
    #endregion

}
