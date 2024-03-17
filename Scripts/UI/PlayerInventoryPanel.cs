using Imitater967.VoxelPlay.InventorySystem.Character;
using Imitater967.VoxelPlay.InventorySystem.Interactable;
using Imitater967.VoxelPlay.InventorySystem.Inventory;

namespace Imitater967.VoxelPlay.InventorySystem.UI
{
    public class PlayerInventoryPanel : InventoryPanel
    {
        private VoxelPlayPlayerBridged m_BridgedPlayer;
        private PlayerInventory m_PlayerInventory;


        public override void Open(IContainer container)
        {
            m_BridgedPlayer = (VoxelPlayPlayerBridged)container;
            m_PlayerInventory = m_BridgedPlayer.Inventory;
            m_Inventory = m_PlayerInventory;
            gameObject.SetActive(true);
        }

        public override void Close()
        {
            gameObject.SetActive(false);
        }

        // protected override void InitializeSlots()
        // {
        //     base.InitializeSlots();
        //     m_PlayerInventory.OnItemSelectedChanged += OnItemSelected;
        //     OnItemSelected(m_PlayerInventory.SelectedItemIndex,0);
        // }

        // private void OnItemSelected(int selecteditemindex, int prevselecteditemindex)
        // {
        //     if (prevselecteditemindex!=-1)
        //     {
        //         m_Slots[prevselecteditemindex].ToggleSelectedMask(false);
        //         
        //     }
        //
        //     if (selecteditemindex != -1)
        //     {
        //         m_Slots[selecteditemindex].ToggleSelectedMask(true);
        //         
        //     }
        // }

        private void OnDestroy()
        {
            if (m_PlayerInventory != null)
            {
                // m_PlayerInventory.OnItemSelectedChanged -= OnItemSelected;
            }
        }
    }
}