using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model
{
    public class ResponsePushModel :ResponseBaseModel
    {
        public virtual ResponsePushDataModel data { get; set; }

        public ResponsePushModel()
        {
            data = new ResponsePushDataModel();
        }
    }

    public class ResponsePushDataModel
    {
        public virtual ObservableCollection<ResponsePushListModel> list { get; set; }
        public ResponsePushDataModel()
        {
            list = new ObservableCollection<ResponsePushListModel>();
        }
    }

    public class ResponsePushListModel
    {
        public virtual string pushType { get; set; }
        public virtual string pushMsg { get; set; }
    }
}
