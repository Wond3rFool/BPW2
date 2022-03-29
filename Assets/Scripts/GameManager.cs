using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public enum BattleState { Start, PlayerAction, EnemyAction }
    public BattleState state;

    public Pathfinding obj;

    private Pathfinding pathFind;
    [SerializeField]
    private Tilemap tileMap;

    public List<Pathfinding> enemies;
    public static bool isPlayerTurn;

    int random;

    private void Awake()
    {
        state = BattleState.PlayerAction;
        isPlayerTurn = true;
        Cursor.visible = false;
        enemies = new List<Pathfinding>();
    }
    private void Start()
    {
        for (int i = 0; i < 1; i++)
        {
            random = Random.Range(0, DungeonGenerator.walkAbleTiles.Count);
            Instantiate(obj, DungeonGenerator.walkAbleTiles[random] + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
            enemies.Add(obj);
        }


    }
    private void Update()
    {
        switch (state) 
        {
            case BattleState.Start: 
                {
                    state = BattleState.PlayerAction;
                }
                break;
            case BattleState.PlayerAction: 
                {
                    if(!isPlayerTurn)
                    {
                        state = BattleState.EnemyAction;
                    }  
                }
                break;
            case BattleState.EnemyAction:
                {
                    for (int i = 0; i < enemies.Count; i++) 
                    {
                        pathFind = enemies[i].GetComponent<Pathfinding>();
                        pathFind.moveEnemy(tileMap);
                    }
                    state = BattleState.PlayerAction;
                    isPlayerTurn = true;
                }
                break;
        
        }
    }
    

}
