using UnityEngine;
using UnityEngine.Serialization;
using VoxelPlay;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Character;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Crafting;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.UI
{
    public class ChestManager: MonoBehaviour
    {
        [SerializeField] protected InventoryPanel m_ChestPanel;
        [SerializeField] protected PlayerInventoryPanel m_InventoryPanel;
        private static ChestManager s_Instance;
        public static ChestManager Instance => s_Instance;
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
            m_InventoryPanel.gameObject.SetActive(false);
            m_ChestPanel.Close();
            VoxelPlayEnvironment.instance.input.enabled = true;
            m_Controller.mouseLook.SetCursorLock(true);
        }

        public static void OpenChestPanel(Interactable.Chest craftingTable)
        {
            s_Instance.OpenChestPanelInternal(craftingTable);
        }

        private void OpenChestPanelInternal(Interactable.Chest chest)
        {
            m_InventoryPanel.Open(m_Controller.GetComponent<VoxelPlayPlayerBridged>());
            m_ChestPanel.Open(chest);
            VoxelPlayEnvironment.instance.input.enabled = false;
            m_Controller.mouseLook.SetCursorLock(false);
        }
    }
}