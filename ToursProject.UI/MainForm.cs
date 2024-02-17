using System;
using System.Windows.Forms;
using ToursProject.UI.ListForms;

namespace ToursProject.UI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            label1.Text = $"{CurrentUser.User.LastName} {CurrentUser.User.FirstName} {CurrentUser.User.Patronymic}";
            отелиToolStripMenuItem.Enabled = !CurrentUser.CompareRole(Context.Enums.Role.Manager);
            заказыToolStripMenuItem.Enabled = !CurrentUser.CompareRole(Context.Enums.Role.Guest) &&
                !CurrentUser.CompareRole(Context.Enums.Role.Client);
        }

        private void турыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ToursForm();

            form.ShowDialog();
        }

        private void отелиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new HotelsForm();

            form.ShowDialog();
        }

        private void заказыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new OrdersGrid();

            form.ShowDialog();
        }
    }
}
