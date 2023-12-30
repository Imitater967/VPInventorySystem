using VoxelPlay;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable
{
    public interface IInventory
    {
        event OnPlayerInventoryItemQuantityChange OnItemAdded;
        event OnPlayerInventoryClear OnItemsClear;
        InventoryItem? GetItemAt(int index);
        void SetItemAt(int index ,InventoryItem inventoryItem);
        bool AddInventoryItem(ItemDefinition newItem, float quantity = 1);
        void AddInventoryItem(ItemDefinition[] newItems);
        bool HasItem (ItemDefinition item);
        float GetItemQuantity(ItemDefinition item);
        void Clear();
        int Size();
    }
}