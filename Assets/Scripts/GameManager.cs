using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public enum BattleState { Start, PlayerAction, EnemyAction }
    public BattleState state;

    public GameObject enemyObj;

    private EnemyController eController;
    [SerializeField]
    private Tilemap tileMap;

    public List<GameObject> enemies;
    public static bool isPlayerTurn;

    int random;

    private void Awake()
    {
        state = BattleState.PlayerAction;
        isPlayerTurn = true;
        Cursor.visible = false;
    }
    private void Start()
    {
        state = BattleState.Start;
    }
    private void Update()
    {
        switch (state) 
        {
            case BattleState.Start: 
                {
                    for (int i = 0; i < 10; i++)
                    {
                        random = Random.Range(0, DungeonGenerator.walkAbleTiles.Count);
                        enemies.Add(Instantiate(enemyObj, DungeonGenerator.walkAbleTiles[random] + new Vector3(0.5f, 0.5f, 0), Quaternion.identity));
                    }
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
                        eController = enemies[i].GetComponentInChildren<EnemyController>();
                        eController.moveEnemy(tileMap);
                    }
                    state = BattleState.PlayerAction;
                    isPlayerTurn = true;
                }
                break;
        
        }
    }
    

}
