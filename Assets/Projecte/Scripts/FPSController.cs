using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FPSController : MonoBehaviour
{


    public float life;
    public float maxLife = 10;

    public Slider playerLife;

    [SerializeField] private float movementSpeed;
    [SerializeField] private float normalSpeed = 5f;



    private bool jump;
    [SerializeField] private float jumpCoolDown;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool holdingSpace;
    [SerializeField] private bool isGrounded;


    private bool dash;

    [SerializeField] private bool canDash;
    [SerializeField] private int dashCapacity;
    [SerializeField] private bool isDashing;
    [SerializeField] private float waitDash;
    [SerializeField] private float waitDash2;
    [SerializeField] private float timeToDash = 2f;
    [SerializeField] private float timeDashing = 0f;
    [SerializeField] private float dashDuration = 1f;
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private int dashes = 2;
    [SerializeField] private bool twoDashes;






    private PlayerControls controls = null;

    private Rigidbody rb;




    // Start is called before the first frame update
    void Awake()
    {

        life = maxLife;
        playerLife.value = CalculateHealth();

        controls = new PlayerControls();
        Cursor.lockState = CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();

        movementSpeed = normalSpeed;
        //canDash = true;
        waitDash = 5;
        waitDash2 = 5;



        //arma =GameObject.FindGameObjectWithTag("Arma").GetComponent<Arma>();
        //metralleta = GameObject.FindGameObjectWithTag("Metralleta").GetComponent<Metralleta>();

    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    // Update is called once per frame

    void FixedUpdate()
    {

        playerLife.value = CalculateHealth();
        Move();

        FixedUpdateJump();
        //FixedUpdateDash();

        waitDash += Time.deltaTime;
        waitDash2 += Time.deltaTime;


        if (dashes < 2)
        {
            if (waitDash > timeToDash && dashes == 1 && twoDashes == false)
            {
                dashes++;
            }

            if (waitDash > timeToDash && dashes == 0 && twoDashes == true)
            {

                dashes++;
            }

            if (waitDash2 > timeToDash && twoDashes == true)
            {
                dashes++;
                twoDashes = false;
            }

        }



        //if (dashes >= 2)
        //{
        //    dashes = 2;
        //}


        if (canDash)
        {
            movementSpeed = dashSpeed;
            timeDashing += Time.deltaTime;


            if (timeDashing > dashDuration)
            {
                movementSpeed = normalSpeed;
                canDash = false;
                timeDashing = 0;
                //waitDash = 0;
                dashes--;

                if (dashes <= 0)
                {
                    dashes = 0;
                }

                if (dashes == 1)
                {
                    waitDash = 0;
                }

                else if (dashes == 0)
                {
                    waitDash2 = 0;
                    twoDashes = true;
                }
            }
        }




    }

    public float CalculateHealth()
    {
        return life / maxLife;
    }



    public void Jumping()
    {
        jump = true;

    }

    //public void Dash()
    //{

    //    dash = true;
    //}

    public void Move()
    {
        FixedUpdateMove();
    }

    public void FixedUpdateMove()
    {
        var movementInput = controls.Gameplay.Move.ReadValue<Vector2>();

        var movement = new Vector3();

        movement.x = movementInput.x;
        movement.z = movementInput.y;


        movement.Normalize();

        transform.Translate(movement * movementSpeed * Time.deltaTime);

        if (movement.x == 0 && movement.z == 0)
        {
            canDash = false;
        }

        Debug.Log(movement.x);

    }

    private void FixedUpdateJump()
    {
        jumpCoolDown -= Time.deltaTime;

        if (jump)
        {
            jump = false;


            if (jumpCoolDown <= 0f && isGrounded)
            {
                jumpCoolDown = 0.2f;
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Force);

            }
        }

    }

    public void Dash()
    {
        //    var movementInput = controls.Gameplay.Move.ReadValue<Vector2>();

        //    var movement = new Vector3();

        //    movement.x = movementInput.x;
        //    movement.z = movementInput.y;


        //    movement.Normalize();
        //    if (dash) { 
        //        if (movement.x > 0)
        //        {
        //            transform.position += Vector3.back * dashSpeed * Time.deltaTime;
        //            rb.AddForce(Vector3.back * dashSpeed, ForceMode.Impulse);
        //            dash = false;
        //        }

        //        if (movement.x < 0)
        //        {
        //            transform.position += Vector3.forward * dashSpeed * Time.deltaTime;
        //            rb.AddForce(Vector3.forward * dashSpeed, ForceMode.Impulse);
        //            dash = false;
        //        }

        //        if (movement.z > 0)
        //        {
        //            transform.forward += Vector3.left * dashSpeed * Time.deltaTime;
        //            rb.AddForce(Vector3.left * dashSpeed, ForceMode.Impulse);
        //            dash = false;
        //        }

        //        if (movement.z < 0)
        //        {
        //            transform.position += Vector3.right * dashSpeed * Time.deltaTime;
        //            rb.AddForce(Vector3.right * dashSpeed, ForceMode.Impulse);
        //            dash = false;
        //        }
        //    }       

        if (waitDash > timeToDash && dashes == 2 || waitDash2 > timeToDash && dashes == 1)
        {
            canDash = true;

        }



    }

    public void GetDamage(float damage)
    {
        life -= damage;
    } 


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
    
}
