using System;
using System.Data;
using System.Windows.Forms;
using AdminDCZOEM.Config;
using AdminDCZOEM.Properties;

namespace AdminDCZOEM
{
    public partial class TipoPersona : Form
    {
        public TipoPersona(Form tPersonaOpt)
        {
            _mp = tPersonaOpt as MenuPrincipal;
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
            if (!Config.Validation.CheckData(Resources.tablaTipoPersona, "tipo_persona", "tipo_persona = '" + textBox1.Text + "'", textBox1.Text))
            {
                string data2Insert = "'" + textBox1.Text + "'";
                if (CrudOp.Create(Resources.tablaTipoPersona, Resources.TipoPersonaInsertar, data2Insert))
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
                MessageBox.Show("Ya existe un tipo de persona '" + textBox1.Text + "',\n verifique sus datos.", Resources.infoWindow, MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (CrudOp.Delete(Resources.tablaTipoPersona, "id_tipo_persona", _id.ToString()))
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
            string data2Update = "tipo_persona = '" + textBox1.Text + "'";
            if (CrudOp.Update(Resources.tablaTipoPersona, data2Update, "id_tipo_persona", _id.ToString()))
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
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
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
            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].Name = "IDENTIFICADOR";
            dataGridView1.Columns[1].Name = "TIPO";
            DataTable tipoPersona = CrudOp.ReadData(Resources.tablaTipoPersona, Resources.TipoPersonaBusqueda);
            foreach (DataRow row in tipoPersona.Rows)
            {
                if (row.Field<string>(1) != "Administrador")
                    dataGridView1.Rows.Add(row.Field<int>(0), row.Field<string>(1));
            }
        }
        #endregion

        #region Windows events
        private void TipoPersona_FormClosed(object sender, FormClosedEventArgs e)
        {
            _mp.TipoPersonaOpt = true;
        }
        private void TipoPersona_Load(object sender, EventArgs e)
        {
            Load_Data();
        }
        #endregion

    }
}
