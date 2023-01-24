using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OwinWebApi.DB;
using OwinWebApi.DB.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace OwinWebApi
{
    public class ValuesController : ApiController
    {
        DataService dataService;
        private static HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("http://localhost:8081/"),
        };
        public ValuesController()
        {
            dataService= new DataService();
        }
        // GET api/values 
        public async Task<HttpResponseMessage> Get()
        {
            Console.WriteLine(DateTime.Now.ToString() + " - Incoming GET request");
            List<Data> result = new List<Data>();
            try
            {
                result = await dataService.GetAllAsync();

            }
            catch (Exception)
            {

            }
            return Request.CreateResponse(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }

        // GET api/values/5 
        public async Task<HttpResponseMessage> Get(int id)
        {
            Console.WriteLine(DateTime.Now.ToString() + " - Incoming GET request with id");

            Data result = await dataService.GetByIdAsync(id);

            return Request.CreateResponse(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);

        }

        // POST api/values 
        public async Task<HttpResponseMessage> Post([FromBody] Data value)
        {
            Console.WriteLine(DateTime.Now.ToString()+" - Incoming POST request");
            
            if (value!=null)
            {
                value.Idopont = DateTime.Now;
                int id = await dataService.AddNewAsync(value);
                return Request.CreateResponse(HttpStatusCode.OK, new { Id = id }, Configuration.Formatters.JsonFormatter); 
            }
            return Request.CreateResponse(HttpStatusCode.NotAcceptable, new { Error="Missing data" }, Configuration.Formatters.JsonFormatter);
        }

        // PUT api/values/5 
        public async Task<HttpResponseMessage> Put([FromBody] CommunicationType data)
        {
            string json = System.Text.Json.JsonSerializer.Serialize(new {tipus=data.Tipus });
            HttpContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            await client.PostAsync(client.BaseAddress, content);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE api/values/5 
        public async Task<HttpResponseMessage> Delete()
        {
            await dataService.DeleteAllAsync();
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}