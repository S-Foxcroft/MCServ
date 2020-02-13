namespace uk.co.ytfox.MCWrap
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
            this.components = new System.ComponentModel.Container();
            this.ServerInput = new System.Windows.Forms.TextBox();
            this.ServerOutput = new System.Windows.Forms.TextBox();
            this.wUI = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // ServerInput
            // 
            this.ServerInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ServerInput.BackColor = System.Drawing.SystemColors.MenuBar;
            this.ServerInput.Location = new System.Drawing.Point(0, 703);
            this.ServerInput.Name = "ServerInput";
            this.ServerInput.Size = new System.Drawing.Size(1414, 26);
            this.ServerInput.TabIndex = 1;
            this.ServerInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ServerInput_KeyDown);
            // 
            // ServerOutput
            // 
            this.ServerOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ServerOutput.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ServerOutput.Enabled = false;
            this.ServerOutput.ForeColor = System.Drawing.Color.Black;
            this.ServerOutput.Location = new System.Drawing.Point(0, -1);
            this.ServerOutput.Multiline = true;
            this.ServerOutput.Name = "ServerOutput";
            this.ServerOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ServerOutput.Size = new System.Drawing.Size(1414, 703);
            this.ServerOutput.TabIndex = 2;
            this.ServerOutput.TabStop = false;
            // 
            // wUI
            // 
            this.wUI.Enabled = true;
            this.wUI.Interval = 10;
            this.wUI.Tick += new System.EventHandler(this.Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1412, 729);
            this.Controls.Add(this.ServerOutput);
            this.Controls.Add(this.ServerInput);
            this.Name = "Form1";
            this.Text = "Loading config - MCServ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox ServerInput;
        private System.Windows.Forms.TextBox ServerOutput;
        private System.Windows.Forms.Timer wUI;
    }
}

