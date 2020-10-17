using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyBehaviour : MonoBehaviour
{

    public float life;
    public float maxLife;

    public GameObject lifeBarUI;
    public Slider slider;

    //private Arma weapon;
    //private Mele mele;

    private Collider col;

    public GameObject malla;

    // Start is called before the first frame update
    void Start()
    {

       
        life = maxLife;
        slider.value = CalculateHealth();
        col = GetComponent<Collider>();

        //weapon = GameObject.FindGameObjectWithTag("Point").GetComponent<Arma>();
        //mele = GameObject.FindGameObjectWithTag("Mele").GetComponent<Mele>();
    }

    // Update is called once per frame
    void Update()
    {

        slider.value = CalculateHealth();
        Dead();

    }

    public void GetDamage (float damage)
    {
        life -= damage;
    }

    public float CalculateHealth()
    {
        return life / maxLife;
    }

    public void ActiveBar()
    {
        lifeBarUI.SetActive(true);
    }

    public void DesBar()
    {
        lifeBarUI.SetActive(false);
    }

    private void Dead()
    {
        if (life <= 0)
        {
            col.enabled = false;

            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);

            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Point")
        {
            ActiveBar();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Point")
        {
            DesBar();
        }
    }




}
