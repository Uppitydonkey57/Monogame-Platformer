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

namespace Platformer.Editor
{
    public partial class NewFile : Form
    {
        MapEditor editor;

        public NewFile(MapEditor editor)
        {
            InitializeComponent();

            this.editor = editor;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = editor.levelFile = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + @"\Levels\" + textBox1.Text + ".txt";

            string name = textBox1.Text;

            string width = textBox3.Text;
            string height = textBox2.Text;

            string tileSize = textBox4.Text;

            editor.tileSize = Convert.ToInt32(textBox4.Text);

            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(name);

                    sw.WriteLine(width);
                    sw.WriteLine(height);

                    sw.WriteLine(tileSize);

                    sw.Close();
                }
            }

            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
