namespace BackUpWindow
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
      this.btnCopyFrom = new System.Windows.Forms.Button();
      this.listBox1 = new System.Windows.Forms.ListBox();
      this.tbCopyFrom = new System.Windows.Forms.TextBox();
      this.tbCopyTo = new System.Windows.Forms.TextBox();
      this.btnCopyTo = new System.Windows.Forms.Button();
      this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
      this.btnBackUp = new System.Windows.Forms.Button();
      this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.Jpg = new System.Windows.Forms.CheckBox();
      this.Tif = new System.Windows.Forms.CheckBox();
      this.Gif = new System.Windows.Forms.CheckBox();
      this.Png = new System.Windows.Forms.CheckBox();
      this.Bmp = new System.Windows.Forms.CheckBox();
      this.dateTimePickerEndCopyDate = new System.Windows.Forms.DateTimePicker();
      this.checkBoxStartCopyDate = new System.Windows.Forms.CheckBox();
      this.cBBkMethod = new System.Windows.Forms.ComboBox();
      this.SuspendLayout();
      // 
      // btnCopyFrom
      // 
      this.btnCopyFrom.Location = new System.Drawing.Point(21, 19);
      this.btnCopyFrom.Name = "btnCopyFrom";
      this.btnCopyFrom.Size = new System.Drawing.Size(75, 23);
      this.btnCopyFrom.TabIndex = 0;
      this.btnCopyFrom.Text = "CopyFrom";
      this.btnCopyFrom.UseVisualStyleBackColor = true;
      this.btnCopyFrom.Click += new System.EventHandler(this.btnCopyFrom_Click);
      // 
      // listBox1
      // 
      this.listBox1.FormattingEnabled = true;
      this.listBox1.Location = new System.Drawing.Point(22, 273);
      this.listBox1.Name = "listBox1";
      this.listBox1.Size = new System.Drawing.Size(507, 212);
      this.listBox1.TabIndex = 1;
      // 
      // tbCopyFrom
      // 
      this.tbCopyFrom.Location = new System.Drawing.Point(117, 22);
      this.tbCopyFrom.Name = "tbCopyFrom";
      this.tbCopyFrom.ReadOnly = true;
      this.tbCopyFrom.Size = new System.Drawing.Size(401, 20);
      this.tbCopyFrom.TabIndex = 2;
      // 
      // tbCopyTo
      // 
      this.tbCopyTo.Location = new System.Drawing.Point(117, 57);
      this.tbCopyTo.Name = "tbCopyTo";
      this.tbCopyTo.ReadOnly = true;
      this.tbCopyTo.Size = new System.Drawing.Size(401, 20);
      this.tbCopyTo.TabIndex = 4;
      // 
      // btnCopyTo
      // 
      this.btnCopyTo.Location = new System.Drawing.Point(21, 54);
      this.btnCopyTo.Name = "btnCopyTo";
      this.btnCopyTo.Size = new System.Drawing.Size(75, 23);
      this.btnCopyTo.TabIndex = 3;
      this.btnCopyTo.Text = "CopyTo";
      this.btnCopyTo.UseVisualStyleBackColor = true;
      this.btnCopyTo.Click += new System.EventHandler(this.btnCopyTo_Click);
      // 
      // btnBackUp
      // 
      this.btnBackUp.Location = new System.Drawing.Point(22, 199);
      this.btnBackUp.Name = "btnBackUp";
      this.btnBackUp.Size = new System.Drawing.Size(75, 23);
      this.btnBackUp.TabIndex = 5;
      this.btnBackUp.Text = "BackUp";
      this.btnBackUp.UseVisualStyleBackColor = true;
      this.btnBackUp.Click += new System.EventHandler(this.btnBackUp_Click);
      // 
      // backgroundWorker1
      // 
      this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
      this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
      this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
      // 
      // progressBar1
      // 
      this.progressBar1.Location = new System.Drawing.Point(22, 238);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(507, 16);
      this.progressBar1.TabIndex = 6;
      // 
      // Jpg
      // 
      this.Jpg.AutoSize = true;
      this.Jpg.Checked = true;
      this.Jpg.CheckState = System.Windows.Forms.CheckState.Checked;
      this.Jpg.Location = new System.Drawing.Point(51, 140);
      this.Jpg.Name = "Jpg";
      this.Jpg.Size = new System.Drawing.Size(53, 17);
      this.Jpg.TabIndex = 7;
      this.Jpg.Text = "JPEG";
      this.Jpg.UseVisualStyleBackColor = true;
      // 
      // Tif
      // 
      this.Tif.AutoSize = true;
      this.Tif.Location = new System.Drawing.Point(51, 164);
      this.Tif.Name = "Tif";
      this.Tif.Size = new System.Drawing.Size(42, 17);
      this.Tif.TabIndex = 8;
      this.Tif.Text = "TIF";
      this.Tif.UseVisualStyleBackColor = true;
      // 
      // Gif
      // 
      this.Gif.AutoSize = true;
      this.Gif.Location = new System.Drawing.Point(138, 140);
      this.Gif.Name = "Gif";
      this.Gif.Size = new System.Drawing.Size(43, 17);
      this.Gif.TabIndex = 9;
      this.Gif.Text = "GIF\r\n";
      this.Gif.UseVisualStyleBackColor = true;
      // 
      // Png
      // 
      this.Png.AutoSize = true;
      this.Png.Location = new System.Drawing.Point(225, 140);
      this.Png.Name = "Png";
      this.Png.Size = new System.Drawing.Size(49, 17);
      this.Png.TabIndex = 10;
      this.Png.Text = "PNG";
      this.Png.UseVisualStyleBackColor = true;
      // 
      // Bmp
      // 
      this.Bmp.AutoSize = true;
      this.Bmp.Location = new System.Drawing.Point(138, 164);
      this.Bmp.Name = "Bmp";
      this.Bmp.Size = new System.Drawing.Size(49, 17);
      this.Bmp.TabIndex = 11;
      this.Bmp.Text = "BMP";
      this.Bmp.UseVisualStyleBackColor = true;
      // 
      // dateTimePickerEndCopyDate
      // 
      this.dateTimePickerEndCopyDate.Location = new System.Drawing.Point(318, 97);
      this.dateTimePickerEndCopyDate.Name = "dateTimePickerEndCopyDate";
      this.dateTimePickerEndCopyDate.Size = new System.Drawing.Size(200, 20);
      this.dateTimePickerEndCopyDate.TabIndex = 12;
      // 
      // checkBoxStartCopyDate
      // 
      this.checkBoxStartCopyDate.AutoSize = true;
      this.checkBoxStartCopyDate.Location = new System.Drawing.Point(21, 100);
      this.checkBoxStartCopyDate.Name = "checkBoxStartCopyDate";
      this.checkBoxStartCopyDate.Size = new System.Drawing.Size(240, 17);
      this.checkBoxStartCopyDate.TabIndex = 13;
      this.checkBoxStartCopyDate.Text = "Set Start Copy Date (default is last copy date)";
      this.checkBoxStartCopyDate.UseVisualStyleBackColor = true;
      this.checkBoxStartCopyDate.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
      // 
      // cBBkMethod
      // 
      this.cBBkMethod.FormattingEnabled = true;
      this.cBBkMethod.Location = new System.Drawing.Point(318, 140);
      this.cBBkMethod.Name = "cBBkMethod";
      this.cBBkMethod.Size = new System.Drawing.Size(121, 21);
      this.cBBkMethod.TabIndex = 14;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(566, 508);
      this.Controls.Add(this.cBBkMethod);
      this.Controls.Add(this.checkBoxStartCopyDate);
      this.Controls.Add(this.dateTimePickerEndCopyDate);
      this.Controls.Add(this.Bmp);
      this.Controls.Add(this.Png);
      this.Controls.Add(this.Gif);
      this.Controls.Add(this.Tif);
      this.Controls.Add(this.Jpg);
      this.Controls.Add(this.progressBar1);
      this.Controls.Add(this.btnBackUp);
      this.Controls.Add(this.tbCopyTo);
      this.Controls.Add(this.btnCopyTo);
      this.Controls.Add(this.tbCopyFrom);
      this.Controls.Add(this.listBox1);
      this.Controls.Add(this.btnCopyFrom);
      this.Name = "Form1";
      this.Text = "Form1";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnCopyFrom;
    private System.Windows.Forms.ListBox listBox1;
    private System.Windows.Forms.TextBox tbCopyFrom;
    private System.Windows.Forms.TextBox tbCopyTo;
    private System.Windows.Forms.Button btnCopyTo;
    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    private System.Windows.Forms.Button btnBackUp;
    private System.ComponentModel.BackgroundWorker backgroundWorker1;
    private System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.CheckBox Jpg;
    private System.Windows.Forms.CheckBox Tif;
    private System.Windows.Forms.CheckBox Gif;
    private System.Windows.Forms.CheckBox Png;
    private System.Windows.Forms.CheckBox Bmp;
    private System.Windows.Forms.DateTimePicker dateTimePickerEndCopyDate;
    private System.Windows.Forms.CheckBox checkBoxStartCopyDate;
    private System.Windows.Forms.ComboBox cBBkMethod;
  }
}

