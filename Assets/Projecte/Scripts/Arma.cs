using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Arma : MonoBehaviour
{
    /*
    private FPSController fpsController;
    public float amount = 0.02f;
    public float maxamount = 0.03f;
    public float smooth = 3;
    private Quaternion def;
    */

 
    private PlayerControls controls;

    private float timeCounter;
    public float cadency = 0.2f;
    public GameObject shootGO;
    public bool shoot = true;
    private Camera mainCamera;

    Ray ray;
    RaycastHit hit;
    private int distance = 50;

    private EnemyBehaviour enemy;


    void Awake()
    {
        /*
        fpsController = FindObjectOfType<FPSController>();
        def = transform.localRotation;
        */

        controls = new PlayerControls();
        mainCamera = Camera.main;
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBehaviour>();

    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }
    void FixedUpdate()
    {
        /*
        float factorZ = -(Input.GetAxis("Horizontal")) * amount;
        //float factorY = -(Input.GetAxis("Jump")) * amount;
        //float factorZ = -Input.GetAxis("Vertical") * amount;

        //if (factorX > maxamount)
        //factorX = maxamount;

        //if (factorX < -maxamount)
        //factorX = -maxamount;

        //if (factorY > maxamount)
        //factorY = maxamount;

        //if (factorY < -maxamount)
        //factorY = -maxamount;

        if (factorZ > maxamount)
            factorZ = maxamount;

        if (factorZ < -maxamount)
            factorZ = -maxamount;

        Quaternion Final = Quaternion.Euler(0, 0, def.z + factorZ);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Final, (Time.deltaTime * amount) * smooth);
        */

        if (!shoot)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= cadency)
            {
                shoot = true;
                timeCounter = 0;
            }
        }

        //PointingEnemy();
    }

    public void Shoot()
    {
        if (!shoot)
        {
            return;
        }

        shoot = false;
        GameObject bulletObject = Instantiate(shootGO, transform.position, Quaternion.identity);
        bulletObject.transform.forward = mainCamera.transform.forward;

    }

}