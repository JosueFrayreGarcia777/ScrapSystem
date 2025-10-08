namespace ScrapSystemm
{
    partial class SubBOMForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Button btnAgregar;
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
        private void InitializeComponent()
        {
            panelMain = new Panel();
            btnAgregar = new Button();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelMain.Location = new Point(17, 20);
            panelMain.Margin = new Padding(4, 5, 4, 5);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(943, 533);
            panelMain.TabIndex = 0;
            // 
            // btnAgregar
            // 
            btnAgregar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAgregar.Location = new Point(817, 563);
            btnAgregar.Margin = new Padding(4, 5, 4, 5);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(143, 65);
            btnAgregar.TabIndex = 1;
            btnAgregar.Text = "Agregar a bitácora";
            btnAgregar.UseVisualStyleBackColor = true;
            // 
            // SubBOMForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(977, 648);
            Controls.Add(btnAgregar);
            Controls.Add(panelMain);
            Margin = new Padding(4, 5, 4, 5);
            Name = "SubBOMForm";
            Text = "BOM de subensamble";
            ResumeLayout(false);
        }
    }
}
