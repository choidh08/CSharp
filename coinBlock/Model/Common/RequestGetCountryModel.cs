using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.Common
{
    public class RequestGetCountryModel : RequestBaseModel
    {
        public RequestGetCountryModel() : base("bt.getCountryList.dp/proc.go") { }
    }
}