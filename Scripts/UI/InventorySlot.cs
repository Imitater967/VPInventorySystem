using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VoxelPlay;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Inventory;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.UI
{
    /// <summary>
    /// The slot represents inventory slot, lol.
    /// V in MVC
    /// The Model is IInventory :D.
    /// </summary>
    public class InventorySlot : MonoBehaviour
    {
        [Tooltip("物品数量")]
        [SerializeField] protected TMP_Text m_QuantityView;

        [Tooltip("物品图标")]
        [SerializeField] protected RawImage m_Icon;

        [Tooltip("物品背景")]
        [SerializeField] protected Image m_Background;

        // I removed this, but it's able to work, uncomment this if you want
        // or may be i should use marco instead?
        // [SerializeField] protected Image m_SelectMask;

        [FormerlySerializedAs("m_DragAndDrop")]
        [Tooltip("背包拖曳组件")]
        [SerializeField] protected ItemDragHandler m_DragHandler;

        [Tooltip("绑定的Slot槽位")]
        [SerializeField] protected int m_SlotIndex;

        [Tooltip("所属的背包")]
        [SerializeField] protected IInventory m_Inventory;

        // [Tooltip("绑定的样式")]
        // [SerializeField] protected InventorySlotStyle m_Style;

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

        public ItemDragHandler DropAndDrag { get => m_DragHandler; }

        public int SlotIndex { get => m_SlotIndex; }

        public IInventory Inventory { get => m_Inventory; }

        /// <summary>
        /// 应用样式
        /// </summary>
        protected virtual void ApplyStyle(InventorySlotStyle style)
        {
            // m_Style = style;
            m_Background.sprite = style.BackgroundTexture;
            // m_SelectMask.sprite = style.SelectMaskTexture;
        }

        /// <summary>
        /// Initialize
        /// </summary>
        /// <param name="inv">Inventory to bind</param>
        /// <param name="i">Index of the slot</param>
        /// <param name="slotStyle">stlpe to apply</param>
        public void Initialize(IInventory inv, int i, InventorySlotStyle slotStyle)
        {
            m_Inventory = inv;
            name = $"Slot - {i}";
            m_SlotIndex = i;
            ApplyStyle(slotStyle);
            // m_SelectMask.gameObject.SetActive(false);
            if (m_DragHandler != null)
            {
                m_DragHandler.Initialize(this);
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

        /// <summary>
        /// Update view
        /// </summary>
        public void UpdateItem(InventoryItem result)
        {
            UpdateItem(result.item, result.quantity);
        }

        /// <summary>
        /// Update view
        /// </summary>
        public void UpdateItem(ItemDefinition result, float quantity)
        {
            m_QuantityView.gameObject.SetActive(quantity != 0);
            m_QuantityView.text = "x" + quantity;
            m_Icon.gameObject.SetActive(result != null);
            m_Icon.texture = result?.icon;
        }
    }
}