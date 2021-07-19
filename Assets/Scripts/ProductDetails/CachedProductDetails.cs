using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class CachedProductDetails : MonoBehaviour, IProductDetailsManager
    {
        //IProductManager productManager;        

        //public CachedProductDetails()
        //{
        //    //productManager = prdtManager;            
        //}

        //public void GetProductDetails(int ProductID)
        //{
        //    ProdDetails prodDetails = CacheManager.Instance.GetProductDetails(ProductID);
        //    ProductManager.Instance.OnProductDetailsReceived(prodDetails);
        //}

        public ProductDimensions GetProductDimensions(int ProductID)
        {
            ProdDetails prodDetails = CacheManager.Instance.GetProductDetails(ProductID);

            return prodDetails.prodDimensions;            
        }

        public void GetAssetBundle(int ProductID)
        {
            ProdDetails prodDetails = CacheManager.Instance.GetProductDetails(ProductID);

            if (prodDetails.model == null)
            {
                AssetBundle assetBundle = CacheManager.Instance.GetBundleCache(prodDetails.BundleURL);

                if (assetBundle == null)
                {
                    Debug.LogError("Product's asset bundle is null");                    
                }
                else
                {
                    //update product cache
                    GameObject gameObject = (GameObject)assetBundle.LoadAsset(prodDetails.NameInBundle);
                    CacheManager.Instance.SetProductModel(ProductID, gameObject);
                }                
            }            
        }          
    }
}
