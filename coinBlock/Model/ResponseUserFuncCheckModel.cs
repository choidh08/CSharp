using System;
using DevExpress.Mvvm;

namespace coinBlock.Model
{
    public class ResponseUserFuncCheckModel : ResponseBaseModel
    {
        public ResponseUserFuncCheckDataModel data { get; set; }
        public ResponseUserFuncCheckModel()
        {
            data = new ResponseUserFuncCheckDataModel();
        }
    }

    public class ResponseUserFuncCheckDataModel
    {
        public virtual string status { get; set; }
    }
}