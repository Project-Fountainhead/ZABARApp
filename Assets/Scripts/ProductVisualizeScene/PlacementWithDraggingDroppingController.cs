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
    // This class is responsible for instantiate the 3D model (when it has been download)
    // on the position of Placement Indicator and then remove this indicator    

    private GameObject placedPrefab;

    private GameObject placedObject;

    [SerializeField]
    private GameObject placementIndicator;

    [SerializeField]
    private ARRaycastManager rayManager;

    [SerializeField]
    private Camera arCamera;

    //[SerializeField]
    //private Text debugText;
        
    int ModelID = 0;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        ModelID = ProductManager.Instance.ModelProductID;

        ProductManager.Instance.GetAssetBundle(ModelID);        

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }   

    void Update()
    {
        if (!CacheManager.Instance.IsModelCached(ModelID) && placedObject != null)
        {
            return;
        }
        else
        {
            InstantiateModel();
        }           
    }

    private void InstantiateModel()
    {
        placedPrefab = CacheManager.Instance.GetProductModel(ModelID);

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                if (rayManager.Raycast(touch.position, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                {
                    Pose hitPose = hits[0].pose;
                    placedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                    ModelModel2 moveModel = placedObject.AddComponent<ModelModel2>();
                    moveModel.ARCamera = arCamera;
                    moveModel.RayManager = rayManager;
                    placedObject.AddComponent<RotateModel>();
                    Destroy(placementIndicator);
                }
            }
        }
    }
} 