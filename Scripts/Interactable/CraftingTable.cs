using Imitater967.VoxelPlay.InventorySystem.Crafting;
using Imitater967.VoxelPlay.InventorySystem.Inventory;
using Imitater967.VoxelPlay.InventorySystem.Manager;
using UnityEngine;
using VoxelPlay;

namespace Imitater967.VoxelPlay.InventorySystem.Interactable
{
    public class CraftingTable : VoxelPlayInteractiveObject, IContainer
    {


        [Tooltip("绑定的合成台背包")]
        [SerializeField]
        private CraftingTableInventory m_Inventory;
        
        [Tooltip("当前合成台使用的配方")]
        [SerializeField]
        protected Recipe m_Recipe;
        public float CraftingProgress { get => m_Inventory.CraftingProgress; }
        public bool IsCrafting { get => m_Inventory.IsCrafting; }
        public CraftingTableInventory Inventory { get => m_Inventory; }
        public Recipe Recipe
        {
            get => m_Recipe;
            set => m_Recipe = value;
        }
        
        /// <summary>
        /// 当玩家按下{Action}按键的时候,开启合成面板
        /// </summary>
        public override void OnPlayerAction()
        {
            CraftingManager.OpenCraftingPanel(this);
        }

        /// <summary>
        /// 开始根据当前配方进行合成
        /// </summary>
        public void Craft()
        {
            m_Inventory.Craft(m_Recipe);
        }

        /// <summary>
        /// IContainer::GetInventory
        /// </summary>
        /// <returns>当前绑定的背包</returns>
        public IInventory GetInventory()
        {
            return m_Inventory;
        }
    }
}