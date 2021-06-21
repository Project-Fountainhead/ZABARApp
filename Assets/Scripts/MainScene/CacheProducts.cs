using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CacheProducts : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI progressText;    

    void Awake()
    {
        // Makes the status bar and navigation bar visible (default)
        ApplicationChrome.statusBarState = ApplicationChrome.States.Visible;
        // ApplicationChrome.navigationBarState = ApplicationChrome.States.Hidden;

        // Makes the status bar and navigation bar visible over the content (different content resize method) 
        //ApplicationChrome.statusBarState = ApplicationChrome.navigationBarState = ApplicationChrome.States.VisibleOverContent;

        Screen.fullScreen = false;
        ProductManager.Instance.CacheProductDetails();
    }            
}
