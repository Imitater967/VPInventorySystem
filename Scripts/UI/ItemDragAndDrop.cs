using UnityEngine;
using UnityEngine.EventSystems;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.UI
{
    public class ItemDragAndDrop : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler,
        ICanvasRaycastFilter
    {
        private RectTransform m_Rect;
        [SerializeField] private bool m_IsDragging;

        [Tooltip("Edit at runtime by code")] [SerializeField]
        private Transform m_DragParent;

        [SerializeField] private Transform m_OriginParent;
        private InventorySlot m_InventorySlot;

        public Transform DragParent
        {
            get => m_DragParent;
            set => m_DragParent = value;
        }

        private void Awake()
        {
            m_Rect = transform as RectTransform;
        }

        public void Initialize(InventorySlot slot)
        {
            m_InventorySlot = slot;
        }

        public void OnDrag(PointerEventData eventData)
        {
            m_Rect.position = Input.mousePosition;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_IsDragging = true;
            m_OriginParent = m_Rect.parent;
            m_Rect.SetParent(m_DragParent);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            m_IsDragging = false;
            m_Rect.SetParent(m_OriginParent);
            m_Rect.transform.localPosition = Vector3.zero;
            if (eventData.pointerCurrentRaycast.gameObject != null)
            {
                if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out InventorySlot slot))
                {
                    if (slot.Inventory.CanSwapItem(m_InventorySlot.Inventory, m_InventorySlot.SlotIndex, slot.Inventory,
                            slot.SlotIndex))
                    {
                        slot.Inventory.SwapItem(m_InventorySlot.Inventory, m_InventorySlot.SlotIndex, slot.Inventory,
                            slot.SlotIndex);
                    }
                }
            }
        }

        public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
        {
            return !m_IsDragging;
        }
    }
}