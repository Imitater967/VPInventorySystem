using VoxelPlay;

namespace Imitater967.VoxelPlay.InventorySystem.Inventory
{
    public interface ICharacterInventory : IInventory
    {
        event OnPlayerInventoryEvent OnItemSelectedChanged;

        int SelectedItemIndex { get; set; }

        InventoryItem? GetItemInHand();
    }
}