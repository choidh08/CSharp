using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace coinBlock.Model.Common
{
    public class ResponseGetMkCoinListModel : ResponseBaseModel
    {
        public ResponseGetMkCoinLisDatatModel data { get; set; }
        public ResponseGetMkCoinListModel()
        {
            data = new ResponseGetMkCoinLisDatatModel();
        }
    }

    public class ResponseGetMkCoinLisDatatModel
    {
        public ObservableCollection<ResponseGetMkCoinListListtModel> list { get; set; }
        public ResponseGetMkCoinLisDatatModel()
        {
            list = new ObservableCollection<ResponseGetMkCoinListListtModel>();
        }
    }

    public class ResponseGetMkCoinListListtModel
    {
        public virtual string curcyNm { get; set; }
        public virtual string curcyCd { get; set; }
        public virtual string mkType { get; set; }
    }
}