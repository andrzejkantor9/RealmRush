using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    //CACHE
    // [Header("CACHE")]
    GridManager m_gridManager;
    Dictionary<Vector2Int, Node> m_grid;

    Node m_startNode = null;
    Node m_destinationNode = null;
    List<Node> m_neighbors = new List<Node>();
    Dictionary<Vector2Int, Node> m_reached = new Dictionary<Vector2Int, Node>();
    Queue<Node> m_frontier = new Queue<Node>();

    //PROPETIES
    // [Space(10)] 
    [Header("PROPERTIES")]
    [SerializeField]
    Vector2Int m_startCoordinates= Vector2Int.zero;
    public Vector2Int startCoordinates {get {return m_startCoordinates;}}

    [SerializeField]
    Vector2Int m_destinationCoordinates = Vector2Int.zero;
    public Vector2Int destinationCoordinates {get {return m_destinationCoordinates;}}

    Vector2Int[] m_directions = {Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down};

    //STATES
    Node m_currentSearchNode = null;

    ///////////////////////////////////////////////////////////////

    void Awake()
    {
        SetupCache();
    }

    void Start()
    {
        GetNewPath();
    }

    #region Initialization

    void SetupCache()
    {
        m_gridManager = FindObjectOfType<GridManager>();
        if(m_gridManager != null)
        {
            m_grid = m_gridManager.grid;
            m_startNode = m_grid[m_startCoordinates];
            m_destinationNode = m_grid[m_destinationCoordinates];
        }
    }

#endregion

#region Pathfinding

    void ExploreNeighbors()
    {
        // List<Node> neighbors = new List<Node>();

        // foreach (Vector2Int direction in m_directions)
        // {
        //     Vector2Int neighborCoordinates = m_currentSearchNode.coordinates + direction;

        //     if(m_grid.ContainsKey(neighborCoordinates))
        //     {
        //         neighbors.Add(m_grid[neighborCoordinates]);
        //     }
        // }

        foreach (Vector2Int direction in m_directions)
        {
            Node node = m_gridManager.GetNode(m_currentSearchNode.coordinates + direction);
            if( node != null)
            {
                m_neighbors.Add(node);
            }               
        }

        // CustomDebug.Log($"m_currentSearchNode: {m_currentSearchNode.coordinates.ToString()}, m_neighbors count: {m_neighbors.Count.ToString()}");
        foreach(Node neighbor in m_neighbors)
        {
            if(!m_reached.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                // CustomDebug.Log($"node: {neighbor.coordinates.ToString()}, connected to: {m_currentSearchNode.coordinates.ToString()}");
                neighbor.connectedTo = m_currentSearchNode;
                m_reached.Add(neighbor.coordinates, neighbor);
                m_frontier.Enqueue(neighbor);
            }
        }

        // CustomDebug.Log($"<color=red>________________________________</color>");
    }

    void BreadthFirstSearch(Vector2Int coordinates)
    {
        m_startNode.isWalkable = true;
        m_destinationNode.isWalkable = true;

        m_frontier.Clear();
        m_reached.Clear();

        bool IsRunning = true;

        // m_gridManager.ResetAllNodesPathVariables();        

        m_frontier.Enqueue(m_grid[coordinates]);
        m_reached.Add(coordinates, m_grid[coordinates]);

        while(m_frontier.Count > 0 && IsRunning)
        {
            m_currentSearchNode = m_frontier.Dequeue();
            m_currentSearchNode.isExplored = true;
            ExploreNeighbors();

            if(m_currentSearchNode.coordinates == m_destinationCoordinates)
            {
                IsRunning = false;
            }
        }
    }
bool once = false;
    public List<Node> GetNewPath()
    {
        return GetNewPath(m_startCoordinates);     
    }

    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        // Debug.Log("GetNewPath start");
        m_gridManager.ResetNodes();
        m_neighbors.Clear();
        BreadthFirstSearch(coordinates);

        return BuildPath();        
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = m_destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true;
        // CustomDebug.Log($"path coord: {currentNode.coordinates.ToString()}, is walkable: {currentNode.isWalkable.ToString()}");

        while (currentNode.connectedTo != null)        
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
            // CustomDebug.Log($"path coord: {currentNode.coordinates.ToString()}, is walkable: {currentNode.isWalkable.ToString()}");
        }

        path.Reverse();
        return path;
    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if(m_grid.ContainsKey(coordinates))
        {
            bool previousState = m_grid[coordinates].isWalkable;

            m_grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            m_grid[coordinates].isWalkable = previousState;

            if(newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }

        return false;
    }

    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }

#endregion
}
