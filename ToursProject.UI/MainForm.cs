using System;
using System.Windows.Forms;
using ToursProject.Context.Enums;
using ToursProject.UI.ListForms;

namespace ToursProject.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            labelUserName.Text = $"{CurrentUser.User.LastName} {CurrentUser.User.FirstName} {CurrentUser.User.Patronymic}";
            заказыToolStripMenuItem.Enabled = CurrentUser.User.Role > Role.Client;
        }

        private void турыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ToursForm().ShowDialog();
        }

        private void отелиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new HotelsForm().ShowDialog();
        }

        private void заказыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new OrdersGrid().ShowDialog();
        }
    }
}
