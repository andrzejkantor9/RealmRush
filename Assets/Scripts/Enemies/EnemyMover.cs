using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    [Header("CACHE")]
    [SerializeField] [HideInInspector]
    Enemy m_enemy = null;

    GridManager m_gridManager;
    Pathfinder m_pathfinder;
    List<Node> m_pathList = new List<Node>();

    [Space(10)] [Header("PROPERTIES")]
    [SerializeField] [Range(0f, 10f)] [Tooltip("How fast enemy moves")]
    float m_speed = 1f;
    ///////////////////////////////////////////////

    private void OnValidate() 
    {   
             
    }

    void Awake() 
    {        
        SetupCache();
        AssertCache();
    }

    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        
        StartCoroutine(FollowPath());
    }

#region SetupScript
    void SetupCache()
    {
        m_enemy = GetComponent<Enemy>();

        m_gridManager = FindObjectOfType<GridManager>();
        m_pathfinder = FindObjectOfType<Pathfinder>();
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
        m_pathList = m_pathfinder.GetNewPath();        
    }

    void ReturnToStart()
    {
        transform.position = m_gridManager.GetPositionFromCoordinates(m_pathfinder.startCoordinates);
    }

    IEnumerator FollowPath()
    {
        for(int i =0; i < m_pathList.Count; ++i)
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = m_gridManager.GetPositionFromCoordinates(m_pathList[i].coordinates);
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
