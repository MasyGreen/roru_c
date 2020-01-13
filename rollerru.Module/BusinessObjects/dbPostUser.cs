using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace rollerru.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class dbPostUser : XPObject
    {
        public dbPostUser(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        #region
        private dbUser dbuser;
        public dbUser DBUser
        {
            get { return dbuser; }

            set { SetPropertyValue("DBUser", ref dbuser, value); }
        }
        private dbPost dbpost;
        [Association("dbPost-dbPostUser")]
        public dbPost DBPost
        {
            get { return dbpost; }
            set { SetPropertyValue("DBPost", ref dbpost, value); }
        }

        #region расшифровка поста
        private string post_code;
        public string Post_Code
        {
            get { return post_code; }
            set { SetPropertyValue("Post_Code", ref post_code, value); }
        }
        #endregion

        #region расшифровка участника
        private string user_code;
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

        #endregion

    }
}