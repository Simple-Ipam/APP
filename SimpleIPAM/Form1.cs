using System.Windows.Forms;

namespace SimpleIPAM
{
    public partial class LoginSIPAM : Form
    {
        public LoginSIPAM()
        {
            InitializeComponent();
            renderWeb.Navigate("https://ipamonline.be");
        }
    }
}
