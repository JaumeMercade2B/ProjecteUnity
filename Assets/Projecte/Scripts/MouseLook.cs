using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{

    private PlayerControls controls;
    [SerializeField] private float mouseSensitivity = 100f;

    public Vector2 look;
    public float direction;

    public float xRotation;

    private Transform playerBody;

    private void Awake()
    {
        controls = new PlayerControls();
        Cursor.lockState = CursorLockMode.Locked;

        playerBody = transform.parent;
        direction = look.x;
    }

    private void Update()
    {
        look = controls.Gameplay.Look.ReadValue<Vector2>();

        var mouseX = look.x * mouseSensitivity * Time.deltaTime;
        var mouseY = look.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }




}
