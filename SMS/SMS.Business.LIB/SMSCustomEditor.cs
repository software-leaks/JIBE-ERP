using AjaxControlToolkit.HTMLEditor;
using System.Web;
/// <summary>
/// Summary description for documentgerator
/// </summary>
/// 
namespace SMS.Custom
{
    public class SMSCustomEditor : Editor
    {
        public bool PrevMode { get; set; }
        public bool HTMLMode { get; set; }
        public bool DesgMode { get; set; }
        public bool PictureButton { get; set; }
        protected override void FillBottomToolbar()
        {
            if (PrevMode)
                BottomToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.PreviewMode());
            if (HTMLMode)
                BottomToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.HtmlMode());
            if (DesgMode)
                BottomToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.DesignMode());
        }
        protected override void FillTopToolbar()
        {
            if (DesgMode)
            {
                base.FillTopToolbar();
            }
            if (PictureButton)
            {
                AjaxControlToolkit.HTMLEditor.ToolbarButton.MethodButton btn = new AjaxControlToolkit.HTMLEditor.ToolbarButton.MethodButton();
                btn.NormalSrc = "../Images/uploadimage.png";
                btn.ID = "btnUplaodImg";
                btn.Attributes.Add("title", "Upload Image");
                btn.Attributes.Add("onclick", "show(event);");
                TopToolbar.Buttons.Add(btn);
            }
            
        }
    }
}