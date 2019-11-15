using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model
{
    public class RequestLoginModel : RequestBaseModel
    {
        /// <summary>
        /// 회원 계정
        /// </summary>
        public string userEmail { get; set; }
        /// <summary>
        /// 회원 비밀번호
        /// </summary>
        public string userPwd { get; set; }

        public string langCd { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        public string regIp { get; set; }

        public RequestLoginModel() : base("bt.auth.login.dp/proc.go") { base.RtpCall(this); }
    }
}