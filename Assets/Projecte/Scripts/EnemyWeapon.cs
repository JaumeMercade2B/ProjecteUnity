using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{

    public GameObject bullet;

    private EnemyBehaviour enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<EnemyBehaviour>();
    }

    public void Shoot()
    {
        GameObject bulletObject = Instantiate(bullet, transform.position, Quaternion.identity);
        bulletObject.transform.forward = enemy.transform.forward;
    }

    

}
