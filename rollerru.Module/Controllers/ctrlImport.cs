using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using rollerru.Module.BusinessObjects;
using System;
using System.Linq;
using System.Xml;

namespace rollerru.Module.Controllers
{
    public partial class ctrlImport : ViewController
    {
        public ctrlImport() { InitializeComponent(); }
        protected override void OnActivated() { base.OnActivated(); }
        protected override void OnViewControlsCreated() { base.OnViewControlsCreated(); }
        protected override void OnDeactivated() { base.OnDeactivated(); }

        private void actImport_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace objectSpace = Application.CreateObjectSpace(typeof(npParamsImp));
            npParamsImp theObject = objectSpace.CreateObject<npParamsImp>();
            theObject.PropInfo = npParamsImp.TypeInfoImp.Root_Data;
            objectSpace.CommitChanges();
            string detailViewId = Application.GetDetailViewId(typeof(npParamsImp));
            DetailView createdView = Application.CreateDetailView(objectSpace, detailViewId, true, theObject);
            createdView.ViewEditMode = ViewEditMode.Edit;
            e.View = createdView;
        }
        private void MyMessage(string msg_str)
        {
            //MessageOptions options = new MessageOptions();
            //options.Duration = 8000;
            //options.Message = msg_str;
            //options.Type = InformationType.Success;
            //options.Web.Position = InformationPosition.Right;
            //options.Win.Caption = "Info";
            //options.Win.Type = WinMessageType.Toast;
            //Application.ShowViewStrategy.ShowMessage(options);
        }

