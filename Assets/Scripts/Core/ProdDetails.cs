using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class ProdDetails
    {
        public string Vendor;

        public string ProductName;

        public string ProductDescription;

        public string ImageURL;

        public GameObject model;

        public string BundleURL;

        public string NameInBundle;

        public Texture2D ImageTexture;

        public string Price;

        public ProdDetails(int ProductID)
        {
            
        }

        public ProdDetails(AWSItems item)
        {
            ProductName = item.ProductName;
            ProductDescription = item.ProductDescription;
            Vendor = item.Vendor;
            ImageURL = item.ImageURL;
            BundleURL = item.BundleURL;
            NameInBundle = item.NameInBundle;
            Price = item.Price;
        }

        public ProdDetails()
        {

        }

        public void SetProductDetails(ProdDetails prodDetails)
        {
            this.Vendor = prodDetails.Vendor;
            this.ProductName = prodDetails.ProductName;
            this.ProductDescription = prodDetails.ProductDescription;
            this.ImageURL = prodDetails.ImageURL;
            this.model = prodDetails.model;
            this.BundleURL = prodDetails.BundleURL;
            this.NameInBundle = prodDetails.NameInBundle;
            this.ImageTexture = prodDetails.ImageTexture;
            this.Price = prodDetails.Price;
        }
    }
}
