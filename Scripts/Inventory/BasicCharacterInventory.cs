using UnityEngine;
using VoxelPlay;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Inventory
{
    public class BasicCharacterInventory : BasicInventory, ICharacterInventory
    {
        /// <summary>
        /// 当玩家手持Index改变的时候召唤的事件
        /// </summary>
        public event OnPlayerInventoryEvent OnItemSelectedChanged;
        [Tooltip("当前手持的Index")]
        [SerializeField] private int m_SelectedItemIndex;
        public int SelectedItemIndex
        {
            get => m_SelectedItemIndex;
            set
            {
                //Check bounds
                if (value > m_Items.Count)
                {
                    return;
                }

                //判断是否是(无选择) 或者目标位置是否没有物品
                if (value != -1 && !GetItemAt(value).HasValue)
                {
                    return;
                }
                
                //执行事件
                var pre = m_SelectedItemIndex;
                m_SelectedItemIndex = value;
                if (OnItemSelectedChanged != null)
                {
                    OnItemSelectedChanged(m_SelectedItemIndex, pre);
                }
            }
        }

        

        protected override void Awake()
        {
            OnItemChange += ChangeIndexIfNull;
        }
        
        
        /// <summary>
        /// ICharacterInventory:GetItemInHand
        /// </summary>
        /// <returns>当前手持的物品</returns>
        public InventoryItem? GetItemInHand()
        {
            if (m_SelectedItemIndex == -1)
            {
                return null;
            }
            return GetItemAt(m_SelectedItemIndex);
        }

        /// <summary>
        /// 当选中的物品修改的时候,判断是否为空
        /// 如果是,则取消选中(标记为-1)
        /// </summary>
        private void ChangeIndexIfNull(int slot, InventoryItem item)
        {
            if (slot == m_SelectedItemIndex && item.item == null)
            {
                SelectedItemIndex = -1;
            }
        }
    }
}