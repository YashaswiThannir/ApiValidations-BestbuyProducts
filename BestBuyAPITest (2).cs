using System.Net;
using NUnit.Framework;
using RestSharp;
using BestbuyTest;


namespace BestbuyTest
{
    public class BestBuyAPITest
    {
        IRestClient restClient;
        
        IRestRequest restRequest;  
           
        string endpointUrl = "http://localhost:3030";  

        [SetUp]
        public void Setup()
        {
            restClient = new RestClient();
        }

        [Test]
        public void VerifyGetProductAPI()
        {
            restRequest = new RestRequest(endpointUrl + "/products");
            
            IRestResponse restResponse = restClient.Get(restRequest);
            
            Assert.AreEqual(HttpStatusCode.OK, restResponse.StatusCode);
            
            Assert.AreEqual(200, (int)restResponse.StatusCode);
        }

        [Test]
        public void VerifyGetProductAPIAndDeserializeResponse()
        {
            restRequest = new RestRequest(endpointUrl + "/products");
            
            IRestResponse<RootProduct> restResponse = restClient.Get<RootProduct>(restRequest);
            
            Assert.AreEqual(HttpStatusCode.OK, restResponse.StatusCode);
            
            Assert.AreEqual(200, (int)restResponse.StatusCode);
            
            int limit = restResponse.Data.limit;
            
            Assert.AreEqual(10, limit);
            
            var priceOfFirstProduct = restResponse.Data.data[0].price;
            
            Assert.AreEqual(5.49, priceOfFirstProduct);
        }

        [Test]
        public void VerifyAddProduct()
        {
            string requestPayload = "{\r\n  \"name\": \"Samsung Mobile\",\r\n  \"type\": \"Mobile\",\r\n  \"price\": 100,\r\n  \"shipping\": 10,\r\n  \"upc\": \"ahsdj324\",\r\n  \"description\": \"Best Samsung Mobile\",\r\n  \"manufacturer\": \"Samsung\",\r\n  \"model\": \"M21\",\r\n  \"url\": \"string\",\r\n  \"image\": \"string\"\r\n}";

            IRestRequest restRequest = new  RestRequest(endpointUrl + "/products");

            restRequest.AddJsonBody(requestPayload);

            IRestResponse restResponse = restClient.Post(restRequest);

            Assert.AreEqual(201, (int)restResponse.StatusCode);
 
        }

        [Test]
        public void VerifyAddProductWithRequestPayloadAsAnObject()
        {
            Product requestPayload = new Product();

            requestPayload.name = "Samsung Mobile";
            requestPayload.price = 700;
            requestPayload.shipping = 30;
            requestPayload.description = "Best Mobile";
            requestPayload.type = "Mobile";
            requestPayload.upc = "dgf";
            requestPayload.manufacturer = "Samsung";
            requestPayload.model = "M21";
            requestPayload.url = "jodwo";
            requestPayload.image = "wequeqk";



            IRestRequest restRequest = new  RestRequest(endpointUrl + "/products");

            restRequest.AddJsonBody(requestPayload);

            IRestResponse restResponse = restClient.Post(restRequest);

            Assert.AreEqual(201, (int)restResponse.StatusCode);
 
        }

        [Test]
        public void VerifyUpdateProductWithRequestPayloadAsAnObject()
        {
            Product requestPayload = new Product();

            requestPayload.name = "Samsung Mobile";
            requestPayload.price = 700;
            requestPayload.shipping = 30;
            requestPayload.description = "Best Mobile";
            requestPayload.type = "Mobile";
            requestPayload.upc = "dgf";
            requestPayload.manufacturer = "Samsung";
            requestPayload.model = "M21";
            requestPayload.url = "jodwo";
            requestPayload.image = "wequeqk";



            IRestRequest restRequest = new  RestRequest(endpointUrl + "/products");

            restRequest.AddJsonBody(requestPayload);

            IRestResponse<Datum> restResponse = restClient.Post<Datum>(restRequest);

            Assert.AreEqual(201, (int)restResponse.StatusCode);

            int id = restResponse.Data.id;

            //PUT request

            Product requestPayloadForEdit = new Product();

            requestPayloadForEdit.name = "Samsung Mobile  Galaxy";
            requestPayloadForEdit.price = 700;
            requestPayloadForEdit.shipping = 30;
            requestPayloadForEdit.description = "Best Mobile Affordable";
            requestPayloadForEdit.type = "Mobile";
            requestPayloadForEdit.upc = "dgf";
            requestPayloadForEdit.manufacturer = "Samsung";
            requestPayloadForEdit.model = "M21";
            requestPayloadForEdit.url = "jodwo";
            requestPayloadForEdit.image = "wequeqk";



            IRestRequest restRequestFromPut = new  RestRequest(endpointUrl + "/products/" + id);

            restRequestFromPut.AddJsonBody(requestPayloadForEdit);


            IRestResponse<Datum> restResponseFromEdit = restClient.Put<Datum>(restRequestFromPut);

            Assert.AreEqual(200, (int)restResponseFromEdit.StatusCode);

            string updatedProductName = restResponseFromEdit.Data.name;

            Assert.AreEqual(requestPayloadForEdit.name, updatedProductName);
        }

         [Test]
        public void VerifyDeleteProductWithRequestPayloadAsAnObject()
        {
            Product requestPayload = new Product();

            requestPayload.name = "Samsung Mobile";
            requestPayload.price = 700;
            requestPayload.shipping = 30;
            requestPayload.description = "Best Mobile";
            requestPayload.type = "Mobile";
            requestPayload.upc = "dgf";
            requestPayload.manufacturer = "Samsung";
            requestPayload.model = "M21";
            requestPayload.url = "jodwo";
            requestPayload.image = "wequeqk";



            IRestRequest restRequest = new  RestRequest(endpointUrl + "/products");

            restRequest.AddJsonBody(requestPayload);

            IRestResponse<Datum> restResponse = restClient.Post<Datum>(restRequest);

            Assert.AreEqual(201, (int)restResponse.StatusCode);

            int id = restResponse.Data.id;

            //Delete request

            IRestRequest restRequestForDelete = new  RestRequest(endpointUrl + "/products/" + id);

            IRestResponse<Datum> restResponseForDelete = restClient.Delete<Datum>(restRequestForDelete);

            Assert.AreEqual(200, (int)restResponseForDelete.StatusCode);

            //Get request

            IRestRequest restRequestForGet = new  RestRequest(endpointUrl + "/products/" + id);

            IRestResponse<Datum> restResponseForGet = restClient.Get<Datum>(restRequestForGet);

            Assert.AreEqual(404, (int)restResponseForGet.StatusCode);

        }
    }
}
