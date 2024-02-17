using System;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ToursProject.Context;
using ToursProject.Context.Models;
using ToursProject.UI.EditForms;

namespace ToursProject.UI.UserControls
{
    public partial class TourView : UserControl
    {
        public Tour tour { get; set; }
        public event Action<Tour> AddToOrder;

        public TourView(Tour tour)
        {
            InitializeComponent();
            this.tour = tour;
            InitialComponent(tour);
        }

        private void InitialComponent(Tour tour)
        {
            labelTitle.Text = tour.Title;
            labelActual.Text = tour.IsActual ? "Актуален" : "Не актуален";
            labelActual.ForeColor = tour.IsActual ? Color.Green : Color.Red;
            labelPrice.Text = $"{tour.Price} руб.";
            labelCount.Text = $"Билетов {tour.TicketCount}";
            if(tour.ImagePreview != null)
            {
                var image = Image.FromStream(new MemoryStream(tour.ImagePreview));
                pictureBoxImage.Image = image;
            }
            buttonEdit.Enabled = buttonReview.Enabled = !CurrentUser.CompareRole(Context.Enums.Role.Guest)
                && !CurrentUser.CompareRole(Context.Enums.Role.Client);
        }

        private void buttonReview_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var image = File.ReadAllBytes(openFileDialog1.FileName);
                using (var db = new ToursDbContext())
                {
                    tour.ImagePreview = image;
                    db.Entry(tour).State = EntityState.Modified;
                    db.SaveChanges();
                }
                pictureBoxImage.Image = Image.FromStream(new MemoryStream(tour.ImagePreview));
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            using (var db = new ToursDbContext())
            {
                var tour1 = db.Tours.FirstOrDefault(x => x.Id == tour.Id);
                var form = new EditTour(tour1);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var ids = form.GetCheckedTypes();
                    tour1.Types.Clear();
                    tour1.Types = db.TypeTours.Where(x => ids.Contains(x.Id)).ToList();
                    db.SaveChanges();
                    InitialComponent(tour1);
                }
            }
        }

        private void добавитьКЗаказуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(tour.IsActual)
            {
                AddToOrder?.Invoke(tour);
            }
        }
    }
}
