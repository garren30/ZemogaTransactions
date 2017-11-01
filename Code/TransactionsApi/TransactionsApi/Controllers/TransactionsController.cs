using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TransactionsApi.Models;

namespace TransactionsApi.Controllers
{
    /** In this class we have all methods that use to work with transaction data apis, just the rigth roles have acces to the different methods */
    public class TransactionsController : ApiController
    {
        private TransactionsBDEntities db = new TransactionsBDEntities();

        [HttpGet]
        /** this method get the total of transactions that we have in the db, if the filters are emptys this will bring all information otherwise the information
         will be filtered by each filter thar send in the url request, will return a TransactionData list with just the basic information */
        public List<TransactionData> GetTransactionList(string initdate = "", string finaldate = "", string destclient = "", int fraud = 0)
        {
            IQueryable<Transactions> transactions = from p in db.Transactions select p;
            List<TransactionData> transactionsList = new List<TransactionData>();
            if (initdate != "")
            {
                DateTime datei = DateTime.Now;
                DateTime.TryParse(initdate, out datei);
                transactions = transactions.Where(p => (p.TransactionDate >= datei));
            }
            if (finaldate != "")
            {
                DateTime datef = DateTime.Now;
                DateTime.TryParse(finaldate, out datef);
                transactions = transactions.Where(p => p.TransactionDate <= datef);
            }
            if (destclient != "")
            {
                transactions = transactions.Where(p => p.Clients1.ClientCode.Contains(destclient));
            }
            if (fraud == 1)
            {
                transactions = transactions.Where(p => p.isfraud == 1);
            }
            foreach (Transactions transaction in transactions)
            {
                transactionsList.Add(
                    new TransactionData() {
                        Code = transaction.Id,
                        TransactionDate = transaction.TransactionDate,
                        TransactionType = transaction.Transactiontypes.transactiontype,
                        TransactionType_code = transaction.transactiontype_id,
                        Amount = float.Parse(transaction.amount.ToString()),
                        ClientOrigin = transaction.Clients.ClientCode,
                        OldBalanceOrg = float.Parse(transaction.oldbalanceorg.ToString()),
                        NewBalanceOrg = float.Parse(transaction.newbalanceorg.ToString()),
                        ClientDestination = transaction.Clients1.ClientCode,
                        OldBalanceDest = float.Parse(transaction.oldbalancedest.ToString()),
                        NewBalanceDest = float.Parse(transaction.newbalancedest.ToString()),
                        IsFraud = transaction.isfraud == 1 ? true : false,
                    }
                );
            }
            return transactionsList;
        }

        [HttpGet]
        /** this method get one transactions that match with de code sended by url, this will return basic information for this transaction */
        public TransactionData GetTransactionByCode(int code)
        {
            Transactions transaction = (from p in db.Transactions where p.Id == code select p).FirstOrDefault();
            if (transaction != null)
            {
                return new TransactionData() {
                    Code = transaction.Id,
                    TransactionDate = transaction.TransactionDate,
                    TransactionType = transaction.Transactiontypes.transactiontype,
                    TransactionType_code = transaction.transactiontype_id,
                    Amount = float.Parse(transaction.amount.ToString()),
                    ClientOrigin = transaction.Clients.ClientCode,
                    OldBalanceOrg = float.Parse(transaction.oldbalanceorg.ToString()),
                    NewBalanceOrg = float.Parse(transaction.newbalanceorg.ToString()),
                    ClientDestination = transaction.Clients1.ClientCode,
                    OldBalanceDest = float.Parse(transaction.oldbalancedest.ToString()),
                    NewBalanceDest = float.Parse(transaction.newbalancedest.ToString()),
                    IsFraud = transaction.isfraud == 1 ? true : false,
                };
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        /** this method will change the isfraud value to 1 to the transaction that match with the code sended to the method, also update the date and user who mark it 
         * as fraud, this method wil return basic information for this transaction*/
        public TransactionData ReportTransactionAsFraud([FromBody] TransactionData code)
        {
            Transactions transaction = (from p in db.Transactions where p.Id == code.Code select p).FirstOrDefault();
            
            if (transaction != null)
            {
                transaction.isfraud = 1;
                transaction.frauddate = DateTime.Now;
                transaction.usermarkfraud_id = 1;
                db.SaveChanges();
                return new TransactionData()
                {
                    Code = transaction.Id,
                    TransactionDate = transaction.TransactionDate,
                    TransactionType = transaction.Transactiontypes.transactiontype,
                    TransactionType_code = transaction.transactiontype_id,
                    Amount = float.Parse(transaction.amount.ToString()),
                    ClientOrigin = transaction.Clients.ClientCode,
                    OldBalanceOrg = float.Parse(transaction.oldbalanceorg.ToString()),
                    NewBalanceOrg = float.Parse(transaction.newbalanceorg.ToString()),
                    ClientDestination = transaction.Clients1.ClientCode,
                    OldBalanceDest = float.Parse(transaction.oldbalancedest.ToString()),
                    NewBalanceDest = float.Parse(transaction.newbalancedest.ToString()),
                    IsFraud = transaction.isfraud == 1 ? true : false,
                };
            }
            else
            {
                return null;
            }
        }

        [HttpPut]
        /** This method will create a new transaction in the db with the information that is recived and using a stored procedure, after create a new record will return 
         this record basic information*/
        public TransactionData GenerateTransferdata([FromBody] TransactionData data)
        {
            try
            {
                ObjectParameter Identity = new ObjectParameter("Identity", typeof(String));
                db.GetTransactionData(data.ClientOrigin, data.ClientDestination, data.Amount, data.TransactionType_code, 1, Identity);
                int idt = Convert.ToInt32(Identity.Value);
                Transactions transaction = (from p in db.Transactions where p.Id == idt select p).FirstOrDefault();

                if (transaction != null)
                {
                    return new TransactionData()
                    {
                        Code = transaction.Id,
                        TransactionDate = transaction.TransactionDate,
                        TransactionType = transaction.Transactiontypes.transactiontype,
                        TransactionType_code = transaction.transactiontype_id,
                        Amount = float.Parse(transaction.amount.ToString()),
                        ClientOrigin = transaction.Clients.ClientCode,
                        OldBalanceOrg = float.Parse(transaction.oldbalanceorg.ToString()),
                        NewBalanceOrg = float.Parse(transaction.newbalanceorg.ToString()),
                        ClientDestination = transaction.Clients1.ClientCode,
                        OldBalanceDest = float.Parse(transaction.oldbalancedest.ToString()),
                        NewBalanceDest = float.Parse(transaction.newbalancedest.ToString()),
                        IsFraud = transaction.isfraud == 1 ? true : false,
                    };
                }
                else
                {
                    return null;
                }
            }
            catch(Exception exp)
            {
                return null;
            }
        }
    }
}
