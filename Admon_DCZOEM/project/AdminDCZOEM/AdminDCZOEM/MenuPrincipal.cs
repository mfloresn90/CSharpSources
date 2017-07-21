using System;
using System.Windows.Forms;
using AdminDCZOEM.Properties;

namespace AdminDCZOEM
{
    public partial class MenuPrincipal : Form
    {

        public MenuPrincipal()
        {
            InitializeComponent();
        }

        #region Variables
        public static string UserName;
        private Area _aa;
        private AreaPersona _ap;
        private CodigoPostal _cp;
        private Equipo _eo;
        private Evidencias _es;
        private Obra _oa;
        private Persona _ps;
        private Responsiva _ra;
        private TipoPersona _tpa;
        private TipoProducto _tpo;
        private Usuario _uo;
        private Vertiente _ve;
        private VertientePersona _vpa;
        private Login ln;
        #endregion

        #region Archivo
        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hide();
            ln = new Login();
            ln.Show();
        }
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Administrador
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _aa = new Area(this);
            _aa.MdiParent = this;
            area_sbm.Enabled = false;
            _aa.Show();
        }
        private void areaper_sbm_Click(object sender, EventArgs e)
        {
            _ap = new AreaPersona(this);
            _ap.MdiParent = this;
            areaper_sbm.Enabled = false;
            _ap.Show();
        }
        private void cp_sbm_Click(object sender, EventArgs e)
        {
            _cp = new CodigoPostal(this);
            _cp.MdiParent = this;
            cp_sbm.Enabled = false;
            _cp.Show();
        }
        private void equipo_sbm_Click(object sender, EventArgs e)
        {
            _eo = new Equipo(this);
            _eo.MdiParent = this;
            equipo_sbm.Enabled = false;
            _eo.Show();
        }
        private void evi_sbm_Click(object sender, EventArgs e)
        {
            _es = new Evidencias(this);
            _es.MdiParent = this;
            evi_sbm.Enabled = false;
            _es.Show();
        }
        private void obra_sbm_Click(object sender, EventArgs e)
        {
            _oa = new Obra(this);
            _oa.MdiParent = this;
            obra_sbm.Enabled = false;
            _oa.Show();
        }
        private void personas_sbm_Click(object sender, EventArgs e)
        {
            _ps = new Persona(this);
            _ps.MdiParent = this;
            personas_sbm.Enabled = false;
            _ps.Show();
        }
        private void respon_sbm_Click(object sender, EventArgs e)
        {
            _ra = new Responsiva(this);
            _ra.MdiParent = this;
            respon_sbm.Enabled = false;
            _ra.Show();
        }
        private void tperson_sbm_Click(object sender, EventArgs e)
        {
            _tpa = new TipoPersona(this);
            _tpa.MdiParent = this;
            tperson_sbm.Enabled = false;
            _tpa.Show();
        }
        private void tproduct_sbm_Click(object sender, EventArgs e)
        {
            _tpo = new TipoProducto(this);
            _tpo.MdiParent = this;
            tproduct_sbm.Enabled = false;
            _tpo.Show();
        }
        private void usuario_sbm_Click(object sender, EventArgs e)
        {
            _uo = new Usuario(this);
            _uo.MdiParent = this;
            usuario_sbm.Enabled = false;
            _uo.Show();
        }
        private void vertiente_sbm_Click(object sender, EventArgs e)
        {
            _ve = new Vertiente(this);
            _ve.MdiParent = this;
            vertiente_sbm.Enabled = false;
            _ve.Show();
        }
        private void vertpers_sbm_Click(object sender, EventArgs e)
        {
            _vpa = new VertientePersona(this);
            _vpa.MdiParent = this;
            vertpers_sbm.Enabled = false;
            _vpa.Show();
        }
        #endregion

        #region Windows
        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }
        #endregion

        #region Windows events
        private void MenuPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void MenuPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Finalizará la sesión.\n ¿Está segur@ de salir?", Resources.infoWindow,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                e.Cancel = true;
        }
        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            toolStripLabel1.Text = UserName;
        }
        #endregion

        #region Communicate bridge other form
        public bool AreaOpt
        {
            get { return area_sbm.Enabled; }
            set { area_sbm.Enabled = value; }
        }
        public bool AreaPersOpt
        {
            get { return areaper_sbm.Enabled; }
            set { areaper_sbm.Enabled = value; }
        }
        public bool CpOpt
        {
            get { return cp_sbm.Enabled; }
            set { cp_sbm.Enabled = value; }
        }
        public bool EquipoOpt
        {
            get { return equipo_sbm.Enabled; }
            set { equipo_sbm.Enabled = value; }
        }
        public bool EvidenciasOpt
        {
            get { return evi_sbm.Enabled; }
            set { evi_sbm.Enabled = value; }
        }
        public bool ObraOpt
        {
            get { return obra_sbm.Enabled; }
            set { obra_sbm.Enabled = value; }
        }
        public bool PersonaOpt
        {
            get { return personas_sbm.Enabled; }
            set { personas_sbm.Enabled = value; }
        }
        public bool ResponsivaOpt
        {
            get { return respon_sbm.Enabled; }
            set { respon_sbm.Enabled = value; }
        }
        public bool TipoPersonaOpt
        {
            get { return tperson_sbm.Enabled; }
            set { tperson_sbm.Enabled = value; }
        }
        public bool TipoProductoOpt
        {
            get { return tproduct_sbm.Enabled; }
            set { tproduct_sbm.Enabled = value; }
        }
        public bool UsuarioOpt
        {
            get { return usuario_sbm.Enabled; }
            set { usuario_sbm.Enabled = value; }
        }
        public bool VertienteOpt
        {
            get { return vertiente_sbm.Enabled; }
            set { vertiente_sbm.Enabled = value; }
        }
        public bool VertientePersonaOpt
        {
            get { return vertpers_sbm.Enabled; }
            set { vertpers_sbm.Enabled = value; }
        }
        #endregion

    }
}
