namespace AmazonApp
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
            this.btnClick = new System.Windows.Forms.Button();
            this.txtListID = new System.Windows.Forms.RichTextBox();
            this.txtListMessage = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtListSock = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSockLive = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnClick
            // 
            this.btnClick.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClick.Location = new System.Drawing.Point(12, 230);
            this.btnClick.Name = "btnClick";
            this.btnClick.Size = new System.Drawing.Size(170, 35);
            this.btnClick.TabIndex = 1;
            this.btnClick.Text = "Run";
            this.btnClick.UseVisualStyleBackColor = true;
            this.btnClick.Click += new System.EventHandler(this.btnClick_Click);
            // 
            // txtListID
            // 
            this.txtListID.Location = new System.Drawing.Point(188, 68);
            this.txtListID.Name = "txtListID";
            this.txtListID.Size = new System.Drawing.Size(170, 156);
            this.txtListID.TabIndex = 2;
            this.txtListID.Text = "";
            // 
            // txtListMessage
            // 
            this.txtListMessage.Location = new System.Drawing.Point(364, 68);
            this.txtListMessage.Name = "txtListMessage";
            this.txtListMessage.Size = new System.Drawing.Size(641, 156);
            this.txtListMessage.TabIndex = 2;
            this.txtListMessage.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(183, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "ID seller";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(359, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "List messages";
            // 
            // txtListSock
            // 
            this.txtListSock.Location = new System.Drawing.Point(12, 68);
            this.txtListSock.Name = "txtListSock";
            this.txtListSock.Size = new System.Drawing.Size(170, 156);
            this.txtListSock.TabIndex = 2;
            this.txtListSock.Text = "";
            this.txtListSock.TextChanged += new System.EventHandler(this.txtListSock_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "List sock";
            // 
            // lblSockLive
            // 
            this.lblSockLive.AutoSize = true;
            this.lblSockLive.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSockLive.Location = new System.Drawing.Point(110, 27);
            this.lblSockLive.Name = "lblSockLive";
            this.lblSockLive.Size = new System.Drawing.Size(0, 25);
            this.lblSockLive.TabIndex = 4;
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(12, 332);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(993, 122);
            this.txtResult.TabIndex = 5;
            this.txtResult.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 466);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.lblSockLive);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtListMessage);
            this.Controls.Add(this.txtListSock);
            this.Controls.Add(this.txtListID);
            this.Controls.Add(this.btnClick);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "App Amazon";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnClick;
        private System.Windows.Forms.RichTextBox txtListID;
        private System.Windows.Forms.RichTextBox txtListMessage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtListSock;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblSockLive;
        private System.Windows.Forms.RichTextBox txtResult;
    }
}

