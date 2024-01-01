using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VoxelPlay;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Crafting;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Inventory
{
    public class CraftingTableInventory : BasicInventory, ICraftingInventory
    {
        public InventoryItem Result
        {
            get => m_Result;
            set => m_Result = value;
        }

        [SerializeField] protected bool m_Crafting;
        [SerializeField] protected InventoryItem m_Result;
        [SerializeField] protected float m_Progress = 0;
        protected Coroutine m_CraftTask;
        public float Progress => m_Progress;

        public override bool CanSwapItem(IInventory invA, int indexA, IInventory invB, int indexB)
        {
            return indexB != -1&&!m_Crafting;
        }

        public virtual void Craft(Recipe recipe)
        {
            //正在合成中
            if (m_CraftTask != null)
            {
                return;
            }
            m_CraftTask = StartCoroutine(StartCraft(recipe));
        }

        protected virtual IEnumerator StartCraft(Recipe recipe)
        {
            var requireTime = recipe.Metadata.Time;
            float craftTime = Time.time + requireTime;
            
            while (Time.time < craftTime)
            {
                m_Progress = (craftTime - Time.time) / requireTime;
                yield return new WaitForSeconds(0.1f);
            }
            
            recipe.Craft(this);
            
            //reset state
            m_Crafting = false;
            m_Progress = 0;
            m_CraftTask = null;
        }

        public override InventoryItem? GetItemAt(int index)
        {
            if (index == -1)
            {
                return m_Result;
            }

            return base.GetItemAt(index);
        }

        public void SetCraftingResult(IReadOnlyList<InventoryItem> items)
        {
            var result = items.FirstOrDefault(x => x.item != null);
            m_Result = result;
        }

        public void SetCraftingResult(InventoryItem[] items)
        {
            SetCraftingResult(items as IReadOnlyList<InventoryItem>);
        }
    }
}