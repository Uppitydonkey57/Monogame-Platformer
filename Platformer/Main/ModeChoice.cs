using Platformer.Editor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Platformer.Main
{
    public partial class ModeChoice : Form
    {
        public ModeChoice()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            Close();

            using (var game = new Game1())
                game.Run();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();

            using (MapEditor editor = new MapEditor())
                editor.Run();
        }
    }
}
