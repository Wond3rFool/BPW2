using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum BattleState { Start, PlayerAction, EnemyAction }

public class GameManager : MonoBehaviour
{
    
    public BattleState state;
   
    [SerializeField]
    private GameObject enemyObj;
    [SerializeField]
    private Tilemap tileMap;
    [SerializeField]
    private GameObject stairs;

    private EnemyController eController;
    private Vector3 offset;
    public List<GameObject> enemies;
    public static bool isPlayerTurn;
    public static bool pressedReset;

    int random;

    private void Awake()
    {
        state = BattleState.PlayerAction;
        isPlayerTurn = true;
        pressedReset = false;
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
                    if (enemies.Count > 0)
                    {
                        foreach (var e in enemies) 
                        {
                            Destroy(e);    
                        }
                        enemies.Clear();
                    }
                        
                    
                    random = Random.Range(0, DungeonGenerator.walkAbleTiles.Count);
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

                    Instantiate(stairs, DungeonGenerator.walkAbleTiles[random] + offset, Quaternion.identity);

                    state = BattleState.PlayerAction;
                }
                break;
            case BattleState.PlayerAction: 
                {
                    if(!isPlayerTurn)
                    {
                        state = BattleState.EnemyAction;
                    }
                    if (pressedReset)
                    {
                        state = BattleState.Start;
                        pressedReset = false;
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
