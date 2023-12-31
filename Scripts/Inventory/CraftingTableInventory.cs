using VoxelPlay;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Character
{
    public class CraftingTableInventory: BasicInventory
    {
        public InventoryItem Result
        {
            get => m_Result;
            set => m_Result = value;
        }
        private InventoryItem m_Result;

    }
}