using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Technical/Other Stuff
    public static GameManager Instance;

    [Header("Inventory")]
    public List<Ingredient> ingredients;
    public Holding inv = Holding.Left;
    public int selected;

    [Header("Camera stuff")]

    public bool isPaused = false;
    public bool isRotating;
    public bool isInGame;

    public int horizontal=0;
    public int vertical=0;

    public GameObject pausedPanel;

    public Camera mainCamera;

    public List<Button> navButtons;
    public List <GameObject> panels;


    //Gameplay stuff
    void Start()
    {
        if (Instance == null)
        {
            Instance = this; 
        }
        else
        {
            Destroy(this.gameObject); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        InvHold();
        Pause(); 
        if (pausedPanel.active == false)
        {
            Navigation(); 
        }
        NavRotate(); 
    }

    // Pauses or resumes the game based on user input
    public void Pause()
    {
        if (pausedPanel.active == true) // If the pause panel is active
        {
            Time.timeScale = 0; // Pause time
            if (Input.GetKeyDown(KeyCode.Escape)) // If Escape key is pressed
            {
                pausedPanel.SetActive(false); // Resume the game
            }
        }
        else // If the pause panel is not active
        {
            Time.timeScale = 1; // Resume time
            if (Input.GetKeyDown(KeyCode.Escape)) // If Escape key is pressed
            {
                pausedPanel.SetActive(true); // Pause the game
            }
        }
    }

    // Handles navigation controls
    public void Navigation()
    {
        // Checks the direction the camera is facing and activates the appropriate navigation buttons
        switch (horizontal, -vertical)
        {
            case (0, 0): // Front
                NavBttn("Front");
                break;
            case (1, 0): // Right
                NavBttn("Right");
                break;
            case (-1, 0): // Left
                NavBttn("Left");
                break;
            case (0, 1): // Top
                NavBttn("Top");
                break;
        }

        if (!isInGame)
        {
            // Keybinds for navigation
            if (Input.GetKeyDown(KeyCode.W) && navButtons[0].interactable == true)
            {
                GameManager.Instance.vertical--;
            }
            if (Input.GetKeyDown(KeyCode.S) && navButtons[3].interactable == true)
            {
                GameManager.Instance.vertical++;
            }
            if (Input.GetKeyDown(KeyCode.A) && navButtons[1].interactable == true)
            {
                GameManager.Instance.horizontal--;
            }
            if (Input.GetKeyDown(KeyCode.D) && navButtons[2].interactable == true)
            {
                GameManager.Instance.horizontal++;
            }
        }
    }

    // Rotates the camera based on the navigation state
    public void NavRotate()
    {
        float rotationSpeed = 100f; // Speed of camera rotation
        float camTargertX = 30 * vertical; // Target rotation angle on X-axis
        float camTargertY = 90 * horizontal; // Target rotation angle on Y-axis

        Quaternion targetRotation = Quaternion.Euler(camTargertX, camTargertY, 0); // Calculate target rotation

        // Rotate the camera towards the target rotation
        if (mainCamera.transform.rotation != targetRotation)
        {
            isRotating = true; // Set rotating to true
            mainCamera.transform.rotation = Quaternion.RotateTowards(mainCamera.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            isRotating = false; // Set rotating to false
        }
    }

    // Updates the navigation buttons panel based on the camera's orientation
    public void NavBttn(string position)
    {
        // Deactivate all button panels
        panels[0].SetActive(false);
        panels[1].SetActive(false);
        panels[2].SetActive(false);
        panels[3].SetActive(false);

        // Deactivate all navigation buttons
        navButtons[0].interactable = false;
        navButtons[1].interactable = false;
        navButtons[2].interactable = false;
        navButtons[3].interactable = false;

        // Activate the appropriate button panel and navigation buttons based on camera orientation
        switch (position)
        {
            case "Front":
                if (!isRotating)
                {
                    panels[0].SetActive(true);
                }
                navButtons[0].interactable = true;
                navButtons[1].interactable = true;
                navButtons[2].interactable = true;
                break;
            case "Top":
                if (!isRotating)
                {
                    panels[3].SetActive(true);
                }
                navButtons[3].interactable = true;
                break;
            case "Right":
                if (!isRotating)
                {
                    panels[2].SetActive(true);
                }
                navButtons[1].interactable = true;
                break;
            case "Left":
                if (!isRotating)
                {
                    panels[1].SetActive(true);
                }
                navButtons[2].interactable = true;
                break;
        }
    }

    public void InvHold() 
    {
        switch (inv)
        {
            case Holding.Left:
                selected = 0;
                break;

            case Holding.Right:
                selected = 1;
                break;
        }
    }


}
