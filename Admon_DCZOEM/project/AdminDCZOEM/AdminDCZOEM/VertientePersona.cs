using System;
using System.Data;
using System.Windows.Forms;
using AdminDCZOEM.Config;
using AdminDCZOEM.Properties;

namespace AdminDCZOEM
{
    public partial class VertientePersona : Form
    {

        public VertientePersona(Form vertientePersonaOpt)
        {
            _mp = vertientePersonaOpt as MenuPrincipal;
            InitializeComponent();
        }

        #region Variables
        private MenuPrincipal _mp;
        private bool[] _elements = new bool[2];
        private int _id;
        #endregion

        #region Buttons
        private void button1_Click(object sender, EventArgs e)
        {
            string[] idVertiente = comboBox1.Text.Split('-');
            string[] idPersona = comboBox2.Text.Split('-');
            if (!Validation.CheckData(Resources.tablaVertientePersona, "id_vertiente", "id_vertiente = " + idVertiente[0], idVertiente[0], "int"))
            {
                if (CrudOp.Create(Resources.tablaVertientePersona, Resources.VertientePersonaInsertar, idVertiente[0] + ", '" + idPersona[0] + "'"))
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
                MessageBox.Show("Ya existe una vertiente '" + idVertiente[1] + "' asignada,\n verifique sus datos.", Resources.infoWindow, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (CrudOp.Delete(Resources.tablaVertientePersona, "id_vertiente", _id.ToString()))
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
            string[] idVertiente = comboBox1.Text.Split('-');
            string[] idPersona = comboBox2.Text.Split('-');
            string data2Update = "id_vertiente = " + idVertiente[0] + ", id_persona = '" + idPersona[0] + "'";
            if (CrudOp.Update(Resources.tablaVertientePersona, data2Update, "id_vertiente", _id.ToString()))
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
        private new void Validate()
        {
            int fields = 0;
            for (int x = 0; x < _elements.Length; x++)
                if (_elements[x])
                    fields++;
            if (fields == 2)
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }
        private void CleanFields()
        {
            comboBox1.Items.Clear();
            comboBox1.Text = "Selecciona una vertiente:";
            comboBox2.Items.Clear();
            comboBox2.Text = "Selecciona una persona:";
            for (int x = 0; x < _elements.Length; x++) _elements[x] = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            _id = 0;
            Load_Data();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_id == 0)
            {
                _elements[0] = true;
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
                _elements[1] = true;
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
                _id = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value + " - " + dataGridView1.Rows[e.RowIndex].Cells[1].Value;
                comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value + " - " + dataGridView1.Rows[e.RowIndex].Cells[3].Value + " " + dataGridView1.Rows[e.RowIndex].Cells[4].Value + " " + dataGridView1.Rows[e.RowIndex].Cells[5].Value;
                button2.Enabled = true;
                button3.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show(Resources.MsgValidateRegisterSelect, Resources.infoWindow,
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void Load_Data()
        {
            dataGridView1.DataSource = CrudOp.ReadData(Resources.VistaVertientePersona, Resources.VertientePersonaBusqueda);
            DataTable idVertiente = CrudOp.ReadData(Resources.tablaVertiente);
            foreach (DataRow row in idVertiente.Rows)
            {
                comboBox1.Items.Add(row.Field<int>(0) + " - " + row.Field<string>(1));
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

        #region Window event
        private void VertientePersona_FormClosed(object sender, FormClosedEventArgs e)
        {
            _mp.VertientePersonaOpt = true;
        }
        private void VertientePersona_Load(object sender, EventArgs e)
        {
            Load_Data();
        }
        #endregion

    }
}
