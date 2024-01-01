using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VoxelPlay;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Crafting;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Inventory;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.UI
{
    public class CraftingPanel: InventoryPanel
    {

        private CraftingTableInventory m_CraftingTableInventory;
        
        [SerializeField]
        private Recipe m_CurrentRecipe;
      
        [SerializeField]
        protected Interactable.CraftingTable m_CraftingTable;

        [Tooltip("Assign on editor")]
        [SerializeField] 
        protected InventorySlot m_ResultSlot;

        [SerializeField] 
        protected InventorySlotStyle m_ResultSlotStyle;

        [SerializeField]
        protected HorizontalLayoutGroup m_LayoutGroup;
        
        [Tooltip("Button for crafting")]
        [SerializeField]
        protected Button m_CraftButton;

        protected override void Start()
        {
        }

        private void OnEnable()
        {
            m_CraftingTableInventory = m_CraftingTable.Inventory;
            m_Inventory = m_CraftingTableInventory;
            InitializeSlots();
            RegisterEvents();
            
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)m_LayoutGroup.transform);
            UpdateCraftButton();
        }

        private void OnDisable()
        {
            UnregisterEvents();
            m_CraftingTable = null;
            m_CraftingTableInventory = null;
            m_Inventory = null;
        }

        /// <summary>
        /// 在物品变更的基础上, 获取满足的Recipe
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="item"></param>
        protected override void RefreshSlot(int slot, InventoryItem item)
        {
            base.RefreshSlot(slot, item);
            UpdateCraftButton();
        }

        private void UpdateCraftButton()
        {
            m_CurrentRecipe = CraftingManager.Instance.Recipes.FirstOrDefault(x => x.CanAccept(m_Inventory.Items));
            m_CraftButton.interactable = m_CurrentRecipe != null;
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