using System;
using System.Data;
using System.Windows.Forms;
using AdminDCZOEM.Config;
using AdminDCZOEM.Properties;

namespace AdminDCZOEM
{
    public partial class Evidencias : Form
    {
        public Evidencias(Form evidenciasOpt)
        {
            _mp = evidenciasOpt as MenuPrincipal;
            InitializeComponent();
        }
    
        #region Variables
        private MenuPrincipal _mp;
        private bool[] _elements = new bool[5];
        private int _id;
        #endregion

        #region Buttons
        private void button1_Click(object sender, EventArgs e)
        {
            string[] idObra = comboBox1.Text.Split('-');
            string data2Insert = textBox1.Text + ", " + textBox2.Text + ", " + textBox3.Text + ", '" + textBox4.Text + "', " + idObra[0];
            if (CrudOp.Create(Resources.tablaEvidencias, Resources.EvidenciasInsertar, data2Insert))
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
            if (CrudOp.Delete(Resources.tablaEvidencias, "id_evidencias", _id.ToString()))
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
            string[] idObra = comboBox1.Text.Split('-');
            string data2Update = "num_fotos = " + textBox1.Text + ", num_videos = " + textBox2.Text + ", num_cphs = " + textBox3.Text + ", actualizo = '" + textBox4.Text + "', fecha = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', id_obra = " + idObra[0];
            if (CrudOp.Update(Resources.tablaEvidencias, data2Update, "id_obra", _id.ToString()))
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
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                e.Handled = true;
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
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                e.Handled = true;
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
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_id == 0)
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
                _id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value + " - " + dataGridView1.Rows[e.RowIndex].Cells[7].Value;
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
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.Items.Clear();
            comboBox1.Text = "Selecciona una obra:";
            for (int x = 0; x < _elements.Length; x++) _elements[x] = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            _id = 0;
            Load_Data();
        }
        private void Load_Data()
        {
            dataGridView1.DataSource = CrudOp.ReadData(Resources.VistaEvidencias, Resources.EvidenciasBusqueda);
            DataTable idObra = CrudOp.ReadData(Resources.VistaObra);
            foreach (DataRow row in idObra.Rows)
            {
                comboBox1.Items.Add(row.Field<int>(0) + " - " + row.Field<string>(1));
            }
        }
        #endregion

        #region Windows events
        private void Evidencias_FormClosed(object sender, FormClosedEventArgs e)
        {
            _mp.EvidenciasOpt = true;
        }
        private void Evidencias_Load(object sender, EventArgs e)
        {
            Load_Data();
        }
        #endregion

    }
}
