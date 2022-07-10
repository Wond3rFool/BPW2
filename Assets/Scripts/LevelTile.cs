using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public enum TileType
{
    ground = 1000,
    highlight = 1001,
    pit = 1002,
    topWall = 1003,
    botWall = 1004
}

[CreateAssetMenu(fileName ="New level Tile", menuName = "2D/Tiles/level Tile")]
public class LevelTile : ScriptableObject
{
    [SerializeField] TileType type;
    [SerializeField] TileBase tile;

    public TileBase TileBase { get { return tile; } }
   
    public TileType Type { get { return type; } }
}


