using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Libreria;
namespace ufficio
{
    public partial class Form1 : Form
    {

        private Libreria.DatabaseLibrary db;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            db = new Libreria.DatabaseLibrary("127.0.0.1", "3306", "pesca", "root", "1234");
            login.Database = db;
        }
    }
}
