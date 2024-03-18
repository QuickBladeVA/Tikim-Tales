using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bttn : MonoBehaviour
{
    public List<GameObject> obj;
    public int currentIndex = 0;
    public int sceneNumber;
    public string specificScene;
    public GameObject nextObj;
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
}
