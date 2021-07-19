using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Assets.Scripts.Core
{
    public class RestWebClient : MonoBehaviour
    {        
        private string productDetailsURL = "https://lkz1ni34e9.execute-api.ap-southeast-1.amazonaws.com/test/myresource?";
        private string productDimensions = "https://sobtcolppf.execute-api.ap-southeast-1.amazonaws.com/test/myresource?";

        public IEnumerator HttpGet(string url, System.Action<UnityWebRequest> callback)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError)
                {
                    Debug.Log("StatusCode: " + webRequest.responseCode);
                    Debug.Log("Error: " + webRequest.error);                    
                }

                if (webRequest.isDone)
                {
                    //string data = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
                    //Debug.Log("Data: " + data);                    
                    callback(webRequest);
                }
            }
        }

        public IEnumerator HttpGet(APICall apiCall, int ProductID, System.Action<string> callback)
        {
            string url = Helper.GetURLFromEnum(apiCall);
            using (UnityWebRequest webRequest = UnityWebRequest.Get($"{url}ButtonID={ProductID}"))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError)
                {
                    Debug.Log("StatusCode: " + webRequest.responseCode);
                    Debug.Log("Error: " + webRequest.error);
                }

                if (webRequest.isDone)
                {
                    string data = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
                    //Debug.Log("Data: " + data);                    
                    callback(data);
                }
            }
        }

        public IEnumerator HttpGet(int ProductID, System.Action<int, string> callback)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get($"{productDetailsURL}ButtonID={ProductID}"))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError || webRequest.isHttpError)
                {
                    Debug.Log("Some network/http error occured: " + webRequest.error);
                }
                else if (webRequest.isDone)
                {
                    string data = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);                    
                    callback(ProductID, data);
                }
            }
        }

        public IEnumerator HttpGet(System.Action<string> callback)
        {            
            using (UnityWebRequest webRequest = UnityWebRequest.Get($"{productDetailsURL}ButtonID={-1}"))
            {
                StartCoroutine(WatForResponse(webRequest));

                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError || webRequest.isHttpError)
                {
                    Debug.Log("Some network/http error occured: " + webRequest.error);
                }
                else if (webRequest.isDone)
                {
                    string data = System.Text.Encoding.UTF8.GetString(webRequest.downloadHandler.data);
                    callback(data);
                }
            }
        }

        public async Task<string> HttpGet()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{productDetailsURL}ButtonID={-1}");
            HttpWebResponse response = (HttpWebResponse)(await request.GetResponseAsync());
            System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
            string jsonResponse = reader.ReadToEnd();
            return jsonResponse;
            // WeatherInfo info = JsonUtility.FromJson<WeatherInfo>(jsonResponse);
            // return info;
        }

        public IEnumerator GetAssetBundle(int ProductID, string url, /*Text debugtxt, */System.Action<int, AssetBundle> callback)
        {                        
            using (UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url))
            {
                StartCoroutine(WatForResponse(www));

                yield return www.SendWebRequest();
                
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(www.error);                    
                }
                else
                {
                    AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
                    Debug.Log("bundle downloaded");                    
                    callback(ProductID, bundle);
                }
            }
        }

        public IEnumerator DownloadImage(string MediaUrl, System.Action<Texture2D> callback)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
                Debug.Log(request.error);
            else
            {
                //YourRawImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                callback(((DownloadHandlerTexture)request.downloadHandler).texture);
            }
        }

        public IEnumerator GetRemoteTexture(int ProductID, string url, /*Text debugtxt, */System.Action<int, Texture2D> callback)            
        {
            using (UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(url))
            {
                //GameObject gameObject2 = new GameObject("RestWebClient");
                //webRequestsMonobehaviour = gameObject2.AddComponent<WebRequestsMonobehaviour>();
                //webRequestsMonobehaviour.StartCoroutine(WatForResponse(textureRequest));

                yield return textureRequest.SendWebRequest();

                if (textureRequest.isNetworkError || textureRequest.isHttpError )
                {
                    Debug.Log("Network/Http Error Occured");
                }
                else
                {
                    DownloadHandlerTexture downloadHandlerTexture = textureRequest.downloadHandler as DownloadHandlerTexture;
                    callback(ProductID, downloadHandlerTexture.texture);
                }

            }
        }

        public Texture2D GetRemoteTexture(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            //request.ContentType = "image/png";
            //request.Headers.Add("Authorization", "Bearer " + AUTH_TOKEN);            

            //Download All the bytes
            byte[] bytes = downloadFullData(request);

            Texture2D texture = new Texture2D(8, 8);
            texture.LoadRawTextureData(bytes);

            return texture;
        }

        byte[] downloadFullData(HttpWebRequest request)
        {
            using (WebResponse response = request.GetResponse())
            {

                if (response == null)
                {
                    return null;
                }

                using (Stream input = response.GetResponseStream())
                {
                    byte[] buffer = new byte[16 * 1024];
                    using (MemoryStream ms = new MemoryStream())
                    {
                        int read;
                        while (input.CanRead && (read = input.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            ms.Write(buffer, 0, read);
                        }
                        return ms.ToArray();
                    }
                }
            }
        }

        IEnumerator WatForResponse(UnityWebRequest request)
        {
            //using try/catch for a Unity bug when request.isDone raises exception as it has been disposed 
            bool isRequestDone = false;

            try
            {
                isRequestDone = request.isDone;
            }
            catch (ArgumentNullException e)
            {
                Debug.Log("Exception: " + e.Message);
                isRequestDone = true;
            }
            while (!isRequestDone)
            {
                //progressBar.value = request.downloadProgress;                
                try
                {
                    //downloadProgress = request.downloadProgress;
                    ProductManager.Instance.DownloadProgress = request.downloadProgress;
                    Debug.Log("Progress: " + (ProductManager.Instance.DownloadProgress * 100));
                }
                catch (ArgumentNullException e)
                {
                    Debug.Log("Exception: " + e.Message);
                    //downloadProgress = 1;
                    ProductManager.Instance.DownloadProgress = 1;
                    isRequestDone = true;
                }

                yield return new WaitForSeconds(1f);            
            }            
        }        
    }
}