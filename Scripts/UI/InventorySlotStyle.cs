using System;
using UnityEngine;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.UI
{
    /// <summary>
    /// Style for inventory slot
    /// </summary>
    [CreateAssetMenu(menuName = "YZHSoftWare/VPCraftingTable/InventorySlotStyle")]
    [Serializable]
    public class InventorySlotStyle : ScriptableObject
    {
        [SerializeField] protected Sprite m_BackgroundTexture;

        // [SerializeField] protected Sprite m_BackgroundHoverTexture;
        [SerializeField] protected Sprite m_SelectMaskTexture;

        public Sprite BackgroundTexture
        {
            get => m_BackgroundTexture;
            set => m_BackgroundTexture = value;
        }

        public Sprite SelectMaskTexture
        {
            get => m_SelectMaskTexture;
            set => m_SelectMaskTexture = value;
        }

        // public Sprite BackgroundHoverTexture
        // {
        //     get => m_BackgroundHoverTexture;
        //     set => m_BackgroundHoverTexture = value;
        // }
    }
}