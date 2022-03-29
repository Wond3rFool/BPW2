using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 5f;
    public Transform movePoint;
    public LayerMask stopMovement;

    private GridController grid;
    private void Awake()
    {
        grid = FindObjectOfType<GridController>();
        
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

        if (Vector3.Distance(transform.position, movePoint.position) == 0f && GameManager.isMenuing == false) 
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f) 
            {
                if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .2f, stopMovement))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    GameManager.isPlayerTurn = false;
                }    
            }
            else if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
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
            GameManager.isMenuing = true;
            grid.HighlightAction(transform.gameObject);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            GameManager.isMenuing = false;
            grid.DeHighlightAction(transform.gameObject);
        }

    }
}