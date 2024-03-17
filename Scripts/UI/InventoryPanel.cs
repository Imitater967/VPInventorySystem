using System.Collections.Generic;
using Imitater967.VoxelPlay.InventorySystem.Interactable;
using Imitater967.VoxelPlay.InventorySystem.Inventory;
using UnityEngine;
using VoxelPlay;

namespace Imitater967.VoxelPlay.InventorySystem.UI
{
    /// <summary>
    /// 背包面板
    /// </summary>
    public abstract class InventoryPanel : MonoBehaviour
    {
        /// <summary>
        /// 绑定的背包
        /// </summary>
        protected IInventory m_Inventory;
        public IInventory Inventory { get => m_Inventory; }

        [Tooltip("拖曳时候物品的Parent, 用于管理层级")]
        [SerializeField]
        protected Transform m_DragParent;

        [Tooltip("预制体")]
        [SerializeField]
        protected InventorySlot m_SlotPrefab;

        [SerializeField]
        public InventorySlotStyle m_SlotStyle;
        //
        // [SerializeField]
        // protected bool m_ResetSlotOnStart;

        [SerializeField]
        protected Transform m_SlotRoot;

        [Tooltip("不需要修改, Change On Runtime")]
        [SerializeField]
        protected List<InventorySlot> m_Slots;

        
        /// <summary>
        /// Setup on enable
        /// </summary>
        protected virtual void OnEnable()
        {
            InitializeSlots();
            RegisterEvents();
        }
        
        /// <summary>
        /// Reset on disable;
        /// </summary>
        protected virtual void OnDisable()
        {
            UnregisterEvents();
        }

        /// <summary>
        /// Open the panel
        /// </summary>
        /// <param name="container">container to bind</param>
        public abstract void Open(IContainer container);
        
        /// <summary>
        /// Close the panel
        /// Don't forget reset state!!!
        /// </summary>
        public abstract void Close();

        /// <summary>
        /// Register events on inventory change
        /// </summary>
        protected virtual void RegisterEvents()
        {
            m_Inventory.OnItemAdded += RefreshSlot;
            m_Inventory.OnItemRemoved += RefreshSlot;
            m_Inventory.OnItemChange += RefreshSlot;
        }

        /// <summary>
        /// Unregister events, prevent memory leak;
        /// </summary>
        protected virtual void UnregisterEvents()
        {
            m_Inventory.OnItemAdded -= RefreshSlot;
            m_Inventory.OnItemRemoved -= RefreshSlot;
        }

        /// <summary>
        /// Initialize slots, create slot 
        /// </summary>
        protected virtual void InitializeSlots()
        {
            m_Slots.AddRange(m_SlotRoot.GetComponentsInChildren<InventorySlot>());
            // if (m_ResetSlotOnStart)
            // {
                foreach (var inventorySlot in m_Slots)
                {
                    Destroy(inventorySlot.gameObject);
                }
                m_Slots.Clear();
                m_Slots.Capacity = m_Inventory.Size();
                for (int i = 0; i < m_Inventory.Size(); i++)
                {
                    var slot = Instantiate(m_SlotPrefab, m_SlotRoot);
                    InitializeSlot(slot, i, m_SlotStyle);
                    m_Slots.Add(slot);
                }
            // }
        }

        /// <summary>
        /// Initialize one slot
        /// </summary>
        /// <param name="slot">The slot to initialize</param>
        /// <param name="i">its index</param>
        /// <param name="slotStyle">its style</param>
        protected virtual void InitializeSlot(InventorySlot slot, int i, InventorySlotStyle slotStyle)
        {
            slot.Initialize(m_Inventory, i, slotStyle);
            slot.UpdateItem(m_Inventory.GetItemAt(i).Value);
            slot.DropAndDrag.DragParent = m_DragParent;
        }

        /// <summary>
        /// Refresh slot adapted for OnItemChange Event
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="item"></param>
        protected virtual void RefreshSlot(int slot, InventoryItem item)
        {
            RefreshSlotInternal(slot);
        }
        /// <summary>
        /// Refresh slot adapted for OnItemAdd & OnItemRemove Event
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="item"></param>
        protected virtual void RefreshSlot(int slot, ItemDefinition item, float quantity)
        {
            RefreshSlotInternal(slot);
        }

        /// <summary>
        /// The actual method for refreshing slot
        /// </summary>
        protected virtual void RefreshSlotInternal(int slotToRefresh)
        {
            m_Slots[slotToRefresh].UpdateItem(m_Inventory.GetItemAt(slotToRefresh).GetValueOrDefault());
        }
        
    }
}