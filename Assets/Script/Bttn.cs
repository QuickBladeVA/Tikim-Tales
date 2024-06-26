using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bttn : MonoBehaviour
{
    [Header("Object/s")]
    public List<GameObject> obj;
    public Transform pos;
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

    public void Spawn(int select) 
    {
        Instantiate(obj[select], pos.transform);
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
        if (ingredient != Ingredient.Trash)
        {
            if (GameManager.Instance.ingredients.Count < 2)
            {
                GameManager.Instance.ingredients.Add(ingredient);
            }
        }
        else 
        {
            if (GameManager.Instance.ingredients.Count > 0)
            {

                    GameManager.Instance.ingredients.RemoveAt(GameManager.Instance.selected);
                 
            }
        }
    }

    public void Serve()
    {
        List<string> listDelete = new List<string>();
        int total= GameManager.Instance.recipeList.Count;
        
        foreach(string item in GameManager.Instance.recipeList)
        {
            if (GameManager.Instance.preparedIngredients.Contains(item))
            {
                GameManager.Instance.preparedIngredients.Remove(item);
                listDelete.Add(item);
            }
        }
        foreach(string item in listDelete)
        {
        GameManager.Instance.recipeList.Remove(item);
        }


        int mistakes= GameManager.Instance.preparedIngredients.Count + GameManager.Instance.recipeList.Count;
        GameManager.Instance.score= GameManager.Instance.score - (mistakes*100/total);
        GameManager.Instance.isGameOver= true;


    }
    //gameplay: sets right / left in inventory holding enum
    public void DeleteItem() 
    {
        if (GameManager.Instance.preparedIngredients.Count == 1)
        {
            GameManager.Instance.preparedIngredients.RemoveAt(0);
        }
        if (GameManager.Instance.preparedIngredients.Count >= 1)
        {
            GameManager.Instance.preparedIngredients.RemoveAt(GameManager.Instance.preparedIngredients.Count - 1);
        }
    }
    public void DestoyObject() 
    {
        foreach (GameObject item in obj) 
        {
            Destroy(item);
        }
        if (GameManager.Instance.isInGame) 
        {
            GameManager.Instance.isInGame = false;
        }
        

    }
    public void InvButton()
    {
        GameManager.Instance.inv = inv;
    }
    //gameplay: Navigation cam dont touch unless me
    public void NaviBttn()
    {
        if (isNavigation && GameManager.Instance.pausedPanel.active == false)
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
