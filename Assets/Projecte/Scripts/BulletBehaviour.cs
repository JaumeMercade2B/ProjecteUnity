using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed = 30f;
    public float range;
    public float maxRange;
    public float damage = 1;

    //public GameObject explosionPrefab;
    

    private EnemyBehaviour  enemy;
    private FPSController player;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    virtual public void Update()
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
            enemy = other.gameObject.GetComponent<EnemyBehaviour>();
            enemy.GetDamage(damage);
            Destroy(gameObject);
        }

        if (other.tag == "Player")
        {
            player = other.gameObject.GetComponent<FPSController>();
            player.GetDamage(damage);
            Destroy(gameObject);
        }

    }



}
