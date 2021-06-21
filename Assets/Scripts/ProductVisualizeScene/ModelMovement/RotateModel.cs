using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class RotateModel : MonoBehaviour
{    
    private Button rotateButton;

    private bool rotate;    

    //private Text debugText;

    // Start is called before the first frame update
    void Awake()
    {
        rotateButton = GameObject.Find("btnRotate").GetComponent<Button>();

        rotate = false;

        if (rotateButton != null)
        {
            rotateButton.onClick.AddListener(RotateObject);
        }        

        //debugText = GameObject.Find("txtDebug").GetComponent<Text>();
    }

    private void RotateObject()
    {
        rotate = !rotate;
    }

    // Update is called once per frame
    void Update()
    {
        if (!rotate)
            return;

        Vector3 rotationSpeed = new Vector3(0, 25, 0);

        transform.Rotate(rotationSpeed * Time.deltaTime, Space.Self);        
    }
}
