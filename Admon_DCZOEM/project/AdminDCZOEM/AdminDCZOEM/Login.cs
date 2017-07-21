using System;
using System.Data;
using System.Security.Cryptography;
using System.Windows.Forms;
using AdminDCZOEM.Config;
using AdminDCZOEM.Properties;

namespace AdminDCZOEM
{
    public partial class Login : Form
    {

        public Login()
        {
            InitializeComponent();
        }

        #region Variables
        private bool[] _elements = new bool[2];
        #endregion

        #region Botones
        private void button1_Click(object sender, EventArgs e)
        {
            string hash;
            using (MD5 md5Hash = MD5.Create())
            {
                hash = Validation.GetMd5Hash(md5Hash, textBox2.Text);
            }
            var check = ValidateUser(textBox1.Text, hash);
            if (check != null)
            {
                MessageBox.Show(check, Resources.infoWindow, MessageBoxButtons.OK, MessageBoxIcon.Information, 0, 0);
            }
            else
                MessageBox.Show("El usuario o la contraseña son incorrectos!", Resources.infoWindow, MessageBoxButtons.OK, MessageBoxIcon.Error, 0, 0);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Methods
        private string ValidateUser(string usuario, string contrasena)
        {
            var condicion = "usuario = '" + usuario + "' && contrasena = '" + contrasena + "'";
            string result = null;
            var data = CrudOp.ReadData(Resources.VistaLogin, condition: condicion);
            foreach (DataRow row in data.Rows)
            {
                if ((row.Field<string>(1) == usuario) && (row.Field<string>(2) == contrasena))
                {
                    result = "Bienvenid@ " + row.Field<string>(4) + " " + row.Field<string>(5) + " " + row.Field<string>(6);
                    var mp = new MenuPrincipal();
                    MenuPrincipal.UserName = row.Field<string>(7)  + ": " + row.Field<string>(4) + " " + row.Field<string>(5) + " " + row.Field<string>(6);
                    CleanFields();
                    this.Hide();
                    if (row.Field<string>(7) == "Administrador")
                        mp.adminMenu.Visible = true;
                    mp.Show();
                }
            }
            return result;
        }
        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBox1.Text != "")
                _elements[0] = true;
            else
                _elements[0] = false;
            Validate();
        }
        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBox2.Text != "")
                _elements[1] = true;
            else
                _elements[1] = false;
            Validate();
        }
        private void Validate() {
            var fields = 0;
            for (var x = 0; x < _elements.Length; x++)
                if(_elements[x] == true)
                    fields++;
            if (fields == 2) 
                button1.Enabled = true;
            else
                button1.Enabled = false;
        }
        private void CleanFields()
        {
            textBox1.Clear();
            textBox2.Clear();
            for (var x = 0; x < _elements.Length; x++) _elements[x] = false;
            button1.Enabled = false;
        }
        #endregion

    }
}
