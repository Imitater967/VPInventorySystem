using System.Collections.Generic;
using UnityEngine;
using VoxelPlay;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Inventory;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Character
{
    public class VoxelPlayPlayerBridged : VoxelPlayPlayer
    {
        public PlayerInventory Inventory => m_Inventory;
        [SerializeField] private PlayerInventory m_Inventory;

        public override List<InventoryItem> items => m_Inventory.GetItemList;

        public override int selectedItemIndex
        {
            get => m_Inventory.SelectedItemIndex;
            set => m_Inventory.SelectedItemIndex = value;
        }

        public override bool AddInventoryItem(ItemDefinition newItem, float quantity = 1)
        {
            return m_Inventory.AddInventoryItem(new InventoryItem() { item = newItem, quantity = quantity });
        }

        public override InventoryItem GetSelectedItem()
        {
            return m_Inventory.GetItemInHand().GetValueOrDefault();
        }

        public override bool SetSelectedItem(int itemIndex)
        {
            if (m_Inventory.GetItemAt(itemIndex).HasValue)
            {
                m_Inventory.SelectedItemIndex = itemIndex;
                return true;
            }
            return false;
        }
    }
}