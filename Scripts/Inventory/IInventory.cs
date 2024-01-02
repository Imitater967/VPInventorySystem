using System.Collections.Generic;
using VoxelPlay;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Inventory
{
    public delegate void OnInventoryItemQuantityChange(int slot, ItemDefinition item, float quantity);

    public delegate void OnInventoryItemSwap(IInventory invA, int slotA, InventoryItem itemA, IInventory invB,
        int slotB, InventoryItem itemB);

    public delegate void OnItemChange(int slot, InventoryItem item);

    public interface IInventory
    {
        event OnInventoryItemQuantityChange OnItemAdded;
        event OnInventoryItemQuantityChange OnItemRemoved;
        event OnInventoryItemSwap OnItemSwap;
        event OnItemChange OnItemChange;
        event OnPlayerInventoryClear OnItemsClear;


        public IReadOnlyList<InventoryItem> Items { get; }
        InventoryItem? GetItemAt(int index);
        void SetItemAt(int index, InventoryItem inventoryItem);
        bool AddInventoryItem(InventoryItem newItem);
        void AddInventoryItem(InventoryItem[] newItems);

        void AddInventoryItemAt(int index, float quantity = 1);
        void RemoveInventoryItemAt(int index, float quantity = 1);
        float RemoveInventoryItem(InventoryItem newItem);
        void RemoveInventoryItem(InventoryItem[] newItems);
        bool HasItem(ItemDefinition item);
        float GetItemQuantity(ItemDefinition item);
        void SwapItem(int indexA, int indexB);
        void SwapItem(IInventory invA, int indexA, IInventory invB, int indexB);
        bool CanSwapItem(IInventory invA, int indexA, IInventory invB, int indexB);

        void Clear();
        int Size();
        int RemainingSlots();
    }
}