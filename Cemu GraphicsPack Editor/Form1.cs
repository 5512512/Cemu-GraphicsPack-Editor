using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

namespace Cemu_GraphicsPack_Editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AddRuleButton.Enabled = false;

            if ((bool)Properties.Settings.Default["FirstStart"] == true)
            {
                Properties.Settings.Default["FirstStart"] = false;
                Properties.Settings.Default.Save();
                MessageBox.Show("You may only change the definition once for right now. \n" +
                "Any edits to these values must be done in the Text View tab. \n" +
                "Sorry about that.");
            }
            else
            {

            }
       

        }


        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFile = new SaveFileDialog()
                {
                    FileName = "rules",
                    Filter = "rules.txt|*.txt",
                    Title = "Save Rules.txt"
                };
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter sw = new StreamWriter(saveFile.FileName);
                    sw.Write(TextViewBox.Text);
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not save file. Details: \n" +
                    "\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void AddRuleButton_Click(object sender, EventArgs e)
        {
            

            using (var addRuleForm = new AddRule())
            {
                var result = addRuleForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string rule = addRuleForm.ReturnRuleString;

                    TextViewBox.AppendText(Environment.NewLine + rule);
                }
            }
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TIDEntryBox.Clear();
            PackNameBox.Clear();
            TextViewBox.Clear();
            AddRuleButton.Enabled = false;
            SetDefinition.Enabled = true;
            TIDEntryBox.Enabled = true;
            PackNameBox.Enabled = true;
        }

        private void SetDefinition_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("[Definition] ");
            if (!string.IsNullOrWhiteSpace(TIDEntryBox.Text))
            {
                sb.AppendLine("titleIds = " + TIDEntryBox.Text);
            }
            else
            {
                sb.AppendLine("titleIds = None provided.");
            }
            if (!string.IsNullOrWhiteSpace(PackNameBox.Text))
            {
                sb.AppendFormat("name = \"{0}\" \n", PackNameBox.Text);
                sb.AppendLine("version = 2");
            }
            else
            {
                sb.AppendLine("name = \"Default\" \n");
                sb.AppendLine("version = 2");
            }
            var setDefinition = sb.ToString();

            TextViewBox.Text = (setDefinition);

            SetDefinition.Enabled = false;
            TIDEntryBox.Enabled = false;
            PackNameBox.Enabled = false;


        }

        private void TIDEntryBox_TextChanged(object sender, EventArgs e)
        {
            AddRuleButton.Enabled = true;
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Version format as follows: Major.Minor.Revision
            MessageBox.Show("Cemu GraphicPack Editor v.0.7.0 By StreakSharpshot \n" +
                "Third-Party C# Class used for Textbox Watermarks");

        }

        private void   OpenRulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFile = new OpenFileDialog()
                {
                    Filter = "rules.txt|*.txt",
                    Title = "Open GraphicPack rules.txt"
                };
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    TextViewBox.Clear();
                    TIDEntryBox.Clear();
                    PackNameBox.Clear();
                    AddRuleButton.Enabled = true;
                    SetDefinition.Enabled = false;
                    TIDEntryBox.Enabled = false;
                    PackNameBox.Enabled = false;

                    StreamReader sr = new StreamReader(openFile.FileName);
                    TextViewBox.Text = sr.ReadToEnd();

                    string titleIDs = "titleIds";
                    string packName = "name";

                    StreamReader sr2 = new StreamReader(openFile.FileName);
                    IList<string> result = new List<string>();
                    using (var reader = sr2)
                    {
                        string currentLine;
                        while ((currentLine = reader.ReadLine()) != null)
                        {
                            if (currentLine.Contains(titleIDs))
                            {
                                var input = (currentLine);
                                var output = input.Replace("titleIds = ", "");

                                TIDEntryBox.Text = (output);
                            }
                            if (currentLine.Contains(packName))
                            {
                                var input = (currentLine);
                                var output = input.Replace("name = ", "");
                                string s = output;
                                var finalOutput = s = s.Replace("\"", "");

                                PackNameBox.Text = (finalOutput);
                                break;
                            }
                        }
                    }
                    sr.Close();
                    sr2.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not open file. Details: \n" +
                    "\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


    }
}
