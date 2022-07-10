using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    int numberOfProjectiles;

    [SerializeField]
    GameObject projectile;

    GameObject[] projectiles;

    Vector2 startPoint;
    float radius, moveSpeed, pRotation;
    // Start is called before the first frame update
    void Start()
    {
        radius = 5f;
        moveSpeed = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.isPlayerTurn) return;

        if (Input.GetKeyDown(KeyCode.Q))
        { 
            startPoint = transform.position;
            SpawnProjectiles(numberOfProjectiles);
            GameManager.isPlayerTurn = false;
        }
    }

    void SpawnProjectiles(int numberOfProjectiles)
    {
        float angleStep = 360f / numberOfProjectiles;

        float angle = 0f;

        pRotation = 0f;

        for (int i = 0; i <= numberOfProjectiles - 1; i++)
        {
            float dirX = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float dirY = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 pVector = new Vector2(dirX, dirY);
            Vector2 pMoveDir = (pVector - startPoint).normalized * moveSpeed;

            var proj = Instantiate(projectile, startPoint, Quaternion.Euler(0,0,pRotation));
            proj.GetComponentInChildren<Rigidbody2D>().velocity = new Vector2(pMoveDir.x, pMoveDir.y);
            pRotation -= 90f;
            Destroy(proj, 0.5f);
            angle += angleStep;
        }
    }
}
