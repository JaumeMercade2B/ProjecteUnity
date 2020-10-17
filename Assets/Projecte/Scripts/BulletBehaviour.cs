using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed = 30f;
    public float range;
    public float maxRange;
    public float damage = 1;

    public GameObject explosionPrefab;
    

    private EnemyBehaviour  enemy;


    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        range += Time.deltaTime;

        if (range >= maxRange)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Boundaries")
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground")
        {
           
           
            Destroy(gameObject);
        }

        if (other.tag == "Enemy")
        {
            enemy.GetDamage(damage);
            Destroy(gameObject);
        }

    }



}
