using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public GameObject pausePanel;

    public Text ammoText;
    public Text metralletaText;
    public GameObject hudArma;
    public GameObject hudMetralleta;
    private ChangeWeapon change;
    public Slider playerLife;
   

    public void Awake()
    {
        change = GameObject.FindGameObjectWithTag("Holder").GetComponent<ChangeWeapon>();
        
    }

    public void Update()
    {
        if (change.ReturnCurrent() == 0)
        {
            hudArma.SetActive(true);
        }

        else if (change.ReturnCurrent() != 0)
        {
            hudArma.SetActive(false);
        }

        if (change.ReturnCurrent() == 1)
        {
            hudMetralleta.SetActive(true);
        }

        else if (change.ReturnCurrent() != 1)
        {
            hudMetralleta.SetActive(false);
        }
    }
    public void SetAmmoText(float ammo)
    {

        ammoText.text = ammo.ToString(); 

    }

    public void SetMetralletaText(float ammo, float maxAmmo)
    {
        metralletaText.text = ammo.ToString() + " - " + maxAmmo.ToString(); ;
    }

   

    public void OpenPausePanel(bool pause)
    {
        pausePanel.SetActive(pause);
    }
}
