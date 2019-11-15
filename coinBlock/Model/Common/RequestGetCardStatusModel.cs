using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Common
{
    public class RequestGetCardStatusModel : RequestBaseModel
    {
        public string orderId { get; set; }

        public RequestGetCardStatusModel() : base("card.getStatusValue.dp/proc.go") { }
    }
}