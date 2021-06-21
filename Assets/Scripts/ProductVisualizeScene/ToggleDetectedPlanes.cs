using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ToggleDetectedPlanes : MonoBehaviour
{
    private ARPlaneManager aRPlaneManager;

    [SerializeField]
    private Button toggleButton;

    private Text toggleButtonText;

    // Start is called before the first frame update
    void Awake()
    {
        if (toggleButton != null)
        {
            toggleButton.onClick.AddListener(TogglePlaneDetection);
        }
        aRPlaneManager = GetComponent<ARPlaneManager>();
        toggleButtonText = toggleButton.GetComponentInChildren<Text>();
    }    

    private void TogglePlaneDetection()
    {
        aRPlaneManager.enabled = !aRPlaneManager.enabled;

        foreach (ARPlane plane in aRPlaneManager.trackables)
        {
            plane.gameObject.SetActive(aRPlaneManager.enabled);
        }

        toggleButtonText.text = aRPlaneManager.enabled ? "Hide Planes" : "Show Planes";
    }
}
