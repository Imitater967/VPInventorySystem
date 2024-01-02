using UnityEngine;
using VoxelPlay;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Inventory;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Manager;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.UI;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Interactable
{
    public class Chest: VoxelPlayInteractiveObject,IContainer
    {
        [SerializeField]
        protected BasicInventory m_Inventory;

        public BasicInventory Inventory => m_Inventory;

        public override void OnPlayerAction()
        {
            base.OnPlayerAction();
            ChestManager.OpenChestPanel(this);
        }

        public IInventory GetInventory()
        {
            return m_Inventory;
        }
    }
}