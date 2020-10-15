using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed = 30;
    public float damage = 1;
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
    }

    public void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Boundaries")
        {
            Debug.Log("Boundarie");
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

            Destroy(gameObject);
            enemy.GetDamage(damage);
            
        }
    }



}
