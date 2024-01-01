using System;
using Unity.VisualScripting;
using UnityEngine;
using VoxelPlay;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.UI
{
    public class PanelManager: MonoBehaviour
    {
        [SerializeField] protected CraftingPanel m_CraftingPanel;
        private static PanelManager s_Instance;
        private VoxelPlayFirstPersonController m_Controller;
        private void Awake()
        {
            s_Instance = this;
        }

        private void Start()
        {
            //第一次执行
            m_Controller = VoxelPlayFirstPersonController.instance;
        }

        private void OnEnable()
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
            m_CraftingPanel.Close();
            VoxelPlayEnvironment.instance.input.enabled = true;
            m_Controller.mouseLook.SetCursorLock(true);
        }

        public static void OpenCraftingPanel(Interactable.CraftingTable craftingTable)
        {
            s_Instance.OpenCraftingPanelInternal(craftingTable);
        }

        private void OpenCraftingPanelInternal(Interactable.CraftingTable craftingTable)
        {
            m_CraftingPanel.Open(craftingTable);
            VoxelPlayEnvironment.instance.input.enabled = false;
            m_Controller.mouseLook.SetCursorLock(false);
        }
        
    }
}