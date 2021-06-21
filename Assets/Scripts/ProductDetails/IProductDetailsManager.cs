using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public interface IProductDetailsManager
    {        
      //  void GetProductDetails(int ProductID);        

        void GetAssetBundle(int ProductID);           
    }

    //public interface IProductManager
    //{
    //    void OnProductDetailsReceived(ProdDetails prodDetails);

    //    void OnModelReceived(UnityEngine.GameObject gameObject);
    //}
}
