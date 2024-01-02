using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using VoxelPlay;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Character;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Crafting;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.UI;


namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Manager
{
    public class CraftingManager: MonoBehaviour
    {
        [SerializeField] protected CraftingPanel m_CraftingPanel;
        [FormerlySerializedAs("m_InventoryPanel")] [SerializeField] protected PlayerInventoryPanel m_PlayerInventoryPanel;
        [SerializeField] protected Recipe[] m_Recipes;
        public Recipe[] Recipes => m_Recipes;
        private static CraftingManager s_Instance;
        public static CraftingManager Instance => s_Instance;
        private VoxelPlayFirstPersonController m_Controller;
        
        private void Awake()
        {
            s_Instance = this;
            m_Recipes = Resources.LoadAll<Recipe>("Recipes");
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
            m_PlayerInventoryPanel.gameObject.SetActive(false);
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
            m_PlayerInventoryPanel.Open(m_Controller.GetComponent<VoxelPlayPlayerBridged>());
            m_CraftingPanel.Open(craftingTable);
            VoxelPlayEnvironment.instance.input.enabled = false;
            m_Controller.mouseLook.SetCursorLock(false);
        }
        
    }
}