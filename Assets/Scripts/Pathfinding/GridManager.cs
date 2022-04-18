using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO add sounds
//TODO add vin condition, lose condition
//TODO add tower cost ui, tower build time ui
//TODO add explosions, arrow trails
//TODO add pause and escape menu
public class GridManager : MonoBehaviour
{
    //CACHE
    [Header("CACHE")]
    [SerializeField]
    Vector2Int m_gridSize = Vector2Int.zero;
    [SerializeField][Tooltip("word grid size - should match UnityEditor snap settings.")]
    int m_unitydGridSize = 10;
    public int UnityGridSize { get {return m_unitydGridSize;}}

    Dictionary<Vector2Int, Node> m_grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> grid { get {return m_grid;}}

    /////////////////////////////////////////////////////////////////////////////////

    void Awake() 
    {        
        CreateGrid();
    }

#region  Initialization

    void CreateGrid()
    {
        for(int x = 0; x < m_gridSize.x; ++x)
        {
            for(int y = 0; y <m_gridSize.y; ++y)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                m_grid.Add(coordinates, new Node(coordinates, true));
                // CustomDebug.Log(m_grid[coordinates].coordinates + " = " + m_grid[coordinates].isWalkable);
            }
        }
    }

#endregion

#region ManipulateNodes

    public Node GetNode(Vector2Int coordinates)
    {
        if(m_grid.ContainsKey(coordinates))
        {
            return m_grid[coordinates];
        }

        return null;
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = Vector2Int.zero;

        coordinates.x = Mathf.RoundToInt(position.x / m_unitydGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / m_unitydGridSize);

        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = Vector3.zero;

        position.x = (float)(coordinates.x * m_unitydGridSize);
        position.z = (float)(coordinates.y * m_unitydGridSize);

        return position;
    }


    public void BlockNode(Vector2Int coordinate)
    {
        if(m_grid.ContainsKey(coordinate))
        {
            m_grid[coordinate].isWalkable = false;
            CustomDebug.Log($"{coordinate.ToString()} is blocked.");
        }
    }

    public void ResetNodes()
    {
        // foreach(var gridCell in m_grid)
        // {
        //     gridCell.Value.isPath = false;
        //     gridCell.Value.isExplored = false;
        // }

        foreach(KeyValuePair<Vector2Int, Node> entry in m_grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

#endregion
}
