    using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Player : LivingCreature
    {
        public int Emeralds { get; set; }
        public int ExperiencePoints { get; private set; }
        public int Level
        {
            get { return ((ExperiencePoints / 100) + 1); }
        }
        public Location CurrentLocation { get; set; }
        public Weapon CurrentWeapon { get; set; }
        public List<InventoryItem> Inventory { get; set; }
        public List<PlayerQuest> Quests { get; set; }

        public Player(int currentHitPoints, int maximumHitPoints, int emeralds, int experiencePoints) : base(currentHitPoints, maximumHitPoints)
        {
            Emeralds = emeralds;
            ExperiencePoints = experiencePoints;
            Inventory = new List<InventoryItem>();
            Quests = new List<PlayerQuest>();
        }

        public void AddExperiencePoints(int experiencePointsToAdd)
        {
            int originalLevel = Level;

            ExperiencePoints += experiencePointsToAdd;

            if (Level > originalLevel)
            {
                MaximumHitPoints = (Level * 5) + 15;
                CurrentHitPoints = MaximumHitPoints;
            }
        }

        public bool HasRequiredItemToEnterLocation(Location location)
        {
            if (location.ItemRequiredToEnter == null)
            {
                return true;
            }

            return Inventory.Exists(ii => ii.Details.ID == location.ItemRequiredToEnter.ID);
        }

        public bool HasQuest(Quest quest)
        {
            return Quests.Exists(pq => pq.Details.ID == quest.ID);
        }

        public bool CompletedQuest(Quest quest)
        {
            foreach(PlayerQuest playerQuest in Quests)
            {
                if(playerQuest.Details.ID == quest.ID)
                {
                    return playerQuest.IsCompleted;
                }
            }

            return false;
        }

        public bool HasAllQuestCompletionItems(Quest quest)
        {
            foreach(QuestCompletionItem qci in quest.QuestCompletionItems)
            {
                if (!Inventory.Exists(ii => ii.Details.ID == qci.Details.ID && ii.Quantity >= qci.Quantity))
                {
                    return false;
                }
            }

            return true;
        }

        public void RemoveQuestCompletionItems(Quest quest)
        {
            foreach(QuestCompletionItem qci in quest.QuestCompletionItems)
            {
                InventoryItem item = Inventory.SingleOrDefault(ii => ii.Details.ID == qci.Details.ID);

                if (item != null)
                {
                    item.Quantity -= qci.Quantity;
                }
            }
        }

        public void AddItemToInventory(Item itemToAdd)
        {
            InventoryItem item = Inventory.SingleOrDefault(ii => ii.Details.ID == itemToAdd.ID);

            if (item == null)
            {
                Inventory.Add(new InventoryItem(itemToAdd, 1));
            }
            else
            {
                item.Quantity++;
            }
        }

        public void MarkQuestCompleted(Quest quest)
        {
            PlayerQuest playerQuest = Quests.SingleOrDefault(pq => pq.Details.ID == quest.ID);

            if (playerQuest != null)
            {
                playerQuest.IsCompleted = true;
            }
        }
    }
}
