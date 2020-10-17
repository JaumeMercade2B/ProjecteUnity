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
    public float damage = 1;
    public float ammo = 10;
    private float maxAmmo = 10;
    public float reloadTime = 3;
    public bool reloading;

    public GameObject shootGO;
    public bool shoot = true;
    private Camera mainCamera;

    private GameManager manager;

    Ray ray;
    RaycastHit hit;
    private int distance = 50;

    private EnemyBehaviour enemy;

    private HUD hud;

    private ChangeWeapon change;

    void Awake()
    {
        /*
        fpsController = FindObjectOfType<FPSController>();
        def = transform.localRotation;
        */

        controls = new PlayerControls();
        mainCamera = Camera.main;

        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent < GameManager > ();

        ammo = maxAmmo;
        reloading = false;

        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();
        change = GameObject.FindGameObjectWithTag("Holder").GetComponent<ChangeWeapon>();

        hud.SetAmmoText(ammo);
    }

    public void Update()
    {
        if (change.ReturnCurrent() == 0)
        {
            gameObject.SetActive(true);
        }

        else if (change.ReturnCurrent() != 0)
        {
            gameObject.SetActive(false);
        }
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

                if (ammo > 0 && reloading == false)
                {
                    shoot = true;
                    timeCounter = 0;
                }

                else
                {
                    Reload();
                }
                
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

        if (manager.pause == false && reloading == false)
        {
            shoot = false;
            GameObject bulletObject = Instantiate(shootGO, transform.position, Quaternion.identity) as GameObject;
            bulletObject.transform.forward = mainCamera.transform.forward;
            ammo--;
            hud.SetAmmoText(ammo);
        }


    }

    public void Reload()
    {
        if (reloading || ammo == maxAmmo)
        {
            return;
        }

        reloading = true;
        StartCoroutine(WaitForReload());
    }


    IEnumerator WaitForReload()
    {

        yield return new WaitForSeconds(reloadTime);
        ammo = maxAmmo;        
        reloading = false;
        hud.SetAmmoText(ammo);

    }

}