using System.Collections.Generic;
using Imitater967.VoxelPlay.InventorySystem.Interactable;
using Imitater967.VoxelPlay.InventorySystem.Inventory;
using UnityEngine;
using VoxelPlay;

namespace Imitater967.VoxelPlay.InventorySystem.Character
{
    /// <summary>
    /// 被我们修改过的,支持背包的玩家
    /// </summary>
    public class VoxelPlayPlayerBridged : VoxelPlayPlayer, IContainer
    {
        /// <summary>
        /// 获取玩家背包
        /// </summary>
        public PlayerInventory Inventory { get => m_Inventory; }
        [Tooltip("绑定的背包")]
        [SerializeField] private PlayerInventory m_Inventory;

        /// <summary>
        /// 重写VoxelPlayPlayer.items
        /// 获取当前背包的所有物品
        /// </summary>
        public override List<InventoryItem> items { get => m_Inventory.GetItemList; }
        
        /// <summary>
        /// 重写VoxelPlayPlayer.selectedItemIndex
        /// 获取当前玩家背包手持物品
        /// </summary>
        public override int selectedItemIndex
        {
            get => m_Inventory.SelectedItemIndex;
            set
            {
                _selectedItemIndex = value;
                m_Inventory.SelectedItemIndex = value;
            }
        }

        /// <summary>
        /// 重写VoxelPlayPlayer.AddInventoryItem
        /// 让物品被添加到我们的背包系统中去
        /// </summary>
        /// <param name="newItem">新的物品定义</param>
        /// <param name="quantity">物品数量</param>
        /// <returns>是否添加成功</returns>
        public override bool AddInventoryItem(ItemDefinition newItem, float quantity = 1)
        {
            return m_Inventory.AddInventoryItem(new InventoryItem { item = newItem, quantity = quantity });
        }

        /// <summary>
        /// 重写VoxelPlayPlayer.GetSelectedItem()
        /// </summary>
        /// <returns>返回当前手中背包选中的物品</returns>
        public override InventoryItem GetSelectedItem()
        {
            return m_Inventory.GetItemInHand().GetValueOrDefault();
        }

        /// <summary>
        /// 重写VoxelPlayPlayer.GetSelectedItem()
        /// 设置当前手持的Index
        /// </summary>
        /// <returns>是否成功设置</returns>
        public override bool SetSelectedItem(int itemIndex)
        {
            if (m_Inventory.GetItemAt(itemIndex).HasValue)
            {
                m_Inventory.SelectedItemIndex = itemIndex;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 消耗指定物品
        /// </summary>
        /// <param name="item">物品类型</param>
        /// <param name="amount">消耗的数量</param>
        public override void ConsumeItem(ItemDefinition item, float amount)
        {
            m_Inventory.RemoveInventoryItem(new InventoryItem { item = item, quantity = amount });
        }

        /// <summary>
        /// 消耗手持物品
        /// </summary>
        /// <returns>消耗了的物品</returns>
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

        /// <summary>
        /// 设置当前手持的物品为某方块
        /// 一般为创造模式的按键
        /// </summary>
        /// <param name="vd">对应的方块定义</param>
        /// <returns>是否设置成功</returns>
        public override bool SetSelectedItem(VoxelDefinition vd)
        {
            if (this.items == null)
            {
                return false;
            }

            List<InventoryItem> items = this.items;
            int count = items.Count;
            for (int k = 0; k < count; k++)
            {
                InventoryItem item = items[k];
                if (item.item == null)
                {
                    continue;
                }
                if (item.item.category == ItemCategory.Voxel && item.item.voxelType == vd)
                {
                    selectedItemIndex = k;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// IContainer::GetInventory();
        /// </summary>
        /// <returns>当前容器绑定的背包</returns>
        public IInventory GetInventory()
        {
            return m_Inventory;
        }
    }
}