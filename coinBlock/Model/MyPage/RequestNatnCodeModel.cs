using System;
using DevExpress.Mvvm;

namespace coinBlock.Model.MyPage
{
    public class RequestNatnCodeModel : RequestBaseModel
    {
        public RequestNatnCodeModel() : base("bt.auth.getNatnCode.dp/proc.go") { }
    }
}