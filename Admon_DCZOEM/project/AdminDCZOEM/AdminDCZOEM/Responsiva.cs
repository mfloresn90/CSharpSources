using System;
using System.Data;
using System.Windows.Forms;
using AdminDCZOEM.Config;
using AdminDCZOEM.Properties;

namespace AdminDCZOEM
{
    public partial class Responsiva : Form
    {
        public Responsiva(Form responsivaOpt)
        {
            _mp = responsivaOpt as MenuPrincipal;
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
            string[] idEquipo = comboBox1.Text.Split('-');
            string[] idPersona = comboBox2.Text.Split('-');
            string data2Insert = "'" + textBox1.Text + "', '" + textBox2.Text + "', '" + dateTimePicker1.Text + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', '', '" + idEquipo[0] + "', '" + idPersona[0] + "'";
            if (CrudOp.Create(Resources.tablaResponsiva, Resources.ResponsivaInsertar, data2Insert))
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
            if (CrudOp.Delete(Resources.tablaResponsiva, "id_responsiva", _id.ToString()))
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
            string data2Update = "fecha_retorno = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', observ_retorno = '" + textBox6.Text + "'";
            if (CrudOp.Update(Resources.tablaResponsiva, data2Update, "id_responsiva", _id.ToString()))
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
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (_id == 0)
            {
                _elements[2] = true;
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
                Validate();
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_id == 0)
            {
                _elements[3] = true;
                Validate();
            }
        }
        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_id == 0)
            {
                _elements[4] = true;
                Validate();
            }
        }
        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
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
                dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value + " - " + dataGridView1.Rows[e.RowIndex].Cells[8].Value;
                comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value + " - " + dataGridView1.Rows[e.RowIndex].Cells[10].Value + " " + dataGridView1.Rows[e.RowIndex].Cells[11].Value + " " + dataGridView1.Rows[e.RowIndex].Cells[12].Value;
                button1.Enabled = false;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                dateTimePicker1.Enabled = false;
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                textBox6.Enabled = true;
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
            dateTimePicker1.Value = new DateTime(Convert.ToInt32(DateTime.Now.ToString("yyyy")), Convert.ToInt32(DateTime.Now.ToString("MM")), Convert.ToInt32(DateTime.Now.ToString("dd")), Convert.ToInt32(DateTime.Now.ToString("HH")), Convert.ToInt32(DateTime.Now.ToString("mm")), Convert.ToInt32(DateTime.Now.ToString("ss")), 0);
            textBox6.Clear();
            textBox6.Enabled = false;
            comboBox1.Items.Clear();
            comboBox1.Text = "Selecciona un equipo:";
            comboBox2.Items.Clear();
            comboBox2.Text = "Selecciona una persona:";
            for (int x = 0; x < _elements.Length; x++) _elements[x] = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            dateTimePicker1.Enabled = true;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            _id = 0;
            Load_Data();
        }
        private void Load_Data()
        {
            dataGridView1.DataSource = CrudOp.ReadData(Resources.VistaResponsiva, Resources.ResponsivaBusqueda);
            DataTable idEquipo = CrudOp.ReadData(Resources.VistaEquipo);
            foreach (DataRow row in idEquipo.Rows)
            {
                comboBox1.Items.Add(row.Field<string>(0) + " - " + row.Field<string>(1));
            }
            DataTable idPersona = CrudOp.ReadData(Resources.VistaPersona);
            foreach (DataRow row in idPersona.Rows)
            {
                if (row.Field<string>(16) != "Administrador")
                {
                    comboBox2.Items.Add(row.Field<string>(0) + " - " + row.Field<string>(1) + " " + row.Field<string>(2) + " " + row.Field<string>(3));
                }
            }
        }
        #endregion

        #region Windows events
        private void Responsiva_FormClosed(object sender, FormClosedEventArgs e)
        {
            _mp.ResponsivaOpt = true;
        }
        private void Responsiva_Load(object sender, EventArgs e)
        {
            Load_Data();
        }
        #endregion

    }
}
