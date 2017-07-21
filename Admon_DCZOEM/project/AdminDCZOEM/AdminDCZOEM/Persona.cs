using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using AdminDCZOEM.Config;
using AdminDCZOEM.Properties;

namespace AdminDCZOEM
{
    public partial class Persona : Form
    {
        public Persona(Form personaOpt)
        {
            _mp = personaOpt as MenuPrincipal;
            InitializeComponent();
        }
    
        #region Variables
        private MenuPrincipal _mp;
        private FileStream _fs;
        private BinaryReader _br;
        private OpenFileDialog _ofd;
        private bool[] _elements = new bool[16];
        private string _id = null;
        #endregion

        #region Buttons
        private void button1_Click(object sender, EventArgs e)
        {
            byte[] imageBt;
            _fs = new FileStream(textBox8.Text, FileMode.Open, FileAccess.Read);
            _br = new BinaryReader(_fs);
            imageBt = _br.ReadBytes((int) _fs.Length);
            string[] idType = comboBox2.Text.Split('-');
            string data2Insert = "'" + textBox1.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "', '" +
                                 textBox4.Text + "', '" + comboBox1.Text + "', '" + textBox5.Text + "', '" +
                                 textBox6.Text + "', '" + textBox7.Text + "', '" + imageBt + "', '" + textBox9.Text +
                                 "', '" + textBox10.Text + "', '" + textBox11.Text + "', '" + textBox12.Text + "', '" +
                                 textBox13.Text + "', '" + textBox14.Text + "', '" + idType[0] + "'";
            if (!Validation.CheckData(Resources.tablaPersona, "id_persona", "id_persona = '" + textBox1.Text + "'", textBox1.Text))
            {
                if (CrudOp.Create(Resources.tablaPersona, Resources.PersonaInsertar, data2Insert))
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
            if (CrudOp.Delete(Resources.tablaPersona, "id_persona", "'" + _id + "'"))
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
            string[] idType = comboBox2.Text.Split('-');
            string data2Update = "id_persona = '" + textBox1.Text + "', nombre = '" + textBox2.Text + "',  ap_pat ='" + textBox3.Text + "', ap_mat = '" +
                                 textBox4.Text + "', sexo = '" + comboBox1.Text + "', contacto = '" + textBox5.Text + "', telefono1 = '" +
                                 textBox6.Text + "', telefono2 = '" + textBox7.Text + "', activo = '" + textBox9.Text +
                                 "', cp = " + textBox10.Text + ", calle = '" + textBox11.Text + "', colonia = '" + textBox12.Text + "', municipio = '" +
                                 textBox13.Text + "', estado = '" + textBox14.Text + "', id_tipo_persona = " + idType[0];

            if (CrudOp.Update(Resources.tablaPersona, data2Update, "id_persona", "'" + _id + "'"))
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
            if (_id == null)
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
            if (_id == null)
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
            if (_id == null)
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
            if (_id == null)
            {
                if (textBox4.Text != "")
                    _elements[3] = true;
                else
                    _elements[3] = false;
                Validate();
            }
        }
        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_id == null)
            {
                _elements[4] = true;
                Validate();
            }
        }
        private void textBox5_KeyUp(object sender, KeyEventArgs e)
        {
            if (_id == null)
            {
                if (textBox5.Text != "")
                    _elements[5] = true;
                else
                    _elements[5] = false;
                Validate();
            }
        }
        private void textBox6_KeyUp(object sender, KeyEventArgs e)
        {
            if (_id == null)
            {
                if (textBox6.Text != "")
                    _elements[6] = true;
                else
                    _elements[6] = false;
                Validate();
            }
        }
        private void textBox7_KeyUp(object sender, KeyEventArgs e)
        {
            if (_id == null)
            {
                if (textBox7.Text != "")
                    _elements[7] = true;
                else
                    _elements[7] = false;
                Validate();
            }
        }
        private void textBox8_MouseClick(object sender, MouseEventArgs e)
        {
            if (_id == null)
            {
                _ofd = new OpenFileDialog();
                _ofd.Filter = "Imágen JPG(*.jpg)|*.jpg|Imágen PNG(*.png)|*.png";
                if (_ofd.ShowDialog() == DialogResult.OK)
                {
                    string picPath = _ofd.FileName;
                    pictureBox1.BackgroundImage = Image.FromFile(picPath);
                    textBox8.Text = picPath;
                    _elements[8] = true;
                    Validate();
                }
            }
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (_id == null)
            {
                _ofd = new OpenFileDialog();
                _ofd.Filter = "Imágen JPG(*.jpg)|*.jpg|Imágen PNG(*.png)|*.png";
                if (_ofd.ShowDialog() == DialogResult.OK)
                {
                    string picPath = _ofd.FileName;
                    pictureBox1.BackgroundImage = Image.FromFile(picPath);
                    textBox8.Text = picPath;
                    _elements[8] = true;
                    Validate();
                }
            }
        }
        private void textBox9_KeyUp(object sender, KeyEventArgs e)
        {
            if (_id == null)
            {
                if (textBox9.Text != "")
                    _elements[9] = true;
                else
                    _elements[9] = false;
                Validate();
            }
        }
        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                e.Handled = true;
        }
        private void textBox10_KeyUp(object sender, KeyEventArgs e)
        {
            if (_id == null)
            {
                if (textBox10.Text != "")
                    _elements[10] = true;
                else
                    _elements[10] = false;
                Validate();
            }
        }
        private void textBox11_KeyUp(object sender, KeyEventArgs e)
        {
            if (_id == null)
            {
                if (textBox11.Text != "")
                    _elements[11] = true;
                else
                    _elements[11] = false;
                Validate();
            }
        }
        private void textBox12_KeyUp(object sender, KeyEventArgs e)
        {
            if (_id == null)
            {
                if (textBox12.Text != "")
                    _elements[12] = true;
                else
                    _elements[12] = false;
                Validate();
            }
        }
        private void textBox13_KeyUp(object sender, KeyEventArgs e)
        {
            if (_id == null)
            {
                if (textBox13.Text != "")
                    _elements[13] = true;
                else
                    _elements[13] = false;
                Validate();
            }
        }
        private void textBox14_KeyUp(object sender, KeyEventArgs e)
        {
            if (_id == null)
            {
                if (textBox14.Text != "")
                    _elements[14] = true;
                else
                    _elements[14] = false;
                Validate();
            }
        }
        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_id == null)
            {
                _elements[15] = true;
                Validate();
            }
        }
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                _id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBox1.Enabled = false;
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                textBox8.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                textBox9.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                textBox10.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                textBox11.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                textBox12.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
                textBox13.Text = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
                textBox14.Text = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
                comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[15].Value + " - " + dataGridView1.Rows[e.RowIndex].Cells[16].Value;
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
            if (fields == 16)
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
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.Text = "Selecciona un género:";
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();
            textBox13.Clear();
            textBox14.Clear();
            comboBox2.Items.Clear();
            comboBox2.Text = "Selecciona un tipo:";
            pictureBox1.Image = null;
            for (int x = 0; x < _elements.Length; x++) _elements[x] = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            _id = null;
            Load_Data();
        }
        private void Load_Data()
        {
            dataGridView1.ColumnCount = 17;
            dataGridView1.Columns[0].Name = "IDENTIFICADOR";
            dataGridView1.Columns[1].Name = "NOMBRE";
            dataGridView1.Columns[2].Name = "APELLIDO PATERNO";
            dataGridView1.Columns[3].Name = "APELLIDO MATERNO";
            dataGridView1.Columns[4].Name = "SEXO";
            dataGridView1.Columns[5].Name = "CONTACTO";
            dataGridView1.Columns[6].Name = "TELEFONO 1";
            dataGridView1.Columns[7].Name = "TELEFONO 2";
            dataGridView1.Columns[8].Name = "FOTO";
            dataGridView1.Columns[9].Name = "ACTIVO";
            dataGridView1.Columns[10].Name = "CODIGO POSTAL";
            dataGridView1.Columns[11].Name = "CALLE";
            dataGridView1.Columns[12].Name = "COLONIA";
            dataGridView1.Columns[13].Name = "MUNICIPIO";
            dataGridView1.Columns[14].Name = "ESTADO";
            dataGridView1.Columns[15].Name = "ID TIPO";
            dataGridView1.Columns[16].Name = "TIPO";
            DataTable persona = CrudOp.ReadData(Resources.VistaPersona, Resources.PersonaBusqueda);
            foreach (DataRow row in persona.Rows)
            {
                if (row.Field<string>(16) != "Administrador")
                    dataGridView1.Rows.Add(row.Field<string>(0), row.Field<string>(1), row.Field<string>(2),
                        row.Field<string>(3), row.Field<string>(4), row.Field<string>(5), row.Field<string>(6),
                        row.Field<string>(7), "", row.Field<string>(9), row.Field<string>(10),
                        row.Field<string>(11), row.Field<string>(12), row.Field<string>(13), row.Field<string>(14),
                        row.Field<int>(15), row.Field<string>(16));
            }
            DataTable idType = CrudOp.ReadData(Resources.tablaTipoPersona);
            foreach (DataRow row in idType.Rows) {
                if (row.Field<string>(1) != "Administrador")
                    comboBox2.Items.Add(row.Field<int>(0) + " - " + row.Field<string>(1));
            }
        }
        #endregion

        #region Windows events
        private void Persona_FormClosed(object sender, FormClosedEventArgs e)
        {
            _mp.PersonaOpt = true;
        }
        private void Persona_Load(object sender, EventArgs e)
        {
            Load_Data();
        }
        #endregion

    }
}
