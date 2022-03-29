using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinding : MonoBehaviour 
{
    private Vector3 yUp, yDown, xRight, xLeft;
    private Vector3Int up, down, right, left;
    private List<Vector3> availablePoints = new List<Vector3>();


    void OnEnable() 
    {
      
    }
    public void moveEnemy(Tilemap walkAbleMap) 
    {

        yUp = transform.position + Vector3.up;
        yDown = transform.position + Vector3.down;
        xRight = transform.position + Vector3.right;
        xLeft = transform.position + Vector3.left;

        up = walkAbleMap.WorldToCell(yUp);
        down = walkAbleMap.WorldToCell(yDown);
        right = walkAbleMap.WorldToCell(xRight);
        left = walkAbleMap.WorldToCell(xLeft);


        if (walkAbleMap.HasTile(up))
            availablePoints.Add(yUp);
        if (walkAbleMap.HasTile(down))
            availablePoints.Add(yDown);
        if (walkAbleMap.HasTile(right))
            availablePoints.Add(xRight);
        if (walkAbleMap.HasTile(left))
            availablePoints.Add(xLeft);




        print(availablePoints.Count);
        int indexxxx = Random.Range(0, availablePoints.Count);
        print(indexxxx);
        transform.position = availablePoints[indexxxx];
        availablePoints.Clear();
    }

}
