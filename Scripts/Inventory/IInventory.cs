using VoxelPlay;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Inventory
{
    
    public delegate void OnInventoryItemQuantityChange(int slot,ItemDefinition item, float quantity);
    public interface IInventory
    {
        event OnInventoryItemQuantityChange OnItemAdded;
        event OnPlayerInventoryClear OnItemsClear;
        InventoryItem? GetItemAt(int index);
        void SetItemAt(int index ,InventoryItem inventoryItem);
        bool AddInventoryItem(InventoryItem newItem);
        void AddInventoryItem(InventoryItem[] newItems);
        float RemoveInventoryItem(InventoryItem newItem);
        void RemoveInventoryItem(InventoryItem[] newItems);
        bool HasItem (ItemDefinition item);
        float GetItemQuantity(ItemDefinition item);
        void SwapItem(int indexA, int indexB);
        void Clear();
        int Size();
        int RemainingSlots();
    }
}