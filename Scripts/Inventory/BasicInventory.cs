using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VoxelPlay;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Inventory
{
    public class BasicInventory : MonoBehaviour, IInventory
    {
        public event OnInventoryItemQuantityChange OnItemAdded;
        public event OnPlayerInventoryClear OnItemsClear;
        
        [SerializeField]
        protected List<InventoryItem> m_Items;
        
        public IReadOnlyList<InventoryItem> Items => m_Items;

        public List<InventoryItem> GetItemList => m_Items;
        
        public InventoryItem? GetItemAt(int index)
        {
            if (m_Items.Count <= index)
            {
                Debug.LogError("错误, Out of bounds");
                return null;
            }
            return m_Items[index];
        }

        public void SetItemAt(int index, InventoryItem inventoryItem)
        {
            if (m_Items.Count <= index)
            {
                Debug.LogError("错误, Out of bounds");
                return;
            }
            m_Items[index] = inventoryItem;
        }
        
        /// <summary>
        /// 需要删除的物品
        /// </summary>
        /// <returns>删除的数量</returns>
        public float RemoveInventoryItem(InventoryItem newItem)
        {
            float countToRemove = newItem.quantity;
            for (var i = 0; i < m_Items.Count && newItem.quantity>0; i++)
            {
                var inventoryItem = m_Items[i];
                if (inventoryItem.IsSameItem(newItem))
                {
                    var originQuantity = inventoryItem.quantity;
                    inventoryItem.quantity -= newItem.quantity;
                    inventoryItem.quantity = Mathf.Max(newItem.quantity, 0);
                    if (inventoryItem.quantity == 0)
                    {
                        inventoryItem.item = null;
                    }
                    m_Items[i] = inventoryItem;
                    newItem.quantity -= originQuantity - inventoryItem.quantity;
                }
            }

            return countToRemove - newItem.quantity;
        }


        public void RemoveInventoryItem(InventoryItem[] newItems)
        {
            if (newItems == null) {
                return;
            }

            for (int k = 0; k < newItems.Length; k++) {
                RemoveInventoryItem(newItems[k]);
            }
        }

        /// <summary>
        /// Returns true if player has this item in the inventory
        /// </summary>
        public virtual bool HasItem(ItemDefinition item) {
            return GetItemQuantity(item) > 0;
        }


        public virtual void AddInventoryItem(InventoryItem[] newItems) {
            if (newItems == null) {
                return;
            }

            for (int k = 0; k < newItems.Length; k++) {
                AddInventoryItem(newItems[k]);
            }
        }


        /// <summary>
        /// Returns the number of units of a ItemDefinition the player has (if any)
        /// </summary>
        public virtual float GetItemQuantity(ItemDefinition item) {
            if (m_Items == null) {
                return 0;
            }

            int itemCount = m_Items.Count;
            float quanity = 0;
            for (int k = 0; k < itemCount; k++) {
                if (m_Items[k].IsSameItem(item))
                {
                    quanity += m_Items[k].quantity;
                }
            }
            return quanity;
        }

        public void SwapItem(int indexA, int indexB)
        {
            var fakeA = m_Items[indexA];
            m_Items[indexA] = m_Items[indexB];
            m_Items[indexB] = fakeA;
        }

        public void Clear()
        {
            m_Items.Clear();
        }

        public int Size()
        {
            return m_Items.Count;
        }

        public int RemainingSlots()
        {
            return m_Items.Count(x => x.quantity <= 0);
        }

        public virtual bool AddInventoryItem(InventoryItem newItem) {
            if (newItem == null || m_Items == null) {
                return false;
            }

            // Check if item is already in inventory
            int itemsCount = m_Items.Count;
            float amountAdded = 0;
            InventoryItem i;
            for (int k = 0; k < itemsCount; k++) {
                if (m_Items[k].IsSameItem(newItem)) {
                    i = m_Items[k];
                    float originQuantity = i.quantity;
                    i.quantity += newItem.quantity;
                    i.quantity = Mathf.Min(m_Items[k].item.MaxStackSize, i.quantity);
                    
                    m_Items[k] = i;
                    amountAdded += originQuantity - i.quantity;
                    newItem.quantity -= amountAdded;
                    if (OnItemAdded != null)
                    {
                        OnItemAdded(k,m_Items[k].item, m_Items[k].quantity);
                    }
                    if (newItem.quantity <= 0)
                    {
                        return true;
                    }
                }
            }

            int stackToAdd = Mathf.FloorToInt(newItem.quantity / newItem.item.MaxStackSize);
            float remainingAmount = newItem.quantity % newItem.item.MaxStackSize;
            
            for (var i1 = 0; i1 < m_Items.Count && stackToAdd>0 ; i1++)
            {
                if (m_Items[i1].quantity > 0)
                {
                    continue;
                }

                stackToAdd--;
                m_Items[i1] = new InventoryItem()
                {
                    quantity = newItem.item.MaxStackSize,
                    item = newItem.item,
                };
                
                if (OnItemAdded != null) OnItemAdded(i1,newItem.item, newItem.item.MaxStackSize);
            }
            
            for (var i1 = 0; i1 < m_Items.Count ; i1++)
            {
                if (m_Items[i1].quantity > 0)
                {
                    continue;
                }

                m_Items[i1] = new InventoryItem()
                {
                    quantity = remainingAmount,
                    item = newItem.item,
                };
                
                if (OnItemAdded != null) OnItemAdded(i1,newItem.item, remainingAmount);
            }
            return true;
        }
    }
}