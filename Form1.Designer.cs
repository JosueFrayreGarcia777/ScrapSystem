namespace ScrapSystemm
{
    partial class Form1
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

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            tabMain = new TabControl();
            tabRechazos = new TabPage();
            panel2 = new Panel();
            btnBOM = new Button();
            btnBoleta = new Button();
            btnVerBoleta = new Button();
            btnBuscar = new Button();
            txtBuscar = new TextBox();
            label6 = new Label();
            txtDefecto = new TextBox();
            txtOperacion = new TextBox();
            txtStatus = new TextBox();
            txtNumber = new TextBox();
            txtLinea = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            labelTurno = new Label();
            txtTurno = new TextBox();
            tabBOMTrw = new TabPage();
            panel3 = new Panel();
            btnBuscar2 = new Button();
            txtBuscar2 = new TextBox();
            label7 = new Label();
            label8 = new Label();
            txtBuscarComponente = new TextBox();
            btnBuscarComponente = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tabMain.SuspendLayout();
            tabRechazos.SuspendLayout();
            tabBOMTrw.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(pictureBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(1657, 133);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(654, 0);
            pictureBox1.Margin = new Padding(4, 5, 4, 5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(291, 133);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // tabMain
            // 
            tabMain.Anchor = AnchorStyles.None;
            tabMain.Controls.Add(tabRechazos);
            tabMain.Controls.Add(tabBOMTrw);
            tabMain.Location = new Point(30, 150);
            tabMain.Margin = new Padding(4, 5, 4, 5);
            tabMain.Name = "tabMain";
            tabMain.SelectedIndex = 0;
            tabMain.Size = new Size(1600, 850);
            tabMain.TabIndex = 100;
            // 
            // tabRechazos
            // 
            tabRechazos.Controls.Add(panel2);
            tabRechazos.Controls.Add(btnBOM);
            tabRechazos.Controls.Add(btnBoleta);
            tabRechazos.Controls.Add(btnVerBoleta);
            tabRechazos.Controls.Add(btnBuscar);
            tabRechazos.Controls.Add(txtBuscar);
            tabRechazos.Controls.Add(label6);
            tabRechazos.Controls.Add(txtDefecto);
            tabRechazos.Controls.Add(txtOperacion);
            tabRechazos.Controls.Add(txtStatus);
            tabRechazos.Controls.Add(txtNumber);
            tabRechazos.Controls.Add(txtLinea);
            tabRechazos.Controls.Add(label5);
            tabRechazos.Controls.Add(label4);
            tabRechazos.Controls.Add(label3);
            tabRechazos.Controls.Add(label2);
            tabRechazos.Controls.Add(label1);
            tabRechazos.Controls.Add(labelTurno);
            tabRechazos.Controls.Add(txtTurno);
            tabRechazos.Location = new Point(4, 34);
            tabRechazos.Margin = new Padding(4, 5, 4, 5);
            tabRechazos.Name = "tabRechazos";
            tabRechazos.Padding = new Padding(11, 13, 11, 13);
            tabRechazos.Size = new Size(1592, 812);
            tabRechazos.TabIndex = 0;
            tabRechazos.Text = "Rechazos";
            tabRechazos.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.Location = new Point(17, 400);
            panel2.Margin = new Padding(4, 5, 4, 5);
            panel2.Name = "panel2";
            panel2.Size = new Size(1377, 385);
            panel2.TabIndex = 32;
            // 
            // btnBOM
            // 
            btnBOM.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBOM.Location = new Point(1426, 292);
            btnBOM.Margin = new Padding(4, 5, 4, 5);
            btnBOM.Name = "btnBOM";
            btnBOM.Size = new Size(151, 60);
            btnBOM.TabIndex = 31;
            btnBOM.Text = "Generar BOM";
            btnBOM.UseVisualStyleBackColor = true;
            btnBOM.Click += btnBOM_Click;
            // 
            // btnBoleta
            // 
            btnBoleta.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnBoleta.Location = new Point(1426, 608);
            btnBoleta.Margin = new Padding(4, 5, 4, 5);
            btnBoleta.Name = "btnBoleta";
            btnBoleta.Size = new Size(151, 88);
            btnBoleta.TabIndex = 30;
            btnBoleta.Text = "Agregar a bitacora";
            btnBoleta.UseVisualStyleBackColor = true;
            // 
            // btnVerBoleta
            // 
            btnVerBoleta.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnVerBoleta.Location = new Point(1430, 725);
            btnVerBoleta.Margin = new Padding(4, 5, 4, 5);
            btnVerBoleta.Name = "btnVerBoleta";
            btnVerBoleta.Size = new Size(143, 60);
            btnVerBoleta.TabIndex = 35;
            btnVerBoleta.Text = "Ver Bitacora";
            btnVerBoleta.UseVisualStyleBackColor = true;
            // 
            // btnBuscar
            // 
            btnBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBuscar.Location = new Point(1380, 28);
            btnBuscar.Margin = new Padding(4, 5, 4, 5);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(143, 38);
            btnBuscar.TabIndex = 29;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = true;
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBuscar.Location = new Point(186, 27);
            txtBuscar.Margin = new Padding(4, 5, 4, 5);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(1141, 31);
            txtBuscar.TabIndex = 28;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.Location = new Point(17, 28);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(171, 28);
            label6.TabIndex = 27;
            label6.Text = "Escanee la pieza:";
            // 
            // txtDefecto
            // 
            txtDefecto.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtDefecto.Location = new Point(261, 347);
            txtDefecto.Margin = new Padding(4, 5, 4, 5);
            txtDefecto.Name = "txtDefecto";
            txtDefecto.Size = new Size(254, 31);
            txtDefecto.TabIndex = 26;
            // 
            // txtOperacion
            // 
            txtOperacion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtOperacion.Location = new Point(143, 273);
            txtOperacion.Margin = new Padding(4, 5, 4, 5);
            txtOperacion.Name = "txtOperacion";
            txtOperacion.Size = new Size(371, 31);
            txtOperacion.TabIndex = 25;
            // 
            // txtStatus
            // 
            txtStatus.Location = new Point(109, 212);
            txtStatus.Margin = new Padding(4, 5, 4, 5);
            txtStatus.Name = "txtStatus";
            txtStatus.Size = new Size(94, 31);
            txtStatus.TabIndex = 24;
            // 
            // txtNumber
            // 
            txtNumber.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtNumber.Location = new Point(166, 148);
            txtNumber.Margin = new Padding(4, 5, 4, 5);
            txtNumber.Name = "txtNumber";
            txtNumber.Size = new Size(348, 31);
            txtNumber.TabIndex = 23;
            
            // 
            // txtLinea
            // 
            txtLinea.Location = new Point(101, 88);
            txtLinea.Margin = new Padding(4, 5, 4, 5);
            txtLinea.Name = "txtLinea";
            txtLinea.Size = new Size(217, 31);
            txtLinea.TabIndex = 22;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(17, 345);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(253, 28);
            label5.TabIndex = 21;
            label5.Text = "Descripcion del defecto : ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(17, 273);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(126, 28);
            label4.TabIndex = 20;
            label4.Text = "Operacion : ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(17, 212);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(88, 28);
            label3.TabIndex = 19;
            label3.Text = "Status : ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(16, 150);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(151, 28);
            label2.TabIndex = 18;
            label2.Text = "TRWNumber : ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(17, 90);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(79, 28);
            label1.TabIndex = 17;
            label1.Text = "Linea : ";
            // 
            // labelTurno
            // 
            labelTurno.AutoSize = true;
            labelTurno.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTurno.Location = new Point(655, 91);
            labelTurno.Margin = new Padding(4, 0, 4, 0);
            labelTurno.Name = "labelTurno";
            labelTurno.Size = new Size(72, 28);
            labelTurno.TabIndex = 33;
            labelTurno.Text = "Turno:";
            // 
            // txtTurno
            // 
            txtTurno.Location = new Point(775, 91);
            txtTurno.Margin = new Padding(4, 5, 4, 5);
            txtTurno.Name = "txtTurno";
            txtTurno.Size = new Size(98, 31);
            txtTurno.TabIndex = 34;
            
            // 
            // tabBOMTrw
            // 
            tabBOMTrw.Controls.Add(panel3);
            tabBOMTrw.Controls.Add(btnBuscar2);
            tabBOMTrw.Controls.Add(txtBuscar2);
            tabBOMTrw.Controls.Add(label7);
            tabBOMTrw.Controls.Add(label8);
            tabBOMTrw.Controls.Add(txtBuscarComponente);
            tabBOMTrw.Controls.Add(btnBuscarComponente);
            tabBOMTrw.Location = new Point(4, 34);
            tabBOMTrw.Margin = new Padding(4, 5, 4, 5);
            tabBOMTrw.Name = "tabBOMTrw";
            tabBOMTrw.Padding = new Padding(11, 13, 11, 13);
            tabBOMTrw.Size = new Size(1592, 812);
            tabBOMTrw.TabIndex = 1;
            tabBOMTrw.Text = "RechazoComponente";
            tabBOMTrw.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel3.Location = new Point(29, 125);
            panel3.Margin = new Padding(4, 5, 4, 5);
            panel3.Name = "panel3";
            panel3.Size = new Size(1507, 650);
            panel3.TabIndex = 39;
            // 
            // btnBuscar2
            // 
            btnBuscar2.Location = new Point(357, 65);
            btnBuscar2.Margin = new Padding(4, 5, 4, 5);
            btnBuscar2.Name = "btnBuscar2";
            btnBuscar2.Size = new Size(107, 42);
            btnBuscar2.TabIndex = 38;
            btnBuscar2.Text = "Buscar";
            btnBuscar2.UseVisualStyleBackColor = true;
            // 
            // txtBuscar2
            // 
            txtBuscar2.Location = new Point(29, 67);
            txtBuscar2.Margin = new Padding(4, 5, 4, 5);
            txtBuscar2.Name = "txtBuscar2";
            txtBuscar2.Size = new Size(313, 31);
            txtBuscar2.TabIndex = 37;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(29, 33);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(222, 28);
            label7.TabIndex = 36;
            label7.Text = "Buscar BOM por TRW:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(500, 33);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(350, 28);
            label8.TabIndex = 40;
            label8.Text = "Buscar por número de componente:";
            // 
            // txtBuscarComponente
            // 
            txtBuscarComponente.Location = new Point(500, 67);
            txtBuscarComponente.Margin = new Padding(4, 5, 4, 5);
            txtBuscarComponente.Name = "txtBuscarComponente";
            txtBuscarComponente.Size = new Size(255, 31);
            txtBuscarComponente.TabIndex = 41;
            // 
            // btnBuscarComponente
            // 
            btnBuscarComponente.Location = new Point(771, 65);
            btnBuscarComponente.Margin = new Padding(4, 5, 4, 5);
            btnBuscarComponente.Name = "btnBuscarComponente";
            btnBuscarComponente.Size = new Size(107, 42);
            btnBuscarComponente.TabIndex = 42;
            btnBuscarComponente.Text = "Buscar";
            btnBuscarComponente.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(41, 41, 41);
            ClientSize = new Size(1657, 1050);
            Controls.Add(tabMain);
            Controls.Add(panel1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tabMain.ResumeLayout(false);
            tabRechazos.ResumeLayout(false);
            tabRechazos.PerformLayout();
            tabBOMTrw.ResumeLayout(false);
            tabBOMTrw.PerformLayout();
            ResumeLayout(false);
            // 
            // Elimina el autocompletado nativo, ya que ahora se usa ListBox personalizado
            // txtLinea.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            // txtLinea.AutoCompleteSource = AutoCompleteSource.CustomSource;
            // txtNumber.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            // txtNumber.AutoCompleteSource = AutoCompleteSource.CustomSource;
            // txtDefecto.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            // txtDefecto.AutoCompleteSource = AutoCompleteSource.CustomSource;
            // txtLinea.TextChanged += txtLinea_AutoComplete_TextChanged;
            // txtNumber.TextChanged += txtNumber_AutoComplete_TextChanged;
            // txtDefecto.TextChanged += txtDefecto_AutoComplete_TextChanged;
        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBox1;
        private TabControl tabMain;
        private TabPage tabRechazos;
        private TabPage tabBOMTrw;
        private Button btnBOM;
        private Button btnBoleta;
        private Button btnVerBoleta;
        private Button btnBuscar;
        private TextBox txtBuscar;
        private Label label6;
        private TextBox txtDefecto;
        private TextBox txtOperacion;
        private TextBox txtStatus;
        private TextBox txtNumber;
        private TextBox txtLinea;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Panel panel2;
        private Label labelTurno;
        private TextBox txtTurno;
        // nuevos
        private Label label7;
        private TextBox txtBuscar2;
        private Button btnBuscar2;
        private Panel panel3;
        private Label label8;
        private TextBox txtBuscarComponente;
        private Button btnBuscarComponente;
    }
}
