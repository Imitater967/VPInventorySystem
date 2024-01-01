using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.UI
{
    public class CraftingProgressBar: MonoBehaviour
    {
        [SerializeField] protected RectTransform m_Fill;
        [Tooltip("Dont edit, change at runtime")]
        [SerializeField] protected float m_FillPositionOfX;
        
        [Tooltip("Dont edit, change at runtime")]
        [SerializeField] protected float m_EmptyPositionOfX;

        private void Awake()
        {
            m_FillPositionOfX = m_Fill.transform.localPosition.x;
            m_EmptyPositionOfX = m_FillPositionOfX - m_Fill.sizeDelta.x;
        }

        public void UpdateProgress(float progress)
        {
            var x = (m_Fill.sizeDelta.x * progress) + m_EmptyPositionOfX;
            m_Fill.localPosition = new Vector3( x, m_Fill.localPosition.y);
        }
    }
}