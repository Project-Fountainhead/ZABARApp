using Assets.Scripts.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadButtonImage : MonoBehaviour
{
    RawImage rawImage;

    public int ProductID;

    bool IsImageReceived = false;
    
    void Awake()
    {
        rawImage = gameObject.GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(UpdateImageTexture());
    }

    IEnumerator UpdateImageTexture()
    {
        if (!IsImageReceived)
        {
            ProdDetails prodDetails = CacheManager.Instance.GetProductDetails(ProductID);

            while (prodDetails.ImageTexture == null)
            {
                yield return null;
            }

            rawImage.texture = prodDetails.ImageTexture;
            IsImageReceived = true;
        }
    }
}
