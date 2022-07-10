using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyController : MovingObject
{
    private Vector3 yUp, yDown, xRight, xLeft;
    private float health;
    private Vector3Int up, down, right, left, myPos;
    private List<Vector3> availablePoints = new List<Vector3>();
    public int playerDamage;
    private Animator animator;
    private Transform target;

    bool playerInRange;
    public bool isDead;

    protected override void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
        health = 10f;
        playerDamage = 1000;
        base.Start();
    }
    public void moveEnemy(Tilemap walkAbleMap)
    {
        if (playerInRange)
        {
            int xDir = 0;
            int yDir = 0;

            if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
                yDir = target.position.y > transform.position.y ? 1 : -1;
            else
                xDir = target.position.x > transform.position.x ? 1 : -1;

            AttemptMove<PlayerController>(xDir, yDir);
        }
        else
        {
            yUp = gameObject.transform.position + Vector3.up;
            yDown = gameObject.transform.position + Vector3.down;
            xRight = gameObject.transform.position + Vector3.right;
            xLeft = gameObject.transform.position + Vector3.left;

            myPos = walkAbleMap.WorldToCell(gameObject.transform.position);
            up = walkAbleMap.WorldToCell(yUp);
            down = walkAbleMap.WorldToCell(yDown);
            right = walkAbleMap.WorldToCell(xRight);
            left = walkAbleMap.WorldToCell(xLeft);


            if (walkAbleMap.HasTile(up))
                availablePoints.Add(yUp);
            if (walkAbleMap.HasTile(down))
                availablePoints.Add(yDown);
            if (walkAbleMap.HasTile(right))
                availablePoints.Add(xRight);
            if (walkAbleMap.HasTile(left))
                availablePoints.Add(xLeft);

            //print(availablePoints.Count);
            int indexxxx = Random.Range(0, availablePoints.Count);
            //print(indexxxx);
            gameObject.transform.position = availablePoints[indexxxx];
            availablePoints.Clear();


        }
        

        
    }
    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        base.AttemptMove<T>(xDir, yDir);
    }

    //OnCantMove is called if Enemy attempts to move into a space occupied by a Player, it overrides the OnCantMove function of MovingObject 
    //and takes a generic parameter T which we use to pass in the component we expect to encounter, in this case Player
    protected override void OnCantMove<T>(T component)
    {
        PlayerController hitPlayer = component as PlayerController;

        hitPlayer.TakeDamage(playerDamage);

        //Set the attack trigger of animator to trigger Enemy attack animation.
        //animator.SetTrigger("enemyAttack");
    }

    public void TakeDamage(float amount)
    {
        if (health > 0)
        {
            health -= amount;
            if (health <= 0)
            {
                isDead = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision);
        if (collision.GetComponent<PlayerController>())
        {
            playerInRange = true;
            Debug.Log("player in range");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            playerInRange = false;
        }
    }
}
