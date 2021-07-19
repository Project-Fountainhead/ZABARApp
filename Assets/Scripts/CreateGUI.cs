using Assets.Scripts;
using Assets.Scripts.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateGUI : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabButton;

    [SerializeField]
    private RectTransform ParentPanel;

    [SerializeField]
    GameObject LoadingSymbol;

    //bool IsGUIConstructed = false;
    bool productDetailsCached = false;

    // Use this for initialization
    void Start()
    {
        ProductManager.Instance.CacheProductDetails();
        StartCoroutine(GenerateListofProducts());
    }

    private IEnumerator GenerateListofProducts()
    {
        yield return new WaitUntil(() => productDetailsCached == true);

        CreateGUIElements();

        LoadingSymbol.SetActive(false);
    }

    private void Update()
    {
        if (!productDetailsCached)
        {
            productDetailsCached = CacheManager.Instance.GetNumberOfCachedProducts() != 0;
        }
    }

    void CreateGUIElements()
    {
        Dictionary<int, ProdDetails> ProductCache = CacheManager.Instance.GetProductCache();

        foreach (KeyValuePair<int, ProdDetails> keyValuePair in ProductCache)
        {
            //yield return null;

            GameObject goButton = (GameObject)Instantiate(prefabButton, ParentPanel);
            //goButton.transform.SetParent(, false);
            //goButton.transform.localScale = new Vector3(1, 1, 1);

            Button tempButton = goButton.GetComponent<Button>();

            int tempInt = keyValuePair.Key;
            tempButton.onClick.AddListener(() => ButtonClicked(tempInt));

            TMPro.TextMeshProUGUI text = goButton.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            text.text = keyValuePair.Value.ProductDescription;

            //GameObject ChildGameObject1 = ParentGameObject.transform.GetChild(0).gameObject;
            Text txtName = goButton.transform.GetChild(2).GetComponent<Text>();
            txtName.text = keyValuePair.Value.ProductName;

            Text txtPrice = goButton.transform.GetChild(3).GetComponent<Text>();
            txtPrice.text = "Rs " + keyValuePair.Value.Price;

            RawImage rawImage = goButton.GetComponentInChildren<RawImage>();
            LoadButtonImage imageComp = rawImage.gameObject.AddComponent<LoadButtonImage>();
            imageComp.ProductID = tempInt;

            //rawImage.texture = keyValuePair.Value.ImageTexture;            
        }
    }

    void ButtonClicked(int ProductID)
    {
        Debug.Log("Button clicked = " + ProductID);
        ProductManager.Instance.ModelProductID = ProductID;
        UnityEngine.SceneManagement.SceneManager.LoadScene("ProductVisualize");
    }
}