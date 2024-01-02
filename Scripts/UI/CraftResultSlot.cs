using UnityEngine;
using UnityEngine.UI;
using VoxelPlay;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.UI
{
    /// <summary>
    /// 合成结果槽位
    /// 在普通的槽位上添加了一个预览的功能
    /// </summary>
    public class CraftResultSlot : InventorySlot
    {
        [Tooltip("预览图片")]
        [SerializeField]
        protected RawImage m_PreviewIcon;

        /// <summary>
        /// 设置预览显示的物品
        /// </summary>
        /// <param name="invItem">用于显示的物品</param>
        public void SetPreview(InventoryItem invItem)
        {
            if (invItem.item != null)
            {
                m_PreviewIcon.texture = invItem.item.icon;
            }
            else
            {
                m_PreviewIcon.texture = null;
            }
        }
    }
}