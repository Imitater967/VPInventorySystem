using System;
using UnityEngine;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Inventory;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.UI
{
    public class CraftingPanel: InventoryPanel
    {
        private static CraftingPanel s_Instance;
        private CraftingTableInventory m_CraftingTableInventory;


        [SerializeField]
        protected Interactable.CraftingTable m_CraftingTable;
        
        private void Awake()
        {
            s_Instance = this;
        }

        protected override void Start()
        {
        }

        private void OnEnable()
        {
            m_CraftingTableInventory = m_CraftingTable.Inventory;
            m_Inventory = m_CraftingTableInventory;
            base.InitializeSlot();
        }

        private void Open(Interactable.CraftingTable craftingTable)
        {
            m_CraftingTable = craftingTable;
            gameObject.SetActive(true);
        }
        
        
        public static void OpenCraftingPanel(Interactable.CraftingTable craftingTable)
        {
            s_Instance.Open(craftingTable);
        }
    }
}