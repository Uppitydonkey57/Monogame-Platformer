using Platformer.Entities;
using Platformer.Main;
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
    public partial class SaveFile : Form
    {
        MapEditor editor;

        public SaveFile(MapEditor editor)
        {
            InitializeComponent();

            this.editor = editor;

            if (editor.levelFile != null)
                using (StreamReader reader = File.OpenText(editor.levelFile))
                    label1.Text = "Would you like to save the Room " + reader.ReadLine() + "?";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(editor.levelFile))
            {
                string name;

                string width;
                string height;

                string tileSize;

                using (StreamReader reader = File.OpenText(editor.levelFile))
                {
                    name = reader.ReadLine();

                    width = reader.ReadLine();
                    height = reader.ReadLine();

                    tileSize = reader.ReadLine();
                }

                using (StreamWriter writer = File.CreateText(editor.levelFile))
                {
                    writer.WriteLine(name);

                    writer.WriteLine(width);
                    writer.WriteLine(height);

                    writer.WriteLine(tileSize);

                    foreach (Entity entity in EntityManager.entities)
                    {
                        if (entity != null)
                        {
                            if (entity.Tag != "UI")
                            {
                                string newData = entity.GetType().Name + " " + entity.Position.X + " " + entity.Position.Y;

                                if (entity.Sprite.tilemap != null)
                                {
                                    newData += (" " + entity.Sprite.tilemap.whichTileX + " " + entity.Sprite.tilemap.whichTileY);
                                }

                                writer.WriteLine(newData);
                            }
                        }
                    }
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
