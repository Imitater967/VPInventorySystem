using System;
using Imitater967.VoxelPlay.InventorySystem.Character;
using Imitater967.VoxelPlay.InventorySystem.Interactable;
using Imitater967.VoxelPlay.InventorySystem.UI;
using UnityEngine;
using UnityEngine.Serialization;
using VoxelPlay;

namespace Imitater967.VoxelPlay.InventorySystem.Manager
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
        protected VoxelPlayFirstPersonController m_Controller;

        /// <summary>
        /// Initialize
        /// </summary>
        protected virtual void Awake()
        {
            s_Instance = this;
        }

        /// <summary>
        /// Get current player
        /// </summary>
        protected virtual void Start()
        {
            m_Controller = VoxelPlayFirstPersonController.instance;
        }

        /// <summary>
        /// 这行代码写的不是很好, 是用于检测关闭的
        /// </summary>
        protected virtual void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ClosePanels();
            }
        }

        /// <summary>
        /// 关闭开启箱子背包的时候所有相关的面板
        /// </summary>
        protected virtual void ClosePanels()
        {
            m_PlayerInventoryPanel.gameObject.SetActive(false);
            m_ChestInventoryPanel.Close();
            VoxelPlayEnvironment.instance.input.enabled = true;
            m_Controller.mouseLook.SetCursorLock(true);
        }

        /// <summary>
        /// 开启箱子GUI
        /// </summary>
        public static void OpenChestPanel(Chest chest)
        {
            s_Instance.OpenChestPanelInternal(chest);
        }

        /// <summary>
        /// 开启箱子GUI
        /// </summary>
        protected virtual void OpenChestPanelInternal(Chest chest)
        {
            m_PlayerInventoryPanel.Open(m_Controller.GetComponent<VoxelPlayPlayerBridged>());
            m_ChestInventoryPanel.Open(chest);
            VoxelPlayEnvironment.instance.input.enabled = false;
            m_Controller.mouseLook.SetCursorLock(false);
        }
    }
}