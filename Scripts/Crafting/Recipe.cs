using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using VoxelPlay;
using ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Inventory;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Crafting
{
    [Serializable]
    public partial class RecipeMetadata
    {
        public float Time;
        public bool Unordered;
    }
    
    [Serializable]
    [CreateAssetMenu(menuName = "YZHSoftWare/VPCraftingTable/Recipe")]
    public partial class Recipe: ScriptableObject
    {
        [SerializeField] protected InventoryItem[] m_Ingredients;
        [SerializeField] protected InventoryItem[] m_Result;
        [SerializeField] protected RecipeMetadata m_Metadata;

        public InventoryItem[] Ingredients => m_Ingredients;

        public InventoryItem[] Result => m_Result;

        public RecipeMetadata Metadata => m_Metadata;

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
        
        public bool CanAccept(IReadOnlyList<InventoryItem> src)
        {
            if (m_Metadata.Unordered)
            {
                return CanAcceptUnordered(src);
            }

            return CanAcceptOrdered(src);
        }

        internal bool CanAcceptOrdered(IReadOnlyList<InventoryItem> src)
        {
            if (!RecipeSizeCheck(src))
            {
                return false;
            }

            for (var i = 0; i < m_Ingredients.Length; i++)
            {
                InventoryItem srcItem = src[i];
                InventoryItem recipeItem = m_Ingredients[i];
                if (srcItem.item != recipeItem.item || srcItem.quantity < recipeItem.quantity)
                {
                    return false;
                }
            }
            return true;
        }

        internal bool CanAcceptUnordered(IReadOnlyList<InventoryItem> src)
        {
            if (!RecipeSizeCheck(src))
            {
                return false;
            }

            for (var i = 0; i < m_Ingredients.Length; i++)
            {
                bool hasEnoughMaterial = src.Any(x => x.item == m_Ingredients[i].item && x.quantity >= m_Ingredients[i].quantity);
                if (!hasEnoughMaterial)
                {
                    return false;
                }
            }
            return true;
        }

      
        /// <summary>
        ///判断配方的长度是否大于原材料的长度, 比如九宫格>四宫格
        /// </summary>
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