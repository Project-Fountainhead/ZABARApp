using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class RestProductDetails : MonoBehaviour, IProductDetailsManager
    {
        private RestWebClient webClient;
        
        void Awake()
        {
            webClient = gameObject.AddComponent<RestWebClient>();            
        }        

        public void CacheHomeFurnitureProductsDetail()
        {
            //webRequestsMonobehaviour.StartCoroutine(webClient.HttpGet((a) => OnHomeFurnitureProductsDetailReceived(a)));            
            StartCoroutine(webClient.HttpGet((a) => OnHomeFurnitureProductsDetailReceived(a)));
        }

        public void OnHomeFurnitureProductsDetailReceived(string response)
        {
            AWSResponse awsResponse = JsonUtility.FromJson<AWSResponse>(response);

            AWSItems[] items = awsResponse.Items;

            for (int i = 0; i < items.Length; i++)
            {
                int productID = int.Parse(items[i].ID);
                //Parse response into ProdDetails
                ProdDetails prodDetails = new ProdDetails(items[i]);

                //Update Dictionary and class object
                CacheManager.Instance.SetProductDetails(productID, prodDetails);

                GetProductImage(productID, prodDetails);

                //ProductManager.Instance.OnProductDetailsReceived(prodDetails);
            }
        }

        public void GetProductDetails(int ProductID)
        {
            //webRequestsMonobehaviour.StartCoroutine(webClient.HttpGet(ProductID, (a, b) => OnProductDetailsReceived(a, b)));
            StartCoroutine(webClient.HttpGet(ProductID, (a, b) => OnProductDetailsReceived(a, b)));
        }

        public void OnProductDetailsReceived(int ProductID, string response)
        {
            AWSResponse awsResponse = JsonUtility.FromJson<AWSResponse>(response);

            AWSItems[] items = awsResponse.Items;

            if (items.Length > 0)
            {               
                //Parse response into ProdDetails
                ProdDetails prodDetails = new ProdDetails(items[0]);

                //Update Dictionary and class object
                CacheManager.Instance.SetProductDetails(ProductID, prodDetails);                

                GetProductImage(ProductID, prodDetails);

                //ProductManager.Instance.OnProductDetailsReceived(prodDetails);
            }
        }

        public void GetProductDimensions(int ProductID)
        {
            StartCoroutine(webClient.HttpGet(APICall.EPRODUCTDIMENSIONS, ProductID, (a) => OnProductDimensionsReceived(a)));
        }

        private void OnProductDimensionsReceived(string response)
        {
            AWSProductDimensionResponse awsResponse = JsonUtility.FromJson<AWSProductDimensionResponse>(response);

            AWSProductDimension[] items = awsResponse.Items;

            if (items.Length > 0)
            {
                //Parse response into ProdDetails
                ProductDimensions prodDimensions = new ProductDimensions(items[0]);

                //Update Dictionary and class object
                int ProductID = int.Parse(items[0].ID);
                CacheManager.Instance.SetProductDimensions(ProductID, prodDimensions);                

                //ProductManager.Instance.OnProductDetailsReceived(prodDetails);
            }
        }

        public void GetProductImage(int ProductID, ProdDetails prodDetails)
        {
            if (!string.IsNullOrEmpty(prodDetails.ImageURL))
            {
                //webRequestsMonobehaviour.StartCoroutine(webClient.GetRemoteTexture(ProductID, prodDetails.ImageURL, (a, b) => OnProductImageReceived(a, b)));

                StartCoroutine(webClient.GetRemoteTexture(ProductID, prodDetails.ImageURL, (a, b) => OnProductImageReceived(a, b)));
            }
        }

        //public void GetProductImage(int ProductID, ProdDetails prodDetails)
        //{
        //    if (!string.IsNullOrEmpty(prodDetails.ImageURL))
        //    {
        //        Texture2D texture = webClient.GetRemoteTexture(prodDetails.ImageURL);

        //        OnProductImageReceived(ProductID, texture);
        //    }
        //}

        public void OnProductImageReceived(int ProductID, UnityEngine.Texture2D texture)
        {
            //Update Cache and class object
            CacheManager.Instance.SetProductImageTexture(ProductID, texture);            
        }

        public void GetAssetBundle(int ProductID)
        {
            ProdDetails prodDetails = CacheManager.Instance.GetProductDetails(ProductID);
            string url = prodDetails.BundleURL;
            //webRequestsMonobehaviour.StartCoroutine(webClient.GetAssetBundle(ProductID, url, (a, b) => OnAssetBundleReceived(a, b)));
            StartCoroutine(webClient.GetAssetBundle(ProductID, url, (a, b) => OnAssetBundleReceived(a, b)));
        }

        public void OnAssetBundleReceived(int ProductID, UnityEngine.AssetBundle assetBundle)
        {
            if (CacheManager.Instance.IsAlreadyCached(ProductID))
            {
                ProdDetails prodDetails = CacheManager.Instance.GetProductDetails(ProductID);

                // Update bundle cache
                CacheManager.Instance.SetBundleCache(prodDetails.BundleURL, assetBundle);
                
                // Load the gameObject
                GameObject gameObject = (GameObject)assetBundle.LoadAsset(prodDetails.NameInBundle);

                //update product cache
                CacheManager.Instance.SetProductModel(ProductID, gameObject);

                //ProductManager.Instance.OnModelReceived(gameObject);                
            }
        }       
    }
}
