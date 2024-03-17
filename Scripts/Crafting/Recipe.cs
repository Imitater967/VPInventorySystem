using System;
using System.Collections.Generic;
using System.Linq;
using Imitater967.VoxelPlay.InventorySystem.Inventory;
using UnityEngine;
using VoxelPlay;

namespace Imitater967.VoxelPlay.InventorySystem.Crafting
{
    /// <summary>
    /// 配方额外数据
    /// </summary>
    [Serializable]
    public class RecipeMetadata
    {
        public float Time;
        public bool Unordered;
    }

    [Serializable]
    [CreateAssetMenu(menuName = "YZHSoftWare/VPCraftingTable/Recipe")]
    public class Recipe : ScriptableObject
    {
        [Tooltip("配方")]
        [SerializeField] protected InventoryItem[] m_Ingredients;
        [Tooltip("结果")]
        [SerializeField] protected InventoryItem[] m_Result;
        [Tooltip("配方额外数据")]
        [SerializeField] protected RecipeMetadata m_Metadata;

        public InventoryItem[] Ingredients { get => m_Ingredients; }

        public InventoryItem[] Result { get => m_Result; }

        public RecipeMetadata Metadata { get => m_Metadata; }
        
        
        /// <summary>
        /// 立刻合成指定物品
        /// </summary>
        /// <param name="inventory">用于合成的背包</param>
        public void Craft(ICraftingInventory inventory)
        {
            if (!CanAccept(inventory.Items))
            {
                return;
            }

            foreach (var ingredientItem in m_Ingredients)
            {
                inventory.RemoveInventoryItem(ingredientItem);
            }

            inventory.SetCraftingResult(m_Result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src">检测的背包</param>
        /// <returns>这个背包的物品是否刚刚好满足这个配方,不多不少</returns>
        public bool CanAccept(IReadOnlyList<InventoryItem> src)
        {
            if (m_Metadata.Unordered)
            {
                return CanAcceptUnordered(src);
            }

            return CanAcceptOrdered(src);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src">需要检测的背包</param>
        /// <returns>按照有序配方,满足合成条件</returns>
        internal bool CanAcceptOrdered(IReadOnlyList<InventoryItem> src)
        {
            if (!RecipeSizeCheck(src))
            {
                return false;
            }

            for (var i = 0; i < src.Count; i++)
            {
                InventoryItem srcItem = src[i];
                //检查配方范围内的物品是否满足
                if (i < m_Ingredients.Length)
                {
                    InventoryItem recipeItem = m_Ingredients[i];
                    if (srcItem.item != recipeItem.item || srcItem.quantity < recipeItem.quantity)
                    {
                        return false;
                    }
                    continue;
                }
                //检查是否有其他杂物
                if (srcItem.item != null)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="src">需要检测的背包</param>
        /// <returns>按照无序配方,满足合成条件</returns>
        internal bool CanAcceptUnordered(IReadOnlyList<InventoryItem> src)
        {
            if (!RecipeSizeCheck(src))
            {
                return false;
            }

            for (var i = 0; i < m_Ingredients.Length; i++)
            {
                bool hasEnoughMaterial = src.Any(x =>
                    x.item == m_Ingredients[i].item && x.quantity >= m_Ingredients[i].quantity);
                if (!hasEnoughMaterial)
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        ///  如果在一个九宫格的配方里使用四宫格的物品, 那么可以直接跳过判断了
        /// </summary>
        /// <returns>配方的长度是否大于原材料的长度, 比如九宫格>四宫格</returns>
        private bool RecipeSizeCheck(IReadOnlyList<InventoryItem> src)
        {
            if (m_Ingredients.Length > src.Count)
            {
                return false;
            }

            return true;
        }
    }
}