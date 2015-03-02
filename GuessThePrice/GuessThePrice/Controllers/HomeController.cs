using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;
using System.IO;

namespace GuessThePrice.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Models.Picture[] pictures = (Models.Picture[])Session["Pictures"];
            int index = 0;

            if (pictures == null)
            {
                pictures = GetAllFromTable();
                index = 0;
                Session.Add("Pictures", pictures);
                Session.Add("Index", index);
            } else
            {
                index = (int)Session["Index"];
                index = (index + 1) % pictures.Length;
                Session["Index"] = index;
            }

            return View(pictures[index]);
        }


        private Models.Picture[] GetAllFromTable()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                 ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("Pictures");
            TableQuery<Models.Picture> query = new TableQuery<Models.Picture>();

            return table.ExecuteQuery(query).ToArray();
        }

        public ActionResult Guess(Models.Picture picture, string guessedPrice)
        {
            Models.Picture[] pictures = (Models.Picture[])Session["Pictures"]; ;
            int index = (int)Session["Index"];
            long price = 0;

            ViewBag.guessedPrice = guessedPrice;
            if (Int64.TryParse(guessedPrice, out price))
            {
                return View(pictures[index]);
            } else
            {
                return View("Index", pictures[index]);
            }
        }
    }
}