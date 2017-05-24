using System;
using System.Text;
using System.Windows.Forms;

namespace Cemu_GraphicsPack_Editor
{
    public partial class AddRule : Form
    {
        public string ReturnRuleString {get;set;}

        public AddRule()
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void OKButton_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(TexRedComment.Text))
            {
                sb.AppendLine("[TextureRedefine] " + "#" + TexRedComment.Text);
            }
            else
            {
                sb.AppendLine("[TextureRedefine] ");
            }
            if (!string.IsNullOrWhiteSpace(WidthBox.Text))
            {
                sb.AppendLine("width = " + WidthBox.Text);
            }
            if (!string.IsNullOrWhiteSpace(HeightBox.Text))
            {
                sb.AppendLine("height = " + HeightBox.Text);
            }
            if (!string.IsNullOrWhiteSpace(FormatsBox.Text))
            {
                sb.AppendLine("formats = " + FormatsBox.Text);
            }
            if (!string.IsNullOrWhiteSpace(ExcludeFormats.Text))
            {
                sb.AppendLine("formatsExcluded = " + ExcludeFormats.Text);
            }
            if (!string.IsNullOrWhiteSpace(RenderWidth.Text))
            {
                sb.AppendLine("overwriteWidth = " + RenderWidth.Text);
            }
            if (!string.IsNullOrWhiteSpace(RenderHeight.Text))
            {
                sb.AppendLine("overwriteHeight = " + RenderHeight.Text);
            }
            if (!string.IsNullOrWhiteSpace(OverwriteFormatBox.Text))
            {
                sb.AppendLine("overwriteFormat = " + OverwriteFormatBox.Text);
            }
            var commitAddRule = sb.ToString();

            this.ReturnRuleString = (commitAddRule);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }



    }
    
}
