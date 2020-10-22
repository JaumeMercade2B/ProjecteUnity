using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BulletBehaviour
{

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    override public void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
