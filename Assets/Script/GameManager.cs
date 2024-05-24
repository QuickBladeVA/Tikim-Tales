using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Technical/Other Stuff
    public static GameManager Instance;

    public List<GameObject> InvPanel;
    public List<TextMeshProUGUI> InvText;

    [Header("Inventory")]
    public List<Ingredient> ingredients;
    public Holding inv = Holding.Left;
    public int selected;

    [Header("Recipe")]
    public List<string> preparedIngredients;
    public List<string> recipeList;
    public Transform phone;
    public Transform dish;

    public GameObject phoneText;
    public GameObject dishText;

    [Header("Camera stuff")]

    public bool isRotating;
    public bool isInGame;

    public int horizontal=0;
    public int vertical=0;

    public GameObject pausedPanel;

    public Camera mainCamera;

    public List<Button> navButtons;
    public List <GameObject> panels;

    public int score = 100;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI commentText;

    public bool isGameOver;

    //Gameplay stuff

    private void Awake()
    {

    }
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

        foreach (string text in recipeList)
        {
            GameObject newText = Instantiate(phoneText, phone.transform);
            TextMeshProUGUI tmp = newText.GetComponent<TextMeshProUGUI>();
            tmp.text = "- " + text.ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            DishScore();
        }
        if (!isGameOver)
        {
            foreach (Transform child in dish)
            {
                Destroy(child.gameObject);
            }

            foreach (string item in preparedIngredients)
            {
                GameObject newText = Instantiate(dishText, dish.transform);
                TextMeshProUGUI tmp = newText.GetComponent<TextMeshProUGUI>();
                tmp.text = "- " + item.ToString();

            }

            Inventory();

            Pause();
            if (pausedPanel.active == false)
            {
                Navigation();
            }
            NavRotate();
        }
    }

    public void RemoveItem(string item) 
    {
        preparedIngredients.Remove(item);
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

        if (!isInGame && pausedPanel.active==false)
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
    public void DishScore()
    {

        scoreText.text="Score: "+score;
        if (score>=100)
        {
            commentText.text="The Taste of Heaven";
        }
        else if (85<=score && score<=99)
        {
            commentText.text="You did a great job!";
        }
        else if (75<=score && score<=84)
        {
            commentText.text="The dish was 'Satisfactory'";
        }
        else if (50<=score && score<=74)
        {
            commentText.text="Nice Try";
        }        
        else if (25<=score && score<=49)
        {
            commentText.text="You should have used AjinoTomo";
        }
         else if (1<=score && score<=24)
        {
            commentText.text="Didn't you mother taught you how to cook";
        }  
        else if (0==score)
        {
            commentText.text="You didn't even try...";
        }      
        else if (-10<=score && score<=-1)
        {
            commentText.text="WHAT DID YOU EVEN PUT IN THIS!!!";
        }
        else if (score<-10)
        {
            commentText.text="*Lola Leah Dies...*";
        
        }
        if (score >= 75) 
        {
            PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex+1);
        }
    }

    public void Inventory()
    {
        if (ingredients.Count == 1) 
        {
            inv = Holding.Left;
        }

        InvHold();

        if (ingredients.Count == 0)
        {
            InvText[0].text = "";
        }

        if (ingredients.Count == 1)
        {
            InvText[0].text = ingredients[0].ToString();
            InvText[1].text = "";
        }
        if (ingredients.Count == 2)
        {
            InvText[1].text = ingredients[1].ToString();
        }

    }

}
