using UnityEngine;
using VoxelPlay;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Interactable
{
    public class Chest: VoxelPlayInteractiveObject
    {
        public override void OnPlayerAction()
        {
            base.OnPlayerAction();
            Debug.LogError("Action Chest");
        }
    }
}