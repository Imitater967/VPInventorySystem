using System;
using UnityEngine;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Character;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Inventory;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.UI
{
    public class PlayerInventoryPanel: InventoryPanel
    {
        private VoxelPlayPlayerBridged m_BridgedPlayer;
        private PlayerInventory m_PlayerInventory;

        protected override void Start()
        {
            m_BridgedPlayer = (VoxelPlayPlayerBridged)VoxelPlayPlayerBridged.instance;
            m_PlayerInventory = m_BridgedPlayer.Inventory;
            m_Inventory = m_PlayerInventory;
            base.Start();
        }

        protected override void InitializeSlots()
        {
            base.InitializeSlots();
            m_PlayerInventory.OnItemSelectedChanged += OnItemSelected;
        }

        private void OnItemSelected(int selecteditemindex, int prevselecteditemindex)
        {
            m_Slots[selecteditemindex].ToggleSelectedMask(true);
            m_Slots[prevselecteditemindex].ToggleSelectedMask(false);
        }

        private void OnDestroy()
        {
            if (m_PlayerInventory != null)
            {
                m_PlayerInventory.OnItemSelectedChanged -= OnItemSelected;
            }
        }
    }
}