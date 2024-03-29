﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Windows.Forms;
using ToursProject.Context;
using ToursProject.Context.Models;

namespace ToursProject.UI.ListForms
{
    public partial class OrdersGrid : Form
    {
        public OrdersGrid()
        {
            InitializeComponent();
            dataGridView.AutoGenerateColumns = false;
            comboBoxDiscount.SelectedIndex = 0;
            using(var db = new ToursDbContext())
            {
                dataGridView.DataSource = db.Orders.Include(x => x.Tours).Include(x => x.User).ToList();
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView.Columns[e.ColumnIndex].Name == "ColumnOrder")
            {
                var tours = (List<Tour>)e.Value;
                var sb = new StringBuilder();

                foreach (var item in tours)
                {
                    sb.Append($"{item.Title}, ");
                }

                e.Value = sb.ToString().Remove(sb.Length - 2, 2);
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            Filter(true);
        }

        private void Filter(bool isOrder)
        {
            List<Order> orders;

            using (var db = new ToursDbContext())
            {
                orders = db.Orders.Include(x => x.Tours).Include(x => x.User).ToList();
            }

            if (isOrder)
            {
                orders = orders.OrderBy(x => x.Price).ToList();
            }

            switch (comboBoxDiscount.SelectedIndex)
            {
                case 1:
                   orders =  orders.Where(x => x.AllSale <= 10).ToList();
                    break;
                case 2:
                    orders = orders.Where(x => x.AllSale > 10 && x.AllSale <= 14).ToList();
                    break;
                case 3:
                    orders = orders.Where(x => x.AllSale > 14).ToList();
                    break;
                default:
                    break;
            }

            dataGridView.DataSource = orders;
        }

        private void comboBoxDiscount_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            Filter(false);
        }
    }
}
