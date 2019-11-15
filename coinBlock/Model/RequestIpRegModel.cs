using System;
using DevExpress.Mvvm;

namespace coinBlock.Model
{
    public class RequestIpRegModel : RequestBaseModel
    {
        /// <summary>
        /// 사용자Email
        /// </summary>
        public string userEmail { get; set; }
        /// <summary>
        /// 아이피
        /// </summary>
        public string ip { get; set; }
        /// <summary>
        /// 허용 시간 (시간 단위입력)
        /// </summary>
        public int limtHr { get; set; }

        public RequestIpRegModel() : base("bt.insUptUserIp.dp/proc.go") { }
    }
}