using System.Collections.Generic;
using UnityEngine;
using VoxelPlay;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable
{
    public class BasicInventory : MonoBehaviour, IInventory
    {
        public event OnPlayerInventoryItemQuantityChange OnItemAdded;
        public event OnPlayerInventoryClear OnItemsClear;
        
        [SerializeField]
        protected List<InventoryItem> m_Items;
        
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
        /// Returns true if player has this item in the inventory
        /// </summary>
        public virtual bool HasItem(ItemDefinition item) {
            return GetItemQuantity(item) > 0;
        }


        public virtual void AddInventoryItem(ItemDefinition[] newItems) {
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
                if (m_Items[k].item == item)
                {
                    quanity += m_Items[k].quantity;
                }
            }
            return quanity;
        }

        public void Clear()
        {
            m_Items.Clear();
        }

        public int Size()
        {
            return m_Items.Count;
        }

        public virtual bool AddInventoryItem(ItemDefinition newItem, float quantity = 1) {
            if (newItem == null || m_Items == null) {
                return false;
            }

            // Check if item is already in inventory
            int itemsCount = m_Items.Count;
            InventoryItem i;
            for (int k = 0; k < itemsCount; k++) {
                if (m_Items[k].item == newItem) {
                    i = m_Items[k];
                    i.quantity += quantity;
                    m_Items[k] = i;
                    if (OnItemAdded != null) OnItemAdded(newItem, quantity);
                    return false;
                }
            }
            i = new InventoryItem
            {
                item = newItem,
                quantity = quantity
            };
            
            m_Items.Add(i);
            if (OnItemAdded != null) OnItemAdded(newItem, quantity);


            return true;
        }
    }
}