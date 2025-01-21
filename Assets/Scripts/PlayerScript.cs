using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float fireRate;

    [Header("Bullet Stats")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletDamage;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject slamVisual;

    private float lastShot;

    private bool dashing;
    private Vector2 dashVel;

    private void Start()
    {
        lastShot = fireRate;
    }

    private void Update()
    {
        MovePlayer();
        PlayerActions();
    }

    void MovePlayer()
    {
        if (dashing) return;

        // WASD and look towards mouse
        Vector3 lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition)
            - transform.position;
        lookDirection.z = 0;
        transform.up = lookDirection.normalized;

        Vector3 moveTowards = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) moveTowards.y = 1;
        if (Input.GetKey(KeyCode.S)) moveTowards.y = -1;
        if (Input.GetKey(KeyCode.D)) moveTowards.x = 1;
        if (Input.GetKey(KeyCode.A)) moveTowards.x = -1;

        transform.position += moveTowards.normalized * moveSpeed * Time.deltaTime;
    }
    
    void PlayerActions()
    {
        lastShot += Time.deltaTime;
        if (Input.GetMouseButton(0) && lastShot >= fireRate) Shoot();
        if (Input.GetKeyDown(KeyCode.LeftShift)) StartCoroutine(Dash());
        if (Input.GetKeyDown(KeyCode.Alpha1)) StartCoroutine(Slam());
    }

    void Shoot()
    {
        // Pool this when creating the actual game
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        bullet.transform.up = transform.up;

        BulletScript bulletScript = bullet.GetComponent<BulletScript>();
        bulletScript.bulletDamage = bulletDamage;
        bulletScript.bulletSpeed = bulletSpeed;

        lastShot = 0;
    }

    IEnumerator Dash()
    {
        dashing = true;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 tranformPos = transform.position;

        Vector2 dashDir = (mousePos - tranformPos).normalized;
        dashVel = dashDir * moveSpeed * 5;

        GetComponent<Rigidbody2D>().velocity = dashVel;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        dashing = false;
    }

    IEnumerator Slam()
    {
        // Create OverlapCircleAll
        Vector2 playerPos = transform.position;
        Collider2D[] collided = Physics2D.OverlapCircleAll(
            playerPos,
            slamVisual.transform.localScale.x/2
        );

        foreach (Collider2D col in collided)
        {
            if (col.gameObject.CompareTag("Enemy")) Destroy(col.gameObject);
        }

        GameObject slamVis = Instantiate(slamVisual, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.4f);
        Destroy(slamVis);
    }

}
