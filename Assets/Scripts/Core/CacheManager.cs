using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core
{
    class CacheManager : Singleton<CacheManager>

    {
        static Dictionary<int, ProdDetails> productCache;
        static Dictionary<string, AssetBundle> dicAssetBundles;

        public override void Awake()
        {
            base.Awake();
            productCache = new Dictionary<int, ProdDetails>();
            dicAssetBundles = new Dictionary<string, AssetBundle>();
        }

        public Dictionary<int, ProdDetails> GetProductCache()
        {
            return productCache;
        }

        public bool IsProductDetailsCached()
        {
            return !(productCache.Count == 0);
        }
        public bool IsAlreadyCached(int ProductID)
        {
            return productCache.ContainsKey(ProductID);            
        }

        public bool IsModelCached(int ProductID)
        {
            return productCache.ContainsKey(ProductID) && productCache[ProductID].model != null;            
        }

        public bool IsAssetBundleCached(int ProductID)
        {
            return productCache[ProductID] != null && dicAssetBundles.ContainsKey(productCache[ProductID].BundleURL);           
        }

        public int GetNumberOfCachedProducts()
        {
            return productCache.Count;
        }

        public ProdDetails GetProductDetails(int ProductID)
        {
            if (productCache.ContainsKey(ProductID))
            {
                return productCache[ProductID];
            }
            else
            {
                return null;
            }
        }

        public void SetProductDetails(int ProductID, ProdDetails prodDetails)
        {
            //Can put a check if this ProductID already exists in the dictionary, then there must be some error
            productCache[ProductID] = prodDetails;
        }

        public void SetProductImageTexture(int ProductID, UnityEngine.Texture2D texture)
        {
            if (productCache.ContainsKey(ProductID))
            {
                productCache[ProductID].ImageTexture = texture;
            }
            else
            {
                Debug.Log("Product ID " + ProductID + " is not cached");
            }
        }

        public void SetBundleCache(string url, AssetBundle bundle)
        {
            if (!dicAssetBundles.ContainsKey(url))
            {
                dicAssetBundles[url] = bundle;
            }
        }

        public AssetBundle GetBundleCache(string url)
        {
            if (!string.IsNullOrEmpty(url) && dicAssetBundles.ContainsKey(url))
            {
                return dicAssetBundles[url];
            }
            else
            {
                return null;
            }
        }

        public void SetProductModel(int ProductID, GameObject gameObject)
        {
            ProdDetails prodDetails = productCache[ProductID];
            prodDetails.model = gameObject;
        }

        public GameObject GetProductModel(int ProductID)
        {
            ProdDetails prodDetails = productCache[ProductID];
            return prodDetails.model;
        }
    }
}
