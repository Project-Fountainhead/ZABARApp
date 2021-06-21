using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class MoveModel : MonoBehaviour
{
    private Vector2 touchPosition = default;
    private ARRaycastManager arRaycastManager;
    //private ARPlaneManager aRPlaneManager;
    //private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private Camera arCamera;
    bool onTouchHold = false;
    private Text debugText;

    // Start is called before the first frame update
    void Awake()
    {        
        //transform.gameObject.AddComponent<ARRaycastManager>();
        arRaycastManager = GameObject.Find("AR Session Origin").GetComponent<ARRaycastManager>();

        //transform.gameObject.AddComponent<ARPlaneManager>();
        //aRPlaneManager = GetComponent<ARPlaneManager>();

        //aRPlaneManager.planePrefab = GameObject.Find("AR Plane Visualizer Light");
        //aRPlaneManager.detectionMode = UnityEngine.XR.ARSubsystems.PlaneDetectionMode.Horizontal;

        arCamera = GameObject.Find("AR Camera").GetComponent<Camera>();
        debugText = GameObject.Find("txtDebug").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {        
        Touch touch;

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);            

            touchPosition = touch.position;

            bool isOverUI = touchPosition.IsPointOverUIObject();

            if (isOverUI)
            {
                return;
            }

            if (touch.phase == TouchPhase.Began)
            {
                debugText.text += "touch phase began";
                Ray ray = arCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;
                if (Physics.Raycast(ray, out hitObject))
                {
                    debugText.text += "hitobject found";
                    onTouchHold = true;                    
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                onTouchHold = false;
            }
        }

        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            debugText.text = "Inside function";
            Pose hitPose = hits[0].pose;

            if (onTouchHold)
            {
                debugText.text = "onTouchHold is true";
                transform.position = hitPose.position;                
                //transform.rotation = hitPose.rotation;                
            }
        }
    }
}
