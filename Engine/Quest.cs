using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Quest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RewardExperiencePoints { get; set; }
        public int RewardEmeralds { get; set; }
        public Item RewardItem { get; set; }
        public List<QuestCompletionItem> QuestCompletionItems { get; set; }

        public Quest( int id, string name, string description, int rewardExperiencePoints, int rewardEmeralds)
        {
            ID = id;
            Name = name;
            Description = description;
            RewardExperiencePoints = rewardExperiencePoints;
            RewardEmeralds = rewardEmeralds;
            QuestCompletionItems = new List<QuestCompletionItem>();
        }
    }
}
