using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ScriptableLevel : ScriptableObject
{
    public int levelIndex;
    public List<SavedTile> groundTiles;
    public List<SavedTile> highLightTiles;
    public List<SavedTile> pitTiles;
    public List<SavedTile> wallTiles;
}

[Serializable]
public class SavedTile 
{
    public Vector3Int position;
    public LevelTile tile;
}

