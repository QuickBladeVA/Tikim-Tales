using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public List<Button> levels;
    public int levelCap;

    private void Update()
    {
        for (int l = 0; l < levels.Count; l++)
        {
            levels[l].interactable = true;
            if (l + 1 > levelCap) 
            {
                levels[l].interactable = false;
            }
        }
    }
    public void AddLevel() 
    {
        levelCap+=1;
    }
}
