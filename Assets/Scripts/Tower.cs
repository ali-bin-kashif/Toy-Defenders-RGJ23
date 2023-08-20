using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public AudioSource bulletaudio;
    public AudioSource audioturrent;
    public float towerRange;
    public float Damage;
    public float fireCoolDown;
    public float shootForce;
    public bool isMortar;
    float _fireTime;

    public Transform turretBarrel;
    public Transform firePoint;
    public GameObject Bullet;

    Transform _enemyTarget;



    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SetEnemyTarget", 0f, 1f);
        audioturrent = GetComponent<AudioSource>();
        audioturrent.Play();
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
        bulletaudio.Play();
        bullet.GetComponent<BulletScript>().Damage = Damage;
        bullet.GetComponent<Rigidbody>().AddForce(firePoint.forward * shootForce,ForceMode.Impulse);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, towerRange);
    }
}
