using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2SI
{
    public partial class FormConnexion : Form
    {
        private string strcon = @"Server = .\SQLEXPRESS; Database=GM;Trusted_Connection=True;";

        public FormConnexion()
        {
            InitializeComponent();
        }

        private static string Hash256(string input)
        {
            SHA256 ca = SHA256.Create();
            byte[] data = ca.ComputeHash(Encoding.UTF8.GetBytes(input));

            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private void checkLastPassword(string login, string hashpwd)
        {
            string sql = "select count(*) as nb from Utilisateur where Login = '" + login +
                "' and Pwd = '" + hashpwd + 
                "' and DATEDIFF(MONTH, Date_Last_Change, GETDATE()) <= 3";
            SqlConnection cn = new SqlConnection(strcon);
            cn.Open();
            SqlCommand com = new SqlCommand(sql, cn);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();

            int nb = Convert.ToInt32(dr["nb"]);

            if (nb == 0)
            {
                FormChangePWD frm = new FormChangePWD();
                frm.ShowDialog();
            }


        }

        private void buttonConnexion_Click(object sender, EventArgs e)
        {
            string login = textBoxLogin.Text;
            string pwd = textBoxPwd.Text;

            string hashpwd = Hash256(pwd);

            SqlConnection cn = new SqlConnection(strcon);
            cn.Open();
            string sql = "select count(*) as nb from Utilisateur" +
                " where Login =  '" + login + "' and Pwd = '" + hashpwd + "'";
            SqlCommand com = new SqlCommand(sql, cn);
            SqlDataReader dr = com.ExecuteReader();
            dr.Read();

            int nb = Convert.ToInt32(dr["nb"]);

            if (nb > 0)
            {
                checkLastPassword(login, hashpwd);
             

                Close();   
            }
            else
            {
                MessageBox.Show("Identifiants Incorrects", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            dr.Close();
            cn.Close();    

        }

        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormConnexion_Load(object sender, EventArgs e)
        {

        }
    }
}
