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

        public virtual void Craft(Recipe recipe)
        {
            
        }
    }
}