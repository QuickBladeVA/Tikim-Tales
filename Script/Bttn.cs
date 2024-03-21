using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Bttn;

public class Bttn : MonoBehaviour
{
    public List<GameObject> obj;
    public int currentIndex = 0;
    public int sceneNumber;
    public string specificScene;
    public GameObject nextObj;

    public bool isNavigation;
    public enum Look { Up, Down, Left, Right }
    public Look direction;


    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void TurnOn(int select)
    {
        obj[select].SetActive(true);
    }

    public void TurnOff(int select)
    {
        obj[select].SetActive(false);
    }

    public void LoadSceneNumber() 
    {
        SceneManager.LoadScene(sceneNumber);
    }
    public void LoadSpecificScene()
    {
        SceneManager.LoadScene(specificScene);
    }
    public void MultipleObj()
    {
        if (currentIndex < obj.Count)
        {
            obj[currentIndex].SetActive(true);
            currentIndex++;
        }
        else
        {
            nextObj.SetActive(true);
        }
    }

    public void NaviBttn()
    {
        if (isNavigation)
        {
            switch (direction)
            {
                case Look.Up:
                    GameManager.Instance.vertical--;
                    break;
                case Look.Down:
                    GameManager.Instance.vertical++;
                    break;
                case Look.Left:
                    GameManager.Instance.horizontal--;
                    break;
                case Look.Right:
                    GameManager.Instance.horizontal++;
                    break;
            }
        }
    }
}
