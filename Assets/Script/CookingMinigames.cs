using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CookingMinigames : MonoBehaviour
{
    public List <Slider> objects;
    public Transform posPanel;

    public Button bttnFirst;
    public Button bttnSecond;
    public GameObject gamePanel;
    public Slider meter;

    public TextMeshProUGUI capText;
    public TextMeshProUGUI currentText;
    public TextMeshProUGUI targetText;
    public TextMeshProUGUI commentText;

    public float capacity;
    public float current;
    public float targetMin;
    public float targetMax;

    private float gameTimer;
    private float delayTimer=3f;
    private float progress = 0.0f;
    public bool isComplete = false;

    public enum Minigame { Pour, Cook, Chop, Stir }
    public Minigame minigame;

    public bool isPressed;

    public void Start()
    {
        switch (minigame) 
        {
            case Minigame.Pour:
                bttnSecond.onClick.AddListener(Check);
                HoldButton(bttnFirst);
                break;
            case Minigame.Cook:
                bttnFirst.onClick.AddListener(AddCurrent);
                break;
        }
        if (minigame == Minigame.Cook || minigame == Minigame.Pour) 
        {
            meter.interactable = false;
            meter.value = current;
            meter.maxValue = capacity;
            meter.minValue = 0;
        }
        if (minigame == Minigame.Chop)
        {
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
        if (minigame == Minigame.Cook || minigame == Minigame.Pour)
        {
            meter.value = current;
            capText.text = "Max Capacity: " + capacity;
            currentText.text = "Current Value:"  + current;
            targetText.text = "Target Value: " + targetMin + "-" + targetMax;
        }
        if (isComplete)
        {
            TaskComplete();
        }
    }

    public void AddCurrent()
    {
        current = Mathf.Clamp(current + 0.1f, 0, capacity);
    }   
    public void ReduceCurrent()
    {
        gameTimer -= Time.deltaTime; // Decrease the countdown timer

        if (gameTimer <= 0f) // If the countdown reaches zero
        {
            current = Mathf.Clamp(current - 0.01f, 0, capacity);
            gameTimer = 0.05f; // Reset the countdown timer
        }

    }
    public void Cap()
    {
        if (current >= capacity)
        {
            current = 0;
            commentText.text = "Try Again";

        }
    }
    public void Check()
    {
        if (current >= targetMin && current <= targetMax) 
        {
            commentText.text = "Well Done";
            isComplete = true;
        }
        else 
        {
            commentText.text = "Wrong Measurement";
        }
    }
    public void Progress() 
    {

        if (progress >= 100 ) 
        {
            isComplete = true;
            commentText.text = "Done";
        }
        if (!isComplete) { 
            if (current >= targetMin && current <= targetMax)
            {
                gameTimer -= Time.deltaTime; // Decrease the countdown timer

                if (gameTimer <= 0f) // If the countdown reaches zero
                {
                    progress = Mathf.Clamp(progress + 1, 0, 100);
                    gameTimer = 0.05f; // Reset the countdown timer
                }
            }
            else
            {
                gameTimer -= Time.deltaTime; // Decrease the countdown timer

                if (gameTimer <= 0f) // If the countdown reaches zero
                {
                    progress = Mathf.Clamp(progress - 1, 0, 100);
                    gameTimer = 0.05f; // Reset the countdown timer
                }
            }
            commentText.text = progress.ToString() + "/100";
        }


    }
    public void TaskComplete() 
    {
        delayTimer -= Time.deltaTime; // Decrease the countdown timer

        if (delayTimer <= 0f) // If the countdown reaches zero
        {
            gamePanel.SetActive(false);

            delayTimer= 5f; // Reset the countdown timer
        }
    }
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

    public void Spawn ()
    {
        
    }

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
                slider.interactable = false;
            }
        }

        if (allMaxValue)
        {
            commentText.text ="All Objects have been complete";
            isComplete = true;
        }
        else
        {
            commentText.text = "Complete the all the objects";
        }
    }

}
