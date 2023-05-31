using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchEngineGUI
{
    public partial class ResultsForm : Form
    {
        private string TimeLabel = "Response Time: ";
        private string FileShare = @"\\" + Endpoint.DOMAIN + @"\Users\ENVY 15\source\repos\SearchEngine\Documents\";

        public ResultsForm(List<string> filePaths, string responseTime)
        {
            InitializeComponent();

            // Display response time
            TimeLabel += responseTime;
            ResponseTimeLabel.Text = TimeLabel;

            // Display API results
            foreach (string filePath in filePaths)
            {
                ResultsListBox.Items.Add(new MaterialSkin.MaterialListBoxItem(filePath));
            }
        }

        private void ResultsListBox_SelectedIndexChanged(object sender, MaterialSkin.MaterialListBoxItem selectedItem)
        {
            progressBar.Value = 100;
            progressBar.Visible = true;
            try
            {
                // Open file with suitable application
                Process process = Process.Start(FileShare + selectedItem.Text);
                process.WaitForExit();
                progressBar.Visible = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Oops, could not open file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressBar.Visible = false;
            }
        }
    }
}
