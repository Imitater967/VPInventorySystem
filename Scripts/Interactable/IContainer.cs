using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Inventory;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Interactable
{
    /// <summary>
    /// 容器, 每一个容器必定绑定一个背包
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>容器绑定的背包</returns>
        public IInventory GetInventory();
    }
}