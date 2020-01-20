using DevExpress.Persistent.Base.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;

namespace rollerru.Web
{
    public class CustomAuthentication : AuthenticationStandard
    {
        private bool IsUserExists()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return false;
            }
            using (IObjectSpace objectSpace = Application.CreateObjectSpace(UserType))
            {
                return FindUser(objectSpace) != null;
            }
        }
        private IAuthenticationStandardUser FindUser(IObjectSpace objectSpace)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return null;
            }
            return (IAuthenticationStandardUser)objectSpace.FindObject(UserType, new BinaryOperator("UserName", UserName));
        }
        private string UserName
        {
            get { return HttpContext.Current.Request.Params["id"]; }
        }

        public override bool AskLogonParametersViaUI
        {
            get
            {
                if (IsUserExists())
                {
                    return false;
                }
                return base.AskLogonParametersViaUI;
            }
        }
        public override object Authenticate(IObjectSpace objectSpace)
        {
            IAuthenticationStandardUser user = FindUser(objectSpace);
            if (user != null)
            {
                user.ChangePasswordOnFirstLogon = true;
                objectSpace.CommitChanges();
                return user;
            }
            return base.Authenticate(objectSpace);
        }
        public XafApplication Application { get; set; }
    }
}