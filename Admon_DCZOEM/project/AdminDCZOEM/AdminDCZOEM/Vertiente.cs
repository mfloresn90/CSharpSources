using System;
using System.Data;
using System.Windows.Forms;
using AdminDCZOEM.Config;
using AdminDCZOEM.Properties;

namespace AdminDCZOEM
{
    public partial class Vertiente : Form
    {
        public Vertiente(Form vertienteOpt)
        {
            _mp = vertienteOpt as MenuPrincipal;
            InitializeComponent();
        }
    
        #region Variables
        private MenuPrincipal _mp;
        private bool[] _elements = new bool[1];
        private int _id;
        #endregion

        #region Buttons
        private void button1_Click(object sender, EventArgs e)
        {
            if (!Validation.CheckData(Resources.tablaVertiente, "nombre_vertiente", "nombre_vertiente = '" + textBox1.Text + "'", textBox1.Text))
            {
                string data2Insert = "'" + textBox1.Text + "'";
                if (CrudOp.Create(Resources.tablaVertiente, Resources.VertienteInsertar, data2Insert))
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
                MessageBox.Show("Ya existe un vertiente '" + textBox1.Text + "',\n verifique sus datos.", Resources.infoWindow, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (CrudOp.Delete(Resources.tablaVertiente, "id_vertiente", _id.ToString()))
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
            string data2Update = "nombre_vertiente = '" + textBox1.Text + "'";
            if (CrudOp.Update(Resources.tablaVertiente, data2Update, "id_vertiente", _id.ToString()))
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
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                _id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
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
            if (fields == 1)
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }
        private void CleanFields()
        {
            textBox1.Clear();
            for (int x = 0; x < _elements.Length; x++) _elements[x] = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            _id = 0;
            Load_Data();
        }
        private void Load_Data()
        {
            dataGridView1.DataSource = CrudOp.ReadData(Resources.tablaVertiente, Resources.VertienteBusqueda);
        }
        #endregion

        #region Windows events
        private void Vertiente_FormClosed(object sender, FormClosedEventArgs e)
        {
            _mp.VertienteOpt = true;
        }
        private void Vertiente_Load(object sender, EventArgs e)
        {
            Load_Data();
        }
        #endregion

    }
}
