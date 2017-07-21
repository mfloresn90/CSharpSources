using System;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace AdminDCZOEM.Config
{
    class Validation
    {

        #region CheckInfo
        public static bool CheckData(string table, string field, string condition, string compare, string type = null)
        {
            bool validate = false;
            DataTable data = CrudOp.ReadData(table, field, condition);
            foreach (DataRow row in data.Rows)
            {
                if (type == "int")
                {
                    int string2Int = Convert.ToInt32(compare);
                    if (row.Field<int>(0) == string2Int)
                        validate = true;
                    else
                        validate = false;
                }
                else
                {
                    if (row.Field<string>(0) == compare)
                        validate = true;
                    else
                        validate = false;
                }
                
            }
            return validate;
        }
        #endregion

        #region MD5 Crypt
        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        #endregion

    }
}
