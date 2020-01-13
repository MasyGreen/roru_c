using DevExpress.Xpo;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;

namespace rollerru.Module.BusinessObjects
{
    [DomainComponent]
    public class npParamsImp : XPObject
    {
        public npParamsImp(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        [XafDisplayName("File")]
        [FileTypeFilter("XML", 2, "*.xml")]
        public UploadFile File
        {
            get; set;
        }
        public TypeInfoImp PropInfo
        {
            get; set;
        }
        public enum TypeInfoImp {Root_Data, Achivement_Data }
    }
}