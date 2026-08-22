using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class World
    {
        public static readonly List<Item> Items = new List<Item>();
        public static readonly List<Mob> Mobs = new List<Mob>();
        public static readonly List<Quest> Quests = new List<Quest>();
        public static readonly List<Location> Locations = new List<Location>();

        public const int ITEM_ID_WOODEN_SWORD = 1;
        public const int ITEM_ID_ROTTEN_FLESH = 2;
        public const int ITEM_ID_CARROT = 3;
        public const int ITEM_ID_BONE = 4;
        public const int ITEM_ID_ARROW = 5;
        public const int ITEM_ID_STONE_AXE = 6;
        public const int ITEM_ID_GOLDEN_APPLE = 7;
        public const int ITEM_ID_SPIDER_EYE = 8;
        public const int ITEM_ID_STRING = 9;
        public const int ITEM_ID_ADVENTURER_PASS = 10;

        public const int MOB_ID_ZOMBIE = 1;
        public const int MOB_ID_SKELETON = 2;
        public const int MOB_ID_CAVE_SPIDER = 3;

        public const int QUEST_ID_CLEAR_CLERIC_GARDEN = 1;
        public const int QUEST_ID_CLEAR_WHEAT_FIELD = 2;

        public const int LOCATION_ID_HOME = 1;
        public const int LOCATION_ID_VILLAGE_CENTER = 2;
        public const int LOCATION_ID_VILLAGE_GATE = 3;
        public const int LOCATION_ID_CLERIC_HOUSE = 4;
        public const int LOCATION_ID_CLERIC_GARDEN = 5;
        public const int LOCATION_ID_FARMER_HOUSE = 6;
        public const int LOCATION_ID_WHEAT_FIELD = 7;
        public const int LOCATION_ID_BRIDGE = 8;
        public const int LOCATION_ID_ABANDONED_MINESHAFT = 9;

        static World()
        {
            PopulateItems();
            PopulateMobs();
            PopulateQuests();
            PopulateLocations();
        }

        private static void PopulateItems()
        {
            Items.Add(new Weapon(ITEM_ID_WOODEN_SWORD, "Wooden Sword", "Wooden Swords", 0, 5));
            Items.Add(new Item(ITEM_ID_ROTTEN_FLESH, "Rotten Flesh", "Rotten Flesh"));
            Items.Add(new Item(ITEM_ID_CARROT, "Carrot", "Carrots"));
            Items.Add(new Item(ITEM_ID_BONE, "Bone", "Bones"));
            Items.Add(new Item(ITEM_ID_ARROW, "Arrow", "Arrows"));
            Items.Add(new Weapon(ITEM_ID_STONE_AXE, "Stone Axe", "Stone Axes", 3, 10));
            Items.Add(new Consumable(ITEM_ID_GOLDEN_APPLE, "Golden Apple", "Golden Apples", 5));
            Items.Add(new Item(ITEM_ID_SPIDER_EYE, "Spider Eye", "Spider Eyes"));
            Items.Add(new Item(ITEM_ID_STRING, "String", "String"));
            Items.Add(new Item(ITEM_ID_ADVENTURER_PASS, "Adventurer Pass", "Adventurer Passes"));
        }

        private static void PopulateMobs()
        {
            Mob zombie = new Mob(MOB_ID_ZOMBIE, "Zombie", 5, 3, 10, 3, 3);
            zombie.LootTable.Add(new LootItem(ItemByID(ITEM_ID_ROTTEN_FLESH), 75, true));
            zombie.LootTable.Add(new LootItem(ItemByID(ITEM_ID_CARROT), 75, false));

            Mob skeleton = new Mob(MOB_ID_SKELETON, "Skeleton", 5, 3, 10, 3, 3);
            skeleton.LootTable.Add(new LootItem(ItemByID(ITEM_ID_ARROW), 75, true));
            skeleton.LootTable.Add(new LootItem(ItemByID(ITEM_ID_BONE), 75, false));

            Mob caveSpider = new Mob(MOB_ID_CAVE_SPIDER, "Cave Spider", 20, 5, 40, 10, 10);
            caveSpider.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SPIDER_EYE), 75, true));
            caveSpider.LootTable.Add(new LootItem(ItemByID(ITEM_ID_STRING), 25, false));

            Mobs.Add(zombie);
            Mobs.Add(skeleton);
            Mobs.Add(caveSpider);
        }

        private static void PopulateQuests()
        {

        }

        private static void PopulateLocations()
        {

        }

        public static Item ItemByID(int id)
        {
            foreach(Item item in Items)
            {
                if(item.ID == id)
                {
                    return item;
                }
            }

            return null;
        }

        public static Mob MobByID(int id)
        {

        }

        public static Quest QuestByID(int id)
        {

        }

        public static Location LocationByID(int id)
        {

        }
    }
}
