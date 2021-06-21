using Assets.Scripts;
using Assets.Scripts.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandler : MonoBehaviour
{
    CachedProductDetails cachedProductDetails;
    RestProductDetails restProductDetails;

    //public ObjectHandler()
    //{
    //    cachedProductDetails = new CachedProductDetails();
    //    restProductDetails = new RestProductDetails();
    //}

    private void Awake()
    {
        cachedProductDetails = gameObject.AddComponent<CachedProductDetails>();

        restProductDetails = gameObject.AddComponent<RestProductDetails>();
    }

    IProductDetailsManager GetObject(int ProductID, bool checkModelCached = false)
    {
        bool isCached = false;

        if (checkModelCached)
        {
            isCached = CacheManager.Instance.IsModelCached(ProductID) || CacheManager.Instance.IsAssetBundleCached(ProductID);
        }
        else
        {
            isCached = CacheManager.Instance.IsAlreadyCached(ProductID);
        }

        if (isCached)
        {
            return cachedProductDetails;
        }
        else
        {
            return restProductDetails;
        }
    }

    public void CacheHomeFurnitureProductsDetail()
    {
        restProductDetails.CacheHomeFurnitureProductsDetail();
    }

    internal void GetAssetBundle(int productID, bool checkModelCached)
    {
        IProductDetailsManager obj = GetObject(productID, checkModelCached);

        obj.GetAssetBundle(productID);
    }
}
