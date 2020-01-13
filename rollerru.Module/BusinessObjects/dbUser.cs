using DevExpress.Xpo;
using DevExpress.Persistent.Base;

namespace rollerru.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class dbUser : XPObject
    {
        public dbUser(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region
        private string user_code;
        [Indexed(Unique = true)]
        public string User_Code
        {
            get { return user_code; }
            set { SetPropertyValue("User_Code", ref user_code, value); }
        }

        private string user_name;
        public string User_Name
        {
            get { return user_name; }
            set { SetPropertyValue("User_Name", ref user_name, value); }
        }
        #endregion
    }
}