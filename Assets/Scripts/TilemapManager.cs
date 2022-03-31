using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap groundMap, highLightMap, pitMap, wallMap;
    [SerializeField]
    private int lvlIndex;


    public void SaveMap()
    {
        var newLevel = ScriptableObject.CreateInstance<ScriptableLevel>();

        newLevel.levelIndex = lvlIndex;
        newLevel.name = $"Level {lvlIndex}";

        newLevel.groundTiles = GetTilesFromMap(groundMap).ToList();
        newLevel.highLightTiles = GetTilesFromMap(highLightMap).ToList();
        newLevel.pitTiles = GetTilesFromMap(pitMap).ToList();
        newLevel.wallTiles = GetTilesFromMap(wallMap).ToList();


        ScriptableObjectUtility.SaveLevelFile(newLevel);

        IEnumerable<SavedTile> GetTilesFromMap(Tilemap map)
        {
            foreach (var pos in map.cellBounds.allPositionsWithin) 
            {
                if (map.HasTile(pos)) 
                {
                    var levelTile = map.GetTile<LevelTile>(pos);

                    yield return new SavedTile()
                    {
                        position = pos,
                        tile = levelTile
                    };
                }         
            }
        }
    }
    public void ClearMap()
    {
        var maps = FindObjectsOfType<Tilemap>();

        foreach (var tilemap in maps) 
        {
            tilemap.ClearAllTiles();
        
        }
    
    }
    public void LoadMap()
    {
        var level = Resources.Load<ScriptableLevel>($"Levels/level {lvlIndex}");
        if (level == null) 
        {
            Debug.Log("Level index does not exist");
            return;
        }

        ClearMap();

        foreach (var savedTile in level.groundTiles) 
        {
            groundMap.SetTile(savedTile.position, savedTile.tile); 
        }
        foreach (var savedTile in level.pitTiles)
        {
            pitMap.SetTile(savedTile.position, savedTile.tile);           
        }
        foreach (var savedTile in level.highLightTiles)
        {
            highLightMap.SetTile(savedTile.position, savedTile.tile);
        }
        foreach (var savedTile in level.wallTiles)
        {
            switch (savedTile.tile.type) 
            {
                case TileType.topWall:
                case TileType.botWall:
                    wallMap.SetTile(savedTile.position, savedTile.tile);
                    break;
            }           
        }
    }
}


#if UNITY_EDITOR
public static class ScriptableObjectUtility 
{
    public static void SaveLevelFile(ScriptableLevel level)
    {
        AssetDatabase.CreateAsset(level, $"Assets/Resources/Levels/{level.name}.asset");

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
#endif