using System;
using System.Data;
using System.Windows.Forms;
using AdminDCZOEM.Config;
using AdminDCZOEM.Properties;

namespace AdminDCZOEM
{
    public partial class Obra : Form
    {
        public Obra(Form obraOpt)
        {
            _mp = obraOpt as MenuPrincipal;
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
            string[] idPersona = comboBox1.Text.Split('-');
            string data2Insert = "'" + textBox1.Text + "', " + textBox2.Text + ", '" + idPersona[0] + "'";
            if (!Config.Validation.CheckData(Resources.tablaObra, "nombre_obra", "nombre_obra = '" + textBox1.Text + "'", textBox1.Text))
            {
                if (CrudOp.Create(Resources.tablaObra, Resources.ObraInsertar, data2Insert))
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
                MessageBox.Show("Ya existe una obra '" + textBox1.Text + "',\n verifique sus datos.", Resources.infoWindow, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (CrudOp.Delete(Resources.tablaObra, "id_obra", _id.ToString()))
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
            string[] idPersona = comboBox1.Text.Split('-');
            string data2Update = "nombre_obra = '" + textBox1.Text + "', no_beneficiarios = " + textBox2.Text + ", id_persona = '" + idPersona[0] + "'";
            if (CrudOp.Update(Resources.tablaObra, data2Update, "id_obra", _id.ToString()))
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
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                e.Handled = true;
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
            textBox1.Clear();
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
            dataGridView1.DataSource = CrudOp.ReadData(Resources.VistaObra, Resources.ObraBusqueda);
            DataTable idPersona = CrudOp.ReadData(Resources.VistaPersona);
            foreach (DataRow row in idPersona.Rows)
            {
                if (row.Field<string>(16) != "Administrador")
                {
                    comboBox1.Items.Add(row.Field<string>(0) + " - " + row.Field<string>(1) + " " + row.Field<string>(2) + " " + row.Field<string>(3));
                }
            }
        }
        #endregion

        #region Windows events
        private void Obra_FormClosed(object sender, FormClosedEventArgs e)
        {
            _mp.ObraOpt = true;
        }
        private void Obra_Load(object sender, EventArgs e)
        {
            Load_Data();
        }
        #endregion

    }
}
