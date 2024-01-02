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
            m_Inventory = null;
        }
    }
}