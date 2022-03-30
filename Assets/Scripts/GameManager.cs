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
    private Vector3 offset;
    public List<GameObject> enemies;
    public static bool isPlayerTurn;

    int random;

    private void Awake()
    {
        state = BattleState.PlayerAction;
        isPlayerTurn = true;
        Cursor.visible = false;
        offset = new Vector3(0.5f, 0.5f, 0);
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
                        enemies.Add(Instantiate(enemyObj, DungeonGenerator.walkAbleTiles[random] + offset, Quaternion.identity));
                    }
                    for (int j = 0; j < Random.Range(10, 100); j++) 
                    {
                        random = Random.Range(0, DungeonGenerator.walkAbleTiles.Count);
                        ItemWorld.SpawnItemWorld(DungeonGenerator.walkAbleTiles[random] + offset, new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
                        ItemWorld.SpawnItemWorld(DungeonGenerator.walkAbleTiles[random] + offset + Vector3.up, new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
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
