using System;
using DevExpress.Mvvm;

namespace coinBlock.Model
{
    public class RequestUserFuncCheckModel : RequestBaseModel
    {
        public string userEmail { get; set; }

        public RequestUserFuncCheckModel() : base("bt.UserFuncCheck.dp/proc.go") { }
    }

}