
namespace WebScraper_CDisney
{
    partial class Form1
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
            this.UI_Gridview = new System.Windows.Forms.DataGridView();
            this.UI_ListBox = new System.Windows.Forms.ListBox();
            this.UI_URLBox = new System.Windows.Forms.TextBox();
            this.UI_Label_GridView = new System.Windows.Forms.Label();
            this.UI_Label_URLBox = new System.Windows.Forms.Label();
            this.UI_Button_Load = new System.Windows.Forms.Button();
            this.UI_Label_Listbox = new System.Windows.Forms.Label();
            this.UI_TextBox_DownloadLocation = new System.Windows.Forms.TextBox();
            this.UI_Button_ChangeDownload = new System.Windows.Forms.Button();
            this.UI_Label_DownloadLocation = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.UI_Gridview)).BeginInit();
            this.SuspendLayout();
            // 
            // UI_Gridview
            // 
            this.UI_Gridview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_Gridview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UI_Gridview.Location = new System.Drawing.Point(9, 68);
            this.UI_Gridview.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.UI_Gridview.Name = "UI_Gridview";
            this.UI_Gridview.RowHeadersWidth = 51;
            this.UI_Gridview.RowTemplate.Height = 24;
            this.UI_Gridview.Size = new System.Drawing.Size(390, 346);
            this.UI_Gridview.TabIndex = 0;
            this.UI_Gridview.VirtualMode = true;
            // 
            // UI_ListBox
            // 
            this.UI_ListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_ListBox.FormattingEnabled = true;
            this.UI_ListBox.Location = new System.Drawing.Point(404, 68);
            this.UI_ListBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.UI_ListBox.Name = "UI_ListBox";
            this.UI_ListBox.Size = new System.Drawing.Size(340, 303);
            this.UI_ListBox.TabIndex = 1;
            // 
            // UI_URLBox
            // 
            this.UI_URLBox.Location = new System.Drawing.Point(9, 32);
            this.UI_URLBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.UI_URLBox.Name = "UI_URLBox";
            this.UI_URLBox.Size = new System.Drawing.Size(154, 20);
            this.UI_URLBox.TabIndex = 2;
            // 
            // UI_Label_GridView
            // 
            this.UI_Label_GridView.AutoSize = true;
            this.UI_Label_GridView.Location = new System.Drawing.Point(9, 52);
            this.UI_Label_GridView.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.UI_Label_GridView.Name = "UI_Label_GridView";
            this.UI_Label_GridView.Size = new System.Drawing.Size(35, 13);
            this.UI_Label_GridView.TabIndex = 3;
            this.UI_Label_GridView.Text = "label1";
            // 
            // UI_Label_URLBox
            // 
            this.UI_Label_URLBox.AutoSize = true;
            this.UI_Label_URLBox.Location = new System.Drawing.Point(10, 13);
            this.UI_Label_URLBox.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.UI_Label_URLBox.Name = "UI_Label_URLBox";
            this.UI_Label_URLBox.Size = new System.Drawing.Size(35, 13);
            this.UI_Label_URLBox.TabIndex = 4;
            this.UI_Label_URLBox.Text = "label2";
            // 
            // UI_Button_Load
            // 
            this.UI_Button_Load.Location = new System.Drawing.Point(167, 30);
            this.UI_Button_Load.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.UI_Button_Load.Name = "UI_Button_Load";
            this.UI_Button_Load.Size = new System.Drawing.Size(56, 19);
            this.UI_Button_Load.TabIndex = 5;
            this.UI_Button_Load.Text = "button1";
            this.UI_Button_Load.UseVisualStyleBackColor = true;
            // 
            // UI_Label_Listbox
            // 
            this.UI_Label_Listbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_Label_Listbox.AutoSize = true;
            this.UI_Label_Listbox.Location = new System.Drawing.Point(404, 52);
            this.UI_Label_Listbox.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.UI_Label_Listbox.Name = "UI_Label_Listbox";
            this.UI_Label_Listbox.Size = new System.Drawing.Size(35, 13);
            this.UI_Label_Listbox.TabIndex = 8;
            this.UI_Label_Listbox.Text = "label3";
            // 
            // UI_TextBox_DownloadLocation
            // 
            this.UI_TextBox_DownloadLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_TextBox_DownloadLocation.Location = new System.Drawing.Point(404, 396);
            this.UI_TextBox_DownloadLocation.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.UI_TextBox_DownloadLocation.Name = "UI_TextBox_DownloadLocation";
            this.UI_TextBox_DownloadLocation.Size = new System.Drawing.Size(272, 20);
            this.UI_TextBox_DownloadLocation.TabIndex = 9;
            // 
            // UI_Button_ChangeDownload
            // 
            this.UI_Button_ChangeDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_Button_ChangeDownload.Location = new System.Drawing.Point(680, 396);
            this.UI_Button_ChangeDownload.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.UI_Button_ChangeDownload.Name = "UI_Button_ChangeDownload";
            this.UI_Button_ChangeDownload.Size = new System.Drawing.Size(56, 19);
            this.UI_Button_ChangeDownload.TabIndex = 10;
            this.UI_Button_ChangeDownload.Text = "button1";
            this.UI_Button_ChangeDownload.UseVisualStyleBackColor = true;
            // 
            // UI_Label_DownloadLocation
            // 
            this.UI_Label_DownloadLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_Label_DownloadLocation.AutoSize = true;
            this.UI_Label_DownloadLocation.Location = new System.Drawing.Point(404, 379);
            this.UI_Label_DownloadLocation.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.UI_Label_DownloadLocation.Name = "UI_Label_DownloadLocation";
            this.UI_Label_DownloadLocation.Size = new System.Drawing.Size(35, 13);
            this.UI_Label_DownloadLocation.TabIndex = 11;
            this.UI_Label_DownloadLocation.Text = "label4";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 431);
            this.Controls.Add(this.UI_Label_DownloadLocation);
            this.Controls.Add(this.UI_Button_ChangeDownload);
            this.Controls.Add(this.UI_TextBox_DownloadLocation);
            this.Controls.Add(this.UI_Label_Listbox);
            this.Controls.Add(this.UI_Button_Load);
            this.Controls.Add(this.UI_Label_URLBox);
            this.Controls.Add(this.UI_Label_GridView);
            this.Controls.Add(this.UI_URLBox);
            this.Controls.Add(this.UI_ListBox);
            this.Controls.Add(this.UI_Gridview);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(769, 470);
            this.Name = "Form1";
            this.Text = "Web Scraper";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.UI_Gridview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView UI_Gridview;
        private System.Windows.Forms.ListBox UI_ListBox;
        private System.Windows.Forms.TextBox UI_URLBox;
        private System.Windows.Forms.Label UI_Label_GridView;
        private System.Windows.Forms.Label UI_Label_URLBox;
        private System.Windows.Forms.Button UI_Button_Load;
        private System.Windows.Forms.Label UI_Label_Listbox;
        private System.Windows.Forms.TextBox UI_TextBox_DownloadLocation;
        private System.Windows.Forms.Button UI_Button_ChangeDownload;
        private System.Windows.Forms.Label UI_Label_DownloadLocation;
    }
}

