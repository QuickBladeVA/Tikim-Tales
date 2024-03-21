using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class GameManager : MonoBehaviour
{
    //Technical/Other Stuff
    public static GameManager Instance;
    public bool isPaused = false;

    public int horizontal=0;
    public int vertical=0;

    public GameObject pausedPanel;

    public Button up;
    public Button down;
    public Button left;
    public Button right;

    public Camera mainCamera;

    //Gameplay stuff


    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject); // Destroy duplicate GameManager instances
        }
    }

    // Update is called once per frame
    void Update()
    {
        Pause();
        if (pausedPanel.active == false)
        {
            Navigation();
        }
        NavRotate();
    }
    public void Pause()
    {
        //checks if  pause panel on
        if (pausedPanel.active == true) 
        {
            Time.timeScale = 0;
            //Press esc to unpause
            if (Input.GetKeyDown(KeyCode.Escape)) 
            {
                pausedPanel.SetActive(false);
            }
        }

        //checks if  pause panel off
        else if (pausedPanel.active == false)
        {
            Time.timeScale = 1;
            //Press esc to pause
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pausedPanel.SetActive(true);
            }
        }
    }

    public void Navigation()
    {
        //Checks positioning of camera (Note: its the direction on where your looking at)
        switch (horizontal, -vertical) 
        {

            case (0,0):
                up.interactable = true;
                down.interactable = false;
                left.interactable = true;
                right.interactable = true;

                break;
            case (1,0):
                up.interactable = false;
                down.interactable = false;
                left.interactable = true;
                right.interactable = false;
                break;
            case (-1,0):
                up.interactable = false;
                down.interactable = false;
                left.interactable = false;
                right.interactable = true;
                break;
            case (0,1):
                up.interactable = false;
                down.interactable = true;
                left.interactable = false;
                right.interactable = false;
                break;
        }

        //Keybinds for nav
        if (Input.GetKeyDown(KeyCode.W) && up.interactable == true)
        {
            GameManager.Instance.vertical--;
        }
        if (Input.GetKeyDown(KeyCode.S) && down.interactable == true)
        {
            GameManager.Instance.vertical++;
        }
        if (Input.GetKeyDown(KeyCode.A) && left.interactable == true)
        {
            GameManager.Instance.horizontal--;
        }
        if (Input.GetKeyDown(KeyCode.D) && right.interactable == true)
        {
            GameManager.Instance.horizontal++;
        }
    }

    //The whole camera Movement
    public void NavRotate()
    {
        
        float rotationSpeed = 100f;
        float camTargertX = 30 * vertical;
        float camTargertY = 90 * horizontal;

        Quaternion targetRotation = Quaternion.Euler(camTargertX, camTargertY, 0);

        if (mainCamera.transform.rotation != targetRotation)
        {
            mainCamera.transform.rotation = Quaternion.RotateTowards(mainCamera.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }



}
