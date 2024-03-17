using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VoxelPlay;

namespace Imitater967.VoxelPlay.InventorySystem.Inventory
{
    /// <summary>
    /// 基础背包
    /// </summary>
    public class BasicInventory : MonoBehaviour, IInventory
    {
        /// <summary>
        /// 物品数量修改时候运行的事件
        /// </summary>
        public event OnInventoryItemQuantityChange OnItemAdded;
        
        /// <summary>
        /// 物品删除的时候,运行的事件
        /// </summary>
        public event OnInventoryItemQuantityChange OnItemRemoved;
        
        /// <summary>
        /// 物品交换的时候运行的事件
        /// </summary>
        public event OnInventoryItemSwap OnItemSwap;
        
        /// <summary>
        /// 物品修改的时候运行的事件
        /// </summary>
        public event OnItemChange OnItemChange;
        
        /// <summary>
        /// 物品清空的时候运行的事件
        /// </summary>
        public event OnPlayerInventoryClear OnItemsClear;
        
        [Tooltip("背包里实际的物品")]
        [SerializeField] protected List<InventoryItem> m_Items;

        public IReadOnlyList<InventoryItem> Items { get => m_Items; }

        public List<InventoryItem> GetItemList { get => m_Items; }

        protected virtual void Awake() { }
        
        /// <summary>
        /// 执行ItemChange事件
        /// </summary>
        /// <param name="index">发生的Index</param>
        /// <param name="item">新的物品</param>
        public void CallItemChange(int index, InventoryItem item)
        {
            if (OnItemChange != null)
            {
                OnItemChange(index, item);
            }
        }
        
        #region IInventory

        /// <summary>
        /// 获取指定Index的物品
        /// </summary>
        public virtual InventoryItem? GetItemAt(int index)
        {
            if (m_Items.Count <= index)
            {
                Debug.LogError($"错误, Out of bounds {index}");
                return null;
            }

            return m_Items[index];
        }
        
        /// <summary>
        /// 强制设置指定位置的物品
        /// </summary>
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

        /// <summary>
        /// Remove item, if quantity returns 0, it's definition will be null
        /// </summary>
        /// <param name="index">index of item</param>
        /// <param name="quantity">quantity to remove</param>
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
                SetItemAt(index, InventoryItem.Null);
                return;
            }
            SetItemAt(index, item);
        }

        /// <summary>
        ///     需要删除的物品
        /// </summary>
        /// <returns>删除的数量</returns>
        public float RemoveInventoryItem(InventoryItem newItem)
        {
            float countToRemove = newItem.quantity;
            for (var i = 0; i < m_Items.Count && newItem.quantity > 0; i++)
            {
                var inventoryItem = m_Items[i];
                if (inventoryItem == newItem)
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

        /// <summary>
        /// 批量删除物品
        /// </summary>
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
        ///     Returns true if player has this item in the inventory
        /// </summary>
        public virtual bool HasItem(ItemDefinition item)
        {
            return GetItemQuantity(item) > 0;
        }


        /// <summary>
        /// 批量添加背包物品
        /// </summary>
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

        /// <summary>
        /// 添加指定位置指定物品数量
        /// </summary>
        /// <param name="index">需要添加的位置</param>
        /// <param name="quantity">添加的数量</param>
        public void AddInventoryItemAt(int index, float quantity = 1)
        {
            var optItem = GetItemAt(index);
            if (optItem == null)
            {
                return;
            }
            var item = optItem.GetValueOrDefault();
            item.quantity += quantity;
            SetItemAt(index, item);
        }


        /// <summary>
        ///     Returns the number of units of a ItemDefinition the player has (if any)
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
                if (m_Items[k].item == item)
                {
                    quanity += m_Items[k].quantity;
                }
            }

            return quanity;
        }

        /// <summary>
        /// 背包内交互物品
        /// </summary>
        public virtual void SwapItem(int indexA, int indexB)
        {
            SwapItem(this, indexA, this, indexB);
        }

        /// <summary>
        /// 两个背包之间交换物品
        /// </summary>
        /// <param name="invA">From</param>
        /// <param name="indexA">From Index</param>
        /// <param name="invB">To</param>
        /// <param name="indexB">To Index</param>
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

        /// <summary>
        /// 两个背包之间交换物品
        /// </summary>
        /// <param name="invA">From</param>
        /// <param name="indexA">From Index</param>
        /// <param name="invB">To</param>
        /// <param name="indexB">To Index</param>
        public virtual bool CanSwapItem(IInventory invA, int indexA, IInventory invB, int indexB)
        {
            return true;
        }

        /// <summary>
        /// Clear all items inside the inventory
        /// </summary>
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

        /// <returns>side of the inventory</returns>
        public int Size()
        {
            return m_Items.Count;
        }

        /// <returns>remaining empty slots</returns>
        public int RemainingSlots()
        {
            return m_Items.Count(x => x.quantity <= 0);
        }

        /// <summary>
        /// Add items to inventory
        /// </summary>
        /// <param name="newItem">item to add</param>
        /// <returns>success</returns>
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
                if (m_Items[k] == newItem)
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
                m_Items[i1] = new InventoryItem { quantity = newItem.item.MaxStackSize, item = newItem.item };

                if (OnItemAdded != null)
                {
                    OnItemAdded(i1, newItem.item, newItem.item.MaxStackSize);
                }
            }

            for (var i1 = 0; i1 < m_Items.Count && remainingAmount > 0; i1++)
            {
                if (m_Items[i1].quantity > 0)
                {
                    continue;
                }

                m_Items[i1] = new InventoryItem { quantity = remainingAmount, item = newItem.item };

                if (OnItemAdded != null)
                {
                    OnItemAdded(i1, newItem.item, remainingAmount);
                }

                break;
            }

            return true;
        }

        #endregion


    }
}