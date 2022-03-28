using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathnode 
{
    private Tilemap tiles;
    private int x;
    private int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public Pathnode cameFromNode;
    public Pathnode(Tilemap tiles, int x, int y)
    {
        this.tiles = tiles;
        this.x = x;
        this.y = y;
        
    }
}
