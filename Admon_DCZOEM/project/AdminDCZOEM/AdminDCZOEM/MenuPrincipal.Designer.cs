namespace AdminDCZOEM
{
    partial class MenuPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesiónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adminMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.area_sbm = new System.Windows.Forms.ToolStripMenuItem();
            this.areaper_sbm = new System.Windows.Forms.ToolStripMenuItem();
            this.cp_sbm = new System.Windows.Forms.ToolStripMenuItem();
            this.equipo_sbm = new System.Windows.Forms.ToolStripMenuItem();
            this.evi_sbm = new System.Windows.Forms.ToolStripMenuItem();
            this.obra_sbm = new System.Windows.Forms.ToolStripMenuItem();
            this.personas_sbm = new System.Windows.Forms.ToolStripMenuItem();
            this.respon_sbm = new System.Windows.Forms.ToolStripMenuItem();
            this.tperson_sbm = new System.Windows.Forms.ToolStripMenuItem();
            this.tproduct_sbm = new System.Windows.Forms.ToolStripMenuItem();
            this.usuario_sbm = new System.Windows.Forms.ToolStripMenuItem();
            this.vertiente_sbm = new System.Windows.Forms.ToolStripMenuItem();
            this.vertpers_sbm = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arrangeIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.adminMenu,
            this.windowsMenu,
            this.helpMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.MdiWindowListItem = this.windowsMenu;
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(632, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cerrarSesiónToolStripMenuItem,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
            this.fileMenu.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(60, 20);
            this.fileMenu.Text = "&Archivo";
            // 
            // cerrarSesiónToolStripMenuItem
            // 
            this.cerrarSesiónToolStripMenuItem.Name = "cerrarSesiónToolStripMenuItem";
            this.cerrarSesiónToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.cerrarSesiónToolStripMenuItem.Text = "&Cerrar Sesión";
            this.cerrarSesiónToolStripMenuItem.Click += new System.EventHandler(this.cerrarSesiónToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "&Salir";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolsStripMenuItem_Click);
            // 
            // adminMenu
            // 
            this.adminMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.area_sbm,
            this.areaper_sbm,
            this.cp_sbm,
            this.equipo_sbm,
            this.evi_sbm,
            this.obra_sbm,
            this.personas_sbm,
            this.respon_sbm,
            this.tperson_sbm,
            this.tproduct_sbm,
            this.usuario_sbm,
            this.vertiente_sbm,
            this.vertpers_sbm});
            this.adminMenu.Name = "adminMenu";
            this.adminMenu.Size = new System.Drawing.Size(95, 20);
            this.adminMenu.Text = "&Administrador";
            this.adminMenu.Visible = false;
            // 
            // area_sbm
            // 
            this.area_sbm.Name = "area_sbm";
            this.area_sbm.Size = new System.Drawing.Size(166, 22);
            this.area_sbm.Text = "&Área";
            this.area_sbm.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // areaper_sbm
            // 
            this.areaper_sbm.Name = "areaper_sbm";
            this.areaper_sbm.Size = new System.Drawing.Size(166, 22);
            this.areaper_sbm.Text = "&Área Persona";
            this.areaper_sbm.Click += new System.EventHandler(this.areaper_sbm_Click);
            // 
            // cp_sbm
            // 
            this.cp_sbm.Name = "cp_sbm";
            this.cp_sbm.Size = new System.Drawing.Size(166, 22);
            this.cp_sbm.Text = "&Código Postal";
            this.cp_sbm.Click += new System.EventHandler(this.cp_sbm_Click);
            // 
            // equipo_sbm
            // 
            this.equipo_sbm.Name = "equipo_sbm";
            this.equipo_sbm.Size = new System.Drawing.Size(166, 22);
            this.equipo_sbm.Text = "&Equipo";
            this.equipo_sbm.Click += new System.EventHandler(this.equipo_sbm_Click);
            // 
            // evi_sbm
            // 
            this.evi_sbm.Name = "evi_sbm";
            this.evi_sbm.Size = new System.Drawing.Size(166, 22);
            this.evi_sbm.Text = "&Evidencias";
            this.evi_sbm.Click += new System.EventHandler(this.evi_sbm_Click);
            // 
            // obra_sbm
            // 
            this.obra_sbm.Name = "obra_sbm";
            this.obra_sbm.Size = new System.Drawing.Size(166, 22);
            this.obra_sbm.Text = "&Obra";
            this.obra_sbm.Click += new System.EventHandler(this.obra_sbm_Click);
            // 
            // personas_sbm
            // 
            this.personas_sbm.Name = "personas_sbm";
            this.personas_sbm.Size = new System.Drawing.Size(166, 22);
            this.personas_sbm.Text = "&Personas";
            this.personas_sbm.Click += new System.EventHandler(this.personas_sbm_Click);
            // 
            // respon_sbm
            // 
            this.respon_sbm.Name = "respon_sbm";
            this.respon_sbm.Size = new System.Drawing.Size(166, 22);
            this.respon_sbm.Text = "&Responsiva";
            this.respon_sbm.Click += new System.EventHandler(this.respon_sbm_Click);
            // 
            // tperson_sbm
            // 
            this.tperson_sbm.Name = "tperson_sbm";
            this.tperson_sbm.Size = new System.Drawing.Size(166, 22);
            this.tperson_sbm.Text = "&Tipo de Persona";
            this.tperson_sbm.Click += new System.EventHandler(this.tperson_sbm_Click);
            // 
            // tproduct_sbm
            // 
            this.tproduct_sbm.Name = "tproduct_sbm";
            this.tproduct_sbm.Size = new System.Drawing.Size(166, 22);
            this.tproduct_sbm.Text = "&Tipo de Producto";
            this.tproduct_sbm.Click += new System.EventHandler(this.tproduct_sbm_Click);
            // 
            // usuario_sbm
            // 
            this.usuario_sbm.Name = "usuario_sbm";
            this.usuario_sbm.Size = new System.Drawing.Size(166, 22);
            this.usuario_sbm.Text = "&Usuario";
            this.usuario_sbm.Click += new System.EventHandler(this.usuario_sbm_Click);
            // 
            // vertiente_sbm
            // 
            this.vertiente_sbm.Name = "vertiente_sbm";
            this.vertiente_sbm.Size = new System.Drawing.Size(166, 22);
            this.vertiente_sbm.Text = "&Vertiente";
            this.vertiente_sbm.Click += new System.EventHandler(this.vertiente_sbm_Click);
            // 
            // vertpers_sbm
            // 
            this.vertpers_sbm.Name = "vertpers_sbm";
            this.vertpers_sbm.Size = new System.Drawing.Size(166, 22);
            this.vertpers_sbm.Text = "&Vertiente Persona";
            this.vertpers_sbm.Click += new System.EventHandler(this.vertpers_sbm_Click);
            // 
            // windowsMenu
            // 
            this.windowsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cascadeToolStripMenuItem,
            this.tileVerticalToolStripMenuItem,
            this.tileHorizontalToolStripMenuItem,
            this.closeAllToolStripMenuItem,
            this.arrangeIconsToolStripMenuItem});
            this.windowsMenu.Name = "windowsMenu";
            this.windowsMenu.Size = new System.Drawing.Size(66, 20);
            this.windowsMenu.Text = "&Ventanas";
            // 
            // cascadeToolStripMenuItem
            // 
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.cascadeToolStripMenuItem.Text = "&Cascada";
            this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.CascadeToolStripMenuItem_Click);
            // 
            // tileVerticalToolStripMenuItem
            // 
            this.tileVerticalToolStripMenuItem.Name = "tileVerticalToolStripMenuItem";
            this.tileVerticalToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.tileVerticalToolStripMenuItem.Text = "Mosaico &vertical";
            this.tileVerticalToolStripMenuItem.Click += new System.EventHandler(this.TileVerticalToolStripMenuItem_Click);
            // 
            // tileHorizontalToolStripMenuItem
            // 
            this.tileHorizontalToolStripMenuItem.Name = "tileHorizontalToolStripMenuItem";
            this.tileHorizontalToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.tileHorizontalToolStripMenuItem.Text = "Mosaico &horizontal";
            this.tileHorizontalToolStripMenuItem.Click += new System.EventHandler(this.TileHorizontalToolStripMenuItem_Click);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.closeAllToolStripMenuItem.Text = "C&errar todo";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.CloseAllToolStripMenuItem_Click);
            // 
            // arrangeIconsToolStripMenuItem
            // 
            this.arrangeIconsToolStripMenuItem.Name = "arrangeIconsToolStripMenuItem";
            this.arrangeIconsToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.arrangeIconsToolStripMenuItem.Text = "&Organizar iconos";
            this.arrangeIconsToolStripMenuItem.Click += new System.EventHandler(this.ArrangeIconsToolStripMenuItem_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(53, 20);
            this.helpMenu.Text = "Ay&uda";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.aboutToolStripMenuItem.Text = "&Acerca de...";
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(632, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "ToolStrip";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(46, 22);
            this.toolStripLabel1.Text = "usuario";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 431);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(632, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel.Text = "Estado";
            // 
            // MenuPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 453);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MenuPrincipal";
            this.Text = "Administración DCZOEM";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MenuPrincipal_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MenuPrincipal_FormClosed);
            this.Load += new System.EventHandler(this.MenuPrincipal_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        public System.Windows.Forms.MenuStrip menuStrip;
        public System.Windows.Forms.ToolStrip toolStrip;
        public System.Windows.Forms.StatusStrip statusStrip;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        public System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem tileHorizontalToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem fileMenu;
        public System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem adminMenu;
        public System.Windows.Forms.ToolStripMenuItem windowsMenu;
        public System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem tileVerticalToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem arrangeIconsToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem helpMenu;
        public System.Windows.Forms.ToolTip toolTip;
        public System.Windows.Forms.ToolStripLabel toolStripLabel1;
        public System.Windows.Forms.ToolStripMenuItem area_sbm;
        private System.Windows.Forms.ToolStripMenuItem cerrarSesiónToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem areaper_sbm;
        private System.Windows.Forms.ToolStripMenuItem cp_sbm;
        private System.Windows.Forms.ToolStripMenuItem equipo_sbm;
        private System.Windows.Forms.ToolStripMenuItem evi_sbm;
        private System.Windows.Forms.ToolStripMenuItem obra_sbm;
        private System.Windows.Forms.ToolStripMenuItem personas_sbm;
        private System.Windows.Forms.ToolStripMenuItem respon_sbm;
        private System.Windows.Forms.ToolStripMenuItem tperson_sbm;
        private System.Windows.Forms.ToolStripMenuItem tproduct_sbm;
        private System.Windows.Forms.ToolStripMenuItem usuario_sbm;
        private System.Windows.Forms.ToolStripMenuItem vertiente_sbm;
        private System.Windows.Forms.ToolStripMenuItem vertpers_sbm;
    }
}



