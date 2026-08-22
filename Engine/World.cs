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
        public const int LOCATION_ID_CAVE_ENTRANCE = 8;
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
            Quest clearClericGarden = new Quest(QUEST_ID_CLEAR_CLERIC_GARDEN, "Clear the cleric's garden", "Kill zombies in the cleric's garden and bring back 3 carrots to help the cleric make potions. You will recieve a golden apple and 10 emeralds", 20, 10);
            clearClericGarden.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_CARROT), 3));
            clearClericGarden.RewardItem = ItemByID(ITEM_ID_GOLDEN_APPLE);

            Quest clearWheatField = new Quest(QUEST_ID_CLEAR_WHEAT_FIELD, "Clear the wheat field", "Kill skeletons in the wheat field and bring back 3 bones so the farmer can make bone meal. You will recieve an adventurer's pass and 20 emeralds", 20, 20);
            clearWheatField.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_BONE), 3));
            clearWheatField.RewardItem = ItemByID(ITEM_ID_ADVENTURER_PASS);

            Quests.Add(clearClericGarden);
            Quests.Add(clearWheatField);
        }

        private static void PopulateLocations()
        {
            Location home = new Location(LOCATION_ID_HOME, "Home", "Your home. A simple hut with some chests, a furnace and a crafting table.");

            Location villageCenter = new Location(LOCATION_ID_VILLAGE_CENTER, "Village center", "You see a bell.");

            Location clericHouse = new Location(LOCATION_ID_CLERIC_HOUSE, "Cleric's house", "There are a lot of brewing stands and cauldrons.");
            clericHouse.QuestAvailableHere = QuestByID(QUEST_ID_CLEAR_CLERIC_GARDEN);

            Location clericGarden = new Location(LOCATION_ID_CLERIC_GARDEN, "Cleric's garden", "Full of nether wart.");
            clericGarden.MobLivingHere = MobByID(MOB_ID_ZOMBIE);

            Location farmerHouse = new Location(LOCATION_ID_FARMER_HOUSE, "Farmer's house", "There are composters and barrels full of wheat.");
            farmerHouse.QuestAvailableHere = QuestByID(QUEST_ID_CLEAR_WHEAT_FIELD);

            Location wheatField = new Location(LOCATION_ID_WHEAT_FIELD, "Wheat field", "Fields and fields of wheat.");
            wheatField.MobLivingHere = MobByID(MOB_ID_SKELETON);

            Location villageGate = new Location(LOCATION_ID_VILLAGE_GATE, "Village gate", "There are two massive iron golems standing guard.", ItemByID(ITEM_ID_ADVENTURER_PASS));

            Location caveEntrance = new Location(LOCATION_ID_CAVE_ENTRANCE, "Cave entrance", "A dark cave entrance in the side of a mountain.");

            Location abandonedMineshaft = new Location(LOCATION_ID_ABANDONED_MINESHAFT, "Abandoned mineshaft", "You see cobwebs and old minecart tracks.");
            abandonedMineshaft.MobLivingHere = MobByID(MOB_ID_CAVE_SPIDER);

            home.LocationToNorth = villageCenter;

            villageCenter.LocationToNorth = clericHouse;
            villageCenter.LocationToSouth = home;
            villageCenter.LocationToEast = villageGate;
            villageCenter.LocationToWest = farmerHouse;

            farmerHouse.LocationToEast = villageCenter;
            farmerHouse.LocationToWest = wheatField;

            wheatField.LocationToEast = farmerHouse;

            clericHouse.LocationToNorth = clericGarden;
            clericHouse.LocationToSouth = villageCenter;

            clericGarden.LocationToSouth = clericHouse;

            villageGate.LocationToEast = caveEntrance;
            villageGate.LocationToWest = villageCenter;

            caveEntrance.LocationToEast = abandonedMineshaft;
            caveEntrance.LocationToWest = villageGate;

            abandonedMineshaft.LocationToWest = caveEntrance;

            Locations.Add(home);
            Locations.Add(villageCenter);
            Locations.Add(villageGate);
            Locations.Add(clericHouse);
            Locations.Add(clericGarden);
            Locations.Add(farmerHouse);
            Locations.Add(wheatField);
            Locations.Add(caveEntrance);
            Locations.Add(abandonedMineshaft);
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
            foreach(Mob mob in Mobs)
            {
                if(mob.ID == id)
                {
                    return mob;
                }
            }

            return null;
        }

        public static Quest QuestByID(int id)
        {
            foreach(Quest quest in Quests)
            {
                if(quest.ID == id)
                {
                    return quest;
                }
            }

            return null;
        }

        public static Location LocationByID(int id)
        {
            foreach(Location location in Locations)
            {
                if(location.ID == id)
                {
                    return location;
                }
            }

            return null;
        }
    }
}
