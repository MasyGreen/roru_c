using DevExpress.Xpo;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;

namespace rollerru.Module.BusinessObjects
{
    [DomainComponent]
    public class UploadFile : FileData, ISupportFullName
    {
        public UploadFile(Session session) : base(session) { }
        [Custom("AllowEdit", "False")]
        public string FullName
        {
            get; set;
        }
    }
}