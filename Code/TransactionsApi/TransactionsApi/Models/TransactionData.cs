using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransactionsApi.Models
{
    /** This class was created to recive and send basic information for each transaction, to avoid send private information*/
    public class TransactionData
    {
        public int Code { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public int TransactionType_code { get; set; }
        public float Amount { get; set; }
        public string ClientOrigin { get; set; }
        public float  OldBalanceOrg { get; set; }
        public float NewBalanceOrg { get; set; }
        public string ClientDestination { get; set; }
        public float OldBalanceDest { get; set; }
        public float NewBalanceDest { get; set; }
        public bool IsFraud { get; set; }
    }
}