using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float bulletSpeed = 0;
    public float bulletDamage = 0;

    private float bulletCurrTime = 0, bulletMaxTime = 2;

    private void Update()
    {
        MoveBullet();
        bulletCurrTime += Time.deltaTime;
        if (bulletCurrTime > bulletMaxTime) Destroy(this.gameObject);
    }

    void MoveBullet()
    {
        transform.position += transform.up * bulletSpeed * Time.deltaTime;
    }
}
