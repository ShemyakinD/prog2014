using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using DevExpress.XtraReports.UI;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            checkBox5.Checked = false;
            checkBox6.Checked = false;

            radioButton1.Checked = false;
            radioButton2.Checked = false;
            listBox1.Items.Clear();
        }

        
        private Pizza CreatePizza()
        {
            Pizza pz = new Pizza();
            pz.ingredient = new List<Ingredients>();
            if (radioButton1.Checked)
                pz.doughType = DoughType.Slender;
            else
                pz.doughType = DoughType.Fat;


          
                  foreach (Ingredients pizza in listBox1.Items)
                {
                    pz.ingredient.Add(pizza);
                }
            
        
                   
          /*  if (checkBox1.Checked)
                pz.ItemType.Add(Ingredients.Sausages);
            if (checkBox2.Checked)
                pz.ItemType.Add(Ingredients.Becon);
            if (checkBox3.Checked)
                pz.ItemType.Add(Ingredients.Chiken);
            if (checkBox4.Checked)
                pz.ItemType.Add(Ingredients.Cats);
            if (checkBox5.Checked)
                pz.ItemType.Add(Ingredients.Mushrooms);
            if (checkBox6.Checked)
                pz.ItemType.Add(Ingredients.Green);*/

            return pz;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog() { Filter = "заказ|*.piz" };
            var result = sfd.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var pz = CreatePizza();
                var fileName = sfd.FileName;
               /*Pizza pz = new Pizza();
                pz.ItemType = new List<Ingredients>();*/
                if (radioButton1.Checked)
                    pz.doughType = DoughType.Slender;
                else
                    pz.doughType = DoughType.Fat;

                /*if (checkBox1.Checked)
                    pz.ItemType.Add(Ingredients.Sausages);
                if (checkBox2.Checked)
                    pz.ItemType.Add(Ingredients.Becon);
                if (checkBox3.Checked)
                    pz.ItemType.Add(Ingredients.Chiken);
                if (checkBox4.Checked)
                    pz.ItemType.Add(Ingredients.Cats);
                if (checkBox5.Checked)
                    pz.ItemType.Add(Ingredients.Mushrooms);
                if (checkBox6.Checked)
                    pz.ItemType.Add(Ingredients.Green);*/

                XmlSerializer xs = new XmlSerializer(typeof(Pizza));
                var fileStream = File.Create(fileName);
                xs.Serialize(fileStream, pz);
                fileStream.Close();
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog() { Filter = "заказ|*.piz" };
            var result = ofd.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var xs = new XmlSerializer(typeof(Pizza));
                var file = File.Open(ofd.FileName, FileMode.Open);
                var pz = (Pizza)xs.Deserialize(file);
                file.Close();

                listBox1.Items.Clear();
                foreach (var pizza in pz.ingredient)
                {
                    listBox1.Items.Add(pizza);
                }
              /*  checkBox1.Checked = pz.ItemType.Contains(Ingredients.Sausages);
                checkBox2.Checked = pz.ItemType.Contains(Ingredients.Becon);
                checkBox3.Checked = pz.ItemType.Contains(Ingredients.Chiken);
                checkBox4.Checked = pz.ItemType.Contains(Ingredients.Cats);
                checkBox5.Checked = pz.ItemType.Contains(Ingredients.Mushrooms);
                checkBox6.Checked = pz.ItemType.Contains(Ingredients.Green);
                radioButton1.Checked = pz.doughType == DoughType.Slender;
                radioButton2.Checked = pz.doughType == DoughType.Fat;*/

            }
        }
        public class Pizza
        {
            public DoughType doughType { get; set; }
            public List<Ingredients> ingredient { get; set; }

            [XmlIgnore]
            public string Caption
            {
                get
                {
                    if (doughType == DoughType.Slender)
                        return "Тонкое тесто";
                    return "Пышное тесто";

                }
            }
        }
        public class Ingredients
        { 
        public bool Sausages { get; set; }
        public bool Becon { get; set; }
        public bool Chiken { get; set; }
        public bool Cats { get; set; }
        public bool Mushrooms { get; set; }
        public bool Green { get; set; }

               
            [XmlIgnore]
        public string Description { get { return this.ToString(); } }
        public override string ToString()
        {
            var s = "Ваша пицца содержит";
            if (Sausages)
                s += " сосиски";
            if (Becon)
                s += " бекон";
            if (Chiken)
                s += " курицу";
            if (Cats)
                s += " котиков";
            if (Mushrooms)
                s += " грибы";
            if (Green)
                s += " траву";
            return s;
        }
        }
        public enum DoughType
        {
            Slender,
            Fat
        }
        public enum ingredients
        {
            Sausages,
            Becon,
            Chiken,
            Cats,
            Mushrooms,
            Green
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var pt = new PizzaReport();
            Pizza pz = CreatePizza();
            pt.DataSource = new BindingSource() { DataSource = pz };
            pt.ShowPreview();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var ing = new Ingredients
            {
                Sausages = checkBox1.Checked,
                Becon = checkBox2.Checked,
                Chiken = checkBox3.Checked,
                Cats = checkBox4.Checked,
                Mushrooms = checkBox5.Checked,
                Green = checkBox6.Checked,
            };
            if (listBox1.Items.Count < 1)
            { listBox1.Items.Add(ing); }
        }

       
    }

        }
    

