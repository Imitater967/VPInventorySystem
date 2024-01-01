using UnityEngine;
using VoxelPlay;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Crafting;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Inventory;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.UI;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Interactable
{
    public class CraftingTable: VoxelPlayInteractiveObject
    {
        [SerializeField]
        private CraftingTableInventory m_Inventory;

        public CraftingTableInventory Inventory => m_Inventory;
        public override void OnPlayerAction()
        {
            CraftingManager.OpenCraftingPanel(this);
        }

        public void Craft()
        {
            Recipe recipe = null;
            //todo
            m_Inventory.Craft(recipe);
        }
    }
}