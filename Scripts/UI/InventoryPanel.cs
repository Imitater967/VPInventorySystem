using System;
using System.Collections.Generic;
using UnityEngine;
using VoxelPlay;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Inventory;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.UI
{
    public class InventoryPanel: MonoBehaviour
    {
        protected IInventory m_Inventory;
        public IInventory Inventory => m_Inventory;

        [SerializeField]
        protected Transform m_DragParent;
        [SerializeField]
        protected InventorySlot m_SlotPrefab;
        [SerializeField]
        public InventorySlotStyle m_SlotStyle;
        [SerializeField]
        protected bool m_ResetSlotOnStart;
        [SerializeField] 
        protected Transform m_SlotRoot;
        [Tooltip("不需要修改, Change On Runtime")]
        [SerializeField]
        protected List<InventorySlot> m_Slots;
        
        protected virtual void Start()
        {
            InitializeSlots();

            RegisterEvents();
        }


        protected void RegisterEvents()
        {
            m_Inventory.OnItemAdded += RefreshSlotInternal;
            m_Inventory.OnItemRemoved += RefreshSlotInternal;
            m_Inventory.OnItemChange += RefreshSlot;
        }


        protected void UnregisterEvents()
        {
            m_Inventory.OnItemAdded -= RefreshSlotInternal;
            m_Inventory.OnItemRemoved -= RefreshSlotInternal;
        }
        
        protected virtual void InitializeSlots()
        {
            m_Slots.AddRange(m_SlotRoot.GetComponentsInChildren<InventorySlot>());
            if (m_ResetSlotOnStart)
            {
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
            }
        }

        protected void InitializeSlot(InventorySlot slot, int i,InventorySlotStyle slotStyle)
        {
            slot.Initialize(this.m_Inventory, i,slotStyle );
            slot.UpdateItem(m_Inventory.GetItemAt(i).Value);
            slot.DropAndDrag.DragParent = m_DragParent;
        }
        protected virtual void RefreshSlot(int slot,InventoryItem item)
        {
            RefreshSlotInternal(slot,item.item,item.quantity);
        }
        protected virtual void RefreshSlotInternal(int slot,ItemDefinition item, float quantity)
        {
            m_Slots[slot].UpdateItem(item,quantity);
        }
    }
}