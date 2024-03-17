using Imitater967.VoxelPlay.InventorySystem.Inventory;
using Imitater967.VoxelPlay.InventorySystem.Manager;
using UnityEngine;
using VoxelPlay;

namespace Imitater967.VoxelPlay.InventorySystem.Interactable
{
    public class Chest : VoxelPlayInteractiveObject, IContainer
    {
        [Tooltip("绑定的背包")]
        [SerializeField]
        protected BasicInventory m_Inventory;

        public BasicInventory Inventory { get => m_Inventory; }

        /// <summary>
        /// 当玩家按下{Action}按键的时候,开启箱子面板
        /// </summary>
        public override void OnPlayerAction()
        {
            base.OnPlayerAction();
            ChestManager.OpenChestPanel(this);
        }
        
        /// <summary>
        /// IContainer::GetInventory
        /// </summary>
        /// <returns>绑定的背包</returns>
        public IInventory GetInventory()
        {
            return m_Inventory;
        }
    }
}