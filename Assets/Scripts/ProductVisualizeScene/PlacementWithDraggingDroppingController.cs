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

    //[SerializeField]
    //private GameObject welcomePanel;

    //[SerializeField]
    //private Button dismissButton;    

    [SerializeField]
    private GameObject placedPrefab;

    //[SerializeField]
    private GameObject placedObject;

    [SerializeField]
    private GameObject placementIndicator;

    //private PlacementIndicator plcIndicator;  

    [SerializeField]
    private ARRaycastManager rayManager;

    [SerializeField]
    private Camera arCamera;

    [SerializeField]
    private Text debugText;
        
    int ModelID = 0;
    private Vector2 touchPosition = default;

    private bool onTouchHold = false;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        ModelID = ProductManager.Instance.ModelProductID;

        ProductManager.Instance.GetAssetBundle(ModelID);

        //plcIndicator = FindObjectOfType<PlacementIndicator>();

        //placedObject = GameObject.Find("GameObject");

        //ProductManager.Instance.callbackModelEventHandler += new ProductManager.DelModelEventHandler(OnRequestComplete);

         //dismissButton.onClick.AddListener(Dismiss);                     

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    //void Start()
    //{
    //    //ProductManager.Instance.GetAssetBundle(ButtonHandler.ProductID);
    //    //StartCoroutine(VisualizeModel());
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

    //private void Dismiss() => welcomePanel.SetActive(false);    

    void Update()
    {
        // do not capture events unless the welcome panel is hidden
        //if (welcomePanel.activeSelf)
        //    return;

        //StartCoroutine(IsModelReady());

        //if (ProductManager.Instance.ProductModel == null)
        //{
        //    return;
        //}

        //placedPrefab = ProductManager.Instance.ProductModel;         

        if (!CacheManager.Instance.IsModelCached(ModelID) && Input.touchCount == 0)
        {
            CentreMovePlmtIndicator();
        }
        else
        {
            //placedPrefab = CacheManager.Instance.GetProductModel(ModelID);

            placementIndicator.SetActive(false);

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                touchPosition = touch.position;

                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = arCamera.ScreenPointToRay(touch.position);
                    RaycastHit hitObject;
                    if (Physics.Raycast(ray, out hitObject))
                    {
                        //PlacementObject placementObject = hitObject.transform.GetComponent<PlacementObject>();
                        //if (placementObject != null)
                        //{
                        //    debugText.text = "PLACEMENTOBJECTFOUND";
                        //    onTouchHold = true;
                        //    //placementObject.SetOverlayText(isLocked ? "AR Object Locked" : "AR Object Unlocked");
                        //}
                        //else
                        //{
                        //    debugText.text = "PLACEMENTOBJECTNOTFOUND " + hitObject.transform.gameObject.name;
                        //}
                        debugText.text = hitObject.transform.gameObject.name;
                        if (!hitObject.transform.gameObject.name.StartsWith("AR", StringComparison.CurrentCulture))
                        {
                            onTouchHold = true;
                        }
                    }
                }

                if (touch.phase == TouchPhase.Ended)
                {
                    onTouchHold = false;
                }
            }

            if (rayManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;

                if (placedObject == null)
                {
                    //if (defaultRotation > 0)
                    //{
                    //    placedObject = Instantiate(placedPrefab, hitPose.position, Quaternion.identity);
                    //    placedObject.transform.Rotate(Vector3.up, defaultRotation);
                    //}
                    //else
                    //{
                        placedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                        placedObject.AddComponent<RotateModel>();
                    //}
                }
                else
                {
                    if (onTouchHold)
                    {
                        placedObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
                        //if (defaultRotation == 0)
                        //{
                        //    placedObject.transform.rotation = hitPose.rotation;
                        //}
                    }
                }
            }
        }           
    }

    private void CentreMovePlmtIndicator()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

        // if we hit an AR plane surface, update the position and rotation
        if (hits.Count > 0)
        {
            placementIndicator.transform.SetPositionAndRotation(hits[0].pose.position, hits[0].pose.rotation);
        }
    }

    //IEnumerator IsModelReady()
    //{
    //    if (!IsModelReceived)
    //    {
    //        ProdDetails prodDetails = CacheManager.Instance.GetProductDetails(ProductManager.Instance.ModelProductID);

    //        while (prodDetails.model == null)
    //        {
    //            yield return null;
    //        }

    //        placedPrefab = prodDetails.model;
    //        IsModelReceived = true;
    //    }       
    //}

    //bool IsModelReady()
    //{
    //    ProdDetails prodDetails = GetProductDetails(ProductManager.Instance.ModelProductID);
    //    return prodDetails.model != null;
    //    if (!IsModelReceived)
    //    {


    //        while ()
    //        {
    //            yield return null;
    //        }

    //        placedPrefab = prodDetails.model;
    //        IsModelReceived = true;
    //    }
    //}

    //void InstantiateModel()
    //{       
    //    placedObject = Instantiate(placedPrefab, placementIndicator.transform.position, placementIndicator.transform.rotation);
    //    //placedObject.AddComponent<MoveModel>();
    //    placedObject.AddComponent<RotateModel>();
    //    //placedObject.transform.SetParent(placementIndicator.transform);
    //    //plcIndicator.DisableIndicator();
    //    //plcIndicator.enabled = false;        
    //}
} 