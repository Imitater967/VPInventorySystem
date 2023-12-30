using UnityEngine;
using VoxelPlay;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable
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