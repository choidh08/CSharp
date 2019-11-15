using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace coinBlock.Model
{
    public class ResponseLoginModel : ResponseBaseModel
    {
        public ResponseLoginDataModel data { get; set; }
        public ResponseLoginModel()
        {
            data = new ResponseLoginDataModel();
        }
    }

    public class ResponseLoginDataModel
    {
        // <summary>
        /// 회원 계정
        /// </summary>
        public string userEmail { get; set; }
        // <summary>
        /// 회원이름
        /// </summary>
        public string userNm { get; set; }
        /// <summary>
        /// 로그인 성공여부('Y':성공,'N':실패)
        /// </summary>
        public virtual string loginYn { get; set; }
        /// <summary>
        /// 이메일 인증여부('Y':성공,'N':실패)
        /// </summary>
        public virtual string emailCertYn { get; set; }
        /// <summary>
        /// 블록여부 ('Y' : 블록)
        /// </summary>
        public virtual string blckYn { get; set; }
        /// <summary>
        /// 블록해재여부 ('Y' : 블록해재)
        /// </summary>
        public virtual string relYn { get; set; }
        /// <summary>
        /// 계좌정보유무
        /// </summary>
        public virtual string accountNo { get; set; }
        /// <summary>
        /// OPT인증여부(존재하면 완료, 없으면 미완료)
        /// </summary>
        public virtual string otpNo { get; set; }
        /// <summary>
        /// SMS인증여부('Y':성공,'N':실패)
        /// </summary>
        public virtual string smsCertYn { get; set; }
        /// <summary>
        /// 공지사항
        /// </summary>
        public virtual string notice { get; set; }
        /// <summary>
        /// 실패코드
        /// </summary>
        public virtual string failCd { get; set; }
        /// <summary>
        /// 사용안함여부('Y':성공,'N':실패)
        /// </summary>
        public virtual string useYn { get; set; }
        /// <summary>
        /// 최초 구분 Y, N
        /// </summary>
        public virtual string isIpFirst { get; set; }
        /// <summary>
        /// 등록 아이피
        /// </summary>
        public virtual string regIp { get; set; }
        /// <summary>
        /// 계정잠금 여부 ('Y':잠금'N':미잠금)
        /// </summary>
        public virtual string lockYn { get; set; }

        public virtual string kycStatus { get; set; }

        // Y: 해외 N: 국내
        public virtual string foreignIp { get; set; }

        public virtual string accCertYn { get; set; }

        public virtual string userInfoYn { get; set; }

        public virtual string title { get; set; }
    }
}