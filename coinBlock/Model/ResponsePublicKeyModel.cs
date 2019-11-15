using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model
{
    public class ResponsePublicKeyModel : ResponseBaseModel
    {
        public ResponsePublicKeyDataModel data { get; set; }
        public ResponsePublicKeyModel()
        {
            data = new ResponsePublicKeyDataModel();
        }
    }

    public class ResponsePublicKeyDataModel
    {
        /// <summary>
        /// RSA Module
        /// </summary>
        public string pubKeyModule { get; set; }
        /// <summary>
        /// RSA Exponent
        /// </summary>
        public string pubKeyExponent { get; set; }
    }
}
