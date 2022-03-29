using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public enum BattleState { Start, PlayerAction, EnemyAction }
    public BattleState state;

    public GameObject obj;

    private Pathfinding pathFind;

    public List<GameObject> gameObjects;

    public static bool isMenuing;

    public static bool isPlayerTurn;

    int random;

    private void Awake()
    {
        state = BattleState.PlayerAction;
        isPlayerTurn = true;
        Cursor.visible = false;
        gameObjects = new List<GameObject>();
    }
    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            random = Random.Range(0, DungeonGenerator.walkAbleTiles.Count);
            Instantiate(obj, DungeonGenerator.walkAbleTiles[random] + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
            gameObjects.Add(obj);
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
                    foreach (GameObject obj in gameObjects) 
                    {
                        pathFind = obj.GetComponent<Pathfinding>();
                        pathFind.moveEnemy();
                    }
                    state = BattleState.PlayerAction;
                    isPlayerTurn = true;
                }
                break;
        
        }
    }
    

}
