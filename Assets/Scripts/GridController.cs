using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class GridController : MonoBehaviour
{
    [SerializeField]
    private Tilemap interactiveMap;
    [SerializeField]
    private Tile hoverTile;
    [SerializeField]
    private Tile groundTile;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Mouse over -> highlight tile
    }
    public void HighlightAction(GameObject playerPos)
    {
        interactiveMap.SetTile(new Vector3Int((int)playerPos.transform.position.x, (int)playerPos.transform.position.y-1, 0), hoverTile); 
        interactiveMap.SetTile(new Vector3Int((int)playerPos.transform.position.x+1, (int)playerPos.transform.position.y, 0), hoverTile);
        interactiveMap.SetTile(new Vector3Int((int)playerPos.transform.position.x, (int)playerPos.transform.position.y+1, 0), hoverTile);
        interactiveMap.SetTile(new Vector3Int((int)playerPos.transform.position.x-1, (int)playerPos.transform.position.y, 0), hoverTile);
    }
    public void DeHighlightAction(GameObject playerPos)
    {
        interactiveMap.SetTile(new Vector3Int((int)playerPos.transform.position.x, (int)playerPos.transform.position.y - 1, 0), groundTile);
        interactiveMap.SetTile(new Vector3Int((int)playerPos.transform.position.x + 1, (int)playerPos.transform.position.y, 0), groundTile);
        interactiveMap.SetTile(new Vector3Int((int)playerPos.transform.position.x, (int)playerPos.transform.position.y + 1, 0), groundTile);
        interactiveMap.SetTile(new Vector3Int((int)playerPos.transform.position.x - 1, (int)playerPos.transform.position.y, 0), groundTile);
    }

}
