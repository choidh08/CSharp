using System;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;

namespace coinBlock.Model.MyPage
{
    public class ResponseAlarmModel : ResponseBaseModel
    {
        public ResponseAlarmDataModel data { get; set; }
        public ResponseAlarmModel()
        {
            data = new ResponseAlarmDataModel();
        }
    }

    public class ResponseAlarmDataModel
    {
        public ObservableCollection<ResponseAlarmListModel> list { get; set; }
        public ResponseAlarmDataModel()
        {
            list = new ObservableCollection<ResponseAlarmListModel>();
        }
    }

    public class ResponseAlarmListModel
    {
        public virtual string rowNo { get; set; }
        // <summary>
        /// 푸쉬종류
        /// </summary>
        public virtual string pushType { get; set; }
        /// <summary>
        /// 발송일
        /// </summary>
        public virtual string regDt { get; set; }
        /// <summary>
        /// 내용
        /// </summary>
        public virtual string cntnsMsg { get; set; }    

        public virtual Visibility contentVisible { get; set; }

    }
}