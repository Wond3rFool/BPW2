using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public enum BattleState { Start, PlayerAction, EnemyAction }
    public BattleState State;

    public GameObject obj;

    public static bool isMenuing;

    int random;

    private void Awake()
    {
        Cursor.visible = false;
    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.P)) 
        {
            for (int i = 0; i < 50; i++)
            {
                random = Random.Range(0, DungeonGenerator.walkAbleTiles.Count);
                Instantiate(obj, DungeonGenerator.walkAbleTiles[random] + new Vector3(0.5f,0.5f,0), Quaternion.identity);
                
            }
            
        
        }
    }
    // Update is called once per frame

}
