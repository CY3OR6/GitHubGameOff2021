using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform firePoint;
    public GameObject BulletPrefab;

    Rigidbody2D rb;

    public float timeBtwBullets = 1f;

    public float BulletForce = 20f;

    public float closetEnemy;

    public GameObject EnemyDir;

    public List<GameObject> enemys = new List<GameObject>();

    // Update is called once per frame
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(Shoot());
    }

    private void Update()
    {
        TurnTowards();
    }

    void TurnTowards()
    {
        Vector2 lookDir;
        if (enemys.Count <= 0)
        {
            lookDir = Vector2.zero;
        }
        else
        {
            lookDir = AutoLock().transform.position;
        }

        
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    IEnumerator  Shoot()
    {
        GameObject Bullet = Instantiate(BulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = Bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * BulletForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(timeBtwBullets);

        StopCoroutine(Shoot());
        StartCoroutine(Shoot());
    }

    GameObject AutoLock()
    {
        if(enemys.Count <= 0)
        {
            return null;
        }

        for(int i = 0; i < enemys.Count; i++)
        {
            if(Vector2.Distance(transform.position,enemys[i].transform.position) < closetEnemy)
            {
                EnemyDir = enemys[i];
                closetEnemy = Vector2.Distance(transform.position, enemys[i].transform.position);
            }
        }

        return EnemyDir;
    }
}
