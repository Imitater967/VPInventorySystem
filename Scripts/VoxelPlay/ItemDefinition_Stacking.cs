using UnityEngine;

namespace VoxelPlay {
    public partial class ItemDefinition
    {
        public int MaxStackSize => m_MaxStackSize;
        [SerializeField]
        private int m_MaxStackSize;
    }
}