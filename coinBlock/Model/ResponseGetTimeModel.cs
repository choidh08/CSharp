using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model
{  
    public class ResponseGetTimeModel : ResponseBaseModel
    {
        public virtual ResponseGetTimeDataModel data { get; set; }
        public ResponseGetTimeModel()
        {
            data = new ResponseGetTimeDataModel();
        }
    }

    public class ResponseGetTimeDataModel
    {
        public virtual long sysTime { get; set; }
    }
}