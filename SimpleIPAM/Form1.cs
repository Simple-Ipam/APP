using System;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleIPAM
{
    public partial class LoginSIPAM : Form
    {

        public string userName = "";
        public StateProgram prgState = StateProgram.USERN_LOG;
        const string API = "https://simpleipam-api/";
        public LoginSIPAM()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (prgState)
            {
                case StateProgram.USERN_LOG:
                    // VERIFY IF THIS USERNAME IS VALID 
                    var result_isAUser = true;
                    // IF IS TRUE, SET THE PROGRAM STATE TO THE PASSWORD ENTRY
                    if (result_isAUser) prgState = StateProgram.PASWO_LOG;
                    else return;
                    break;
                case StateProgram.PASWO_LOG:
                    // VERFIFY THE PASSWORD
                    //TRY TO CONNECT
                    break;
            }
        }

        public enum StateProgram
        {
            USERN_LOG = 0,
            PASWO_LOG = 1,
            CONNECTED = 2
        };

    }
}
