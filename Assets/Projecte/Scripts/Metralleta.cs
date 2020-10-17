using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Metralleta : MonoBehaviour
{

    public bool canShoot = true;

    public int maxAmmo = 30;
    public int ammo;

    public float reloadTime = 0.5f;
    public bool reloading;

    public float damage = 1;
    public float cadency = 0;
    public float waitCadency= 3;

    public float distance = 20;

    private Camera mainCamera;

    private GameManager manager;

    private ChangeWeapon change;

    private HUD hud;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        canShoot = true;
        ammo = maxAmmo;
        cadency = 4;

        manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        change = GameObject.FindGameObjectWithTag("Holder").GetComponent<ChangeWeapon>();
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();

        hud.SetMetralletaText(ammo, maxAmmo);
    }

    public void Update()
    {
        if (change.ReturnCurrent() == 1)
        {
            gameObject.SetActive(true);
        }

        else if (change.ReturnCurrent() != 1)
        {
            gameObject.SetActive(false);
        }
    }
    public void FixedUpdate()
    {
        cadency += 1 * Time.deltaTime;
    }

    // Update is called once per frame
    public void OnShoot()
    {
        

        if (reloading || manager.pause == true)
        {
            return;
        }

        if (ammo <= 0)
        {
            canShoot = false;
            Reload();
            return;
        }

        

        if (canShoot && cadency > waitCadency)
        {

            //Ray ray = mainCamera.ScreenPointToRay(screenPos);


            cadency = 0;


            RaycastHit hit;
            for (int i = 0; i <= 2; i++)
            {

            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, distance)) //Aquí diem si el raig ha xocat amb quelcom
                {
                    if (hit.transform.CompareTag("Enemy"))
                    {

                        EnemyBehaviour enemigo = hit.transform.GetComponent<EnemyBehaviour>();
                        enemigo.GetDamage(damage);


                    }

                }
            }

            Debug.Log("Shoot");
            ammo-= 3;
            hud.SetMetralletaText(ammo, maxAmmo);
        }


        
        
        
        
    }

    public void Reload()
    {
        if (reloading || ammo == maxAmmo || manager.pause == true)
        {
            return;
        }

        reloading = true;
        StartCoroutine(WaitForReload());
    }

    IEnumerator WaitForReload()
    {
        canShoot = false;
        yield return new WaitForSeconds(reloadTime);
        ammo = maxAmmo;
        hud.SetMetralletaText(ammo, maxAmmo);
        reloading = false;
        canShoot = true;
   
    }

    //IEnumerator WaitForCadency() //La pots cridar cada x temps, el que tu vulguis
    //{
    //    canShoot = false;
    //    yield return new WaitForSeconds(cadency); //Quant temps espera per passar a la seguent linea. Es un corrutina
    //    canShoot = true;
    //}

}