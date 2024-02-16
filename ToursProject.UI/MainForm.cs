using System;
using System.Windows.Forms;

namespace ToursProject
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            label1.Text = $"{WorkToUser.User.LastName} {WorkToUser.User.FirstName} {WorkToUser.User.Patronymic}";
            отелиToolStripMenuItem.Enabled = !WorkToUser.CompareRole(Context.Enums.Role.Meneger);
            заказыToolStripMenuItem.Enabled = !WorkToUser.CompareRole(Context.Enums.Role.Quest) &&
                !WorkToUser.CompareRole(Context.Enums.Role.Client);
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
