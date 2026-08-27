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
using System.Reflection;
using Engine;


namespace MinecraftRPG
{
    public partial class WorldMap : Form
    {
        readonly Assembly _thisAssembly = Assembly.GetExecutingAssembly();
        public WorldMap(Player player)
        {
            InitializeComponent();

            SetImage(pic_0_2, player.LocationsVisited.Contains(5) ? "cleric_garden" : "undiscovered");
            SetImage(pic_1_2, player.LocationsVisited.Contains(4) ? "cleric_house" : "undiscovered");
            SetImage(pic_1_4, player.LocationsVisited.Contains(9) ? "cave_entrance" : "undiscovered");
            SetImage(pic_1_5, player.LocationsVisited.Contains(10) ? "mineshaft" : "undiscovered");
            SetImage(pic_2_0, player.LocationsVisited.Contains(7) ? "wheat_field" : "undiscovered");
            SetImage(pic_2_1, player.LocationsVisited.Contains(6) ? "farmer_house" : "undiscovered");
            SetImage(pic_2_2, player.LocationsVisited.Contains(2) ? "village_center" : "undiscovered");
            SetImage(pic_2_3, player.LocationsVisited.Contains(3) ? "village_gate" : "undiscovered");
            SetImage(pic_2_4, player.LocationsVisited.Contains(8) ? "forest" : "undiscovered");
            SetImage(pic_3_2, player.LocationsVisited.Contains(1) ? "home" : "undiscovered");
        }

        private void SetImage(PictureBox pictureBox, string imageName)
        {
            using (Stream resourceStream = _thisAssembly.GetManifestResourceStream(_thisAssembly.GetName().Name + ".Images." + imageName + ".png"))
            {
                if (resourceStream!= null)
                {
                    pictureBox.Image = new Bitmap(resourceStream);
                }
            }
        }
    }
}
