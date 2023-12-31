using UnityEngine;
using VoxelPlay;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Character
{
    public class BasicCharacterInventory : BasicInventory, ICharacterInventory
    {
        public event OnPlayerInventoryEvent OnItemSelectedChanged;

        public int SelectedItemIndex
        {
            get => m_SelectedItemIndex;
            set
            {
                if (m_SelectedItemIndex > m_Items.Count)
                {
                    return;
                }
                m_SelectedItemIndex = value;
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