        private void actImport_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //разобрать параметры
            npParamsImp parameter = (npParamsImp)e.PopupWindowViewCurrentObject;
            //Основные данные
            if (parameter.PropInfo == npParamsImp.TypeInfoImp.Root_Data)
            {
                Session currentSession = ((XPObjectSpace)ObjectSpace).Session;

                //User In Post
                using (UnitOfWork uow = new UnitOfWork(currentSession.DataLayer))
                {
                    MyMessage("Start delete: dbPostUser");
                    XPCollection<dbPostUser> ColDelete = new XPCollection<dbPostUser>(PersistentCriteriaEvaluationBehavior.InTransaction, uow, null);
                    uow.Delete(ColDelete);
                    uow.CommitChanges();
                    //MyMessage("End delete: dbPostUser");
                }

                //Post
                using (UnitOfWork uow = new UnitOfWork(currentSession.DataLayer))
                {
                    MyMessage("Start delete: dbPost");
                    XPCollection<dbPost> ColDelete = new XPCollection<dbPost>(PersistentCriteriaEvaluationBehavior.InTransaction, uow, null);
                    uow.Delete(ColDelete);
                    uow.CommitChanges();
                    //MyMessage("End delete: dbPost");
                }

                //User
                using (UnitOfWork uow = new UnitOfWork(currentSession.DataLayer))
                {
                    MyMessage("Start delete: dbUser");
                    XPCollection<dbUser> ColDelete = new XPCollection<dbUser>(PersistentCriteriaEvaluationBehavior.InTransaction, uow, null);
                    uow.Delete(ColDelete);
                    uow.CommitChanges();
                    //MyMessage("End delete: dbUser");
                }
                currentSession.PurgeDeletedObjects();


                XmlDocument doc = new XmlDocument();
                doc.Load(parameter.File.FullName);
                XmlNode nodeRoot = doc.SelectSingleNode("root");

                int _error = 0;
                int _sesion_insertcount = 0;
                int _insertcount = 0;

                #region User
                using (UnitOfWork uow = new UnitOfWork(currentSession.DataLayer))
                {
                    XmlNodeList ItemsUser = nodeRoot.SelectNodes("references/reference[@reference_name = 'user']/item");
                    _sesion_insertcount = 0;
                    _insertcount = 0;
                    foreach (XmlNode itemUser in ItemsUser)
                    {
                        int j = 0;
                        string _USER_CODE = ""; string _USER_NAME = "";
                        for (j = 0; j <= itemUser.Attributes.Count - 1; j++)
                        {

                            if (string.Equals(itemUser.Attributes[j].Name.ToString().ToUpper(), "USER_CODE"))
                                _USER_CODE = itemUser.Attributes[j].Value;

                            if (string.Equals(itemUser.Attributes[j].Name.ToString().ToUpper(), "USER_NAME"))
                                _USER_NAME = itemUser.Attributes[j].Value;
                        }

                        dbUser el = new dbUser(uow);
                        el.User_Name = _USER_NAME;
                        el.User_Code = _USER_CODE;
                        el.Save();
                        _insertcount++;
                        _sesion_insertcount++;


                        if (_sesion_insertcount == 1000)
                        {
                            uow.CommitChanges();
                            _sesion_insertcount = 0;
                        }
                    }
                    uow.CommitChanges();
                    MyMessage(string.Format("End import: User - {0}/{1} ", ItemsUser.Count.ToString(), _insertcount.ToString()));
                }
                #endregion

                #region Post
                using (UnitOfWork uow = new UnitOfWork(currentSession.DataLayer))
                {
                    XmlNodeList ItemsPost = nodeRoot.SelectNodes("references/reference[@reference_name = 'post']/item");
                    _error = 0;
                    _sesion_insertcount = 0;
                    _insertcount = 0;
                    foreach (XmlNode itemPost in ItemsPost)
                    {
                        int j = 0;
                        string _USER_CODE = ""; string _USER_NAME = "";

                        string _POST_CODE = ""; string _POST_NAME = "";
                        DateTime _POST_DATE = DateTime.Now;

                        for (j = 0; j <= itemPost.Attributes.Count - 1; j++)
                        {

                            if (string.Equals(itemPost.Attributes[j].Name.ToString().ToUpper(), "POST_CODE"))
                                _POST_CODE = itemPost.Attributes[j].Value;

                            if (string.Equals(itemPost.Attributes[j].Name.ToString().ToUpper(), "POST_NAME"))
                                _POST_NAME = itemPost.Attributes[j].Value;


                            if (string.Equals(itemPost.Attributes[j].Name.ToString().ToUpper(), "POST_DATE"))
                                try
                                {
                                    _POST_DATE = DateTime.Parse(itemPost.Attributes[j].Value);
                                }
                                catch (Exception exp)
                                {
                                    _error++;
                                }

                            if (string.Equals(itemPost.Attributes[j].Name.ToString().ToUpper(), "USER_CODE"))
                                _USER_CODE = itemPost.Attributes[j].Value;

                            if (string.Equals(itemPost.Attributes[j].Name.ToString().ToUpper(), "USER_NAME"))
                                _USER_NAME = itemPost.Attributes[j].Value;

                        }

                        dbUser dbuser = uow.FindObject<dbUser>(CriteriaOperator.Parse("[User_Code] == ?", _USER_CODE));
                        if (dbuser != null)
                        {
                            dbPost el = new dbPost(uow);
                            el.DBUser = dbuser;
                            el.Post_Code = _POST_CODE;
                            el.Post_Name = _POST_NAME;
                            el.Post_Date = _POST_DATE;

                            if (_POST_NAME.ToUpper().IndexOf("ОТМЕНА") == 0)
                                el.Post_Status = dbPost.TypeStatus.Cancel;
                            else
                                el.Post_Status = dbPost.TypeStatus.Assept;

                            el.User_Code = _USER_CODE;
                            el.User_Name = _USER_NAME;
                            el.Save();
                            _insertcount++;
                            _sesion_insertcount++;
                        }
                        else
                            _error++;
                        if (_sesion_insertcount == 1000)
                        {
                            uow.CommitChanges();
                            _sesion_insertcount = 0;
                        }
                    }

                    uow.CommitChanges();
                    MyMessage(string.Format("End import: Post - {0}/{1} ", ItemsPost.Count.ToString(), _insertcount.ToString()));
                }
                #endregion

                #region PostUser
                using (UnitOfWork uow = new UnitOfWork(currentSession.DataLayer))
                {
                    XmlNodeList ItemsPostUser = nodeRoot.SelectNodes("references/reference[@reference_name = 'post_user']/item");
                    _error = 0;
                    _sesion_insertcount = 0;
                    _insertcount = 0;
                    foreach (XmlNode itemPostUser in ItemsPostUser)
                    {
                        _sesion_insertcount++;
                        int j = 0;
                        string _POST_CODE = ""; string _USER_CODE = ""; string _USER_NAME = "";
                        for (j = 0; j <= itemPostUser.Attributes.Count - 1; j++)
                        {

                            if (string.Equals(itemPostUser.Attributes[j].Name.ToString().ToUpper(), "POST_CODE"))
                                _POST_CODE = itemPostUser.Attributes[j].Value;

                            if (string.Equals(itemPostUser.Attributes[j].Name.ToString().ToUpper(), "USER_CODE"))
                                _USER_CODE = itemPostUser.Attributes[j].Value;

                            if (string.Equals(itemPostUser.Attributes[j].Name.ToString().ToUpper(), "USER_NAME"))
                                _USER_NAME = itemPostUser.Attributes[j].Value;

                        }
                        //УЧАСТНИК
                        dbUser dbuser = uow.FindObject<dbUser>(CriteriaOperator.Parse("[User_Code] == ?", _USER_CODE));
                        //ПОКАТУШКА
                        dbPost dbpost = uow.FindObject<dbPost>(CriteriaOperator.Parse("[Post_Code] == ?", _POST_CODE));
                        if (dbpost != null && dbuser != null)
                        {
                            //Участник покатушки
                            dbPostUser el = new dbPostUser(uow);
                            el.DBUser = dbuser;
                            el.User_Code = _USER_CODE;
                            el.User_Name = _USER_NAME;
                            el.DBPost = dbpost;
                            el.Post_Code = _POST_CODE;
                            el.Save();
                            _insertcount++;
                            _sesion_insertcount++;
                        }

                        if (_sesion_insertcount == 5000)
                        {
                            uow.CommitChanges();
                            _sesion_insertcount = 0;
                        }
                    }
                    uow.CommitChanges();
                    MyMessage(string.Format("End import: PostUser - {0}/{1} ", ItemsPostUser.Count.ToString(), _insertcount.ToString()));
                }
                #endregion
                ObjectSpace.CommitChanges();
                ObjectSpace.Refresh();
                MyMessage(string.Format("Файл - {0}, импортирован! ( Ошибок: {1})", parameter.File.FullName, _error.ToString()));

            }

        }
    }
}
