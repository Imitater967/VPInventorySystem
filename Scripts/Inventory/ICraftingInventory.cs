using System.Collections.Generic;
using VoxelPlay;

namespace Imitater967.VoxelPlay.InventorySystem.Inventory
{
    public interface ICraftingInventory : IInventory
    {
        void SetCraftingResult(IReadOnlyList<InventoryItem> items);
        void SetCraftingResult(InventoryItem[] items);
    }
}