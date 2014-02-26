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

            richTextBox1.Clear();

            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog() { Filter = "заказ|*.piz" };
            var result = sfd.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                var fileName = sfd.FileName;
                Pizza pz = new Pizza();
                pz.ItemType = new List<Ingredients>();
                if (radioButton1.Checked)
                    pz.doughType = DoughType.Slender;
                else
                    pz.doughType = DoughType.Fat;

                if (checkBox1.Checked)
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
                    pz.ItemType.Add(Ingredients.Green);

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

                checkBox1.Checked = pz.ItemType.Contains(Ingredients.Sausages);
                checkBox2.Checked = pz.ItemType.Contains(Ingredients.Becon);
                checkBox3.Checked = pz.ItemType.Contains(Ingredients.Chiken);
                checkBox4.Checked = pz.ItemType.Contains(Ingredients.Cats);
                checkBox5.Checked = pz.ItemType.Contains(Ingredients.Mushrooms);
                checkBox6.Checked = pz.ItemType.Contains(Ingredients.Green);
                radioButton1.Checked = pz.doughType == DoughType.Slender;
                radioButton2.Checked = pz.doughType == DoughType.Fat;

            }
        }
        public class Pizza
        {
            public DoughType doughType { get; set; }
            public List<Ingredients> ItemType { get; set; }
        }
        public enum DoughType
        {
            Slender,
            Fat
        }
        public enum Ingredients
        {
            Sausages,
            Becon,
            Chiken,
            Cats,
            Mushrooms,
            Green
        }


    }
}
