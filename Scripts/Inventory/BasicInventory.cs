using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VoxelPlay;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Inventory
{
    public class BasicInventory : MonoBehaviour, IInventory
    {
        public event OnInventoryItemQuantityChange OnItemAdded;
        public event OnInventoryItemQuantityChange OnItemRemoved;
        public event OnInventoryItemSwap OnItemSwap;
        public event OnItemChange OnItemChange;
        public event OnPlayerInventoryClear OnItemsClear;

        [SerializeField] protected List<InventoryItem> m_Items;

        public IReadOnlyList<InventoryItem> Items => m_Items;

        public List<InventoryItem> GetItemList => m_Items;

        protected virtual void Awake()
        {
            
        }

        public virtual InventoryItem? GetItemAt(int index)
        {
            if (m_Items.Count <= index)
            {
                Debug.LogError($"错误, Out of bounds {index}");
                return null;
            }

            return m_Items[index];
        }

        public virtual void SetItemAt(int index, InventoryItem inventoryItem)
        {
            if (m_Items.Count <= index)
            {
                Debug.LogError($"错误, Out of bounds  {index}");
                return;
            }
            m_Items[index] = inventoryItem;
            
            if (OnItemChange != null)
            {
                OnItemChange(index, inventoryItem);
            }
        }

        public void CallItemChange(int index, InventoryItem item)
        {
            if (OnItemChange != null)
            {
                OnItemChange(index, item);
            }
        }

        public void RemoveInventoryItemAt(int index, float quantity = 1)
        {
            var optItem = GetItemAt(index);
            if (optItem == null)
            {
                return;
            }
            var item = optItem.GetValueOrDefault();
            item.quantity -= quantity;
            if (item.quantity == 0)
            {
                SetItemAt(index,InventoryItem.Null);
                return;
            }
            SetItemAt(index,item);
        }

        /// <summary>
        /// 需要删除的物品
        /// </summary>
        /// <returns>删除的数量</returns>
        public float RemoveInventoryItem(InventoryItem newItem)
        {
            float countToRemove = newItem.quantity;
            for (var i = 0; i < m_Items.Count && newItem.quantity > 0; i++)
            {
                var inventoryItem = m_Items[i];
                if (inventoryItem.IsSameItem(newItem))
                {
                    //储存原来的数量
                    var originQuantity = inventoryItem.quantity;
                    //当前背包物品减数量
                    inventoryItem.quantity -= newItem.quantity;
                    //预防负数
                    inventoryItem.quantity = Mathf.Max(inventoryItem.quantity, 0);
                    //物品为空,删除物品
                    if (inventoryItem.quantity == 0)
                    {
                        inventoryItem.item = null;
                    }

                    m_Items[i] = inventoryItem;
                    var amountRemoved = originQuantity - inventoryItem.quantity;
                    newItem.quantity -= amountRemoved;
                    if (OnItemRemoved != null)
                    {
                        OnItemRemoved(i, newItem.item, amountRemoved);
                    }
                }
            }

            return countToRemove - newItem.quantity;
        }


        public void RemoveInventoryItem(InventoryItem[] newItems)
        {
            if (newItems == null)
            {
                return;
            }

            for (int k = 0; k < newItems.Length; k++)
            {
                RemoveInventoryItem(newItems[k]);
            }
        }

        /// <summary>
        /// Returns true if player has this item in the inventory
        /// </summary>
        public virtual bool HasItem(ItemDefinition item)
        {
            return GetItemQuantity(item) > 0;
        }


        public virtual void AddInventoryItem(InventoryItem[] newItems)
        {
            if (newItems == null)
            {
                return;
            }

            for (int k = 0; k < newItems.Length; k++)
            {
                AddInventoryItem(newItems[k]);
            }
        }

        public void AddInventoryItemAt(int index, float quantity = 1)
        {
            var optItem = GetItemAt(index);
            if (optItem == null)
            {
                return;
            }
            var item = optItem.GetValueOrDefault();
            item.quantity += quantity;
            SetItemAt(index,item);
        }


        /// <summary>
        /// Returns the number of units of a ItemDefinition the player has (if any)
        /// </summary>
        public virtual float GetItemQuantity(ItemDefinition item)
        {
            if (m_Items == null)
            {
                return 0;
            }

            int itemCount = m_Items.Count;
            float quanity = 0;
            for (int k = 0; k < itemCount; k++)
            {
                if (m_Items[k].IsSameItem(item))
                {
                    quanity += m_Items[k].quantity;
                }
            }

            return quanity;
        }

        public void SwapItem(int indexA, int indexB)
        {
            SwapItem(this, indexA, this, indexB);
        }

        public virtual void SwapItem(IInventory invA, int indexA, IInventory invB, int indexB)
        {
            var originItem = invB.GetItemAt(indexB);
            var itemForB = invA.GetItemAt(indexA).GetValueOrDefault();
            invB.SetItemAt(indexB, itemForB);
            var itemForA = originItem.GetValueOrDefault();
            invA.SetItemAt(indexA, itemForA);
            if (OnItemSwap != null)
            {
                OnItemSwap(invA, indexA, itemForA, invB, indexB, itemForB);
            }
        }

        public virtual bool CanSwapItem(IInventory invA, int indexA, IInventory invB, int indexB)
        {
            return true;
        }

        public void Clear()
        {
            for (var i = 0; i < m_Items.Count; i++)
            {
                m_Items[i] = InventoryItem.Null;
                OnItemChange(i, m_Items[i]);
            }
            if (OnItemsClear != null)
            {
                OnItemsClear();
            }

        }

        public int Size()
        {
            return m_Items.Count;
        }

        public int RemainingSlots()
        {
            return m_Items.Count(x => x.quantity <= 0);
        }

        public virtual bool AddInventoryItem(InventoryItem newItem)
        {
            if (newItem == null || m_Items == null)
            {
                return false;
            }

            // Check if item is already in inventory
            int itemsCount = m_Items.Count;
            float amountAdded = 0;
            InventoryItem i;
            for (int k = 0; k < itemsCount; k++)
            {
                if (m_Items[k].IsSameItem(newItem))
                {
                    i = m_Items[k];
                    float originQuantity = i.quantity;
                    i.quantity += newItem.quantity;
                    i.quantity = Mathf.Min(m_Items[k].item.MaxStackSize, i.quantity);

                    m_Items[k] = i;
                    var amountChange = i.quantity - originQuantity;
                    if (amountChange <= 0)
                    {
                        continue;
                    }

                    amountAdded += amountChange;
                    newItem.quantity -= amountAdded;
                    if (OnItemAdded != null)
                    {
                        OnItemAdded(k, m_Items[k].item, m_Items[k].quantity);
                    }

                    if (newItem.quantity <= 0)
                    {
                        return true;
                    }
                }
            }

            int stackToAdd = Mathf.FloorToInt(newItem.quantity / newItem.item.MaxStackSize);
            float remainingAmount = newItem.quantity % newItem.item.MaxStackSize;

            for (var i1 = 0; i1 < m_Items.Count && stackToAdd > 0; i1++)
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

                if (OnItemAdded != null) OnItemAdded(i1, newItem.item, newItem.item.MaxStackSize);
            }

            for (var i1 = 0; i1 < m_Items.Count && remainingAmount > 0; i1++)
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

                if (OnItemAdded != null)
                {
                    OnItemAdded(i1, newItem.item, remainingAmount);
                }

                break;
            }

            return true;
        }
    }
}