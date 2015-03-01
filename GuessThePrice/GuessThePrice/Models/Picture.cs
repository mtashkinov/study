using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessThePrice.Models

{
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Auth;
    using Microsoft.WindowsAzure.Storage.Table;
    using System.Configuration;
    public class Picture : TableEntity
    {
        public Picture(string author, string name)
        {
            this.PartitionKey = name;
            this.RowKey = author;
        }

        public Picture() { }
        public long Price { get; set; }
        public string URL { get; set; }
    }
}