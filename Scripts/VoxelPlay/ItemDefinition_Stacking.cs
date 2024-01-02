using UnityEngine;

namespace VoxelPlay
{
    /// <summary>
    /// Because I'm not allowed to included any VoxelPlay code, so I can't create an editor for it.
    /// Please edit this on Debug mode of inspector gui.
    /// Sorry for that
    /// </summary>
    public partial class ItemDefinition
    {
        public int MaxStackSize { get => Mathf.Max(1, m_MaxStackSize); }

        [Tooltip("Max stack size")]
        [SerializeField]
        private int m_MaxStackSize = 16;
    }
}