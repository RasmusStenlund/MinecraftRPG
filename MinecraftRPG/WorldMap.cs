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
    

namespace MinecraftRPG
{
    public partial class WorldMap : Form
    {
        readonly Assembly _thisAssembly = Assembly.GetExecutingAssembly();
        public WorldMap()
        {
            InitializeComponent();

            SetImage(pic_0_2, "cleric_garden");
            SetImage(pic_1_2, "cleric_house");
            SetImage(pic_1_4, "cave_entrance");
            SetImage(pic_1_5, "mineshaft");
            SetImage(pic_2_0, "wheat_field");
            SetImage(pic_2_1, "farmer_house");
            SetImage(pic_2_2, "village_center");
            SetImage(pic_2_3, "village_gate");
            SetImage(pic_2_4, "forest");
            SetImage(pic_3_2, "home");
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
