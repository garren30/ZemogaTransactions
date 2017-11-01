﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TransactionsWebApp.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class TransactionsBDEntities : DbContext
    {
        public TransactionsBDEntities()
            : base("name=TransactionsBDEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<Transactiontypes> Transactiontypes { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    
        public virtual int GetTransactionData(string clientorgdcode, string clientdestcode, Nullable<double> amount, Nullable<int> transactiontype_id, Nullable<int> usercreation_id, ObjectParameter identity)
        {
            var clientorgdcodeParameter = clientorgdcode != null ?
                new ObjectParameter("clientorgdcode", clientorgdcode) :
                new ObjectParameter("clientorgdcode", typeof(string));
    
            var clientdestcodeParameter = clientdestcode != null ?
                new ObjectParameter("clientdestcode", clientdestcode) :
                new ObjectParameter("clientdestcode", typeof(string));
    
            var amountParameter = amount.HasValue ?
                new ObjectParameter("amount", amount) :
                new ObjectParameter("amount", typeof(double));
    
            var transactiontype_idParameter = transactiontype_id.HasValue ?
                new ObjectParameter("transactiontype_id", transactiontype_id) :
                new ObjectParameter("transactiontype_id", typeof(int));
    
            var usercreation_idParameter = usercreation_id.HasValue ?
                new ObjectParameter("usercreation_id", usercreation_id) :
                new ObjectParameter("usercreation_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GetTransactionData", clientorgdcodeParameter, clientdestcodeParameter, amountParameter, transactiontype_idParameter, usercreation_idParameter, identity);
        }
    }
}
