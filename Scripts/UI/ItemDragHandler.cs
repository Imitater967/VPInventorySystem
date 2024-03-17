using UnityEngine;
using UnityEngine.EventSystems;

namespace Imitater967.VoxelPlay.InventorySystem.UI
{
    
    /// <summary>
    /// Drag Handler, C in MVC
    /// The logic handler.
    /// </summary>
    public class ItemDragHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler,
        ICanvasRaycastFilter
    {
        [Tooltip("Its rectTransform")]
        private RectTransform m_Rect;
        
        [Tooltip("Is current dragging")]
        [SerializeField] private bool m_IsDragging;

        [Tooltip("Edit at runtime by code")] [SerializeField]
        private Transform m_DragParent;
        
        [Tooltip("Origin parent, it should be the inventory slot at any time")]
        [SerializeField] private Transform m_OriginParent;
        private InventorySlot m_InventorySlot;

        public Transform DragParent
        {
            get => m_DragParent;
            set => m_DragParent = value;
        }

        /// <summary>
        /// Initialize Values.
        /// </summary>
        protected virtual void Awake()
        {
            m_Rect = transform as RectTransform;
        }

        /// <summary>
        /// Initialize by binding a slot.
        /// </summary>
        public void Initialize(InventorySlot slot)
        {
            m_InventorySlot = slot;
        }

        /// <summary>
        /// Keep item follows mouse.
        /// </summary>
        public void OnDrag(PointerEventData eventData)
        {
            m_Rect.position = Input.mousePosition;
        }

        /// <summary>
        /// When pressed, start dragging.
        /// </summary>
        public void OnPointerDown(PointerEventData eventData)
        {
            m_IsDragging = true;
            m_OriginParent = m_Rect.parent;
            m_Rect.SetParent(m_DragParent);
        }

        /// <summary>
        /// When released. execute logic if possible.
        /// </summary>
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

        /// <summary>
        /// Preventing raycast itself
        /// </summary>
        public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
        {
            return !m_IsDragging;
        }
    }
}