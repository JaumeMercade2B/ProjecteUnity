using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition.Attributes;
using UnityEngine.UI;


public class EnemyBehaviour : MonoBehaviour
{
    public float emissionIntensity = 100000;
    public float life;
    public float maxLife;

    public Renderer renderer;
    public Renderer renderer2;
    private Material mymat;
    private Material mymat2;

    //public GameObject lifeBarUI;
    //public Slider slider;

    //private Arma weapon;
    //private Mele mele;

    private Collider col;

    //public GameObject malla;

    // Start is called before the first frame update
    void Start()
    {
        mymat = renderer.material;
        mymat2 = renderer2.material;
        mymat.EnableKeyword("_EMISSION");
        mymat2.EnableKeyword("_EMISSION");

        life = maxLife;
        //slider.value = CalculateHealth();
        col = GetComponent<Collider>();

        //weapon = GameObject.FindGameObjectWithTag("Point").GetComponent<Arma>();
        //mele = GameObject.FindGameObjectWithTag("Mele").GetComponent<Mele>();
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 10)
        {
            mymat.SetColor("_EmissiveColor", Color.cyan * 200f);
            mymat2.SetColor("_EmissiveColor", Color.cyan * 200f);
        }

        if (life <= 7)
        {
            mymat.SetColor("_EmissiveColor", Color.yellow * 200f);
            mymat2.SetColor("_EmissiveColor", Color.yellow * 200f);
        }

        if (life <= 5)
        {
            mymat.SetColor("_EmissiveColor", Color.red * 300f);
            mymat2.SetColor("_EmissiveColor", Color.red * 300f);
        }
        //slider.value = CalculateHealth();
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

    //public void ActiveBar()
    //{
    //    lifeBarUI.SetActive(true);
    //}

    //public void DesBar()
    //{
    //    lifeBarUI.SetActive(false);
    //}

    private void Dead()
    {
        if (life <= 0)
        {
            //col.enabled = false;

            //foreach (Transform child in transform)
            //{
            //    GameObject.Destroy(child.gameObject);

            //}

            Destroy(gameObject);
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.tag == "Point")
    //    {
    //        ActiveBar();
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Point")
    //    {
    //        DesBar();
    //    }
    //}


}
