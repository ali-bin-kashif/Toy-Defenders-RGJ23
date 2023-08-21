using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    
    public float towerRange;
    public float Damage;
    public float fireCoolDown;
    public float shootForce;
    public bool isMortar, isHumanoid;
    float _fireTime;

    public Transform turretBarrel;
    public Transform firePoint;
    public GameObject Bullet;

    Transform _enemyTarget;
    AudioSource towerAudio;

    public AudioClip bulletShot, towerPlace;



    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SetEnemyTarget", 0f, 0.5f);
        towerAudio = GetComponent<AudioSource>();
        towerAudio.PlayOneShot(towerPlace);

    }


    // Update is called once per frame
    void Update()
    {
        if (_enemyTarget != null)
        {
            Vector3 dir = _enemyTarget.position - turretBarrel.position;

            Quaternion lookTarget = Quaternion.LookRotation(dir);

            Vector3 rotate = Quaternion.Lerp(turretBarrel.rotation, lookTarget, 15 * Time.deltaTime).eulerAngles;
            if(isMortar)
            {
                turretBarrel.rotation = Quaternion.Euler(-65f, rotate.y, 0f);
            }
            else if(isHumanoid)
            {
                turretBarrel.rotation = Quaternion.Euler(0f, rotate.y, 0f);
                firePoint.rotation = Quaternion.Euler(rotate.x, rotate.y, 0f);
            }
            else
            {
                turretBarrel.rotation = Quaternion.Euler(rotate.x, rotate.y, 0f);
            }
            

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
            if (distance < shortestDistance && !enemy.GetComponent<EnemyScript>().hasDied)
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
        towerAudio.PlayOneShot(bulletShot);
        if(isMortar)
        {
            bullet.GetComponent<Bomb>().Damage = Damage;
        }
        else
        {
            bullet.GetComponent<BulletScript>().Damage = Damage;
        }
        bullet.GetComponent<Rigidbody>().AddForce(firePoint.forward * shootForce,ForceMode.Impulse);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, towerRange);
    }
}
