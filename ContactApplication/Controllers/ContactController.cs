using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ContactApplication.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Configuration;
using ContactApplication.CustomAttributes;

namespace ContactApplication.Controllers
{
    [CustomException]
    [CustomAuthorize]
    public class ContactController : Controller
    {

        string Baseurl = ConfigurationManager.AppSettings["Baseurl"].ToString();
        public async Task<ActionResult> Index()

        {

            List<ContactViewModel> ContactInfo = new List<ContactViewModel>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/contacts/get");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    ContactInfo = JsonConvert.DeserializeObject<List<ContactViewModel>>(EmpResponse);
                }
                return View(ContactInfo);
            }
        }
        public ActionResult Details(int id)
        {

            ContactViewModel contact = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                var responseTask = client.GetAsync("api/Contact/GetContact?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ContactViewModel>();
                    readTask.Wait();

                    contact = readTask.Result;
                }
            }

            return View(contact);

        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ContactViewModel CVM)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                var postTask = client.PostAsJsonAsync<ContactViewModel>("api/Contact/PostContact", CVM);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError(string.Empty, "Please contact administrator.");

            return View();
        }

        public ActionResult Edit(int id)
        {

            ContactViewModel contact = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                var responseTask = client.GetAsync("api/Contact/GetContact?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ContactViewModel>();
                    readTask.Wait();

                    contact = readTask.Result;
                }
            }

            return View(contact);
        }

        [HttpPost]
        public ActionResult Edit(ContactViewModel contact)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                var putTask = client.PutAsJsonAsync<ContactViewModel>("Api/Contact/Put", contact);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return View(contact);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                var deleteTask = client.DeleteAsync("api/Contact/Delete?id=" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

    }
}
