using System;
using System.Data;
using System.Windows.Forms;
using AdminDCZOEM.Config;
using AdminDCZOEM.Properties;

namespace AdminDCZOEM
{
    public partial class Equipo : Form
    {

        public Equipo(Form equipoOpt)
        {
            _mp = equipoOpt as MenuPrincipal;
            InitializeComponent();
        }

        #region Variables
        private MenuPrincipal _mp;
        private bool[] _elements = new bool[5];
        private string _id;
        #endregion

        #region Buttons
        private void button1_Click(object sender, EventArgs e)
        {
            string[] idTipoProducto = comboBox1.Text.Split('-');
            string data2Insert = "'" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + idTipoProducto[0] + "'";
            if (!Validation.CheckData(Resources.tablaEquipo, "id_equipo", "id_equipo = '" + textBox1.Text + "'", textBox1.Text))
            {
                if (CrudOp.Create(Resources.tablaEquipo, Resources.EquipoInsertar, data2Insert))
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
                MessageBox.Show("Ya existe un equipo '" + textBox1.Text + "',\n verifique sus datos.", Resources.infoWindow, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (CrudOp.Delete(Resources.tablaEquipo, "id_equipo", "'" + _id + "'"))
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
            string[] idTipo = comboBox1.Text.Split('-');
            string data2Update = "id_equipo = '" + textBox1.Text + "', marca = '" + textBox2.Text + "', serie = '" +
                                  textBox3.Text + "', descripcion = '" + textBox4.Text + "', id_tipo_producto = " + idTipo[0];
            if (CrudOp.Update(Resources.tablaEquipo, data2Update, "id_equipo", "'" + _id + "'"))
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
        private void textBox4_KeyUp(object sender, KeyEventArgs e)
        {
            if (_id == null)
            {
                if (textBox1.Text != "")
                    _elements[0] = true;
                else
                    _elements[0] = false;
                Validate();
            }
        }
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (_id == null)
            {
                if (textBox2.Text != "")
                    _elements[1] = true;
                else
                    _elements[1] = false;
                Validate();
            }
        }
        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (_id == null)
            {
                if (textBox3.Text != "")
                    _elements[2] = true;
                else
                    _elements[2] = false;
                Validate();
            }
        }
        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            if (_id == null)
            {
                if (textBox4.Text != "")
                    _elements[3] = true;
                else
                    _elements[3] = false;
                Validate();
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_id == null)
            {
                _elements[4] = true;
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
                _id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value + " - " + dataGridView1.Rows[e.RowIndex].Cells[5].Value;
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
            if (fields == 5)
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }
        private void CleanFields()
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox1.Clear();
            comboBox1.Items.Clear();
            comboBox1.Text = "Selecciona un tipo de producto:";
            for (int x = 0; x < _elements.Length; x++) _elements[x] = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            _id = null;
            Load_Data();
        }
        private void Load_Data()
        {
            dataGridView1.DataSource = CrudOp.ReadData(Resources.VistaEquipo, Resources.EquipoBusqueda);
            DataTable idType = CrudOp.ReadData(Resources.tablaTipoProducto);
            foreach (DataRow row in idType.Rows)
            {
                comboBox1.Items.Add(row.Field<int>(0) + " - " + row.Field<string>(1));
            }
        }
        #endregion

        #region Windows events
        private void Equipo_FormClosed(object sender, FormClosedEventArgs e)
        {
            _mp.EquipoOpt = true;
        }
        private void Equipo_Load(object sender, EventArgs e)
        {
            Load_Data();
        }
        #endregion

    }
}
