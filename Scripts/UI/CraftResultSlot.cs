using UnityEngine;
using UnityEngine.UI;
using VoxelPlay;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.UI
{
    public class CraftResultSlot : InventorySlot
    {
        [SerializeField]
        protected RawImage m_PreviewIcon;

        public void SetPreview(InventoryItem itemDefinition)
        {
            if (itemDefinition.item != null)
            {
                m_PreviewIcon.texture = itemDefinition.item.icon;
            }
            else
            {
                m_PreviewIcon.texture = null;
            }
        }
    }
}