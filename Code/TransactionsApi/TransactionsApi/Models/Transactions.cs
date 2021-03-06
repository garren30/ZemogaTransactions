//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TransactionsApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transactions
    {
        public int Id { get; set; }
        public System.DateTime TransactionDate { get; set; }
        public int transactiontype_id { get; set; }
        public double amount { get; set; }
        public int clientorg_Id { get; set; }
        public Nullable<double> oldbalanceorg { get; set; }
        public Nullable<double> newbalanceorg { get; set; }
        public int clientdest_Id { get; set; }
        public Nullable<double> oldbalancedest { get; set; }
        public Nullable<int> isfraud { get; set; }
        public int usercreation_id { get; set; }
        public Nullable<int> usermarkfraud_id { get; set; }
        public Nullable<System.DateTime> frauddate { get; set; }
        public Nullable<double> newbalancedest { get; set; }
    
        public virtual Clients Clients { get; set; }
        public virtual Clients Clients1 { get; set; }
        public virtual Transactiontypes Transactiontypes { get; set; }
        public virtual Users Users { get; set; }
        public virtual Users Users1 { get; set; }
    }
}
