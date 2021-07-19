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
        
        public ProductDimensions prodDimensions;

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
            //prodDimensions = new ProductDimensions(item.Depth, item.Height, item.Width);
            prodDimensions = null;
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
            this.prodDimensions = new ProductDimensions(prodDetails.prodDimensions);            
        }

        public void SetProductDimensions(ProductDimensions productDimensions)
        {
            this.prodDimensions = new ProductDimensions(productDimensions);
        }

        public ProductDimensions GetProductDimensions()
        {
            return prodDimensions;
        }
    }

    public class ProductDimensions
    {
        public string Depth;

        public string Height;

        public string Width;

        public ProductDimensions(string depth, string height, string width)
        {
            Depth = depth;
            Height = height;
            Width = width;
        }

        public ProductDimensions(ProductDimensions productDimensions)
        {
            Depth = productDimensions.Depth;
            Height = productDimensions.Height;
            Width = productDimensions.Width;
        }

        public ProductDimensions(AWSProductDimension awsProductDimension)
        {
            Depth = awsProductDimension.Depth;
            Height = awsProductDimension.Height;
            Width = awsProductDimension.Width;
        }
    }
}
