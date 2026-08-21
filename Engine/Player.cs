using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Player : LivingCreature
    {
        public int Emeralds { get; set; }
        public int ExperiencePoints { get; set; }
        public int Level { get; set; }

        public Player(int currentHitPoints, int maximumHitPoints, int emeralds, int experiencePoints, int level) : base(currentHitPoints, maximumHitPoints)
        {
            Emeralds = emeralds;
            ExperiencePoints = experiencePoints;
            Level = level;
        }
    }
}
