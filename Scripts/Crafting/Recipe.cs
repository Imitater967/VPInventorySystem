using System;
using System.Collections.Generic;
using System.Linq;
using VoxelPlay;

namespace ZhaoHuiSoftware.VoxelPlayMod.CraftingTable.Crafting
{
    [Serializable]
    public partial class RecipeMetadata
    {
        public float Time;
        public bool Unordered;
    }
    
    [Serializable]
    public partial class Recipe
    {
        public InventoryItem[] Ingredients;
        public InventoryItem[] Result;
        
        public RecipeMetadata Metadata;
        
        
        public bool CanAccept(IReadOnlyList<InventoryItem> src)
        {
            if (Metadata.Unordered)
            {
                return false;
            }

            return CanAcceptOrdered(src);
        }

        internal bool CanAcceptOrdered(IReadOnlyList<InventoryItem> src)
        {
            if (!RecipeSizeCheck(src))
            {
                return false;
            }

            for (var i = 0; i < Ingredients.Length; i++)
            {
                InventoryItem srcItem = src[i];
                InventoryItem recipeItem = Ingredients[i];
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

            for (var i = 0; i < Ingredients.Length; i++)
            {
                bool hasEnoughMaterial = src.Any(x => x.item == Ingredients[i].item && x.quantity >= Ingredients[i].quantity);
                if (!hasEnoughMaterial)
                {
                    return false;
                }
            }

            return true;
        }

        public void Merge()
        {
            
        }
        
        /// <summary>
        ///判断配方的长度是否大于原材料的长度, 比如九宫格>四宫格
        /// </summary>
        private bool RecipeSizeCheck(IReadOnlyList<InventoryItem> src)
        {
            if (Ingredients.Length > src.Count)
            {
                return false;
            }

            return true;
        }
    }
}