using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    [SerializeField] private float moveSpeed;

    private void Update()
    {
        MoveEnemy();
    }

    void MoveEnemy()
    {
        // Get move direction vector and multiply by moveSpeed
        Vector3 move = (GameObject.FindGameObjectWithTag("Player").transform.position - 
            transform.position).normalized;
        transform.up = move;
        transform.position += move * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
