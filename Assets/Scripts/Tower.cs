using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float towerRange;
    public float Damage;
    public float fireCoolDown;
    float _fireTime;

    public Transform turretBarrel;
    public Transform firePoint;
    public GameObject Bullet;

    Transform _enemyTarget;



    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SetEnemyTarget", 0f, 1f);
    }


    // Update is called once per frame
    void Update()
    {
        if (_enemyTarget != null)
        {
            Vector3 dir = _enemyTarget.position - turretBarrel.position;

            Quaternion lookTarget = Quaternion.LookRotation(dir);

            Vector3 rotate = Quaternion.Lerp(turretBarrel.rotation, lookTarget, 15 * Time.deltaTime).eulerAngles;

            turretBarrel.rotation = Quaternion.Euler(rotate.x, rotate.y, 0f);

            _fireTime += Time.deltaTime;
            if (_fireTime > fireCoolDown)
            {
                _fireTime = 0;
                Shoot();

            }


        }

    }

    void SetEnemyTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        Transform nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy.transform;
            }

        }

        if (shortestDistance <= towerRange)
        {
            _enemyTarget = nearestEnemy;
        }
        else
        {
            _enemyTarget = null;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(Bullet, firePoint.position, firePoint.rotation);
        bullet.GetComponent<BulletScript>().Damage = Damage;
        bullet.GetComponent<Rigidbody>().AddForce(firePoint.right * 10,ForceMode.Impulse);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, towerRange);
    }
}
