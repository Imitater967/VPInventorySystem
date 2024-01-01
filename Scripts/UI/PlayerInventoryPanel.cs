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
            OnItemSelected(m_PlayerInventory.SelectedItemIndex,0);
        }

        private void OnItemSelected(int selecteditemindex, int prevselecteditemindex)
        {
            if (prevselecteditemindex!=-1)
            {
                m_Slots[prevselecteditemindex].ToggleSelectedMask(false);
                
            }

            if (selecteditemindex != -1)
            {
                m_Slots[selecteditemindex].ToggleSelectedMask(true);
                
            }
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