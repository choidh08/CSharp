using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model
{
    public class ResponseTestLoginModel : RequestBaseModel
    {
        public string userPwd { get; set; }
        public ResponseTestLoginModel() : base("bt.auth.loginTest.dp/proc.go") { base.RtpCall(this); }
    }
}