using System;
using DevExpress.Mvvm;

namespace coinBlock.Model
{
    public class RequestSampleViewModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public RequestSampleViewModel() : base("bt.arbitrage.encTest.dp/proc.go") { }        
    }
}