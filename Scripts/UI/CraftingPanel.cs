using System;
using UnityEngine;
using UnityEngine.UI;
using VoxelPlay;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Inventory;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.UI
{
    public class CraftingPanel: InventoryPanel
    {

        private CraftingTableInventory m_CraftingTableInventory;

        [SerializeField]
        protected Interactable.CraftingTable m_CraftingTable;

        [Tooltip("Assign on editor")]
        [SerializeField] 
        protected InventorySlot m_ResultSlot;

        [SerializeField] 
        protected InventorySlotStyle m_ResultSlotStyle;

        [SerializeField]
        protected HorizontalLayoutGroup m_LayoutGroup;


        protected override void Start()
        {
        }

        private void OnEnable()
        {
            m_CraftingTableInventory = m_CraftingTable.Inventory;
            m_Inventory = m_CraftingTableInventory;
            InitializeSlots();
            this.RegisterEvents();
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)m_LayoutGroup.transform);
        }

        private void OnDisable()
        {
            this.UnregisterEvents();
            m_CraftingTable = null;
            m_CraftingTableInventory = null;
            m_Inventory = null;
        }

        protected override void RefreshSlotInternal(int slot, ItemDefinition item, float quantity)
        {
            if (slot == -1)
            {
                m_ResultSlot.UpdateItem(item,quantity);
                return;
            }
            base.RefreshSlotInternal(slot, item, quantity);
        }

        protected override void InitializeSlots()
        {
            base.InitializeSlots();
            InitializeSlot(m_ResultSlot, -1, m_ResultSlotStyle);
        }

        public void Open(Interactable.CraftingTable craftingTable)
        {
            m_CraftingTable = craftingTable;
            gameObject.SetActive(true);
        }


        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}