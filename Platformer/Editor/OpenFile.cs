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
using Microsoft.Xna.Framework;
using Platformer.Entities;
using Platformer.Main;

namespace Platformer.Editor
{
    public partial class OpenFile : Form
    {
        MapEditor editor;

        public OpenFile(MapEditor editor)
        {
            InitializeComponent();

            this.editor = editor;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + @"\Levels\" + textBox1.Text + ".txt";

            editor.levelFile = path;

            using (StreamReader reader = new StreamReader(path))
            {
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();

                editor.tileSize = Convert.ToInt32(reader.ReadLine());
            }

            EntityManager.LoadList(textBox1.Text);

            EntityManager.AddObject(new Entities.Button(editor.NewFile), new Vector2(60, 640));
            EntityManager.AddObject(new Entities.Button(editor.OpenFile), new Vector2(120, 640));
            EntityManager.AddObject(new Entities.Button(editor.SaveFile), new Vector2(180, 640));
            EntityManager.AddObject(new Entities.Button(editor.ClearRoom), new Vector2(240, 640));

            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
        }

    }
}
