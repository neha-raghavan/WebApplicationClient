using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using WebApplicationClient.Models;

namespace WebApplicationClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        string BaseUrl = "https://localhost:7109/api/Employee";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<ActionResult> Index()
        {
            DataTable dt = new DataTable();
            using (var client = new HttpClient())
            {
                string endpoint = BaseUrl + "/GetEmployees";
                client.BaseAddress = new Uri(endpoint);
                
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await client.GetAsync("");

                if (getData.IsSuccessStatusCode)
                {
                    string Results = getData.Content.ReadAsStringAsync().Result;
                    dt = JsonConvert.DeserializeObject<DataTable>(Results);
                }
                else
                {
                    Console.WriteLine("Error in calling web api");
                }
                ViewData.Model = dt;
            }

            return View();
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Employee model)
        {
            var client = new HttpClient();
            string endpoint = BaseUrl + "/CreateEmployee";
            client.BaseAddress = new Uri(endpoint);
            string data=JsonConvert.SerializeObject(model);
            StringContent content=new StringContent(data,Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            var client = new HttpClient();
            string endpoint = BaseUrl + "/DeleteEmployee?id=";
            client.BaseAddress = new Uri(endpoint);
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress+id.ToString()).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            Employee emp = null;
            DataTable dt = new DataTable();
            using (var client = new HttpClient())
            {

                //HTTP GET
                string endpoint = BaseUrl + "/GetEmployee?id=";
                client.BaseAddress = new Uri(endpoint);
                var responseTask = client.GetAsync(client.BaseAddress + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    string Results = result.Content.ReadAsStringAsync().Result;
                    emp = JsonConvert.DeserializeObject<Employee>(Results);

                    
                }
            }

            return View(emp);
        }
        [HttpPost]
        public IActionResult Edit(Employee emp)
        {
            using (var client = new HttpClient())
            {
                

                //HTTP POST
                //var putTask = client.PutAsJsonAsync<Employee>("student", emp);
                string endpoint = BaseUrl + "/UpdateEmployee";
                client.BaseAddress = new Uri(endpoint);
                string data = JsonConvert.SerializeObject(emp);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
    
    public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
