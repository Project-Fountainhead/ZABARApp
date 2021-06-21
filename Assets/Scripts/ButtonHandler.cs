﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadCategoriesMenuScene()
    {
        SceneManager.LoadSceneAsync("CategoriesScene");
        //LoadScene("CategoriesScene");
    }
    
    public void LoadHomeFurniMenuScene()
    {
        SceneManager.LoadSceneAsync("HomeFurnitureListScene");
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainScene");
    }    
}
