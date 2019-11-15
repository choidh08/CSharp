using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using DevExpress.Mvvm.POCO;
using coinBlock.Model.Trading;
using System.ComponentModel;
using System.Windows;
using DevExpress.Data;
using coinBlock.Model.Dashboard;
using coinBlock.Utility;
using System.Threading.Tasks;
using System.Threading;

namespace coinBlock.ViewModels.Wallet
{
    [POCOViewModel]
    public class ViewModel1 : ISupportParameter, ISupportParentViewModel
    {
        public virtual string popupdata { get; set; }
        public virtual string subBtcData { get; set; }
        public virtual string subEthData { get; set; }
        public virtual object ParentViewModel { get; set; }

        public object Parameter { get; set; }

        protected void OnParentViewModelChanged()
        {
            subBtcData = ((WebSocketTestViewModel)ParentViewModel).BtcData;
            subEthData = ((WebSocketTestViewModel)ParentViewModel).EthData;
            this.RaisePropertyChanged((x) => x.subBtcData);
            ((INotifyPropertyChanged)ParentViewModel).PropertyChanged += ViewModel1_PropertyChanged;
        }

        private void ViewModel1_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "BtcData")
            {
                subBtcData = ((WebSocketTestViewModel)sender).BtcData;
                this.RaisePropertyChanged((x) => x.subBtcData);
            }

            if (e.PropertyName == "EthData")
            {
                subEthData = ((WebSocketTestViewModel)sender).EthData;
                this.RaisePropertyChanged((x) => x.subEthData);
            }
        }

        public virtual void Change()
        {
            //((WebSocketTestViewModel)ParentViewModel).BtcData = "subBtcData";
            //((WebSocketTestViewModel)ParentViewModel).EthData = "subEthData";
            //((WebSocketTestViewModel)ParentViewModel).Change();
        }

        public static ViewModel1 Create()
        {
            return ViewModelSource.Create(() => new ViewModel1());
        }

        public ViewModel1()
        {

        }

        public void Loaded()
        {
            if (Parameter != null)
            {
                string a = Parameter.ToString();
                popupdata = Parameter.ToString();
            }
        }
    }
}
