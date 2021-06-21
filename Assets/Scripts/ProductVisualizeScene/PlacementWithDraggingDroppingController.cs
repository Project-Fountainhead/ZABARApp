using Assets.Scripts;
using Assets.Scripts.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class PlacementWithDraggingDroppingController : MonoBehaviour
{
    /*Responsibilities of DnD class:
        1) Toggle detected plane            Removed
        2) move model                       Removed
        3) rotate model                     Removed
        4) show progress bar                Removed
        5) hide and show detected ground    Removed
        6) show welcome panel and later hide it
        7) Fetch and Initiate the 3d model      sole responsibility
        */

    [SerializeField]
    private GameObject welcomePanel;

    [SerializeField]
    private Button dismissButton;    

    //[SerializeField]
    private GameObject placedPrefab;

    //[SerializeField]
    private GameObject placedObject;

    private PlacementIndicator plcIndicator;   

    [SerializeField]
    private Text debugText;

    bool IsModelReceived = false;

    void Awake()
    {
        ProductManager.Instance.GetAssetBundle(ProductManager.Instance.ModelProductID);

        plcIndicator = FindObjectOfType<PlacementIndicator>();

        placedObject = GameObject.Find("GameObject");

        //ProductManager.Instance.callbackModelEventHandler += new ProductManager.DelModelEventHandler(OnRequestComplete);

         dismissButton.onClick.AddListener(Dismiss);                     

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    //void Start()
    //{
    //    //ProductManager.Instance.GetAssetBundle(ButtonHandler.ProductID);
        
    //}

    //private void OnDestroy()
    //{
    //    //ProductManager.Instance.callbackModelEventHandler -= new ProductManager.DelModelEventHandler(OnRequestComplete);
    //}

    //void OnRequestComplete(GameObject gameObject)
    //{        
    //    try
    //    {
    //        placedPrefab = gameObject;            
    //    }
    //    catch(Exception e)
    //    {
    //        //    debugText.text = "Exception occured: " + e.Message;
    //        Debug.Log("Exception occured: " + e.Message);
    //        return;
    //    }
    //    //debugText.text += "prefab loaded!";
    //}

    private void Dismiss() => welcomePanel.SetActive(false);    

    void Update()
    {
        // do not capture events unless the welcome panel is hidden
        if (welcomePanel.activeSelf)
            return;

        StartCoroutine(IsModelReady());

        //if (ProductManager.Instance.ProductModel == null)
        //{
        //    return;
        //}

        //placedPrefab = ProductManager.Instance.ProductModel;

        Touch touch;

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (placedPrefab != null && plcIndicator.IndicatorHitPlane && touch.phase == TouchPhase.Ended)
            {
                InstantiateModel();

                //Once the model has been instantiated, then disable the component as it is no longer required
                this.enabled = false;
            }            
        }        
    }

    IEnumerator IsModelReady()
    {
        if (!IsModelReceived)
        {
            ProdDetails prodDetails = CacheManager.Instance.GetProductDetails(ProductManager.Instance.ModelProductID);

            while (prodDetails.model == null)
            {
                yield return null;
            }
            
            placedPrefab = prodDetails.model;
            IsModelReceived = true;
        }       
    }

    void InstantiateModel()
    {       
        placedObject = Instantiate(placedPrefab, plcIndicator.transform.position, plcIndicator.transform.rotation);
        placedObject.AddComponent<MoveModel>();
        placedObject.AddComponent<RotateModel>();
        plcIndicator.DisableIndicator();
        plcIndicator.enabled = false;        
    }
} 