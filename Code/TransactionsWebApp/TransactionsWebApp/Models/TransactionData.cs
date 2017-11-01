using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransactionsWebApp.Models
{
    public class TransactionData
    {
        public int Id { get; set; }
        public float amount { get; set; }
        public string clientorgcode { get; set; }
        public string clientdestcode { get; set; }
        public Transactiontypes transactionType { get; set; }
    }
}