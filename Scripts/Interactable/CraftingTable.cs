using UnityEngine;
using VoxelPlay;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Crafting;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Inventory;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Interactable
{
    public class CraftingTable: VoxelPlayInteractiveObject
    {
        [SerializeField]
        private CraftingTableInventory m_Inventory;

        public CraftingTableInventory Inventory => m_Inventory;
        public override void OnPlayerAction()
        {
            base.OnPlayerAction();
            Debug.LogError("Action");
        }

        public void Craft()
        {
            Recipe recipe = null;
            //todo
            m_Inventory.Craft(recipe);
        }
    }
}