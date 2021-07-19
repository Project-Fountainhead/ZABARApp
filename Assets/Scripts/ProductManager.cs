using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class ProductManager : Singleton<ProductManager>//, IProductManager
    {
        private ObjectHandler objHandler;        

        int modelProductID = 0;
        public int ModelProductID { get => modelProductID; set => modelProductID = value; }        

        float downloadProgress;
        public float DownloadProgress { get => downloadProgress; set => downloadProgress = value; }

        public override void Awake()
        {
            base.Awake();            
            DownloadProgress = 0;
            objHandler = gameObject.AddComponent<ObjectHandler>();
        }        

        public void CacheProductDetails()
        {
            if (!CacheManager.Instance.IsProductDetailsCached())
            {
                objHandler.CacheHomeFurnitureProductsDetail();
            }
        }               

        public void GetAssetBundle(int ProductID)
        {
            objHandler.GetAssetBundle(ProductID, true);            
        }

        public void GetProductDimensions(int ProductID)
        {
            objHandler.GetProductDimensions(ProductID);
        }
    }    
}
