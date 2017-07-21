using System;
using System.Windows.Forms;
using AdminDCZOEM.Config;
using AdminDCZOEM.Properties;

namespace AdminDCZOEM
{
    public partial class CodigoPostal : Form
    {

        public CodigoPostal(Form cpOpt)
        {
            _mp = cpOpt as MenuPrincipal;
            InitializeComponent();
        }
    
        #region Variables
        private MenuPrincipal _mp;
        private bool[] _elements = new bool[6];
        private int _id;
        #endregion

        #region Buttons
        private void button1_Click(object sender, EventArgs e)
        {
            string data2Insert = "'" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox5.Text + "', '" + textBox6.Text + "'";
            if (CrudOp.Create(Resources.tablaCodigoPostal, Resources.CPInsertar, data2Insert))
            {
                MessageBox.Show(Resources.MsgRegistroOk, Resources.infoWindow, MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                CleanFields();
            }
            else
                MessageBox.Show(Resources.MsgRegistroFail, Resources.infoWindow, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (CrudOp.Delete(Resources.tablaCodigoPostal, "id_codigo", _id.ToString()))
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
            string data2Update = "cp = " + textBox1.Text + ", asentamiento = '" + textBox2.Text + "', tipo = '" + textBox3.Text + "', municipio = '" + textBox4.Text + "', ciudad = '" + textBox5.Text + "', estado = '" + textBox6.Text + "'";
            if (CrudOp.Update(Resources.tablaCodigoPostal, data2Update, "id_codigo", _id.ToString()))
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
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                e.Handled = true;
        }
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
        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            if (_id == 0)
            {
                if (textBox3.Text != "")
                    _elements[2] = true;
                else
                    _elements[2] = false;
                Validate();
            }
        }
        private void textBox4_KeyUp(object sender, KeyEventArgs e)
        {
            if (_id == 0)
            {
                if (textBox4.Text != "")
                    _elements[3] = true;
                else
                    _elements[3] = false;
                Validate();
            }
        }
        private void textBox5_KeyUp(object sender, KeyEventArgs e)
        {
            if (_id == 0)
            {
                if (textBox5.Text != "")
                    _elements[4] = true;
                else
                    _elements[4] = false;
                Validate();
            }
        }
        private void textBox6_KeyUp(object sender, KeyEventArgs e)
        {
            if (_id == 0)
            {
                if (textBox6.Text != "")
                    _elements[5] = true;
                else
                    _elements[5] = false;
                Validate();
            }
        }
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                _id = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
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
            if (fields == 6)
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }
        private void CleanFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            for (int x = 0; x < _elements.Length; x++) _elements[x] = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            _id = 0;
            Load_Data();
        }
        private void Load_Data()
        {
            dataGridView1.DataSource = CrudOp.ReadData(Resources.tablaCodigoPostal, Resources.CPBusqueda);
        }
        #endregion

        #region Windows events
        private void CodigoPostal_FormClosed(object sender, FormClosedEventArgs e)
        {
            _mp.CpOpt = true;
        }
        private void CodigoPostal_Load(object sender, EventArgs e)
        {
            Load_Data();
        }
        #endregion

    }
}
