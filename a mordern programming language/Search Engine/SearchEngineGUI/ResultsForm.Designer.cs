
namespace SearchEngineGUI
{
    partial class ResultsForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("ListItem1");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("ListItem2");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("");
            this.ResponseTimeLabel = new System.Windows.Forms.Label();
            this.ResultsListView = new MaterialSkin.Controls.MaterialListView();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.ResultsListBox = new MaterialSkin.Controls.MaterialListBox();
            this.SuspendLayout();
            // 
            // ResponseTimeLabel
            // 
            this.ResponseTimeLabel.AutoSize = true;
            this.ResponseTimeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResponseTimeLabel.Location = new System.Drawing.Point(12, 24);
            this.ResponseTimeLabel.Name = "ResponseTimeLabel";
            this.ResponseTimeLabel.Size = new System.Drawing.Size(136, 20);
            this.ResponseTimeLabel.TabIndex = 1;
            this.ResponseTimeLabel.Text = "Response Time: ";
            // 
            // ResultsListView
            // 
            this.ResultsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultsListView.AutoSizeTable = false;
            this.ResultsListView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ResultsListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ResultsListView.Depth = 0;
            this.ResultsListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F);
            this.ResultsListView.FullRowSelect = true;
            this.ResultsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.ResultsListView.HideSelection = false;
            this.ResultsListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.ResultsListView.Location = new System.Drawing.Point(1, 58);
            this.ResultsListView.MinimumSize = new System.Drawing.Size(200, 100);
            this.ResultsListView.MouseLocation = new System.Drawing.Point(-1, -1);
            this.ResultsListView.MouseState = MaterialSkin.MouseState.OUT;
            this.ResultsListView.Name = "ResultsListView";
            this.ResultsListView.OwnerDraw = true;
            this.ResultsListView.Size = new System.Drawing.Size(958, 483);
            this.ResultsListView.TabIndex = 0;
            this.ResultsListView.UseCompatibleStateImageBehavior = false;
            this.ResultsListView.View = System.Windows.Forms.View.Details;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(1, -2);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(958, 23);
            this.progressBar.TabIndex = 3;
            this.progressBar.Visible = false;
            // 
            // ResultsListBox
            // 
            this.ResultsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResultsListBox.BackColor = System.Drawing.Color.White;
            this.ResultsListBox.BorderColor = System.Drawing.Color.LightGray;
            this.ResultsListBox.Depth = 0;
            this.ResultsListBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.ResultsListBox.Location = new System.Drawing.Point(1, 58);
            this.ResultsListBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.ResultsListBox.Name = "ResultsListBox";
            this.ResultsListBox.SelectedIndex = -1;
            this.ResultsListBox.SelectedItem = null;
            this.ResultsListBox.ShowBorder = false;
            this.ResultsListBox.Size = new System.Drawing.Size(958, 483);
            this.ResultsListBox.TabIndex = 4;
            this.ResultsListBox.SelectedIndexChanged += new MaterialSkin.Controls.MaterialListBox.SelectedIndexChangedEventHandler(this.ResultsListBox_SelectedIndexChanged);
            // 
            // ResultsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(958, 538);
            this.Controls.Add(this.ResultsListBox);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.ResultsListView);
            this.Controls.Add(this.ResponseTimeLabel);
            this.Name = "ResultsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label ResponseTimeLabel;
        private MaterialSkin.Controls.MaterialListView ResultsListView;
        private System.Windows.Forms.ProgressBar progressBar;
        private MaterialSkin.Controls.MaterialListBox ResultsListBox;
    }
}