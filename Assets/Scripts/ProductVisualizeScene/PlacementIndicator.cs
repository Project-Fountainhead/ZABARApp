using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicator : MonoBehaviour
{
    [SerializeField]
    private ARRaycastManager rayManager;
    
    [SerializeField]
    GameObject plane;

    GameObject planeIndicator;
    public GameObject PlaneIndicator { get => planeIndicator; set => planeIndicator = value; }    

    void Update ()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        // if we hit an AR plane surface, update the position and rotation
        if (hits.Count > 0)
        {
            if (planeIndicator == null)
            {
                planeIndicator = Instantiate(plane, hits[0].pose.position, hits[0].pose.rotation);
                planeIndicator.transform.SetParent(transform);
            }
            else
            {
                transform.SetPositionAndRotation(hits[0].pose.position, hits[0].pose.rotation);
            }
        }
    }    
}