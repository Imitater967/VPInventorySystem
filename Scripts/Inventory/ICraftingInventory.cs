using System.Collections.Generic;
using VoxelPlay;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Inventory
{
    public interface ICraftingInventory : IInventory
    {
        void SetCraftingResult(IReadOnlyList<InventoryItem> items);
        void SetCraftingResult(InventoryItem[] items);
    }
}