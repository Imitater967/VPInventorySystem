using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VoxelPlay;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Inventory;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.UI
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] protected TMP_Text m_QuantityView;

        [SerializeField] protected RawImage m_Icon;

        [SerializeField] protected Image m_Background;


        // [SerializeField] protected Image m_SelectMask;

        [SerializeField] protected ItemDragAndDrop m_DragAndDrop;

        [SerializeField] protected int m_SlotIndex;

        [SerializeField] protected IInventory m_Inventory;
        
        protected InventorySlotStyle m_Style;

        public TMP_Text QuantityView
        {
            get => m_QuantityView;
        }

        public RawImage Icon
        {
            get => m_Icon;
        }

        public Image Background
        {
            get => m_Background;
        }

        // public Image SelectMask
        // {
        //     get => m_SelectMask;
        // }

        public ItemDragAndDrop DropAndDrag => m_DragAndDrop;
        
        public int SlotIndex => m_SlotIndex;

        public IInventory Inventory => m_Inventory;
        
        public virtual void ApplyStyle(InventorySlotStyle style)
        {
            m_Style = style;
            m_Background.sprite = style.BackgroundTexture;
            // m_SelectMask.sprite = style.SelectMaskTexture;
        }

        public void Initialize(IInventory inv, int i, InventorySlotStyle slotStyle)
        {
            m_Inventory = inv;
            name = $"Slot - {i}";
            m_SlotIndex = i;
            ApplyStyle(slotStyle);
            // m_SelectMask.gameObject.SetActive(false);
            if (m_DragAndDrop != null)
            {
                m_DragAndDrop.Initialize(this);
            }
        }


        // public void ToggleSelectedMask(bool enable)
        // {
        //
        //     m_SelectMask.gameObject.SetActive(enable);
        // }

        // public void ToggleHover(bool hover)
        // {
        //     m_Background.sprite = hover ? m_Style.BackgroundHoverTexture : m_Style.BackgroundTexture;
        // }

        public void UpdateItem(InventoryItem result)
        {
            UpdateItem(result.item, result.quantity);
        }

        public void UpdateItem(ItemDefinition result, float quantity)
        {
            m_QuantityView.gameObject.SetActive(quantity != 0);
            m_QuantityView.text = "x" + quantity;
            m_Icon.gameObject.SetActive(result != null);
            m_Icon.texture = result?.icon;
        }
    }
}