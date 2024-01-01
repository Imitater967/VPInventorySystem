using System.Collections.Generic;
using Unity.VisualScripting;
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
            set
            {
                _selectedItemIndex = value;
                m_Inventory.SelectedItemIndex = value;
            }
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

        public override void ConsumeItem(ItemDefinition item, float amount)
        {
            m_Inventory.RemoveInventoryItem(new InventoryItem(){item = item,quantity = amount});
        }

        public override InventoryItem ConsumeItem()
        {
            var optItem = m_Inventory.GetItemInHand();
            if (!optItem.HasValue)
            {
                return InventoryItem.Null;
            }
            m_Inventory.RemoveInventoryItemAt(m_Inventory.SelectedItemIndex);
            //值类型,所以不需要担心上行代码的影响
            return optItem.GetValueOrDefault();
        }

        public override bool SetSelectedItem(VoxelDefinition vd) {
            if (this.items == null) {
                return false;
            }

            List<InventoryItem> items = this.items;
            int count = items.Count;
            for (int k = 0; k < count; k++) {
                InventoryItem item = items[k];
                if (item.item == null)
                {
                    continue;
                }
                if (item.item.category == ItemCategory.Voxel && item.item.voxelType == vd) {
                    selectedItemIndex = k;
                    return true;
                }
            }
            return false;
        }

    }
}