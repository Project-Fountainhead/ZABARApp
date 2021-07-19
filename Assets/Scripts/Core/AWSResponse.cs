using System;

[Serializable]
public class AWSResponse
{
    public AWSItems[] Items;

    public string Count;

    public string ScannedCount;

    public string ResponseMetadata;
}

[Serializable]
public class AWSItems
{
    public string Vendor;

    public string ID;

    public string ProductName;

    public string ProductDescription;

    public string ImageURL;

    public string BundleURL;

    public string NameInBundle;

    public string Price;

    public string Depth;

    public string Height;

    public string Width;
}

[Serializable]
public class AWSProductDimensionResponse
{
    public AWSProductDimension[] Items;

    public string Count;

    public string ScannedCount;

    public string ResponseMetadata;
}

[Serializable]
public class AWSProductDimension
{
    public string ID;

    public string Depth;

    public string Height;

    public string Width;
}


/*{
  "Items": [
    {
      "Vendor": "Hoid",
      "ID": "1",
      "ProductName": "Sofa",
      "ProductDescription": "Leather Sofa with Resham wood base",
      "URL": "https://logodix.com/logo/1330708.png"
    }
  ],
  "Count": 1,
  "ScannedCount": 1,
  "ResponseMetadata": {
    "RequestId": "7444CPGFH1J41TLND17N4EGTMFVV4KQNSO5AEMVJF66Q9ASUAAJG",
    "HTTPStatusCode": 200,
    "HTTPHeaders": {
      "server": "Server",
      "date": "Sun, 25 Apr 2021 13:05:22 GMT",
      "content-type": "application/x-amz-json-1.0",
      "content-length": "219",
      "connection": "keep-alive",
      "x-amzn-requestid": "7444CPGFH1J41TLND17N4EGTMFVV4KQNSO5AEMVJF66Q9ASUAAJG",
      "x-amz-crc32": "732661657"
    },
    "RetryAttempts": 0
  }
}*/
