using DAL;
using DomainEntity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnketV2
{
    public partial class Form2 : Form
    {
        public Soru GelenSoru { get; set; }
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox2.Text = GelenSoru.SoruCumlesi;
        }

     
        private void button2_Click(object sender, EventArgs e)
        { //Kaydet
            AnketContext db = new AnketContext(); //Düzenle kısmı db üzerinden gitmeli
            //EF üzerinde bir kayıtta değişiklik yapabilmesi için AnketContext üzerinden geliyorsa mümkün
            var duzenlenecek = db.Sorular.Find(GelenSoru.SoruID);
            duzenlenecek.SoruCumlesi = textBox2.Text;
            //Kayıtta değişiklik olduğunda yapılır.(ID değişmeden)
            db.Entry(duzenlenecek).State = EntityState.Modified;
            db.SaveChanges();
            Form1 f =(Form1) Application.OpenForms["Form1"];
            f.SorularıYenile();
            f.CevaplariYenile();
            this.Close();

        }
    }
}
