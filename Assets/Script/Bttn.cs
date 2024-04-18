using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bttn : MonoBehaviour
{
    [Header("Object/s")]
    public List<GameObject> obj;

    //public int currentIndex = 0;
    //public GameObject nextObj;
    [Header("Load Scenes")]
    public int sceneNumber;
    public string specificScene;

    [Header("Camera")]
    public bool isNavigation;
    public Look direction;

    [Header("Ingredient to put in inventory")]
    public Ingredient ingredient;
    [Header("left and right hand inventory")]
    public Holding inv;
    [Header("Requirement ingredient to open the mini games")]
    public List<Ingredient> neededItem;

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

    //turns on gameobjects in list and has index for specific obj
    //ref to the obj list
    public void TurnOn(int select)
    {
        obj[select].SetActive(true);
    }
    //turns off gameobjects in list and has index for specific obj
    //ref to the obj list
    public void TurnOff(int select)
    {
        obj[select].SetActive(false);
    }

    public void LoadSceneNumber() //Load scent using int
    {
        SceneManager.LoadScene(sceneNumber);
    }
    public void LoadSpecificScene()//Load scent using string
    {
        SceneManager.LoadScene(specificScene);
    }

    //to future me from past me:  use for multiple obj like visual novel
    //public void MultipleObj()
    //{
    //    if (currentIndex < obj.Count)
    //    {
    //        obj[currentIndex].SetActive(true);
    //        currentIndex++;
    //        obj[currentIndex-1].SetActive(false);
    //    }
    //    else
    //    {
    //        nextObj.SetActive(true);
    //    }
    //}

    //gameplay: add ingredient to the inventory in gameManager
    public void IngredientButton() 
    {
        if (GameManager.Instance.ingredients.Count < 2)
        {
            GameManager.Instance.ingredients.Add(ingredient);
        }
    }
    //gameplay: sets right / left in inventory holding enum
    public void InvButton()
    {
        GameManager.Instance.inv = inv;
    }
    //gameplay: Navigation cam dont touch unless me
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
