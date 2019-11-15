using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model.Trading
{
    public class ResponseTradingSellModel : ResponseBaseModel
    {
        public ResponseTradingSellDataModel data { get; set; }
        public ResponseTradingSellModel()
        {
            data = new ResponseTradingSellDataModel();
        }
    }

    public class ResponseTradingSellDataModel
    {
        public virtual string failCd { get; set; }
    }
}