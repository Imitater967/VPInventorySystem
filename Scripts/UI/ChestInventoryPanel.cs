using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Interactable;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Inventory;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.UI
{
    public class ChestInventoryPanel: InventoryPanel
    {
        public override void Open(IContainer container)
        {
            m_Inventory = container.GetInventory();
            gameObject.SetActive(true);
        }

        public override void Close()
        {
            gameObject.SetActive(false);
            //这个非常必要,因为VoxelPlay只是临时启用这个方块, 所以会有内存相关的问题
            m_Inventory = null;
        }
    }
}