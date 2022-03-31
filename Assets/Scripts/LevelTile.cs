using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName ="New level Tile", menuName = "2D/Tiles/level Tile")]
public class LevelTile : Tile
{
    public TileType type;
}

[Serializable]
public enum TileType 
{
    ground =    1000,
    highlight = 1001,
    pit =       1002,
    topWall =   1003,
    botWall =   1004
}