using DAL;
using DomainEntity.Models;
using DomainEntity.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnketV2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        AnketContext db = new AnketContext(); //veritabanımız
        private void button2_Click(object sender, EventArgs e)
        {
            Soru s = new Soru();
            s.SoruCumlesi = textBox2.Text;
            db.Sorular.Add(s);
            db.SaveChanges();
            MessageBox.Show("Eklendi");
            SorularıYenile();
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SorularıYenile();
            CevaplariYenile();
           
        }
        public void CevaplariYenile()
        {
            dataGridView2.DataSource = null;
            //dataGridView2.DataSource = db.Cevaplar.ToList();
            dataGridView2.DataSource = db.Cevaplar.Select(x => new CevapViewModel()
            {
                CevapID = x.CevapID,
                AdSoyad = x.CevabiVerenKisi.AdSoyad,
                Cevap = x.Yanit.ToString(),
                Soru=x.Sorusu.SoruCumlesi
            }).ToList();

        }
        public void SorularıYenile()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = db.Sorular.ToList();
            flowLayoutPanel1.AutoScroll = true; // çok soru eklendikçe aşağı kaydırmak için
            flowLayoutPanel1.Controls.Clear();

            foreach (Soru soru in db.Sorular)
            {
                Label lbl = new Label();
                lbl.AutoSize = true;
                lbl.Text = soru.SoruCumlesi;
                flowLayoutPanel1.Controls.Add(lbl);

                RadioButton r1 = new RadioButton();
                r1.Name = "Soru_" + soru.SoruID;
                r1.Text = "Evet";
                r1.Height = 50;
                //flowLayoutPanel1.Controls.Add(r1);
                RadioButton r2 = new RadioButton();
                r2.Name = "Soru_" + soru.SoruID;
                r2.Text = "Hayır";
                r2.Height = 50;

                FlowLayoutPanel p = new FlowLayoutPanel();
                p.Size = new Size(300, 100);
                p.AutoScroll = false;
                p.AutoSize = false;
                p.Controls.Add(r1);
                p.Controls.Add(r2);
                flowLayoutPanel1.Controls.Add(p);
                flowLayoutPanel1.SetFlowBreak(p, true);
                // flowLayoutPanel1.SetFlowBreak(r2,true);

               /* ComboBox c1 = new ComboBox();
                * c1.Name="Soru_"+soru.SoruID;
                c1.Items.Add("Evet");
                c1.Items.Add("Hayır");
                flowLayoutPanel1.Controls.Add(c1);
                ==combobox ile böyle yazılır.
                */
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            foreach (Control item in flowLayoutPanel1.Controls)
            {
                if (item is ComboBox)
                {
                   string soruID=item.Name.Replace("Soru_","");
                   int SID=Convert.toInt32(soruID);
                   int y=((Combobox))item.SelectedIndex+1)%2;
                   c.Yanit=(Yanit)y;
                   
                }
            }
            */
           
            foreach (Control pnl in flowLayoutPanel1.Controls)
            {
                if (pnl is FlowLayoutPanel)
                {
                    foreach (RadioButton item in ((FlowLayoutPanel)pnl).Controls)
                    {
                        RadioButton r = item;
                            if (r.Checked)
                            {
                                string soruID = item.Name.Replace("Soru_", "");
                                int SID = Convert.ToInt32(soruID);
                                Soru s = new Soru();
                                Cevap c = new Cevap();
                                c.SoruID = SID;
                                c.Yanit = r.Text == "Evet" ? Yanit.Evet : Yanit.Hayir;

                                Kisi k = db.Kisiler.Where(x => x.AdSoyad == textBox1.Text).FirstOrDefault();
                                if (k != null)
                                    c.KisiID = k.KisiID;
                                else
                                {
                                    k = new Kisi();
                                    k.AdSoyad = textBox1.Text;
                                    db.Kisiler.Add(k);
                                    db.SaveChanges();
                                    c.KisiID = k.KisiID;
                                }

                                db.Cevaplar.Add(c);
                                db.SaveChanges();
                                CevaplariYenile();
                            }

                        }
                    } 
                }
            }

        private void button3_Click(object sender, EventArgs e)
        {//Soru Silme
            if (dataGridView1.SelectedRows.Count == 0)
                MessageBox.Show("Soru Seçiniz:");
            else
            {
                foreach (DataGridViewRow item in dataGridView1.SelectedRows)
                {
                    int soruId = (int)item.Cells[0].Value;
                    Soru silinecek = db.Sorular.Find(soruId);
                    db.Sorular.Remove(silinecek);
                }
                MessageBox.Show("Soru silindi");
                db.SaveChanges();
                SorularıYenile();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                MessageBox.Show("Düzenlenecek Soruyu Seçiniz:");
            else
            {
                Form2 f = new Form2();
                int sID = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                Soru duzenleSoru = db.Sorular.Find(sID);//Find ID üzerinden kayıt bulmaya yarar.
                f.GelenSoru =duzenleSoru;
                f.Show();
            }
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
                MessageBox.Show("Silinecek Cevabı Seçiniz:");
            else
            {
                foreach (DataGridViewRow cevap in dataGridView2.SelectedRows)
                {
                    int cevapID = (int)cevap.Cells[3].Value;
                    Cevap cevapsilinecek = db.Cevaplar.Find(cevapID);
                    db.Cevaplar.Remove(cevapsilinecek);
                }
                MessageBox.Show("Cevap Silindi");
                db.SaveChanges();
                CevaplariYenile();
            }
        }
    }
    }

