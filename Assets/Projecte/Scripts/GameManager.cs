using System.Collections;
using System.Collections.Generic;
using System.Media;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameManager : MonoBehaviour
{
    private HUD hud;
    public bool pause;


    // Start is called before the first frame update
    void Start()
    {
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<HUD>();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void Pause()
    {
        pause = !pause;
        hud.OpenPausePanel(pause);

        if (pause)
        {
            Time.timeScale = 0;
            UnlockCursor();
        }

        else
        {
            Time.timeScale = 1;
            LockCursor();
        }

    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
