using Imitater967.VoxelPlay.InventorySystem.Character;
using Imitater967.VoxelPlay.InventorySystem.Crafting;
using Imitater967.VoxelPlay.InventorySystem.UI;
using UnityEngine;
using UnityEngine.Serialization;
using VoxelPlay;

namespace Imitater967.VoxelPlay.InventorySystem.Manager
{
    public class CraftingManager : MonoBehaviour
    {
        [Tooltip("绑定的合成台面板")]
        [SerializeField] protected CraftingPanel m_CraftingPanel;

        
        [Tooltip("绑定的玩家背包面板")]
        [SerializeField] protected PlayerInventoryPanel m_PlayerInventoryPanel;
        
        [Tooltip("加载了的所有的配方")]
        [SerializeField] protected Recipe[] m_Recipes;

        protected static CraftingManager s_Instance;
        
        public Recipe[] Recipes { get => m_Recipes; }
        public static CraftingManager Instance { get => s_Instance; }
        protected VoxelPlayFirstPersonController m_Controller;

        /// <summary>
        /// Initialize
        /// Load Recipes
        /// </summary>
        protected virtual  void Awake()
        {
            s_Instance = this;
            m_Recipes = Resources.LoadAll<Recipe>("Recipes");
        }

        /// <summary>
        /// Fetch character instance
        /// </summary>
        private void Start()
        {
            //第一次执行
            m_Controller = VoxelPlayFirstPersonController.instance;
        }

        /// <summary>
        /// close panel, when escape was pressed
        /// </summary>
        protected virtual  void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ClosePanels();
            }
        }

        /// <summary>
        /// The actual closing method
        /// </summary>
        protected virtual void ClosePanels()
        {
            m_PlayerInventoryPanel.gameObject.SetActive(false);
            m_CraftingPanel.Close();
            VoxelPlayEnvironment.instance.input.enabled = true;
            m_Controller.mouseLook.SetCursorLock(true);
        }

        /// <summary>
        /// create a panel for a crafting table
        /// </summary>
        /// <param name="craftingTable">the table to open</param>
        public static void OpenCraftingPanel(Interactable.CraftingTable craftingTable)
        {
            s_Instance.OpenCraftingPanelInternal(craftingTable);
        }
        
        /// <summary>
        /// create a panel for a crafting table
        /// </summary>
        /// <param name="craftingTable">the table to open</param>
        protected virtual  void OpenCraftingPanelInternal(Interactable.CraftingTable craftingTable)
        {
            m_PlayerInventoryPanel.Open(m_Controller.GetComponent<VoxelPlayPlayerBridged>());
            m_CraftingPanel.Open(craftingTable);
            VoxelPlayEnvironment.instance.input.enabled = false;
            m_Controller.mouseLook.SetCursorLock(false);
        }
    }
}