using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Inventory;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Interactable
{
    public interface IContainer
    {
        public IInventory GetInventory();
    }
}