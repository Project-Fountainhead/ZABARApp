using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlacementIndicator : MonoBehaviour
{
    private ARRaycastManager rayManager;   
    
    bool indicatorHitPlane;
    public bool IndicatorHitPlane { get => indicatorHitPlane; set => indicatorHitPlane = value; }

    [SerializeField]
    GameObject Indicator;

    void Start ()
    {
        // get the components
        rayManager = FindObjectOfType<ARRaycastManager>();
        
        IndicatorHitPlane = false;

        //myRenderer = transform.GetComponentInChildren<Renderer>();
        //Indicator.enabled = false;
    }

    void Update ()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        rayManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        // if we hit an AR plane surface, update the position and rotation
        if (hits.Count > 0)
        {
            indicatorHitPlane = true;
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;
            Indicator.SetActive(true);
        }
    }
    
    public void DisableIndicator()
    {
        Indicator.SetActive(false);
    }
}