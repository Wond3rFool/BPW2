using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask stopMovement;

    public static bool onStairs;
    private GridController grid;
    private Inventory inventory;

    [SerializeField]
    private UI_Inventory uiInventory;
    private void Awake()
    {
        grid = FindObjectOfType<GridController>();
        inventory = new Inventory(UseItem);
        uiInventory.SetInventory(inventory);
        onStairs = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, movePoint.position) == 0f) 
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f && GameManager.isPlayerTurn) 
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, stopMovement))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    GameManager.isPlayerTurn = false;
                }    
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f && GameManager.isPlayerTurn)
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .2f, stopMovement))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                    GameManager.isPlayerTurn = false;
                }
            }

        }
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            //GameManager.isMenuing = true;
            grid.HighlightAction(transform.gameObject);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
           // GameManager.isMenuing = false;
            grid.DeHighlightAction(transform.gameObject);
        }
        if (onStairs) 
        {
            if (Input.GetKeyDown(KeyCode.L)) 
            {
                DungeonGenerator generator = FindObjectOfType<DungeonGenerator>();
                generator.RegenSquares();
                GameManager.pressedReset = true;
                onStairs = false;
            }
            if (Input.GetKeyDown(KeyCode.Escape)) 
            {
                Application.Quit();
            
            }
        
        }
    }

    private void UseItem(Item item) 
    {
        switch (item.itemType) 
        {
            case Item.ItemType.HealthPotion:
                //do health stuff here
                inventory.RemoveItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
                print("Healing");
                GameManager.isPlayerTurn = false;
                break;
            case Item.ItemType.ManaPotion:
                //do mana stuff here
                inventory.RemoveItem(new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
                print("Mana regenerated");
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();

        if(itemWorld != null) 
        {
            print("Touching an item");
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }

    }

}
