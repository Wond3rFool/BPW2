using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Toolbox;

public class Pathfinding : MonoBehaviour 
{
    private Vector3 startPos;
    private Vector3 thisPos;
    private Vector3 endPos;
    [SerializeField]
    private Tilemap walkAbleMap;
    private List<Vector3> wayPoints;

    private void Awake()
    {
        wayPoints = new List<Vector3>();
    }

    private void Update()
    {
       
           
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            thisPos = transform.position;
            startPos = walkAbleMap.WorldToCell(thisPos);
            endPos = DungeonGenerator.walkAbleTiles[Random.Range(0,DungeonGenerator.walkAbleTiles.Count)];
            AStar.FindPath(walkAbleMap, startPos, endPos);
        }
    }
}
