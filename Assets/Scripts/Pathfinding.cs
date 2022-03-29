using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinding : MonoBehaviour 
{
    private Vector3 yUp, yDown, xRight, xLeft;

    private Tilemap walkAbleMap;
    private GroundMap groundMap;
    private List<Vector3> availablePoints = new List<Vector3>();


    void OnEnable() 
    {
        groundMap = FindObjectOfType<GroundMap>();
        walkAbleMap = groundMap.GetComponent<Tilemap>();
      
    }
    public void moveEnemy() 
    {

        yUp = new Vector3(transform.position.x, transform.position.y + 1, 0);
        yDown = new Vector3(transform.position.x, transform.position.y - 1, 0);
        xRight = new Vector3(transform.position.x + 1, transform.position.y, 0);
        xLeft = new Vector3(transform.position.x - 1, transform.position.y, 0);

        if (walkAbleMap.HasTile(Vector3Int.FloorToInt(yUp)))
            availablePoints.Add(yUp);
        if (walkAbleMap.HasTile(Vector3Int.FloorToInt(yDown)))
            availablePoints.Add(yDown);
        if (walkAbleMap.HasTile(Vector3Int.FloorToInt(xRight)))
            availablePoints.Add(xRight);
        if (walkAbleMap.HasTile(Vector3Int.FloorToInt(xLeft)))
            availablePoints.Add(xLeft);



        print(availablePoints.Count);
        int indexxxx = Random.Range(0, availablePoints.Count);
        print(indexxxx);
        transform.position = availablePoints[indexxxx];
        availablePoints.Clear();
    }

}
