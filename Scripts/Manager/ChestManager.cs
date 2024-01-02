using System;
using UnityEngine;
using UnityEngine.Serialization;
using VoxelPlay;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Character;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Interactable;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.UI;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Manager
{
    public class ChestManager : MonoBehaviour
    {
        [Tooltip("箱子UI")]
        [SerializeField]
        protected ChestInventoryPanel m_ChestInventoryPanel;

        [Tooltip("玩家UI")]        
        [SerializeField]
        protected PlayerInventoryPanel m_PlayerInventoryPanel;

        private static ChestManager s_Instance;
        public static ChestManager Instance { get => s_Instance; }
        private VoxelPlayFirstPersonController m_Controller;

        /// <summary>
        /// Initialize
        /// </summary>
        private void Awake()
        {
            s_Instance = this;
        }

        /// <summary>
        /// Get current player
        /// </summary>
        private void Start()
        {
            m_Controller = VoxelPlayFirstPersonController.instance;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ClosePanels();
            }
        }

        private void ClosePanels()
        {
            m_PlayerInventoryPanel.gameObject.SetActive(false);
            m_ChestInventoryPanel.Close();
            VoxelPlayEnvironment.instance.input.enabled = true;
            m_Controller.mouseLook.SetCursorLock(true);
        }

        public static void OpenChestPanel(Chest chest)
        {
            s_Instance.OpenChestPanelInternal(chest);
        }

        private void OpenChestPanelInternal(Chest chest)
        {
            m_PlayerInventoryPanel.Open(m_Controller.GetComponent<VoxelPlayPlayerBridged>());
            m_ChestInventoryPanel.Open(chest);
            VoxelPlayEnvironment.instance.input.enabled = false;
            m_Controller.mouseLook.SetCursorLock(false);
        }
    }
}