using UnityEngine;

namespace VoxelPlay
{
    public partial class ItemDefinition
    {
        public int MaxStackSize { get => Mathf.Max(1, m_MaxStackSize); }

        [SerializeField]
        private int m_MaxStackSize = 16;
    }
}