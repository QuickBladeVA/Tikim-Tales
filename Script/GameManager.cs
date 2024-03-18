using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isPaused = false;
    public GameObject pausedPanel;
    // Start is called before the first frame update
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
    }
    public void Pause()
    {
        if (isPaused == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = true;
                Time.timeScale = 0;
                pausedPanel.SetActive(true);

            }
        }
        else if (isPaused == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPaused = false;
                Time.timeScale = 1;
                pausedPanel.SetActive(false);
            }
        }
    }
}
