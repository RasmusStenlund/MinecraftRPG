using Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinecraftRPG
{
    public partial class MinecraftRPG : Form
    {
        private Player _player;
        public MinecraftRPG()
        {
            InitializeComponent();

            _player = new Player();
            _player.CurrentHitPoints = 10;
            _player.MaximumHitPoints = 10;
            _player.Emeralds = 20;
            _player.ExperiencePoints = 0;
            _player.Level = 1;

            lblHitPoints.Text = _player.CurrentHitPoints.ToString();
            lblEmeralds.Text = _player.Emeralds.ToString();
            lblExperience.Text = _player.ExperiencePoints.ToString();
            lblLevel.Text = _player.Level.ToString();
        }
    }
}
