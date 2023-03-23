using SklepElektroniczny1501.Implementations;
using SklepElektroniczny1501.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SklepElektroniczny1501
{
    public partial class ProduktyEdycja : Form
    {
        //c.	ProduktyEdycja (Umożliwienie dodawania/edycji produktów, Nazwa, Model, Kategoria, Ilość dostępna, Cena)
        Domen.Produkt produkt;
        DatabaseOpeartions _databaseOpeartions = new DatabaseOpeartions();
        DtoContex dtoContex = new DtoContex();
        Dictionary<Guid, string> map;

        public delegate void FormClosedEventHandler(Domen.Produkt produkt);
        public event FormClosedEventHandler Zamkniety;
        public ProduktyEdycja(Domen.Produkt produkt)
        {
            InitializeComponent();
            this.produkt = produkt;
            if(produkt == null)
                this.produkt = new Domen.Produkt();
            var kategorie = dtoContex.kategoria.ToList();
            map = kategorie.ToDictionary(k => k.id , v=>v.kategoria1);
            comboBoxKategoria.Items.AddRange(map.Select(x => x.Value).ToArray());
            if (produkt?.Akcja == Domen.Akcja.Edycja)
                completeForm();
        }

        private void completeForm()
        {
            textBoxNazwa.Text = produkt.Nazwa;
            textBoxModel.Text = produkt.Model;
            textBoxIlosc.Text = produkt.Ilosc.ToString();
            textBoxCena.Text = produkt.Cena.ToString();
            comboBoxKategoria.Text = produkt.Kategoria.KategoriaNazwa;
        }


        private bool addProdukt()
        {            
            bool validation = true;
            if(produkt.Akcja == Domen.Akcja.Nowy)
            {
                produkt = new Domen.Produkt();
                produkt.Id = Guid.NewGuid();
            }
            produkt.Nazwa = textBoxNazwa.Text;
            produkt.Model = textBoxModel.Text;
            string wybranaWartosc = comboBoxKategoria.SelectedItem.ToString();
            var klucz = map.FirstOrDefault(x => x.Value.ToString() == wybranaWartosc).Key;
            produkt.Kategoria = new Domen.Kategoria { Id = klucz,KategoriaNazwa = wybranaWartosc };


            int ilosc;
            if( Int32.TryParse(textBoxIlosc.Text,out ilosc))
            {
                produkt.Ilosc = ilosc;
                validation = true;
            }
            else
            {
                MessageBox.Show("Ilość musi być cyfrą", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                validation = false;
            }
            decimal cena;
            if(decimal.TryParse(textBoxCena.Text, out cena))
            {
                produkt.Cena = cena;
                validation = true;
            }
            else
            {
                MessageBox.Show("Cena musi być cyfrą", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                validation = false;
            }
            return validation;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(produkt != null)
            {
                if (addProdukt())
                {
                    if(produkt.Akcja == Domen.Akcja.Nowy)
                    {
                        createProdukt();
                    }
                    else if(produkt.Akcja == Domen.Akcja.Edycja)
                    {
                        editProduct();
                    }

                }
                else
                {
                    MessageBox.Show("Rekord nie został dodany");
                    //TODO
                }
            }
        }

        private void comboBoxKategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            string wybranaWartosc = comboBoxKategoria.SelectedItem.ToString();
            var klucz = map.FirstOrDefault(x => x.Value.ToString() == wybranaWartosc).Key;
            produkt.Kategoria = new Domen.Kategoria { Id = klucz };

        }


        private async void editProduct()
        {
            var edit = await _databaseOpeartions.UpdateAsync<produkt>(new produkt
            {
                id = produkt.Id,
                nazwa = produkt.Nazwa,
                model = produkt.Model,
                opis = produkt.Opis,
                ilosc_dostepna = produkt.Ilosc,
                cena = produkt.Cena
            });
            edit = await _databaseOpeartions.AddAsync<produkt_kategoria>(new produkt_kategoria
            {
                id = Guid.NewGuid(),
                id_produkt = produkt.Id,
                id_kategoria = produkt.Kategoria.Id
            });
            if (edit)
            {
                MessageBox.Show("Pomyślnie dodano rekord");
                Zamkniety(produkt);
                this.Close();
            }
        }
        private async void createProdukt()
        {
            var create = await _databaseOpeartions.AddAsync<produkt>(new produkt
            {
                id = produkt.Id,
                nazwa = produkt.Nazwa,
                model = produkt.Model,
                opis = produkt.Opis,
                ilosc_dostepna = produkt.Ilosc,
                cena = produkt.Cena
            });
            create = await _databaseOpeartions.AddAsync<produkt_kategoria>(new produkt_kategoria
            {
                id = Guid.NewGuid(),
                id_produkt = produkt.Id,
                id_kategoria = produkt.Kategoria.Id
            });
            if (create)
            {
                MessageBox.Show("Pomyślnie dodano rekord");
                Zamkniety(produkt);
                this.Close();
            }
        }
    }
}
