using System;
using System.Data;
using System.Security.Cryptography;
using System.Windows.Forms;
using AdminDCZOEM.Config;
using AdminDCZOEM.Properties;

namespace AdminDCZOEM
{
    public partial class Usuario : Form
    {
        public Usuario(Form usuarioOpt)
        {
            _mp = usuarioOpt as MenuPrincipal;
            InitializeComponent();
        }
    
        #region Variables
        private MenuPrincipal _mp;
        private bool[] _elements = new bool[3];
        private int _id;
        #endregion

        #region Buttons
        private void button1_Click(object sender, EventArgs e)
        {
            string hash;
            using (MD5 md5Hash = MD5.Create())
            {
                hash = Validation.GetMd5Hash(md5Hash, textBox2.Text);
            }
            string[] idPersona = comboBox1.Text.Split('-');
            string data2Insert = "'" + textBox1.Text + "', '" + hash + "', '" + idPersona[0] + "'";
            if (!Validation.CheckData(Resources.tablaUsuario, "usuario", "usuario = '" + textBox1.Text + "'", textBox1.Text))
            {
                if (CrudOp.Create(Resources.tablaUsuario, Resources.UsuarioInsertar, data2Insert))
                {
                    MessageBox.Show(Resources.MsgRegistroOk, Resources.infoWindow, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    CleanFields();
                }
                else
                    MessageBox.Show(Resources.MsgRegistroFail, Resources.infoWindow, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Ya existe un usuario '" + textBox1.Text + "',\n verifique sus datos.", Resources.infoWindow, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (CrudOp.Delete(Resources.tablaUsuario, "id_usuario", _id.ToString()))
            {
                MessageBox.Show(Resources.MsgEliminacionOk, Resources.infoWindow, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                CleanFields();
            }
            else
                MessageBox.Show(Resources.MsgRelEliFail, Resources.infoWindow, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string hash;
            using (MD5 md5Hash = MD5.Create())
            {
                hash = Validation.GetMd5Hash(md5Hash, textBox2.Text);
            }
            string[] idPersona = comboBox1.Text.Split('-');
            string data2Update = "usuario = '" + textBox1.Text + "', contrasena = '" + hash + "', id_persona = '" + idPersona[0] + "'";
            if (CrudOp.Update(Resources.tablaUsuario, data2Update, "id_usuario", _id.ToString()))
            {
                MessageBox.Show(Resources.MsgActualizacionOk, Resources.infoWindow, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                CleanFields();
            }
            else
                MessageBox.Show(Resources.MsgActualizacionFail, Resources.infoWindow, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
        }
        private void button4_Click(object sender, EventArgs e)
        {
            CleanFields();
        }
        #endregion

        #region Methods
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (_id == 0)
            {
                if (textBox1.Text != "")
                    _elements[0] = true;
                else
                    _elements[0] = false;
                Validate();
            }
        }
        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (_id == 0)
            {
                if (textBox2.Text != "")
                    _elements[1] = true;
                else
                    _elements[1] = false;
                Validate();
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_id == 0)
            {
                _elements[2] = true;
                Validate();
            }
        }
        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                _id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox1.Enabled = false;
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value + " - " + dataGridView1.Rows[e.RowIndex].Cells[4].Value + " " + dataGridView1.Rows[e.RowIndex].Cells[5].Value + " " + dataGridView1.Rows[e.RowIndex].Cells[6].Value;
                button1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.MsgValidateRegisterSelect, Resources.infoWindow,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private new void Validate()
        {
            int fields = 0;
            for (int x = 0; x < _elements.Length; x++)
                if (_elements[x])
                    fields++;
            if (fields == 3)
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }
        private void CleanFields()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            textBox1.Clear();
            textBox1.Enabled = true;
            textBox2.Clear();
            comboBox1.Items.Clear();
            comboBox1.Text = "Selecciona una persona:";
            for (int x = 0; x < _elements.Length; x++) _elements[x] = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            _id = 0;
            Load_Data();
        }
        private void Load_Data()
        {
            dataGridView1.ColumnCount = 8;
            dataGridView1.Columns[0].Name = "IDENTIFICADOR";
            dataGridView1.Columns[1].Name = "USUARIO";
            dataGridView1.Columns[2].Name = "CONTRASEÑA";
            dataGridView1.Columns[3].Name = "ID PERSONA";
            dataGridView1.Columns[4].Name = "NOMBRE";
            dataGridView1.Columns[5].Name = "APELLIDO PATERNO";
            dataGridView1.Columns[6].Name = "APELLIDO MATERNO";
            dataGridView1.Columns[7].Name = "TIPO";
            DataTable tipoPersona = CrudOp.ReadData(Resources.VistaLogin, Resources.UsuarioBusqueda);
            foreach (DataRow row in tipoPersona.Rows)
                if (row.Field<string>(7) != "Administrador")
                    dataGridView1.Rows.Add(row.Field<int>(0), row.Field<string>(1), row.Field<string>(2), row.Field<string>(3), row.Field<string>(4), row.Field<string>(5), row.Field<string>(6), row.Field<string>(7));
            DataTable idPersona = CrudOp.ReadData(Resources.VistaPersona);
            foreach (DataRow row in idPersona.Rows)
                if (row.Field<string>(16) != "Administrador")
                    comboBox1.Items.Add(row.Field<string>(0) + " - " + row.Field<string>(1) + " " + row.Field<string>(2) + " " + row.Field<string>(3));
        }
        #endregion

        #region Windows events
        private void Usuario_FormClosed(object sender, FormClosedEventArgs e)
        {
            _mp.UsuarioOpt = true;
        }
        private void Usuario_Load(object sender, EventArgs e)
        {
            Load_Data();
        }
        #endregion

    }
}
