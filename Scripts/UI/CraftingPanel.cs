using System;
using UnityEngine;
using UnityEngine.UI;
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
            base.InitializeSlot();
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)m_LayoutGroup.transform);
        }

        protected override void InitializeSlot()
        {
            base.InitializeSlot();
            m_ResultSlot.ApplyStyle(m_ResultSlotStyle);
            m_ResultSlot.Initialize(-1,m_ResultSlotStyle);
        }

        public void Open(Interactable.CraftingTable craftingTable)
        {
            m_CraftingTable = craftingTable;
            gameObject.SetActive(true);
        }


        public void Close()
        {
            gameObject.SetActive(false);
            m_CraftingTable = null;
        }
    }
}