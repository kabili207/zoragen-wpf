namespace OracleHack
{
	partial class DecoderForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DecoderForm));
			this.btnRings = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.secretControl1 = new OracleHack.SecretControl();
			this.SuspendLayout();
			// 
			// btnRings
			// 
			this.btnRings.Location = new System.Drawing.Point(298, 12);
			this.btnRings.Name = "btnRings";
			this.btnRings.Size = new System.Drawing.Size(75, 23);
			this.btnRings.TabIndex = 162;
			this.btnRings.Text = "Rings";
			this.btnRings.UseVisualStyleBackColor = true;
			this.btnRings.Click += new System.EventHandler(this.btnRings_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(315, 69);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 163;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// secretControl1
			// 
			this.secretControl1.Location = new System.Drawing.Point(13, 12);
			this.secretControl1.Name = "secretControl1";
			this.secretControl1.Size = new System.Drawing.Size(273, 188);
			this.secretControl1.TabIndex = 164;
			// 
			// DecoderForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(412, 212);
			this.Controls.Add(this.secretControl1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.btnRings);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "DecoderForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Zelda Oracle Secret Decoder";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnRings;
		private System.Windows.Forms.Button button1;
		private SecretControl secretControl1;
	}
}

