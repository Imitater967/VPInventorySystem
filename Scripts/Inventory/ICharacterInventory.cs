using VoxelPlay;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Inventory
{
    public interface ICharacterInventory : IInventory
    {
        event OnPlayerInventoryEvent OnItemSelectedChanged;

        int SelectedItemIndex { get; set; }

        InventoryItem? GetItemInHand();
    }
}