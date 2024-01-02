using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using VoxelPlay;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Crafting;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Interactable;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Inventory;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Manager;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.UI
{
    /// <summary>
    /// Crafting panel
    /// </summary>
    public class CraftingPanel : InventoryPanel
    {
        /// <summary>
        /// The crafting inventory that bounded.
        /// </summary>
        private CraftingTableInventory m_CraftingTableInventory;

        /// <summary>
        /// Current recipe for crafting.
        /// CraftingPanel -> Check Recipes -> Send to Crafting Table
        /// </summary>
        [SerializeField]
        private Recipe m_CurrentRecipe;

        /// <summary>
        /// The table that bounded.
        /// </summary>
        [SerializeField]
        protected Interactable.CraftingTable m_CraftingTable;

        [Tooltip("Assign on editor, the slot showing crafting result")]
        [SerializeField]
        protected CraftResultSlot m_ResultSlot;

        [Tooltip("The style for crafting")]
        [SerializeField]
        protected InventorySlotStyle m_ResultSlotStyle;

        [Tooltip("We need this for fixing layout problem")]
        [SerializeField]
        protected HorizontalLayoutGroup m_LayoutGroup;

        [Tooltip("Button for crafting")]
        [SerializeField]
        protected Button m_CraftButton;

        [Tooltip("Progress bar showing the crafting progress")]
        [SerializeField]
        protected CraftingProgressBar m_ProgressBar;


        /// <summary>
        /// Initialize
        /// </summary>
        protected override void OnEnable()
        {
            //初始化变量
            m_CraftingTableInventory = m_CraftingTable.Inventory;
            m_Inventory = m_CraftingTableInventory;

            //初始化槽位
            base.OnEnable();

            //更新UI
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)m_LayoutGroup.transform);
            UpdateCraftState();
        }

        /// <summary>
        /// Always Update Progress
        /// </summary>
        private void Update()
        {
            m_ProgressBar.UpdateProgress(m_CraftingTable.CraftingProgress);
        }

        /// <summary>
        /// Uninitialize
        /// </summary>
        protected override void OnDisable()
        {
            base.OnEnable();
            m_CraftingTable = null;
            m_CraftingTableInventory = null;
            m_Inventory = null;
        }

        /// <summary>
        /// Craft, called by crafting button
        /// </summary>
        public void Craft()
        {
            m_CraftingTable.Craft();
        }

        /// <summary>
        ///     在物品变更的基础上, 获取满足的Recipe
        /// </summary>
        /// <param name="slot"></param>
        /// <param name="item"></param>
        protected override void RefreshSlot(int slot, InventoryItem item)
        {
            base.RefreshSlot(slot, item);
            UpdateCraftState();
        }

        /// <summary>
        /// Update crafting state, is crafting or not
        /// </summary>
        private void UpdateCraftState()
        {
            m_CurrentRecipe = CraftingManager.Instance.Recipes.FirstOrDefault(x => x.CanAccept(m_Inventory.Items));
            m_CraftingTable.Recipe = m_CurrentRecipe;
            UpdateResultPreview();
            m_CraftButton.interactable = m_CurrentRecipe != null;
        }

        /// <summary>
        /// update result preview
        /// </summary>
        private void UpdateResultPreview()
        {
            if (m_CurrentRecipe != null)
            {
                var item = m_CurrentRecipe.Result.FirstOrDefault();
                if (item.item != null)
                {
                    m_ResultSlot.SetPreview(item);
                    return;
                }
            }
            m_ResultSlot.SetPreview(InventoryItem.Null);
        }

        /// <summary>
        /// Refresh slots, added extra support for m_ResultSlot.
        /// </summary>
        protected override void RefreshSlotInternal(int slot, ItemDefinition item, float quantity)
        {
            if (slot == -1)
            {
                m_ResultSlot.UpdateItem(m_Inventory.GetItemAt(slot).GetValueOrDefault());
                return;
            }
            base.RefreshSlotInternal(slot, item, quantity);
        }

        /// <summary>
        /// Initialize Slot, added extra support for m_ResultSlot.
        /// </summary>
        protected override void InitializeSlots()
        {
            base.InitializeSlots();
            InitializeSlot(m_ResultSlot, -1, m_ResultSlotStyle);
        }

        /// <summary>
        /// Open method
        /// </summary>
        /// <param name="craftingTable">crafting table to bind</param>
        public override void Open(IContainer craftingTable)
        {
            m_CraftingTable = (Interactable.CraftingTable)craftingTable;
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Close method
        /// </summary>
        public override void Close()
        {
            gameObject.SetActive(false);
        }
    }
}