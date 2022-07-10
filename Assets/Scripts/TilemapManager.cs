using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{ 
    Dictionary<string, Tilemap> tilemaps = new Dictionary<string, Tilemap>();
    Dictionary<TileBase, LevelTile> tileBaseToLevelTile = new Dictionary<TileBase, LevelTile>();
    Dictionary<string, TileBase> guidToTileBase = new Dictionary<string, TileBase>();

    [SerializeField] Bounds bounds;
    [SerializeField] string filename = "tilemapData.json";

    private void Start()
    {
        InitTileMaps();
        InitTileReferences();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) SaveMap();
        if (Input.GetKeyDown(KeyCode.M)) LoadMap();
    }
    private void InitTileReferences() 
    {
        LevelTile[] levelTiles = Resources.LoadAll<LevelTile>("Tiles/");

        foreach (LevelTile lTile in levelTiles) 
        {
            if (!tileBaseToLevelTile.ContainsKey(lTile.TileBase)) 
            {
                tileBaseToLevelTile.Add(lTile.TileBase, lTile);
                guidToTileBase.Add(lTile.name, lTile.TileBase);
            }
        
        }
    }
    private void InitTileMaps() 
    {
        Tilemap[] maps = FindObjectsOfType<Tilemap>();

        foreach (var map in maps) 
        {
            tilemaps.Add(map.name, map);    
        
        }
    }

    public void SaveMap()
    {
       List<TilemapData> data = new List<TilemapData>();

        foreach (var mapObj in tilemaps) 
        {
            TilemapData mapData = new TilemapData();
            mapData.key = mapObj.Key;

            BoundsInt boundsForThisMap = mapObj.Value.cellBounds;

            for (int x = boundsForThisMap.xMin; x < boundsForThisMap.xMax; x++)
            {
                for (int y = boundsForThisMap.yMin; y < boundsForThisMap.yMax; y++)
                {
                    Vector3Int pos = new Vector3Int(x, y, 0);
                    TileBase tile = mapObj.Value.GetTile(pos);

                    if (tile != null && tileBaseToLevelTile.ContainsKey(tile))
                    {
                        String guid = tileBaseToLevelTile[tile].name;
                        TileInfo ti = new TileInfo(pos, guid);
                        // Add "TileInfo" to "Tiles" List of "TilemapData"
                        mapData.tiles.Add(ti);
                    }
                }
            }

            data.Add(mapData);
        }

        FileHandler.SaveToJSON<TilemapData>(data, filename);


    }

    public void LoadMap()
    {
        List<TilemapData> data = FileHandler.ReadListFromJSON<TilemapData>(filename);

        foreach (var mapData in data)
        {
            // if key does NOT exist in dictionary skip it
            if (!tilemaps.ContainsKey(mapData.key))
            {
                Debug.LogError("Found saved data for tilemap called '" + mapData.key + "', but Tilemap does not exist in scene.");
                continue;
            }

            // get according map
            var map = tilemaps[mapData.key];

            // clear map
            map.ClearAllTiles();

            if (mapData.tiles != null && mapData.tiles.Count > 0)
            {
                foreach (var tile in mapData.tiles)
                {

                    if (guidToTileBase.ContainsKey(tile.guidForBuildable))
                    {
                        map.SetTile(tile.position, guidToTileBase[tile.guidForBuildable]);
                    }
                    else
                    {
                        Debug.LogError("Refernce " + tile.guidForBuildable + " could not be found.");
                    }

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

    
}
[Serializable]
public class TilemapData
{
    public string key;
    public List<TileInfo> tiles = new List<TileInfo>();

}
[Serializable]
public class TileInfo 
{
    public string guidForBuildable;
    public Vector3Int position;

    public TileInfo(Vector3Int pos, string guid) 
    {
        position = pos;
        guidForBuildable = guid;
    }
}
