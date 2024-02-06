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
			tableLayoutPanel2 = new TableLayoutPanel();
			labPrinSer = new Label();
			labPrinSerDrop = new Label();
			textPrinterServer = new TextBox();
			textPrinServerDrop = new TextBox();
			tableLayoutPanel3 = new TableLayoutPanel();
			labPortName = new Label();
			labPortAddress = new Label();
			labPrinterName = new Label();
			labComment = new Label();
			labLocation = new Label();
			labGroups = new Label();
			textPortname = new TextBox();
			textPortAddress = new TextBox();
			textPrintername = new TextBox();
			textComment = new TextBox();
			textLocation = new TextBox();
			textGroups = new TextBox();
			labDriverDrop = new Label();
			comboDrivers = new ComboBox();
			tableLayoutPanel4 = new TableLayoutPanel();
			InstallPrinter = new Button();
			tableLayoutPanel1.SuspendLayout();
			tableLayoutPanel2.SuspendLayout();
			tableLayoutPanel3.SuspendLayout();
			tableLayoutPanel4.SuspendLayout();
			SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.ColumnCount = 3;
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4.80427027F));
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 95.19573F));
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 27F));
			tableLayoutPanel1.Controls.Add(label1, 1, 1);
			tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 1, 2);
			tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 1, 3);
			tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 1, 4);
			tableLayoutPanel1.Dock = DockStyle.Fill;
			tableLayoutPanel1.Location = new Point(0, 0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 6;
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 52.68817F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 47.31183F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 213F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 41F));
			tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 23F));
			tableLayoutPanel1.Size = new Size(583, 406);
			tableLayoutPanel1.TabIndex = 0;
			// 
			// label1
			// 
			label1.Anchor = AnchorStyles.None;
			label1.AutoSize = true;
			label1.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
			label1.Location = new Point(210, 27);
			label1.Name = "label1";
			label1.Size = new Size(161, 19);
			label1.TabIndex = 0;
			label1.Text = "Printer Queue Installation";
			// 
			// tableLayoutPanel2
			// 
			tableLayoutPanel2.ColumnCount = 2;
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel2.Controls.Add(labPrinSer, 0, 0);
			tableLayoutPanel2.Controls.Add(labPrinSerDrop, 0, 1);
			tableLayoutPanel2.Controls.Add(textPrinterServer, 1, 0);
			tableLayoutPanel2.Controls.Add(textPrinServerDrop, 1, 1);
			tableLayoutPanel2.Location = new Point(29, 51);
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.RowCount = 2;
			tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			tableLayoutPanel2.Size = new Size(523, 73);
			tableLayoutPanel2.TabIndex = 1;
			// 
			// labPrinSer
			// 
			labPrinSer.Anchor = AnchorStyles.Left;
			labPrinSer.AutoSize = true;
			labPrinSer.Location = new Point(3, 10);
			labPrinSer.Name = "labPrinSer";
			labPrinSer.Size = new Size(77, 15);
			labPrinSer.TabIndex = 0;
			labPrinSer.Text = "Printer Server";
			// 
			// labPrinSerDrop
			// 
			labPrinSerDrop.Anchor = AnchorStyles.Left;
			labPrinSerDrop.AutoSize = true;
			labPrinSerDrop.Location = new Point(3, 47);
			labPrinSerDrop.Name = "labPrinSerDrop";
			labPrinSerDrop.Size = new Size(88, 15);
			labPrinSerDrop.TabIndex = 1;
			labPrinSerDrop.Text = "Printer Server-1";
			// 
			// textPrinterServer
			// 
			textPrinterServer.Dock = DockStyle.Fill;
			textPrinterServer.Location = new Point(97, 3);
			textPrinterServer.Name = "textPrinterServer";
			textPrinterServer.Size = new Size(423, 23);
			textPrinterServer.TabIndex = 2;
			// 
			// textPrinServerDrop
			// 
			textPrinServerDrop.Dock = DockStyle.Fill;
			textPrinServerDrop.Location = new Point(97, 39);
			textPrinServerDrop.Name = "textPrinServerDrop";
			textPrinServerDrop.Size = new Size(423, 23);
			textPrinServerDrop.TabIndex = 3;
			// 
			// tableLayoutPanel3
			// 
			tableLayoutPanel3.ColumnCount = 2;
			tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
			tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel3.Controls.Add(labPortName, 0, 0);
			tableLayoutPanel3.Controls.Add(labPortAddress, 0, 1);
			tableLayoutPanel3.Controls.Add(labPrinterName, 0, 2);
			tableLayoutPanel3.Controls.Add(labComment, 0, 3);
			tableLayoutPanel3.Controls.Add(labLocation, 0, 4);
			tableLayoutPanel3.Controls.Add(labGroups, 0, 5);
			tableLayoutPanel3.Controls.Add(textPortname, 1, 0);
			tableLayoutPanel3.Controls.Add(textPortAddress, 1, 1);
			tableLayoutPanel3.Controls.Add(textPrintername, 1, 2);
			tableLayoutPanel3.Controls.Add(textComment, 1, 3);
			tableLayoutPanel3.Controls.Add(textLocation, 1, 4);
			tableLayoutPanel3.Controls.Add(textGroups, 1, 5);
			tableLayoutPanel3.Controls.Add(labDriverDrop, 0, 6);
			tableLayoutPanel3.Controls.Add(comboDrivers, 1, 6);
			tableLayoutPanel3.Location = new Point(29, 131);
			tableLayoutPanel3.Name = "tableLayoutPanel3";
			tableLayoutPanel3.RowCount = 7;
			tableLayoutPanel3.RowStyles.Add(new RowStyle());
			tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
			tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
			tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 29F));
			tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
			tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
			tableLayoutPanel3.Size = new Size(523, 205);
			tableLayoutPanel3.TabIndex = 2;
			// 
			// labPortName
			// 
			labPortName.Anchor = AnchorStyles.Left;
			labPortName.AutoSize = true;
			labPortName.Location = new Point(3, 7);
			labPortName.Name = "labPortName";
			labPortName.Size = new Size(59, 15);
			labPortName.TabIndex = 0;
			labPortName.Text = "Portname";
			// 
			// labPortAddress
			// 
			labPortAddress.Anchor = AnchorStyles.Left;
			labPortAddress.AutoSize = true;
			labPortAddress.Location = new Point(3, 35);
			labPortAddress.Name = "labPortAddress";
			labPortAddress.Size = new Size(84, 15);
			labPortAddress.TabIndex = 1;
			labPortAddress.Text = "PortAddress IP";
			// 
			// labPrinterName
			// 
			labPrinterName.Anchor = AnchorStyles.Left;
			labPrinterName.AutoSize = true;
			labPrinterName.Location = new Point(3, 63);
			labPrinterName.Name = "labPrinterName";
			labPrinterName.Size = new Size(72, 15);
			labPrinterName.TabIndex = 2;
			labPrinterName.Text = "Printername";
			// 
			// labComment
			// 
			labComment.Anchor = AnchorStyles.Left;
			labComment.AutoSize = true;
			labComment.Location = new Point(3, 91);
			labComment.Name = "labComment";
			labComment.Size = new Size(61, 15);
			labComment.TabIndex = 3;
			labComment.Text = "Comment";
			// 
			// labLocation
			// 
			labLocation.Anchor = AnchorStyles.Left;
			labLocation.AutoSize = true;
			labLocation.Location = new Point(3, 121);
			labLocation.Name = "labLocation";
			labLocation.Size = new Size(53, 15);
			labLocation.TabIndex = 4;
			labLocation.Text = "Location";
			// 
			// labGroups
			// 
			labGroups.Anchor = AnchorStyles.Left;
			labGroups.AutoSize = true;
			labGroups.Location = new Point(3, 150);
			labGroups.Name = "labGroups";
			labGroups.Size = new Size(45, 15);
			labGroups.TabIndex = 5;
			labGroups.Text = "Groups";
			// 
			// textPortname
			// 
			textPortname.Dock = DockStyle.Fill;
			textPortname.Location = new Point(93, 3);
			textPortname.Name = "textPortname";
			textPortname.Size = new Size(427, 23);
			textPortname.TabIndex = 8;
			// 
			// textPortAddress
			// 
			textPortAddress.Dock = DockStyle.Fill;
			textPortAddress.Location = new Point(93, 32);
			textPortAddress.Name = "textPortAddress";
			textPortAddress.Size = new Size(427, 23);
			textPortAddress.TabIndex = 9;
			// 
			// textPrintername
			// 
			textPrintername.Dock = DockStyle.Fill;
			textPrintername.Location = new Point(93, 60);
			textPrintername.Name = "textPrintername";
			textPrintername.Size = new Size(427, 23);
			textPrintername.TabIndex = 10;
			// 
			// textComment
			// 
			textComment.Dock = DockStyle.Fill;
			textComment.Location = new Point(93, 87);
			textComment.Name = "textComment";
			textComment.Size = new Size(427, 23);
			textComment.TabIndex = 11;
			// 
			// textLocation
			// 
			textLocation.Dock = DockStyle.Fill;
			textLocation.Location = new Point(93, 117);
			textLocation.Name = "textLocation";
			textLocation.Size = new Size(427, 23);
			textLocation.TabIndex = 12;
			// 
			// textGroups
			// 
			textGroups.Dock = DockStyle.Fill;
			textGroups.Location = new Point(93, 146);
			textGroups.Name = "textGroups";
			textGroups.Size = new Size(427, 23);
			textGroups.TabIndex = 13;
			// 
			// labDriverDrop
			// 
			labDriverDrop.Anchor = AnchorStyles.Left;
			labDriverDrop.AutoSize = true;
			labDriverDrop.Location = new Point(3, 181);
			labDriverDrop.Name = "labDriverDrop";
			labDriverDrop.Size = new Size(43, 15);
			labDriverDrop.TabIndex = 6;
			labDriverDrop.Text = "Drivers";
			// 
			// comboDrivers
			// 
			comboDrivers.Dock = DockStyle.Fill;
			comboDrivers.FormattingEnabled = true;
			comboDrivers.Location = new Point(93, 176);
			comboDrivers.Name = "comboDrivers";
			comboDrivers.Size = new Size(427, 23);
			comboDrivers.TabIndex = 14;
			// 
			// tableLayoutPanel4
			// 
			tableLayoutPanel4.ColumnCount = 3;
			tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 244F));
			tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 127F));
			tableLayoutPanel4.Controls.Add(InstallPrinter, 2, 0);
			tableLayoutPanel4.Dock = DockStyle.Fill;
			tableLayoutPanel4.Location = new Point(29, 344);
			tableLayoutPanel4.Name = "tableLayoutPanel4";
			tableLayoutPanel4.RowCount = 1;
			tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			tableLayoutPanel4.Size = new Size(523, 35);
			tableLayoutPanel4.TabIndex = 3;
			// 
			// InstallPrinter
			// 
			InstallPrinter.Dock = DockStyle.Fill;
			InstallPrinter.Location = new Point(399, 3);
			InstallPrinter.Name = "InstallPrinter";
			InstallPrinter.Size = new Size(121, 29);
			InstallPrinter.TabIndex = 0;
			InstallPrinter.Text = "Install Printer";
			InstallPrinter.UseVisualStyleBackColor = true;
			InstallPrinter.Click += InstallPrinter_Click;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(583, 406);
			Controls.Add(tableLayoutPanel1);
			Name = "MainForm";
			Text = "Printer Queue";
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			tableLayoutPanel2.ResumeLayout(false);
			tableLayoutPanel2.PerformLayout();
			tableLayoutPanel3.ResumeLayout(false);
			tableLayoutPanel3.PerformLayout();
			tableLayoutPanel4.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private TableLayoutPanel tableLayoutPanel1;
		private Label label1;
		private TableLayoutPanel tableLayoutPanel2;
		private Label labPrinSer;
		private Label labPrinSerDrop;
		private TableLayoutPanel tableLayoutPanel3;
		private Label labPortName;
		private Label labPortAddress;
		private Label labPrinterName;
		private Label labComment;
		private Label labLocation;
		private Label labGroups;
		private Label labDriverDrop;
		private TextBox textPortname;
		private TextBox textPortAddress;
		private TextBox textPrintername;
		private TextBox textComment;
		private TextBox textLocation;
		private TextBox textGroups;
		private TextBox textPrinterServer;
		private TextBox textPrinServerDrop;
		private TableLayoutPanel tableLayoutPanel4;
		private Button InstallPrinter;
		private ComboBox comboDrivers;
	}
}
