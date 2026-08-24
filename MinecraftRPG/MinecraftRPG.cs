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
        private Mob _currentMob;
        public MinecraftRPG()
        {
            InitializeComponent();

            _player = new Player(10, 10, 20, 0);
            MoveTo(World.LocationByID(World.LOCATION_ID_HOME));
            _player.Inventory.Add(new InventoryItem(World.ItemByID(World.ITEM_ID_WOODEN_SWORD), 1));

            UpdatePlayerStats();
        }

        private void btnNorth_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToNorth);
        }

        private void btnSouth_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToSouth);
        }

        private void btnEast_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToEast);
        }

        private void btnWest_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToWest);
        }

        private void MoveTo(Location newLocation)
        {
            if (!_player.HasRequiredItemToEnterLocation(newLocation))   
            {
                rtbMessages.Text += "You must have a " + newLocation.ItemRequiredToEnter.Name + " to enter this location." + Environment.NewLine;
                ScrollToBottomOfMessages();
                return;
            }

            _player.CurrentLocation = newLocation;

            btnNorth.Visible = (newLocation.LocationToNorth != null);
            btnSouth.Visible = (newLocation.LocationToSouth != null);
            btnEast.Visible = (newLocation.LocationToEast != null);
            btnWest.Visible = (newLocation.LocationToWest != null);

            rtbLocation.Text = newLocation.Name + Environment.NewLine;
            rtbLocation.Text += newLocation.Description + Environment.NewLine;

            if (newLocation.QuestAvailableHere != null)
            {
                bool playerAlreadyHasQuest = _player.HasQuest(newLocation.QuestAvailableHere);
                bool playerAlreadyCompletedQuest = _player.CompletedQuest(newLocation.QuestAvailableHere);

                if (playerAlreadyHasQuest)
                {
                    if (!playerAlreadyCompletedQuest)
                    {
                        bool playerHasAllItemsToCompleteQuest = _player.HasAllQuestCompletionItems(newLocation.QuestAvailableHere);

                        if (playerHasAllItemsToCompleteQuest)
                        {
                            rtbMessages.Text += Environment.NewLine;
                            rtbMessages.Text += "You complete the '" + newLocation.QuestAvailableHere.Name + "' quest." + Environment.NewLine;

                            _player.RemoveQuestCompletionItems(newLocation.QuestAvailableHere);

                            rtbMessages.Text += "You receive: " + Environment.NewLine;
                            rtbMessages.Text += newLocation.QuestAvailableHere.RewardExperiencePoints.ToString() + " experience points" + Environment.NewLine;
                            rtbMessages.Text += newLocation.QuestAvailableHere.RewardEmeralds.ToString() + " emeralds" + Environment.NewLine;
                            rtbMessages.Text += newLocation.QuestAvailableHere.RewardItem.Name + Environment.NewLine;
                            rtbMessages.Text += Environment.NewLine;

                            ScrollToBottomOfMessages();

                            _player.ExperiencePoints += newLocation.QuestAvailableHere.RewardExperiencePoints;
                            _player.Emeralds += newLocation.QuestAvailableHere.RewardEmeralds;

                            _player.AddItemToInventory(newLocation.QuestAvailableHere.RewardItem);
                            _player.MarkQuestCompleted(newLocation.QuestAvailableHere);

                            UpdatePlayerStats();
                        }
                    }
                }

                else
                {
                    rtbMessages.Text += "You receive the " + newLocation.QuestAvailableHere.Name + " quest." + Environment.NewLine;
                    rtbMessages.Text += newLocation.QuestAvailableHere.Description + Environment.NewLine;
                    rtbMessages.Text += "To complete it, return with:" + Environment.NewLine;
                    ScrollToBottomOfMessages();

                    foreach (QuestCompletionItem qci in newLocation.QuestAvailableHere.QuestCompletionItems)
                    {
                        if(qci.Quantity == 1)
                        {
                            rtbMessages.Text += qci.Quantity.ToString() + " " + qci.Details.Name + Environment.NewLine;
                        }
                        else
                        {
                            rtbMessages.Text += qci.Quantity.ToString() + " " + qci.Details.NamePlural + Environment.NewLine;
                        }
                    }
                    rtbMessages.Text += Environment.NewLine;
                    ScrollToBottomOfMessages();
                    _player.Quests.Add(new PlayerQuest(newLocation.QuestAvailableHere));
                }
            }

            if (newLocation.MobLivingHere != null)
            {
                rtbMessages.Text += "You see a " + newLocation.MobLivingHere.Name + Environment.NewLine;
                ScrollToBottomOfMessages();
                Mob standardMob = World.MobByID(newLocation.MobLivingHere.ID);

                _currentMob = new Mob(standardMob.ID, standardMob.Name, standardMob.MaximumDamage, standardMob.RewardExperiencePoints, standardMob.RewardEmeralds, standardMob.CurrentHitPoints, standardMob.MaximumHitPoints);

                foreach(LootItem lootItem in standardMob.LootTable)
                {
                    _currentMob.LootTable.Add(lootItem);
                }

                cboWeapons.Visible = true;
                cboConsumables.Visible = true;
                btnUseWeapon.Visible = true;
                btnUseConsumable.Visible = true;
            }
            else
            {
                _currentMob = null;

                cboWeapons.Visible = false;
                cboConsumables.Visible = false;
                btnUseWeapon.Visible = false;
                btnUseConsumable.Visible = false;
            }

            UpdateInventoryListInUI();
            UpdateQuestListInUI();
            UpdateWeaponListInUI();
            UpdateConsumableListInUI();
        }

        private void UpdateInventoryListInUI()
        {
            dgvInventory.RowHeadersVisible = false;

            dgvInventory.ColumnCount = 2;
            dgvInventory.Columns[0].Name = "Name";
            dgvInventory.Columns[0].Width = 197;
            dgvInventory.Columns[1].Name = "Quantity";

            dgvInventory.Rows.Clear();

            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Quantity > 0)
                {
                    dgvInventory.Rows.Add(new[] { inventoryItem.Details.Name, inventoryItem.Quantity.ToString() });
                }
            }
        }

        private void UpdateQuestListInUI()
        {
            dgvQuests.RowHeadersVisible = false;

            dgvQuests.ColumnCount = 2;
            dgvQuests.Columns[0].Name = "Name";
            dgvQuests.Columns[0].Width = 197;
            dgvQuests.Columns[1].Name = "Done?";

            dgvQuests.Rows.Clear();

            foreach (PlayerQuest playerQuest in _player.Quests)
            {
                dgvQuests.Rows.Add(new[] { playerQuest.Details.Name, playerQuest.IsCompleted.ToString() });
            }
        }

        private void UpdateWeaponListInUI()
        {
            List<Weapon> weapons = new List<Weapon>();

            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Details is Weapon)
                {
                    if (inventoryItem.Quantity > 0)
                    {
                        weapons.Add((Weapon)inventoryItem.Details);
                    }
                }
            }

            if (weapons.Count == 0)
            {
                cboWeapons.Visible = false;
                btnUseWeapon.Visible = false;
            }
            else
            {
                cboWeapons.DataSource = weapons;
                cboWeapons.DisplayMember = "Name";
                cboWeapons.ValueMember = "ID";
                cboWeapons.SelectedIndex = 0;
            }
        }

        private void UpdateConsumableListInUI()
        {
            List<Consumable> consumables = new List<Consumable>();

            foreach (InventoryItem inventoryItem in _player.Inventory)
            {
                if (inventoryItem.Details is Consumable)
                {
                    if (inventoryItem.Quantity > 0)
                    {
                        consumables.Add((Consumable)inventoryItem.Details);
                    }
                }
            }

            if (consumables.Count == 0)
            {
                cboConsumables.Visible = false;
                btnUseConsumable.Visible = false;
            }
            else
            {
                cboConsumables.DataSource = consumables;
                cboConsumables.DisplayMember = "Name";
                cboConsumables.ValueMember = "ID";
                cboConsumables.SelectedIndex = 0;
            }
        }

        private void btnUseWeapon_Click(object sender, EventArgs e)
        {
            Weapon currentWeapon = (Weapon)cboWeapons.SelectedItem;
            int damageToMob = RandomNumberGenerator.NumberBetween(currentWeapon.MinimumDamage, currentWeapon.MaximumDamage);

            _currentMob.CurrentHitPoints -= damageToMob;

            rtbMessages.Text += "You hit the " + _currentMob.Name + " for " + damageToMob.ToString() + " points." + Environment.NewLine;
            ScrollToBottomOfMessages();

            if (_currentMob.CurrentHitPoints <= 0)
            {
                rtbMessages.Text += Environment.NewLine;
                rtbMessages.Text += "You defeated the " + _currentMob.Name + Environment.NewLine;

                _player.ExperiencePoints += _currentMob.RewardExperiencePoints;
                rtbMessages.Text += "You recieve " + _currentMob.RewardExperiencePoints + " experience points" + Environment.NewLine;

                _player.Emeralds += _currentMob.RewardEmeralds;
                rtbMessages.Text += "You recieve " + _currentMob.RewardEmeralds + " emeralds" + Environment.NewLine;

                ScrollToBottomOfMessages();

                List<InventoryItem> lootedItems = new List<InventoryItem>();

                foreach(LootItem lootItem in _currentMob.LootTable)
                {
                    if(RandomNumberGenerator.NumberBetween(1, 100) <= lootItem.DropPercentage)
                    {
                        lootedItems.Add(new InventoryItem(lootItem.Details, 1));
                    }
                }

                if(lootedItems.Count == 0)
                {
                    foreach(LootItem lootItem in _currentMob.LootTable)
                    {
                        if (lootItem.IsDefaultItem)
                        {
                            lootedItems.Add(new InventoryItem(lootItem.Details, 1));
                        }
                    }
                }

                foreach(InventoryItem inventoryItem in lootedItems)
                {
                    _player.AddItemToInventory(inventoryItem.Details);

                    if (inventoryItem.Quantity == 1)
                    {
                        rtbMessages.Text += "You loot " + inventoryItem.Quantity.ToString() + " " + inventoryItem.Details.Name + Environment.NewLine;
                    }
                    else
                    {
                        rtbMessages.Text += "You loot " + inventoryItem.Quantity.ToString() + " " + inventoryItem.Details.NamePlural + Environment.NewLine;
                    }
                }

                UpdatePlayerStats();

                UpdateInventoryListInUI();
                UpdateWeaponListInUI();
                UpdateConsumableListInUI();

                rtbMessages.Text += Environment.NewLine;
                ScrollToBottomOfMessages();
                MoveTo(_player.CurrentLocation);
            }
            else
            {
                int damageToPlayer = RandomNumberGenerator.NumberBetween(0, _currentMob.MaximumDamage);
                rtbMessages.Text += "The " + _currentMob.Name + " did " + damageToPlayer.ToString() + " points of damage." + Environment.NewLine;
                ScrollToBottomOfMessages();
                _player.CurrentHitPoints -= damageToPlayer;
                UpdatePlayerStats();

                if (_player.CurrentHitPoints <= 0)
                {
                    rtbMessages.Text += "The " + _currentMob.Name + " killed you." + Environment.NewLine;
                    ScrollToBottomOfMessages();
                    MoveTo(World.LocationByID(World.LOCATION_ID_HOME));
                    _player.CurrentHitPoints = _player.MaximumHitPoints;
                    UpdatePlayerStats();
                }
            }
        }

        private void btnUseConsumable_Click(object sender, EventArgs e)
        {
            Consumable consumable = (Consumable)cboConsumables.SelectedItem;

            _player.CurrentHitPoints += consumable.AmountToHeal;

            if (_player.CurrentHitPoints > _player.MaximumHitPoints)
            {
                _player.CurrentHitPoints = _player.MaximumHitPoints;
            }

            foreach(InventoryItem ii in _player.Inventory)
            {
                if(ii.Details.ID == consumable.ID)
                {
                    ii.Quantity -= 1;
                    break;
                }
            }

            rtbMessages.Text += "You eat a " + consumable.Name + Environment.NewLine;
            ScrollToBottomOfMessages();

            int damageToPlayer = RandomNumberGenerator.NumberBetween(0, _currentMob.MaximumDamage);
            rtbMessages.Text += "The " + _currentMob.Name + " did " + damageToPlayer.ToString() + " points of damage." + Environment.NewLine;
            ScrollToBottomOfMessages();
            _player.CurrentHitPoints -= damageToPlayer;

            if (_player.CurrentHitPoints <= 0)
            {
                rtbMessages.Text += "The " + _currentMob.Name + " killed you." + Environment.NewLine;
                ScrollToBottomOfMessages();

                MoveTo(World.LocationByID(World.LOCATION_ID_HOME));
                _player.CurrentHitPoints = _player.MaximumHitPoints;
                UpdatePlayerStats();
            }

            UpdatePlayerStats();
            UpdateInventoryListInUI();
            UpdateConsumableListInUI();
        }

        private void ScrollToBottomOfMessages()
        {
            rtbMessages.SelectionStart = rtbMessages.Text.Length;
            rtbMessages.ScrollToCaret();
        }

        private void UpdatePlayerStats()
        {
            lblHitPoints.Text = _player.CurrentHitPoints.ToString();
            lblEmeralds.Text = _player.Emeralds.ToString();
            lblExperience.Text = _player.ExperiencePoints.ToString();
            lblLevel.Text = _player.Level.ToString();
        }
    }
}
