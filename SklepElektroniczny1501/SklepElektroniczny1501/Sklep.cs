using Autofac;
using SklepElektroniczny1501.Implementations;
using SklepElektroniczny1501.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SklepElektroniczny1501
{
    public partial class Sklep : Form
    {
        //a.	Sklep (przejście do ewidencji produktów lub zamówień)

        private readonly Autofac.IContainer _container;
        public Sklep(Autofac.IContainer container)
        {
            _container = container;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var produkty = new Produkty(
                _container.Resolve<IDatabaseOpeartions>(), _container.Resolve<IMapper>());
            produkty.Show();

        }
    }
}
