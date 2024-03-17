using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Imitater967.VoxelPlay.InventorySystem.Crafting;
using UnityEngine;
using VoxelPlay;

namespace Imitater967.VoxelPlay.InventorySystem.Inventory
{
    public class CraftingTableInventory : BasicInventory, ICraftingInventory
    {
        public InventoryItem Result
        {
            get => m_Result;
            set => m_Result = value;
        }

        [SerializeField] protected bool m_IsCrafting;
        [SerializeField] protected InventoryItem m_Result;
        [SerializeField] protected float m_CraftingProgress;
        protected Coroutine m_CraftTask;
        public float CraftingProgress { get => m_CraftingProgress; }

        public bool IsCrafting { get => m_IsCrafting; }

        public override bool CanSwapItem(IInventory invA, int indexA, IInventory invB, int indexB)
        {
            return indexB != -1 && !m_IsCrafting;
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
                m_CraftingProgress = 1 - ((craftTime - Time.time) / requireTime);
                yield return new WaitForSeconds(0.1f);
            }

            recipe.Craft(this);

            //reset state
            m_IsCrafting = false;
            m_CraftingProgress = 0;
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

        public override void SetItemAt(int index, InventoryItem inventoryItem)
        {
            if (index == -1)
            {
                m_Result = inventoryItem;
                CallItemChange(index, inventoryItem);
                return;
            }
            base.SetItemAt(index, inventoryItem);
        }

        public void SetCraftingResult(IReadOnlyList<InventoryItem> items)
        {
            var result = items.FirstOrDefault(x => x.item != null);
            m_Result = result;
            CallItemChange(-1, m_Result);
        }

        public void SetCraftingResult(InventoryItem[] items)
        {
            SetCraftingResult(items as IReadOnlyList<InventoryItem>);
        }
    }
}