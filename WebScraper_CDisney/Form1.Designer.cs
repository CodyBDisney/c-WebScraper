
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
            this.UI_Button_Save = new System.Windows.Forms.Button();
            this.UI_Button_Cancel = new System.Windows.Forms.Button();
            this.UI_Label_Listbox = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.UI_Gridview)).BeginInit();
            this.SuspendLayout();
            // 
            // UI_Gridview
            // 
            this.UI_Gridview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UI_Gridview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UI_Gridview.Location = new System.Drawing.Point(12, 116);
            this.UI_Gridview.Name = "UI_Gridview";
            this.UI_Gridview.RowHeadersWidth = 51;
            this.UI_Gridview.RowTemplate.Height = 24;
            this.UI_Gridview.Size = new System.Drawing.Size(600, 304);
            this.UI_Gridview.TabIndex = 0;
            this.UI_Gridview.VirtualMode = true;
            // 
            // UI_ListBox
            // 
            this.UI_ListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_ListBox.FormattingEnabled = true;
            this.UI_ListBox.ItemHeight = 16;
            this.UI_ListBox.Location = new System.Drawing.Point(626, 117);
            this.UI_ListBox.Name = "UI_ListBox";
            this.UI_ListBox.Size = new System.Drawing.Size(297, 244);
            this.UI_ListBox.TabIndex = 1;
            // 
            // UI_URLBox
            // 
            this.UI_URLBox.Location = new System.Drawing.Point(12, 39);
            this.UI_URLBox.Name = "UI_URLBox";
            this.UI_URLBox.Size = new System.Drawing.Size(204, 22);
            this.UI_URLBox.TabIndex = 2;
            // 
            // UI_Label_GridView
            // 
            this.UI_Label_GridView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UI_Label_GridView.AutoSize = true;
            this.UI_Label_GridView.Location = new System.Drawing.Point(12, 92);
            this.UI_Label_GridView.Name = "UI_Label_GridView";
            this.UI_Label_GridView.Size = new System.Drawing.Size(46, 17);
            this.UI_Label_GridView.TabIndex = 3;
            this.UI_Label_GridView.Text = "label1";
            // 
            // UI_Label_URLBox
            // 
            this.UI_Label_URLBox.AutoSize = true;
            this.UI_Label_URLBox.Location = new System.Drawing.Point(14, 16);
            this.UI_Label_URLBox.Name = "UI_Label_URLBox";
            this.UI_Label_URLBox.Size = new System.Drawing.Size(46, 17);
            this.UI_Label_URLBox.TabIndex = 4;
            this.UI_Label_URLBox.Text = "label2";
            // 
            // UI_Button_Load
            // 
            this.UI_Button_Load.Location = new System.Drawing.Point(223, 37);
            this.UI_Button_Load.Name = "UI_Button_Load";
            this.UI_Button_Load.Size = new System.Drawing.Size(75, 23);
            this.UI_Button_Load.TabIndex = 5;
            this.UI_Button_Load.Text = "button1";
            this.UI_Button_Load.UseVisualStyleBackColor = true;
            // 
            // UI_Button_Save
            // 
            this.UI_Button_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_Button_Save.Location = new System.Drawing.Point(626, 368);
            this.UI_Button_Save.Name = "UI_Button_Save";
            this.UI_Button_Save.Size = new System.Drawing.Size(142, 52);
            this.UI_Button_Save.TabIndex = 6;
            this.UI_Button_Save.Text = "button2";
            this.UI_Button_Save.UseVisualStyleBackColor = true;
            // 
            // UI_Button_Cancel
            // 
            this.UI_Button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_Button_Cancel.Location = new System.Drawing.Point(781, 368);
            this.UI_Button_Cancel.Name = "UI_Button_Cancel";
            this.UI_Button_Cancel.Size = new System.Drawing.Size(142, 52);
            this.UI_Button_Cancel.TabIndex = 7;
            this.UI_Button_Cancel.Text = "button3";
            this.UI_Button_Cancel.UseVisualStyleBackColor = true;
            // 
            // UI_Label_Listbox
            // 
            this.UI_Label_Listbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UI_Label_Listbox.AutoSize = true;
            this.UI_Label_Listbox.Location = new System.Drawing.Point(626, 91);
            this.UI_Label_Listbox.Name = "UI_Label_Listbox";
            this.UI_Label_Listbox.Size = new System.Drawing.Size(46, 17);
            this.UI_Label_Listbox.TabIndex = 8;
            this.UI_Label_Listbox.Text = "label3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 432);
            this.Controls.Add(this.UI_Label_Listbox);
            this.Controls.Add(this.UI_Button_Cancel);
            this.Controls.Add(this.UI_Button_Save);
            this.Controls.Add(this.UI_Button_Load);
            this.Controls.Add(this.UI_Label_URLBox);
            this.Controls.Add(this.UI_Label_GridView);
            this.Controls.Add(this.UI_URLBox);
            this.Controls.Add(this.UI_ListBox);
            this.Controls.Add(this.UI_Gridview);
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
        private System.Windows.Forms.Button UI_Button_Save;
        private System.Windows.Forms.Button UI_Button_Cancel;
        private System.Windows.Forms.Label UI_Label_Listbox;
    }
}

