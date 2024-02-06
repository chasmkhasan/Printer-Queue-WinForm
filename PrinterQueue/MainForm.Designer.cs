namespace PrinterQueue
{
	partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			tableLayoutPanel1 = new TableLayoutPanel();
			label1 = new Label();
			tableLayoutPanel4 = new TableLayoutPanel();
			InstallPrinter = new Button();
			tableLayoutPanel2 = new TableLayoutPanel();
			textGroups = new TextBox();
			textLocation = new TextBox();
			textComment = new TextBox();
			label2 = new Label();
			label3 = new Label();
			label4 = new Label();
			label5 = new Label();
			label6 = new Label();
			label7 = new Label();
			label8 = new Label();
			lblPortName = new Label();
			lblPrinterQueue = new Label();
			textPrintername = new TextBox();
			comboDrivers = new ComboBox();
			tableLayoutPanel1.SuspendLayout();
			tableLayoutPanel4.SuspendLayout();
			tableLayoutPanel2.SuspendLayout();
			SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 3;
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4.80427027F));
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 95.19573F));
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 27F));
			tableLayoutPanel1.Controls.Add(label1, 1, 1);
			tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 1, 4);
			tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 3);
			tableLayoutPanel1.Dock = DockStyle.Fill;
			tableLayoutPanel1.Location = new Point(0, 0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 6;
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 52.68817F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 47.31183F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 18F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 280F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 41F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 23F));
			tableLayoutPanel1.Size = new Size(589, 423);
			tableLayoutPanel1.TabIndex = 0;
			// 
			// label1
			// 
			label1.Anchor = AnchorStyles.None;
			label1.AutoSize = true;
			label1.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
			label1.Location = new Point(214, 36);
			label1.Name = "label1";
			label1.Size = new Size(161, 19);
			label1.TabIndex = 0;
			label1.Text = "Printer Queue Installation";
			// 
			// tableLayoutPanel4
			// 
			tableLayoutPanel4.ColumnCount = 3;
			tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 244F));
			tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 127F));
			tableLayoutPanel4.Controls.Add(InstallPrinter, 2, 0);
			tableLayoutPanel4.Dock = DockStyle.Fill;
			tableLayoutPanel4.Location = new Point(30, 361);
			tableLayoutPanel4.Name = "tableLayoutPanel4";
			tableLayoutPanel4.RowCount = 1;
			tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			tableLayoutPanel4.Size = new Size(529, 35);
			tableLayoutPanel4.TabIndex = 3;
			// 
			// InstallPrinter
			// 
			InstallPrinter.Dock = DockStyle.Fill;
			InstallPrinter.Location = new Point(405, 3);
			InstallPrinter.Name = "InstallPrinter";
			InstallPrinter.Size = new Size(121, 29);
			InstallPrinter.TabIndex = 0;
			InstallPrinter.Text = "Install Printer";
			InstallPrinter.UseVisualStyleBackColor = true;
			InstallPrinter.Click += InstallPrinter_Click;
			// 
			// tableLayoutPanel2
			// 
			tableLayoutPanel2.ColumnCount = 2;
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 21.1026611F));
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 78.89734F));
			tableLayoutPanel2.Controls.Add(textGroups, 1, 5);
			tableLayoutPanel2.Controls.Add(textLocation, 1, 4);
			tableLayoutPanel2.Controls.Add(textComment, 1, 3);
			tableLayoutPanel2.Controls.Add(label2, 0, 0);
			tableLayoutPanel2.Controls.Add(label3, 0, 1);
			tableLayoutPanel2.Controls.Add(label4, 0, 2);
			tableLayoutPanel2.Controls.Add(label5, 0, 3);
			tableLayoutPanel2.Controls.Add(label6, 0, 4);
			tableLayoutPanel2.Controls.Add(label7, 0, 5);
			tableLayoutPanel2.Controls.Add(label8, 0, 6);
			tableLayoutPanel2.Controls.Add(lblPortName, 1, 0);
			tableLayoutPanel2.Controls.Add(lblPrinterQueue, 1, 1);
			tableLayoutPanel2.Controls.Add(textPrintername, 1, 2);
			tableLayoutPanel2.Controls.Add(comboDrivers, 1, 6);
			tableLayoutPanel2.Location = new Point(30, 81);
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.RowCount = 7;
			tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
			tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
			tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
			tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
			tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
			tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
			tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 14.2857141F));
			tableLayoutPanel2.Size = new Size(526, 222);
			tableLayoutPanel2.TabIndex = 4;
			// 
			// textGroups
			// 
			textGroups.Dock = DockStyle.Fill;
			textGroups.Location = new Point(114, 158);
			textGroups.Name = "textGroups";
			textGroups.Size = new Size(409, 23);
			textGroups.TabIndex = 12;
			// 
			// textLocation
			// 
			textLocation.Dock = DockStyle.Fill;
			textLocation.Location = new Point(114, 127);
			textLocation.Name = "textLocation";
			textLocation.Size = new Size(409, 23);
			textLocation.TabIndex = 11;
			// 
			// textComment
			// 
			textComment.Dock = DockStyle.Fill;
			textComment.Location = new Point(114, 96);
			textComment.Name = "textComment";
			textComment.Size = new Size(409, 23);
			textComment.TabIndex = 10;
			// 
			// label2
			// 
			label2.Anchor = AnchorStyles.Left;
			label2.AutoSize = true;
			label2.Location = new Point(3, 8);
			label2.Name = "label2";
			label2.Size = new Size(64, 15);
			label2.TabIndex = 0;
			label2.Text = "Port Name";
			// 
			// label3
			// 
			label3.Anchor = AnchorStyles.Left;
			label3.AutoSize = true;
			label3.Location = new Point(3, 39);
			label3.Name = "label3";
			label3.Size = new Size(80, 15);
			label3.TabIndex = 1;
			label3.Text = "Printer Queue";
			// 
			// label4
			// 
			label4.Anchor = AnchorStyles.Left;
			label4.AutoSize = true;
			label4.Location = new Point(3, 70);
			label4.Name = "label4";
			label4.Size = new Size(77, 15);
			label4.TabIndex = 2;
			label4.Text = "Printer Name";
			// 
			// label5
			// 
			label5.Anchor = AnchorStyles.Left;
			label5.AutoSize = true;
			label5.Location = new Point(3, 101);
			label5.Name = "label5";
			label5.Size = new Size(61, 15);
			label5.TabIndex = 3;
			label5.Text = "Comment";
			// 
			// label6
			// 
			label6.Anchor = AnchorStyles.Left;
			label6.AutoSize = true;
			label6.Location = new Point(3, 132);
			label6.Name = "label6";
			label6.Size = new Size(53, 15);
			label6.TabIndex = 4;
			label6.Text = "Location";
			// 
			// label7
			// 
			label7.Anchor = AnchorStyles.Left;
			label7.AutoSize = true;
			label7.Location = new Point(3, 163);
			label7.Name = "label7";
			label7.Size = new Size(45, 15);
			label7.TabIndex = 5;
			label7.Text = "Groups";
			// 
			// label8
			// 
			label8.Anchor = AnchorStyles.Left;
			label8.AutoSize = true;
			label8.Location = new Point(3, 196);
			label8.Name = "label8";
			label8.Size = new Size(73, 15);
			label8.TabIndex = 6;
			label8.Text = "Driver Name";
			// 
			// lblPortName
			// 
			lblPortName.AutoSize = true;
			lblPortName.BackColor = SystemColors.ControlLightLight;
			lblPortName.BorderStyle = BorderStyle.Fixed3D;
			lblPortName.Dock = DockStyle.Fill;
			lblPortName.Location = new Point(114, 0);
			lblPortName.Name = "lblPortName";
			lblPortName.Size = new Size(409, 31);
			lblPortName.TabIndex = 7;
			lblPortName.Text = "label9";
			// 
			// lblPrinterQueue
			// 
			lblPrinterQueue.AutoSize = true;
			lblPrinterQueue.BackColor = SystemColors.ControlLightLight;
			lblPrinterQueue.BorderStyle = BorderStyle.Fixed3D;
			lblPrinterQueue.Dock = DockStyle.Fill;
			lblPrinterQueue.Location = new Point(114, 31);
			lblPrinterQueue.Name = "lblPrinterQueue";
			lblPrinterQueue.Size = new Size(409, 31);
			lblPrinterQueue.TabIndex = 8;
			lblPrinterQueue.Text = "label9";
			// 
			// textPrintername
			// 
			textPrintername.Dock = DockStyle.Fill;
			textPrintername.Location = new Point(114, 65);
			textPrintername.Name = "textPrintername";
			textPrintername.Size = new Size(409, 23);
			textPrintername.TabIndex = 9;
			// 
			// comboDrivers
			// 
			comboDrivers.Dock = DockStyle.Fill;
			comboDrivers.FormattingEnabled = true;
			comboDrivers.Location = new Point(114, 189);
			comboDrivers.Name = "comboDrivers";
			comboDrivers.Size = new Size(409, 23);
			comboDrivers.TabIndex = 13;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(589, 423);
			Controls.Add(tableLayoutPanel1);
			Name = "MainForm";
			Text = "Printer Queue";
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			tableLayoutPanel4.ResumeLayout(false);
			tableLayoutPanel2.ResumeLayout(false);
			tableLayoutPanel2.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private TableLayoutPanel tableLayoutPanel1;
		private Label label1;
		private TableLayoutPanel tableLayoutPanel4;
		private Button InstallPrinter;
		private TableLayoutPanel tableLayoutPanel2;
		private Label label2;
		private Label label3;
		private Label label4;
		private Label label5;
		private Label label6;
		private Label label7;
		private Label label8;
		private Label lblPortName;
		private Label lblPrinterQueue;
		private TextBox textGroups;
		private TextBox textLocation;
		private TextBox textComment;
		private TextBox textPrintername;
		private ComboBox comboDrivers;
	}
}
