using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model
{
    public class RequestMainAssetModel : RequestBaseModel
    {
        /// <summary>
        /// 회원계정
        /// </summary>
        public virtual string userEmail { get; set; }
        
        public RequestMainAssetModel() : base("bt.user.asset.dp/proc.go") { base.RtpCall(this); }
    }
}
