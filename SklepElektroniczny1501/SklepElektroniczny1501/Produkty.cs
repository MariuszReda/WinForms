using SklepElektroniczny1501.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SklepElektroniczny1501
{
    public partial class Produkty : Form
    {
        private readonly IDatabaseOpeartions _databaseOpeartions;
        private readonly IMapper _mapper;
        List<Domen.Produkt> produkty = new List<Domen.Produkt>();
        Domen.Produkt record;
        
        public Produkty(IDatabaseOpeartions databaseOpeartions, IMapper mapper)
        {
            _databaseOpeartions = databaseOpeartions;
            _mapper = mapper;
            InitializeComponent();
            IncludeRecords();         
        }

        public async void IncludeRecords()
        {
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Nazwa kategorii",
                DataPropertyName = "Kategoria.Nazwa"
            });

            var produkt_kategoria = await _databaseOpeartions.GetAllAsync<produkt_kategoria>();
            foreach (var item in produkt_kategoria)
            { 
                Domen.Produkt produkt = new Domen.Produkt();
                produkt = _mapper.ProduktMapper(item);
                produkty.Add(produkt);
                
            }
            produktBindingSource.DataSource = produkty;
            
            for(int i = 0; i < produkty.Count; i++)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                row.Cells[5].Value = produkty[i].Kategoria.KategoriaNazwa.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(record != null)
            {
                record.Akcja = Domen.Akcja.Nowy;
            }
            ProduktyEdycja produktyEdycja = new ProduktyEdycja(record);
            produktyEdycja.Show();
            produktyEdycja.Zamkniety += ProduktyEdycja_Zamkniety;
        }

        private void ProduktyEdycja_Zamkniety(Domen.Produkt produkt)
        {
            produkty.Add(produkt);
            var nowyWiersz = new DataGridViewRow();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = produkty;
            for (int i = 0; i < produkty.Count; i++)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                row.Cells[5].Value = produkty[i].Kategoria.KategoriaNazwa.ToString();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                record = (Domen.Produkt)row.DataBoundItem;
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            record = (Domen.Produkt)row.DataBoundItem;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (record != null)
            {
                record.Akcja = Domen.Akcja.Edycja;
            }
            ProduktyEdycja produktyEdycja = new ProduktyEdycja(record);
            produktyEdycja.Show();
        }
    }
}
