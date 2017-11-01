using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using TransactionsApi.Models;

namespace TransactionsApi.MessageHandlers
{
    public class APIKeyMessageHandler : DelegatingHandler
    {
        /** we override this method to validate if the information thas is in the headers is rigth, we could add more validation with more information in headers like token
          or time also we validate the acces that have to each method by role*/
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken)
        {
            TransactionsBDEntities db = new TransactionsBDEntities();
            bool validKey = false;
            IEnumerable<string> requestuser;
            var checkuserExist = httpRequestMessage.Headers.TryGetValues("User", out requestuser);
            var checkn = httpRequestMessage.RequestUri.AbsolutePath;
            if (checkuserExist)
            {
                try
                {
                    int usrid = Convert.ToInt32(requestuser.FirstOrDefault());
                    Users user = (from p in db.Users where p.Id == usrid select p).FirstOrDefault();
                    if (user != null)
                    {
                        if (user.rol_id == 1)
                            validKey = true;
                        else if (user.rol_id == 2 && (checkn == "api/Transactions/GetTransactionList"))
                            validKey = true;
                        else if (user.rol_id == 3 && (checkn == "api/Transactions/ReportTransactionAsFraud" || checkn == "api/Transactions/GetTransactionByCode"))
                            validKey = true;
                        else if (user.rol_id == 4 && (checkn == "api/Transactions/GenerateTransferdata"))
                            validKey = true;
                    }
                }
                catch (Exception exp)
                { }

            }
            else if (checkn == "/api/Users/ValidateUser")
            {
                validKey = true;
            }
            if (!validKey)
            {
                return httpRequestMessage.CreateResponse(HttpStatusCode.Forbidden, "Invalid Access");
            }

            var response = await base.SendAsync(httpRequestMessage, cancellationToken);
            return response;
        }

    }
}