using UnityEngine;
using VoxelPlay;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Crafting;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Inventory;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.UI;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Interactable
{
    public class CraftingTable: VoxelPlayInteractiveObject, IContainer
    {
        public float CraftingProgress => m_Inventory.CraftingProgress;
        public bool IsCrafting =>m_Inventory.IsCrafting;

        [SerializeField]
        private CraftingTableInventory m_Inventory;

        public CraftingTableInventory Inventory => m_Inventory;

        [SerializeField]
        protected Recipe m_Recipe = null;

        public Recipe Recipe
        {
            get => m_Recipe;
            set => m_Recipe = value;
        }

        public override void OnPlayerAction()
        {
            CraftingManager.OpenCraftingPanel(this);
        }

        public void Craft()
        {
            m_Inventory.Craft(m_Recipe);
        }

        public IInventory GetInventory()
        {
            return m_Inventory;
        }
    }
}