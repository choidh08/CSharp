using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;

namespace coinBlock.Model
{   
    public class RequestGetTimeModel : RequestBaseModel
    {
        public virtual long sysTime { get; set; }

        public RequestGetTimeModel() : base("bt.getTime.dp/proc.go") { }
    }
}