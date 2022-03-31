using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    DungeonGenerator generator;

    private void Awake()
    {
        generator = FindObjectOfType<DungeonGenerator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>()) 
        {

            PlayerController.onStairs = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>()) 
        {
            if (PlayerController.onStairs == true)
                Destroy(this);
        }
    }
}
