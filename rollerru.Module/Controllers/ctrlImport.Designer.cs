namespace rollerru.Module.Controllers
{
    partial class ctrlImport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.actImport = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // actImport
            // 
            this.actImport.AcceptButtonCaption = null;
            this.actImport.CancelButtonCaption = null;
            this.actImport.Caption = "act Import";
            this.actImport.Category = "View";
            this.actImport.ConfirmationMessage = null;
            this.actImport.Id = "actImport";
            this.actImport.ToolTip = null;
            this.actImport.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.actImport_CustomizePopupWindowParams);
            this.actImport.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.actImport_Execute);
            // 
            // ctrlImport
            // 
            this.Actions.Add(this.actImport);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction actImport;
    }
}
