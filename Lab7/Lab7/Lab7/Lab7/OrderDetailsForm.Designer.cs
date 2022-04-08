
namespace Lab7
{
    partial class OrderDetailsForm
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
            this.dvgOrderDtails = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dvgOrderDtails)).BeginInit();
            this.SuspendLayout();
            // 
            // dvgOrderDtails
            // 
            this.dvgOrderDtails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgOrderDtails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvgOrderDtails.Location = new System.Drawing.Point(0, 0);
            this.dvgOrderDtails.Name = "dvgOrderDtails";
            this.dvgOrderDtails.RowHeadersWidth = 51;
            this.dvgOrderDtails.RowTemplate.Height = 24;
            this.dvgOrderDtails.Size = new System.Drawing.Size(800, 450);
            this.dvgOrderDtails.TabIndex = 0;
            // 
            // OrderDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dvgOrderDtails);
            this.Name = "OrderDetailsForm";
            this.Text = "OrderDetailsForm";
            ((System.ComponentModel.ISupportInitialize)(this.dvgOrderDtails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dvgOrderDtails;
    }
}