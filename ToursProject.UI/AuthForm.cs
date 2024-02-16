using System;
using System.Linq;
using System.Windows.Forms;
using ToursProject.Context;

namespace ToursProject
{
    public partial class AutorizationForm : Form
    {
        public AutorizationForm()
        {
            InitializeComponent();
        }

        private void buttonEntranceGuest_Click(object sender, EventArgs e)
        {
            var form = new MainForm();
            form.Show();
            this.Hide();
        }

        private void buttonEntrance_Click(object sender, EventArgs e)
        {
            using(var db = new ToursDbContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Login == textBoxLogin.Text && x.Password== textBoxPassword.Text);

                if(user == null) 
                {
                    MessageBox.Show("Неправильное имя пользователя или пароль!");
                    textBoxPassword.Clear();
                }
                else
                {
                    WorkToUser.User = user;
                    var form = new MainForm();
                    form.Show();
                    this.Hide();
                }
            }
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            buttonEntrance.Enabled = !string.IsNullOrWhiteSpace(textBoxLogin.Text) 
                && !string.IsNullOrWhiteSpace(textBoxPassword.Text);
        }
    }
}
