using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;
using ToursProject.AddForm;
using ToursProject.Context;
using ToursProject.Context.Models;

namespace ToursProject
{
    public partial class HotelsForm : Form
    {
        private int pageSize = 7;
        private int oldCountPage = -1;
        private readonly BindingSource bindingSource = new BindingSource();

        public HotelsForm()
        {
            InitializeComponent();
            bindingSource.CurrentItemChanged += Bs_CurrentItemChanged;
            dataGridView1.AutoGenerateColumns = false;
            Print();      
            buttonAdd.Enabled = buttonEdit.Enabled = buttonDeleted.Enabled = WorkToUser.CompareRole(Context.Enums.Role.Admin);
        }


        private void Print()
        {
            using (var db = new ToursDbContext())
            {
                var count = db.Hotels.Count();
                var countPage = (int)Math.Ceiling((decimal)count / pageSize);

                if(oldCountPage != countPage)
                {
                    oldCountPage = countPage;
                    var current = bindingSource.Position;

                    if(current > countPage)
                    {
                        current = countPage;
                    }

                    bindingSource.DataSource = Enumerable.Range(1, countPage);

                    if(current != -1)
                    {
                        bindingSource.Position = current;
                    }

                    bindingNavigator1.BindingSource = bindingSource;
                }

                dataGridView1.DataSource = db.Hotels.Include(x => x.Country)
                    .OrderBy(x => x.Title)
                    .Skip(bindingSource.Position * pageSize)
                    .Take(pageSize)
                    .ToList();

                toolStripStatusLabelAllCount.Text = $"Кол-во записей: {count}";
            }
        }

        private void Bs_CurrentItemChanged(object sender, EventArgs e)
        {
            Print();
        }
                       
        private void buttonDeleted_Click(object sender, System.EventArgs e)
        {
            var hotel = (Hotel)dataGridView1.SelectedRows[0].DataBoundItem;

            if (hotel == null)
            {
                return;
            }

            using (var db = new ToursDbContext())
            {
                var hotelDB = db.Hotels.Include(x => x.Tours).FirstOrDefault(x => x.Id == hotel.Id);

                if (hotelDB.Tours.Count(x => x.IsActual) != 0)
                {
                    MessageBox.Show("Этот отель подходит к актуальным турам!!");
                }
                else if (MessageBox.Show($"Удалить ли отель {hotel.Title}?", "Подтвердите!", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    db.Hotels.Remove(hotelDB);
                    db.SaveChanges();
                    Print();
                }
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var form = new EditHotel();

            if (form.ShowDialog() == DialogResult.OK)
            {
                using (var db = new ToursDbContext())
                {
                    db.Hotels.Add(form.Hotel);
                    db.SaveChanges();
                    Print();
                }
            }
        }

        private void buttonEdit_Click(object sender, System.EventArgs e)
        {
            var hotelId = (Hotel)dataGridView1.SelectedRows[0].DataBoundItem;

            if (hotelId == null)
            {
                return;
            }
            using (var db = new ToursDbContext())
            {
                var hotel1 = db.Hotels.FirstOrDefault(x => x.Id == hotelId.Id);
                var form = new EditHotel(hotel1);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    db.SaveChanges();
                    Print();
                }
            }
        }
    }
}
