namespace ASCOM.INDIGO {
  partial class TestForm {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.buttonChoose = new System.Windows.Forms.Button();
      this.buttonConnect = new System.Windows.Forms.Button();
      this.labelDriverId = new System.Windows.Forms.Label();
      this.textDec = new System.Windows.Forms.TextBox();
      this.textRA = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.labelDescription = new System.Windows.Forms.Label();
      this.textAz = new System.Windows.Forms.TextBox();
      this.textAlt = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.buttonSync = new System.Windows.Forms.Button();
      this.buttonSlew = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // buttonChoose
      // 
      this.buttonChoose.Location = new System.Drawing.Point(659, 19);
      this.buttonChoose.Margin = new System.Windows.Forms.Padding(6);
      this.buttonChoose.Name = "buttonChoose";
      this.buttonChoose.Size = new System.Drawing.Size(144, 44);
      this.buttonChoose.TabIndex = 0;
      this.buttonChoose.Text = "Choose";
      this.buttonChoose.UseVisualStyleBackColor = true;
      this.buttonChoose.Click += new System.EventHandler(this.buttonChoose_Click);
      // 
      // buttonConnect
      // 
      this.buttonConnect.Location = new System.Drawing.Point(659, 75);
      this.buttonConnect.Margin = new System.Windows.Forms.Padding(6);
      this.buttonConnect.Name = "buttonConnect";
      this.buttonConnect.Size = new System.Drawing.Size(144, 44);
      this.buttonConnect.TabIndex = 1;
      this.buttonConnect.Text = "Connect";
      this.buttonConnect.UseVisualStyleBackColor = true;
      this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
      // 
      // labelDriverId
      // 
      this.labelDriverId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.labelDriverId.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::ASCOM.INDIGO.Properties.Settings.Default, "DriverId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
      this.labelDriverId.Location = new System.Drawing.Point(15, 19);
      this.labelDriverId.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.labelDriverId.Name = "labelDriverId";
      this.labelDriverId.Size = new System.Drawing.Size(632, 44);
      this.labelDriverId.TabIndex = 2;
      this.labelDriverId.Text = global::ASCOM.INDIGO.Properties.Settings.Default.DriverId;
      this.labelDriverId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // textDec
      // 
      this.textDec.Location = new System.Drawing.Point(333, 138);
      this.textDec.Margin = new System.Windows.Forms.Padding(6);
      this.textDec.Name = "textDec";
      this.textDec.Size = new System.Drawing.Size(150, 31);
      this.textDec.TabIndex = 19;
      // 
      // textRA
      // 
      this.textRA.Location = new System.Drawing.Point(171, 138);
      this.textRA.Margin = new System.Windows.Forms.Padding(6);
      this.textRA.Name = "textRA";
      this.textRA.Size = new System.Drawing.Size(150, 31);
      this.textRA.TabIndex = 18;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(15, 141);
      this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(91, 25);
      this.label3.TabIndex = 17;
      this.label3.Text = "RA/Dec:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(10, 85);
      this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(123, 25);
      this.label1.TabIndex = 16;
      this.label1.Text = "description:";
      // 
      // labelDescription
      // 
      this.labelDescription.AutoSize = true;
      this.labelDescription.Location = new System.Drawing.Point(166, 85);
      this.labelDescription.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.labelDescription.Name = "labelDescription";
      this.labelDescription.Size = new System.Drawing.Size(116, 25);
      this.labelDescription.TabIndex = 15;
      this.labelDescription.Text = "desctiption";
      // 
      // textAz
      // 
      this.textAz.Location = new System.Drawing.Point(333, 194);
      this.textAz.Margin = new System.Windows.Forms.Padding(6);
      this.textAz.Name = "textAz";
      this.textAz.Size = new System.Drawing.Size(150, 31);
      this.textAz.TabIndex = 22;
      // 
      // textAlt
      // 
      this.textAlt.Location = new System.Drawing.Point(171, 194);
      this.textAlt.Margin = new System.Windows.Forms.Padding(6);
      this.textAlt.Name = "textAlt";
      this.textAlt.Size = new System.Drawing.Size(150, 31);
      this.textAlt.TabIndex = 21;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(15, 197);
      this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(74, 25);
      this.label2.TabIndex = 20;
      this.label2.Text = "Alt/Az:";
      // 
      // buttonSync
      // 
      this.buttonSync.Location = new System.Drawing.Point(659, 187);
      this.buttonSync.Margin = new System.Windows.Forms.Padding(6);
      this.buttonSync.Name = "buttonSync";
      this.buttonSync.Size = new System.Drawing.Size(144, 44);
      this.buttonSync.TabIndex = 23;
      this.buttonSync.Text = "Sync";
      this.buttonSync.UseVisualStyleBackColor = true;
      // 
      // buttonSlew
      // 
      this.buttonSlew.Location = new System.Drawing.Point(659, 131);
      this.buttonSlew.Margin = new System.Windows.Forms.Padding(6);
      this.buttonSlew.Name = "buttonSlew";
      this.buttonSlew.Size = new System.Drawing.Size(144, 44);
      this.buttonSlew.TabIndex = 24;
      this.buttonSlew.Text = "Slew";
      this.buttonSlew.UseVisualStyleBackColor = true;
      // 
      // TestForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(818, 504);
      this.Controls.Add(this.buttonSlew);
      this.Controls.Add(this.buttonSync);
      this.Controls.Add(this.textAz);
      this.Controls.Add(this.textAlt);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.textDec);
      this.Controls.Add(this.textRA);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.labelDescription);
      this.Controls.Add(this.labelDriverId);
      this.Controls.Add(this.buttonConnect);
      this.Controls.Add(this.buttonChoose);
      this.Margin = new System.Windows.Forms.Padding(6);
      this.Name = "TestForm";
      this.Text = "INDIGO Telescope Tester";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TestForm_FormClosing);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonChoose;
    private System.Windows.Forms.Button buttonConnect;
    private System.Windows.Forms.Label labelDriverId;
    private System.Windows.Forms.TextBox textDec;
    private System.Windows.Forms.TextBox textRA;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label labelDescription;
    private System.Windows.Forms.TextBox textAz;
    private System.Windows.Forms.TextBox textAlt;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button buttonSync;
    private System.Windows.Forms.Button buttonSlew;
  }
}

