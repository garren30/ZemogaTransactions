using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransactionsApi.Models
{
    public class UserData
    {
        public int Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}