using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2SI
{
    public partial class FormChangePWD : Form
    {
        private string strcon = @"Server = .\SQLEXPRESS; Database=GM;Trusted_Connection=True;";

        public FormChangePWD()
        {
            InitializeComponent();
        }

        private void buttonAnnuler_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
        private void FormChangePWD_Load(object sender, EventArgs e)
        {

        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            string loginUser = textBoxMDP.Text;
            string loginPassword = textBoxConfMDP.Text;

            if () {
            
            }
            else if () {
            
            }
            else {
            
            }
        }
    }
}
