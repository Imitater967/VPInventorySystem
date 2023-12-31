using UnityEngine;
using VoxelPlay;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Character
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