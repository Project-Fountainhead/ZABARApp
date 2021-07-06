using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Linq;

public class ModelModel2 : MonoBehaviour
{
    private Camera arCamera;
    public Camera ARCamera { get => arCamera; set => arCamera = value; }

    private bool onTouchHold = false;

    private ARRaycastManager rayManager;
    public ARRaycastManager RayManager { get => rayManager; set => rayManager = value; }

    private Vector2 touchPosition = default;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();    

    // Update is called once per frame
    void Update()
    {
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
                    if (!HitObjectIsARPlane(hitObject.transform.gameObject.name))
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
            
            if (onTouchHold)
            {
                if (hitPose.rotation.z != 0)
                {
                    hitPose.rotation.z = 0;
                }
                transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);                
            }
        }
    }

    private bool HitObjectIsARPlane(string name)
    {
        if (name.Length > 8)  // taking 8 randomly because it was observered that the name of AR plane is usually a large string
        {
            string sub1 = name.Substring(0, 2);
            if (string.Compare(sub1, "AR") != 0)
            {
                return false;
            }
            string sub2 = name.Substring(2);
            if (sub2.All(char.IsDigit))
            {
                return true;
            }
        }
        return false;
    }
}
