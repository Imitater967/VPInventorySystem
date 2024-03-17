using UnityEngine;

namespace Imitater967.VoxelPlay.InventorySystem.UI
{
    public class CraftingProgressBar : MonoBehaviour
    {
        [Tooltip("The fill area of progress bar")]
        [SerializeField] protected RectTransform m_Fill;

        [Tooltip("Dont edit, change at runtime\n, The x coord when it's empty")]
        [SerializeField] protected float m_EmptyPositionOfX;

        /// <summary>
        /// Initialize
        /// </summary>
        private void Awake()
        {
            m_EmptyPositionOfX = m_Fill.localPosition.x - m_Fill.sizeDelta.x;
        }

        /// <summary>
        /// Update the progress of the progressbar
        /// </summary>
        /// <param name="progress">the progress 0~1</param>
        public void UpdateProgress(float progress)
        {
            var x = (m_Fill.sizeDelta.x * progress) + m_EmptyPositionOfX;
            m_Fill.localPosition = new Vector3(x, m_Fill.localPosition.y);
        }
    }
}