using System;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Collections.ObjectModel;
using System.Windows;
using coinBlock.Views;
using coinBlock.Utility;
using coinBlock.Model;
using System.Linq;

namespace coinBlock.ViewModels
{
    [POCOViewModel]
    public class BookMarkViewModel
    {
        public virtual ICurrentWindowService WindowService { get { return null; } }


        public static BookMarkViewModel Create()
        {
            return ViewModelSource.Create(() => new BookMarkViewModel());
        }

        protected BookMarkViewModel()
        {
            ImageSet();
        }

        public void Loaded()
        {
            try
            {
                IsBusy = true;

                dataLeft = new MenuLeftList();
                dataRight = new MenuRightList();

                IsBusy = false;
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public virtual ObservableCollection<Menu> dataLeft { get; set; }
        public virtual ObservableCollection<Menu> dataRight { get; set; }      

        public virtual bool dataLeftAllCheck { get; set; }
        public virtual bool dataRightAllCheck { get; set; }
        public virtual string img_text_bookmart_set { get; set; }
        public virtual string btn_menu_enrollment { get; set; }
        public virtual string btn_menu_enrollment_on { get; set; }
        public virtual string btn_menu_release { get; set; }
        public virtual string btn_menu_release_on { get; set; }
        public virtual string btn_bookmark_save { get; set; }
        public virtual string btn_bookmark_save_on { get; set; }

        public virtual bool IsBusy { get; set; }

        /// <summary>
        /// 전체선택 Left
        /// </summary>
        public void CmdDataLeftAllCheckChanged()
        {
            try
            {
                foreach (var itemL in dataLeft)
                {
                    itemL.CheckState = dataLeftAllCheck;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// 전체선택 Right
        /// </summary>
        public void CmdDataRightAllCheckChanged()
        {
            try
            {
                foreach (var itemR in dataRight)
                {
                    itemR.CheckState = dataRightAllCheck;
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 메뉴 등록
        /// </summary>
        public void MenuRegister()
        {
            try
            {
                foreach (var itemL in dataLeft)
                {
                    if (itemL.CheckState)
                    {
                        bool itemAddCheck = true;

                        foreach (var itemR in dataRight)
                        {
                            if (itemR.Name.Equals(itemL.Name))
                            {
                                itemAddCheck = false;
                            }
                        }
                        if (itemAddCheck)
                        {
                            Menu temp = new Menu(itemL);
                            dataRight.Add(temp);
                        }
                        itemL.CheckState = false;
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            finally
            {
                dataLeftAllCheck = false;
            }
        }
        /// <summary>
        /// 동록 해재
        /// </summary>
        public void MenuUnRegister()
        {
            try
            {
                ObservableCollection<Menu> Temp = new ObservableCollection<Menu>();

                foreach (var itemR in dataRight)
                {
                    if (itemR.CheckState)
                    {
                        Temp.Add(itemR);
                    }
                    itemR.CheckState = false;
                }

                if (Temp.Count > 0)
                {
                    foreach (var itemT in Temp)
                    {
                        dataRight.Remove(itemT);
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            finally
            {
                dataRightAllCheck = false;
            }
        }
        /// <summary>
        /// 저장
        /// </summary>
        public async void MenuSave()
        {
            try
            {
                IsBusy = true;

                string CodeSeq = string.Empty;

                if (dataRight.Count.Equals(0))
                {
                    Alert alert = new Alert(Localization.Resource.Bookmark_6, 300);
                    alert.ShowDialog();
                    return;
                }

                foreach (var itemR in dataRight)
                {
                    CodeSeq += "," + itemR.Code;
                }

                if (!string.IsNullOrWhiteSpace(CodeSeq))
                {
                    CodeSeq = CodeSeq.Substring(1);
                }

                using (RequestBookMarkSaveModel req = new RequestBookMarkSaveModel())
                {
                    req.userEmail = MainViewModel.LoginDataModel.userEmail;
                    req.menuCd = CodeSeq;

                    using (ResponseBookMarkSaveModel res = await WebApiLib.AsyncCall<ResponseBookMarkSaveModel, RequestBookMarkSaveModel>(req))
                    {
                        //리스트 저장
                        Alert alert = new Alert(Localization.Resource.Common_Alert_2);
                        if (alert.ShowDialog() == true)
                        {
                            IsBusy = false;
                            Messenger.Default.Send("QuickMenuRefresh");
                            WindowService.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public class MenuLeftList : ObservableCollection<Menu>
        {
            public MenuLeftList()
            {
                try
                {
                    string SC = CommonLib.StandardCurcyNm;

                    Add(ViewModelSource.Create(() => new Menu() { Name = Localization.Resource.Main_QuickMenu_20, CheckState = false, Code = StringEnum.GetStringValue(EnumLib.MenuCode.BuySell) }));
                    Add(ViewModelSource.Create(() => new Menu() { Name = string.Format(Localization.Resource.Main_QuickMenu_1, SC), CheckState = false, Code = StringEnum.GetStringValue(EnumLib.MenuCode.Recharge) }));
                    Add(ViewModelSource.Create(() => new Menu() { Name = string.Format(Localization.Resource.Main_QuickMenu_2, SC), CheckState = false, Code = StringEnum.GetStringValue(EnumLib.MenuCode.Withdraw) }));
                    Add(ViewModelSource.Create(() => new Menu() { Name = Localization.Resource.Main_QuickMenu_7, CheckState = false, Code = StringEnum.GetStringValue(EnumLib.MenuCode.Deposit) }));
                    Add(ViewModelSource.Create(() => new Menu() { Name = Localization.Resource.Main_QuickMenu_8, CheckState = false, Code = StringEnum.GetStringValue(EnumLib.MenuCode.Transfer) }));
                    Add(ViewModelSource.Create(() => new Menu() { Name = Localization.Resource.Main_QuickMenu_9, CheckState = false, Code = StringEnum.GetStringValue(EnumLib.MenuCode.TradeHistory) }));
                    Add(ViewModelSource.Create(() => new Menu() { Name = Localization.Resource.Main_QuickMenu_10, CheckState = false, Code = StringEnum.GetStringValue(EnumLib.MenuCode.MyAssets) }));
                    Add(ViewModelSource.Create(() => new Menu() { Name = Localization.Resource.Main_QuickMenu_12, CheckState = false, Code = StringEnum.GetStringValue(EnumLib.MenuCode.QnA) }));
                }
                catch (Exception ex)
                {
                    SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
                }
            }
        }

        public class MenuRightList : ObservableCollection<Menu>
        {
            public MenuRightList()
            {
                try
                {   
                    ObservableCollection<ResponseBookMarkMenuListModel> sMenuList;
                    string SC = CommonLib.StandardCurcyNm;

                    using (RequestBookMarkMenuModel req = new RequestBookMarkMenuModel())
                    {
                        req.userEmail = MainViewModel.LoginDataModel.userEmail;

                        using (ResponseBookMarkMenuModel res = WebApiLib.SyncCall<ResponseBookMarkMenuModel, RequestBookMarkMenuModel>(req))
                        {
                            sMenuList = res.data.list;
                            sMenuList = new ObservableCollection<ResponseBookMarkMenuListModel>(sMenuList.OrderBy(x => x.sn));
                        }
                    }
                    if (sMenuList.Count.Equals(0))
                    {
                        Add(ViewModelSource.Create(() => new Menu() { Name = Localization.Resource.Main_QuickMenu_20, CheckState = false, Code = StringEnum.GetStringValue(EnumLib.MenuCode.BuySell) }));
                        Add(ViewModelSource.Create(() => new Menu() { Name = string.Format(Localization.Resource.Main_QuickMenu_1, SC), CheckState = false, Code = StringEnum.GetStringValue(EnumLib.MenuCode.Recharge) }));
                        Add(ViewModelSource.Create(() => new Menu() { Name = string.Format(Localization.Resource.Main_QuickMenu_2, SC), CheckState = false, Code = StringEnum.GetStringValue(EnumLib.MenuCode.Withdraw) }));
                        Add(ViewModelSource.Create(() => new Menu() { Name = Localization.Resource.Main_QuickMenu_7, CheckState = false, Code = StringEnum.GetStringValue(EnumLib.MenuCode.Deposit) }));
                        Add(ViewModelSource.Create(() => new Menu() { Name = Localization.Resource.Main_QuickMenu_8, CheckState = false, Code = StringEnum.GetStringValue(EnumLib.MenuCode.Transfer) }));
                        Add(ViewModelSource.Create(() => new Menu() { Name = Localization.Resource.Main_QuickMenu_9, CheckState = false, Code = StringEnum.GetStringValue(EnumLib.MenuCode.TradeHistory) }));
                        Add(ViewModelSource.Create(() => new Menu() { Name = Localization.Resource.Main_QuickMenu_10, CheckState = false, Code = StringEnum.GetStringValue(EnumLib.MenuCode.MyAssets) }));
                        Add(ViewModelSource.Create(() => new Menu() { Name = Localization.Resource.Main_QuickMenu_12, CheckState = false, Code = StringEnum.GetStringValue(EnumLib.MenuCode.QnA) }));
                    }
                    else
                    {
                        foreach (var item in sMenuList)
                        {                    
                            if (item.menuCd.Equals(StringEnum.GetStringValue(EnumLib.MenuCode.BuySell)))
                                Add(ViewModelSource.Create(() => new Menu() { Name = Localization.Resource.Main_QuickMenu_20, CheckState = false, Code = StringEnum.GetStringValue(EnumLib.MenuCode.BuySell) }));
                            else if (item.menuCd.Equals(StringEnum.GetStringValue(EnumLib.MenuCode.Recharge)))
                                Add(ViewModelSource.Create(() => new Menu() { Name = string.Format(Localization.Resource.Main_QuickMenu_1, SC), CheckState = false, Code = StringEnum.GetStringValue(EnumLib.MenuCode.Recharge) }));
                            else if (item.menuCd.Equals(StringEnum.GetStringValue(EnumLib.MenuCode.Withdraw)))
                                Add(ViewModelSource.Create(() => new Menu() { Name = string.Format(Localization.Resource.Main_QuickMenu_2, SC), CheckState = false, Code = StringEnum.GetStringValue(EnumLib.MenuCode.Withdraw) }));
                            else if (item.menuCd.Equals(StringEnum.GetStringValue(EnumLib.MenuCode.Deposit)))
                                Add(ViewModelSource.Create(() => new Menu() { Name = Localization.Resource.Main_QuickMenu_7, CheckState = false, Code = StringEnum.GetStringValue(EnumLib.MenuCode.Deposit) }));
                            else if (item.menuCd.Equals(StringEnum.GetStringValue(EnumLib.MenuCode.Transfer)))
                                Add(ViewModelSource.Create(() => new Menu() { Name = Localization.Resource.Main_QuickMenu_8, CheckState = false, Code = StringEnum.GetStringValue(EnumLib.MenuCode.Transfer) }));
                            else if (item.menuCd.Equals(StringEnum.GetStringValue(EnumLib.MenuCode.TradeHistory)))
                                Add(ViewModelSource.Create(() => new Menu() { Name = Localization.Resource.Main_QuickMenu_9, CheckState = false, Code = StringEnum.GetStringValue(EnumLib.MenuCode.TradeHistory) }));
                            else if (item.menuCd.Equals(StringEnum.GetStringValue(EnumLib.MenuCode.MyAssets)))
                                Add(ViewModelSource.Create(() => new Menu() { Name = Localization.Resource.Main_QuickMenu_10, CheckState = false, Code = StringEnum.GetStringValue(EnumLib.MenuCode.MyAssets) }));
                            else if (item.menuCd.Equals(StringEnum.GetStringValue(EnumLib.MenuCode.QnA)))
                                Add(ViewModelSource.Create(() => new Menu() { Name = Localization.Resource.Main_QuickMenu_12, CheckState = false, Code = StringEnum.GetStringValue(EnumLib.MenuCode.QnA) }));
                        }
                    }

                }
                catch (Exception ex)
                {
                    SysLog.Error("Message[{0}], StackTrace[{1}]", ex.Message, ex.StackTrace);
                }
            }
        }

        public class Menu
        {
            public Menu()
            {

            }

            public Menu(Menu item)
            {
                Name = item.Name;
                Code = item.Code;
                CheckState = false;
            }

            public virtual string Name { get; set; }

            public virtual bool CheckState { get; set; }

            public virtual string Code { get; set; }
        }

        public void ImageSet()
        {
            string sLanguage = LoginViewModel.LanguagePack;

            img_text_bookmart_set = sLanguage + "img_text_bookmart_set.png";
            btn_menu_enrollment = sLanguage + "btn_menu_enrollment.png";
            btn_menu_enrollment_on = sLanguage + "btn_menu_enrollment_on.png";
            btn_menu_release = sLanguage + "btn_menu_release.png";
            btn_menu_release_on = sLanguage + "btn_menu_release_on.png";
            btn_bookmark_save = sLanguage + "btn_bookmark_save.png";
            btn_bookmark_save_on = sLanguage + "btn_bookmark_save_on.png";
        }
    }
}