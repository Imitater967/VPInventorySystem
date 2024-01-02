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
        /// <summary>
        /// 物品数量修改时候运行的事件
        /// </summary>
        event OnInventoryItemQuantityChange OnItemAdded;
        /// <summary>
        /// 物品删除的时候,运行的事件
        /// </summary>
        event OnInventoryItemQuantityChange OnItemRemoved;
        /// <summary>
        /// 物品交换的时候运行的事件
        /// </summary>
        event OnInventoryItemSwap OnItemSwap;
        /// <summary>
        /// 物品修改的时候运行的事件
        /// </summary>
        event OnItemChange OnItemChange;
        /// <summary>
        /// 物品清空的时候运行的事件
        /// </summary>
        event OnPlayerInventoryClear OnItemsClear;
        
        /// <summary>
        /// 获取背包里的物品
        /// </summary>
        public IReadOnlyList<InventoryItem> Items { get; }
        
        /// <summary>
        /// 获取指定Index的物品
        /// </summary>
        InventoryItem? GetItemAt(int index);
        
        /// <summary>
        /// 强制设置指定位置的物品
        /// </summary>
        void SetItemAt(int index, InventoryItem inventoryItem);
        
        /// <summary>
        /// Add items to inventory
        /// </summary>
        /// <param name="newItem">item to add</param>
        /// <returns>success</returns>
        bool AddInventoryItem(InventoryItem newItem);
        
        /// <summary>
        /// 批量添加背包物品
        /// </summary>
        void AddInventoryItem(InventoryItem[] newItems);

        /// <summary>
        /// 添加指定位置指定物品数量
        /// </summary>
        /// <param name="index">需要添加的位置</param>
        /// <param name="quantity">添加的数量</param>
        void AddInventoryItemAt(int index, float quantity = 1);
        
        
        /// <summary>
        /// Remove item, if quantity returns 0, it's definition will be null
        /// </summary>
        /// <param name="index">index of item</param>
        /// <param name="quantity">quantity to remove</param>
        void RemoveInventoryItemAt(int index, float quantity = 1);
        
        /// <summary>
        ///     需要删除的物品
        /// </summary>
        /// <returns>删除的数量</returns>
        float RemoveInventoryItem(InventoryItem newItem);
        
        /// <summary>
        /// 批量删除物品
        /// </summary>
        void RemoveInventoryItem(InventoryItem[] newItems);
        
        /// <summary>
        ///     Returns true if player has this item in the inventory
        /// </summary>
        bool HasItem(ItemDefinition item);
        
        /// <summary>
        ///     Returns the number of units of a ItemDefinition the player has (if any)
        /// </summary>
        float GetItemQuantity(ItemDefinition item);
        
        /// <summary>
        /// 背包内交互物品
        /// </summary>
        void SwapItem(int indexA, int indexB);
        
        
        /// <summary>
        /// 两个背包之间交换物品
        /// </summary>
        /// <param name="invA">From</param>
        /// <param name="indexA">From Index</param>
        /// <param name="invB">To</param>
        /// <param name="indexB">To Index</param>
        void SwapItem(IInventory invA, int indexA, IInventory invB, int indexB);
        
        
        /// <summary>
        /// 两个背包之间交换物品
        /// </summary>
        /// <param name="invA">From</param>
        /// <param name="indexA">From Index</param>
        /// <param name="invB">To</param>
        /// <param name="indexB">To Index</param>
        bool CanSwapItem(IInventory invA, int indexA, IInventory invB, int indexB);

        /// <summary>
        /// Clear all items inside the inventory
        /// </summary>
        void Clear();
        
        /// <returns>side of the inventory</returns>
        int Size();
        
        
        /// <returns>remaining empty slots</returns>
        int RemainingSlots();
    }
}