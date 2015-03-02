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
            Models.Picture[] pictures;
            int index;

            pictures = (Models.Picture[])Session["Pictures"];

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
            Models.Picture[] pictures;
            int index;
            pictures = (Models.Picture[])Session["Pictures"];
            index = (int)Session["Index"];
            ViewBag.guessedPrice = guessedPrice;

            return View(pictures[index]);
        }
    }
}