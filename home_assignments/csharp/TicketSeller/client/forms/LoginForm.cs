using System;
using System.Windows.Forms;

namespace client.forms
{
    public partial class LoginForm : MyForm
    {
        public LoginForm(MainController controller) : base(controller)
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string username = usernameTextBox.Text;
                string password = passwordTextBox.Text;
                _controller.login(username, password);
                MainForm mainForm = new MainForm(_controller);
                mainForm.Show();
                this.Hide();
                usernameTextBox.Clear();
                passwordTextBox.Clear();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}