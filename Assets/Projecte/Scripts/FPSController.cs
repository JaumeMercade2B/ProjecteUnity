using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSController : MonoBehaviour
{

    [SerializeField] private float movementSpeed = 5f;
   

    private bool jump;
    [SerializeField] private float jumpCoolDown;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool holdingSpace;
    [SerializeField] private bool isGrounded;
    

    private bool dash;
    [SerializeField] private float remainingDashTime;
    [SerializeField] private float timeDashing;
    [SerializeField] private float dashForce;

    private PlayerControls controls = null;

    private Rigidbody rb;


    //DASH
    public float dashSpeed = 1.0f;

    // Start is called before the first frame update
    void Awake()
    {
        controls = new PlayerControls();
        Cursor.lockState = CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();



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
        Move();

        FixedUpdateJump();
        //FixedUpdateDash();
        

    }
    

    public void Jumping()
    {
        jump = true;       

    }

    public void Dash()
    {

        dash = true;
    }

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

    //private void FixedUpdateDash()
    //{
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
    //}


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
