using System.Collections.ObjectModel;

namespace coinBlock.Model
{
    public class MenuModel
    {   
        /// <summary>
        /// 메뉴 구분자
        /// </summary>
        public virtual string Id { get; set; }
        /// <summary>
        /// 메뉴명
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 메뉴명 별칭
        /// </summary>
        public virtual string NickName { get; set; }
        /// <summary>
        /// 자식 메뉴(3뎁스 이상 사용할 경우)
        /// </summary>
        public ObservableCollection<MenuModel> Children { get; set; }
        /// <summary>
        /// 자식 메뉴 유무
        /// </summary>
        public virtual bool IsHasChildren { get { return Children != null && Children.Count != 0; } }
        /// <summary>
        /// 메뉴 아이콘 패스
        /// </summary>
        public virtual string IconPath { get; set; }
        /// <summary>
        /// 툴팁 메세지
        /// </summary>
        public virtual string ToolTip { get; set; }
        /// <summary>
        /// 파라미터
        /// </summary>
        public virtual object Param { get; set; }
        /// <summary>
        /// 메뉴접근 권한(0:기본(아무나 접근가능), 1:인증3단계 완료 ,2:OPT 인증)
        /// </summary>
        public int Certify { get; set; } = 0;
    }
}