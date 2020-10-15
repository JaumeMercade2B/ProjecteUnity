using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyBehaviour : MonoBehaviour
{

    public float life = 3;
    public float maxLife = 3;

    public GameObject lifeBarUI;
    public Slider slider;


    // Start is called before the first frame update
    void Start()
    {
        life = maxLife;
        slider.value = CalculateHealth();
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
            Destroy(gameObject);
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
