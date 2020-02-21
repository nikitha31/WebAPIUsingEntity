using ProductsMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ProductsMVC.Controllers
{
    public class ProductsTablesController : Controller
    {
        // GET: Prods
        public ActionResult Index()
        {
            IEnumerable<ProdModel> ListOfProducts;
            HttpResponseMessage response = GlobalClient.webApiClient.GetAsync("ProductsTables").Result;
            ListOfProducts = response.Content.ReadAsAsync<IEnumerable<ProdModel>>().Result;
            //ListOfProducts.Wait();
            return View(ListOfProducts);
        }
        public ActionResult Create()
        {
            return View(new ProdModel());
        }
        [HttpPost]
        public ActionResult Create(ProdModel obj)
        {
            HttpResponseMessage response = GlobalClient.webApiClient.PostAsJsonAsync("ProductsTables", obj).Result;
            return (RedirectToAction("Index"));
        }
        public ActionResult Edit(int id)
        {
            HttpResponseMessage response = GlobalClient.webApiClient.GetAsync("ProductsTables/" + id.ToString()).Result;
            return View(response.Content.ReadAsAsync<ProdModel>().Result);

        }
        [HttpPost]
        public ActionResult Edit(int id,ProdModel obj)
        {
            HttpResponseMessage response = GlobalClient.webApiClient.PutAsJsonAsync("ProductsTables/"+obj.Pid, obj).Result;
            return (RedirectToAction("Index"));
        }
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalClient.webApiClient.DeleteAsync("ProductsTables/" + id.ToString()).Result;
            return (RedirectToAction("Index"));
        }

    }
}