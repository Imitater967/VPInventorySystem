using Imitater967.VoxelPlay.InventorySystem.Interactable;

namespace Imitater967.VoxelPlay.InventorySystem.UI
{
    /// <summary>
    /// Chest Panel
    /// </summary>
    public class ChestInventoryPanel : InventoryPanel
    {
        /// <summary>
        /// Open the panel
        /// </summary>
        /// <param name="container">chest to bind</param>
        public override void Open(IContainer container)
        {
            m_Inventory = container.GetInventory();
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Close the panel
        /// </summary>
        public override void Close()
        {
            gameObject.SetActive(false);
            //这个非常必要,因为VoxelPlay只是临时启用这个方块, 所以会有内存相关的问题
            m_Inventory = null;
        }
    }
}