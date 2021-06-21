using Assets.Scripts;
using Assets.Scripts.Core;
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

    bool IsGUIConstructed = false;

    // Use this for initialization
    //void Start()
    //{
    //    //StartCoroutine(CreateSceneGUI());
    //    //CreateSceneGUI();
    //}

    private void Update()
    {
        StartCoroutine(CreateSceneGUI());
    }

    IEnumerator CreateSceneGUI()
    {
        yield return null;

        while (!IsGUIConstructed && IsProductsDetailReady())
        {
            //StartCoroutine(CreateSceneGUI());
            CreateGUIElements();

            yield return null;
        }        
    }

    bool IsProductsDetailReady()
    {
        Dictionary<int, ProdDetails> ProductCache = CacheManager.Instance.GetProductCache();

        if (ProductCache.Count == 0)
        {
            return false;
        }

        return true;

        //bool ProductsDetailReady = true;

        //foreach (KeyValuePair<int, ProdDetails> keyValuePair in ProductCache)
        //{
        //    if (keyValuePair.Value.ImageTexture == null)
        //    {
        //        ProductsDetailReady = false;
        //    }
        //}

        //return ProductsDetailReady;
    }

    void CreateGUIElements()
    {
        Dictionary<int, ProdDetails> ProductCache = CacheManager.Instance.GetProductCache();        

        foreach (KeyValuePair<int, ProdDetails> keyValuePair in ProductCache)
        {
            //yield return null;

            GameObject goButton = (GameObject)Instantiate(prefabButton,ParentPanel);
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
        IsGUIConstructed = true;
    }

    void ButtonClicked(int ProductID)
    {
        Debug.Log("Button clicked = " + ProductID);
        ProductManager.Instance.ModelProductID = ProductID;
        //StartCoroutine(GetAssetBundle(ProductID));
        UnityEngine.SceneManagement.SceneManager.LoadScene("ProductVisualize");
    }

    IEnumerator GetAssetBundle(int ProductID)
    {
        ProductManager.Instance.GetAssetBundle(ProductID);

        yield return null;
    }
}