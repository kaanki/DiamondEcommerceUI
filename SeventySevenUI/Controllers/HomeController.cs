using Newtonsoft.Json;
using SeventySevenUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SeventySevenUI.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Constants.UrlConstant);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync(string.Format("{0}{1}", Constants.UrlConstant, Constants.ItemPhotosApiName));
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        List<ItemPhotosViewModel> ProductList = new List<ItemPhotosViewModel>();

                        ProductList = JsonConvert.DeserializeObject<List<ItemPhotosViewModel>>(data);

                        ViewBag.ProductPhotos = ProductList;
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "ErrorPage");
            }
        }

        [HttpPost]
        public ActionResult UpdatePhoto(int imgid)
        {
            try
            {

                UpdatePhotoMethod(imgid);

                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "ErrorPage");
            }
        }

        private void UpdatePhotoMethod(int imgid)
        {
            if (Request.Files[0].FileName.Trim(' ') != string.Empty )
            {
                var file = Request.Files[0];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Constants.UrlConstant);
                    var value = new
                    {
                        id = imgid,
                        path = file.FileName
                    };

                    var postTask = client.PostAsJsonAsync("ItemPhotos/UpdatePhoto", value);
                    postTask.Wait();

                    var result = postTask.Result;

                }

            }
        }

        [HttpPost]
        public ActionResult DeletePhoto(int dataId)
        {
            try
            {
                DeletePhotomethod(dataId);
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "ErrorPage");
            }
        }
        private void DeletePhotomethod(int dataId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Constants.UrlConstant);
                var value = new
                {
                    id = dataId
                };

                //HTTP POST
                var postTask = client.PostAsJsonAsync("ItemPhotos/DeletePhoto", value);
                postTask.Wait();

                var result = postTask.Result;
            }


        }

    }
}