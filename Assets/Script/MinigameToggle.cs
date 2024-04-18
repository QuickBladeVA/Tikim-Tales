using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameToggle : MonoBehaviour
{
    public List<GameObject> Button;

    private void Update()
    {
        foreach (GameObject obj in Button)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in Button)
        {
            Bttn bttn = obj.GetComponent<Bttn>();
            List<Ingredient> neededItem = bttn.neededItem;
            if (GameManager.Instance.ingredients.Count > 0)
            {
                foreach (Ingredient item in neededItem)
                {
                    if (GameManager.Instance.ingredients[GameManager.Instance.selected] == item)
                    {
                        obj.SetActive(true);
                    }

                }
            }
        }
    }

}
