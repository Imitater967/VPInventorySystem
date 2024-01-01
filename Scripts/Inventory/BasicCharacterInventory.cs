using UnityEngine;
using VoxelPlay;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Inventory
{
    public class BasicCharacterInventory : BasicInventory, ICharacterInventory
    {
        public event OnPlayerInventoryEvent OnItemSelectedChanged;

        public int SelectedItemIndex
        {
            get => m_SelectedItemIndex;
            set
            {
                if (value > m_Items.Count)
                {
                    return;
                }
                var pre = m_SelectedItemIndex;
                m_SelectedItemIndex = value;
                if (OnItemSelectedChanged != null)
                {
                    OnItemSelectedChanged( m_SelectedItemIndex,pre);
                }
            }
        }

        public InventoryItem? GetItemInHand()
        {
            if (m_SelectedItemIndex == -1)
            {
                return null;
            }
            return GetItemAt(m_SelectedItemIndex);
        }

        [SerializeField] private int m_SelectedItemIndex;
        
    }
}