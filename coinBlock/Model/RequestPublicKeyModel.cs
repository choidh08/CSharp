using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model
{
    public class RequestPublicKeyModel :RequestBaseModel
    {
        public RequestPublicKeyModel() : base("bt.security.getPublicKey.dp/proc.go") { }
    }
}
