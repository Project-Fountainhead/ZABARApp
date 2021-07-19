using Assets.Scripts;
using Assets.Scripts.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDimension : MonoBehaviour
{
    [SerializeField]
    PlacementWithDraggingDroppingController placementWithDraggingDroppingController;

    [SerializeField]
    GameObject dimPrefab;
    GameObject DimObject;        

    [SerializeField]
    Text debugText;    

    bool show = false;

    //void Awake()
    //{
    //    Button btnDimension = GetComponent<Button>();
    //    btnDimension.onClick.AddListener(ShowHideDimension);
    //}    

    public void ShowHideProductDimension()
    {
        show = !show;

        ProductDimensions productDimensions = CacheManager.Instance.GetProductDimensions(ProductManager.Instance.ModelProductID);
        if (productDimensions == null)
        {
            debugText.text = "dimensions not received";
            return;
        }

        if (show && DimObject == null)
        {
            //debugText.text += "plane is null. Caculating...";
            CalDimension();
        }
        
        DimObject?.SetActive(show);        
    }      

    private void CalDimension()
    {
        GameObject model = placementWithDraggingDroppingController.PlacedObject;
        if (model == null)
        {
            //debugText.text += "model is null.";
            return;
        }

        //debugText.text += "model is not null.";
        BoxCollider boxCollider = model.GetComponent<BoxCollider>();
        Vector3 size = boxCollider.size;
        Vector3 centre = boxCollider.center;

        //Vector3 dimObjectPosition = new Vector3(-size.x / 2, 0, -size.z / 2);        
        //Vector3 dimObjectPosition = new Vector3(0, 0, 0);

        //Set z-axis of Dimension Prefab
        Transform zaxisTranform = dimPrefab.transform.GetChild(0);
        zaxisTranform.position = new Vector3(0, 0, size.z / 2);
        zaxisTranform.localScale = new Vector3(0.01f, 0.01f, size.z);

        //Set y-axis of Dimension Prefab
        Transform yaxisTranform = dimPrefab.transform.GetChild(1);
        yaxisTranform.position = new Vector3(0, size.y / 2, 0);
        yaxisTranform.localScale = new Vector3(0.01f, size.y, 0.01f);

        //Set x-axis of Dimension Prefab
        Transform xaxisTranform = dimPrefab.transform.GetChild(2);
        xaxisTranform.position = new Vector3(size.x / 2, 0, 0);
        xaxisTranform.localScale = new Vector3(size.x, 0.01f, 0.01f);        

        Vector3 modelPosition = placementWithDraggingDroppingController.PlacedObject.transform.position;
        Quaternion modelRotation = placementWithDraggingDroppingController.PlacedObject.transform.rotation;
        Vector3 dimObjectPosition = new Vector3(modelPosition.x - size.x / 2, modelPosition.y, modelPosition.z - size.z / 2);

        ProductDimensions productDimensions = CacheManager.Instance.GetProductDimensions(ProductManager.Instance.ModelProductID);

        //Set Depth dimension text
        GameObject txtObj1 = dimPrefab.transform.GetChild(3).gameObject;
        txtObj1.transform.position = new Vector3(txtObj1.transform.position.x, txtObj1.transform.position.y, size.z / 2);
        TMPro.TMP_Text txtDim = txtObj1.GetComponent<TMPro.TMP_Text>();
        txtDim.text = productDimensions.Depth + " cm";

        //Set Height dimension text
        GameObject txtObj2 = dimPrefab.transform.GetChild(4).gameObject;
        txtObj2.transform.position = new Vector3(txtObj2.transform.position.x, centre.y + 0.4f, txtObj2.transform.position.z);
        TMPro.TMP_Text txtDim2 = txtObj2.GetComponent<TMPro.TMP_Text>();
        txtDim2.text = productDimensions.Height + " cm";

        //Set Width dimension text
        GameObject txtObj3 = dimPrefab.transform.GetChild(5).gameObject;
        txtObj3.transform.position = new Vector3(size.x / 2 + 0.4f, txtObj3.transform.position.y, txtObj3.transform.position.z);
        TMPro.TMP_Text txtDim3 = txtObj3.GetComponent<TMPro.TMP_Text>();
        txtDim3.text = productDimensions.Width + " cm";

        DimObject = Instantiate(dimPrefab, dimObjectPosition, modelRotation, model.transform);        
    }
}
