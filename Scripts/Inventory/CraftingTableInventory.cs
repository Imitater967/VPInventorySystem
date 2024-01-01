using UnityEngine;
using VoxelPlay;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Crafting;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Inventory
{
    public class CraftingTableInventory: BasicInventory
    {
        public InventoryItem Result
        {
            get => m_Result;
            set => m_Result = value;
        }
        [SerializeField] protected InventoryItem m_Result;

        public override bool CanSwapItem(IInventory invA, int indexA, IInventory invB, int indexB)
        {
            return indexB != -1;
        }

        public virtual void Craft(Recipe recipe)
        {
            
        }

        public override InventoryItem? GetItemAt(int index)
        {
            if (index == -1)
            {
                return m_Result;
            }
            return base.GetItemAt(index);
        }
    }
}