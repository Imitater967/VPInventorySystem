using UnityEngine;
using VoxelPlay;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable
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
            var ingredient = m_Inventory.Items;
            var result = new InventoryItem();
        }
    }
}