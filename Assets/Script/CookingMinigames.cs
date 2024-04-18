using System.Collections.Generic;
using TMPro; // TextMeshPro for UI text
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CookingMinigames : MonoBehaviour
{
    // References to UI elements
    public List<Slider> objects; // List of sliders for the Chop minigame
    public Transform posPanel; // Panel holding the sliders

    public Button bttnFirst; // First button for the minigame
    public Button bttnSecond; // Second button for the minigame
    public GameObject gamePanel; // Panel for the minigame
    public Slider meter; // Slider representing the current value

    public TextMeshProUGUI capText; // Text for displaying capacity
    public TextMeshProUGUI currentText; // Text for displaying current value
    public TextMeshProUGUI targetText; // Text for displaying target value range
    public TextMeshProUGUI commentText; // Text for displaying comments

    // Minigame parameters
    public float capacity; // Maximum capacity
    public float current; // Current value
    public float targetMin; // Minimum target value
    public float targetMax; // Maximum target value

    private float gameTimer; // Timer for minigame
    private float delayTimer = 3f; // Timer for delaying task completion
    private float progress = 0.0f; // Progress percentage
    public bool isComplete = false; // Flag indicating if the minigame is complete

    public Minigame minigame; // Current minigame type

    public bool isPressed; // Flag indicating if a button is pressed



    public void Start()
    {
        GameManager.Instance.isInGame=true;
        switch (minigame)
        {
            case Minigame.Pour:
                bttnSecond.onClick.AddListener(Check); // Add listener for the second button
                HoldButton(bttnFirst); // Enable holding functionality for the first button
                break;
            case Minigame.Cook:
                bttnFirst.onClick.AddListener(AddCurrent); // Add listener for the first button
                break;
        }
        // Set up initial state for certain minigames
        if (minigame == Minigame.Cook || minigame == Minigame.Pour)
        {
            meter.interactable = false; // Disable interaction with the meter slider
            meter.value = current; // Set initial value for the meter
            meter.maxValue = capacity; // Set maximum value for the meter
            meter.minValue = 0; // Set minimum value for the meter
        }
        if (minigame == Minigame.Chop)
        {
            // Initialize the list of sliders for the Chop minigame
            objects = new List<Slider>();
            int childCount = posPanel.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform child = posPanel.GetChild(i);
                Slider slider = child.GetComponent<Slider>();
                if (slider != null)
                {
                    objects.Add(slider);
                }
            }
        }
    }

    public void Update()
    {
        // Update behavior based on the current minigame
        switch (minigame)
        {
            case Minigame.Pour:

                if (isPressed)
                {
                    AddCurrent();
                }
                Cap();
                break;
            case Minigame.Cook:
                ReduceCurrent();
                Progress();
                break;
            case Minigame.Chop:
                MultipleItems(objects);
                break;
        }
        // Update UI elements for certain minigames
        if (minigame == Minigame.Cook || minigame == Minigame.Pour)
        {
            meter.value = current;
            capText.text = "Max Capacity: " + capacity;
            currentText.text = "Current Value: " + current;
            targetText.text = "Target Value: " + targetMin + "-" + targetMax;
        }
        // Handle task completion
        if (isComplete)
        {
            TaskComplete();
        }
    }

    // Method for increasing the current value
    public void AddCurrent()
    {
        current = Mathf.Clamp(current + 0.1f, 0, capacity);
    }

    // Method for reducing the current value (used in the Cook minigame)
    public void ReduceCurrent()
    {
        gameTimer -= Time.deltaTime; // Decrease the countdown timer

        if (gameTimer <= 0f) // If the countdown reaches zero
        {
            current = Mathf.Clamp(current - 0.01f, 0, capacity); // Reduce the current value
            gameTimer = 0.05f; // Reset the countdown timer
        }
    }

    // Method for checking if current value exceeds capacity
    public void Cap()
    {
        if (current >= capacity)
        {
            current = 0; // Reset the current value
            commentText.text = "Try Again";
        }
    }

    // Method for checking if the current value falls within the target range
    public void Check()
    {
        if (current >= targetMin && current <= targetMax)
        {
            commentText.text = "Well Done";
            isComplete = true; // Set completion flag to true
        }
        else
        {
            commentText.text = "Wrong Measurement";
        }
    }

    // Method for simulating progress in the Cook minigame
    public void Progress()
    {
        if (progress >= 100)
        {
            isComplete = true; // Set completion flag to true
            commentText.text = "Done";
        }
        if (!isComplete)
        {
            if (current >= targetMin && current <= targetMax)
            {
                gameTimer -= Time.deltaTime; // Decrease the countdown timer

                if (gameTimer <= 0f) // If the countdown reaches zero
                {
                    progress = Mathf.Clamp(progress + 1, 0, 100); // Increment progress
                    gameTimer = 0.05f; // Reset the countdown timer
                }
            }
            else
            {
                gameTimer -= Time.deltaTime; // Decrease the countdown timer

                if (gameTimer <= 0f) // If the countdown reaches zero
                {
                    progress = Mathf.Clamp(progress - 1, 0, 100); // Decrement progress
                    gameTimer = 0.05f; // Reset the countdown timer
                }
            }
            commentText.text = progress.ToString() + "/100"; // Update progress text
        }
    }

    // Method for handling task completion
    public void TaskComplete()
    {
        delayTimer -= Time.deltaTime; // Decrease the countdown timer

        if (delayTimer <= 0f) // If the countdown reaches zero
        {
            GameManager.Instance.isInGame = false;
            gamePanel.SetActive(false); // Deactivate the game panel
            delayTimer = 5f; // Reset the countdown timer
        }
    }

    // Method for enabling holding functionality for a button
    public void HoldButton(Button button)
    {
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { isPressed = true; });
        trigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback.AddListener((data) => { isPressed = false; });
        trigger.triggers.Add(entry);
    }

    // Method for checking completion status of multiple sliders
    public void MultipleItems(List<Slider> sliders)
    {
        bool allMaxValue = true;

        foreach (Slider slider in sliders)
        {
            if (slider.value != slider.maxValue)
            {
                allMaxValue = false;
                break;
            }
            if (slider.value >= slider.maxValue)
            {
                slider.interactable = false; // Disable interaction with the slider
            }
        }

        if (allMaxValue)
        {
            commentText.text = "All Objects have been completed";
            isComplete = true; // Set completion flag to true
        }
        else
        {
            commentText.text = "Complete all the objects";
        }


    }

}
