using System.Collections.Generic;
using UnityEngine;

namespace BatchProcessing
{
    internal static class LayerBaseCraftPatch
    {
        internal static void GetReqIngredientPostfix(LayerBaseCraft __instance, int index, ref int __result)
        {
            if (EClass.pc?.ai is AI_UseCrafter == false)
            {
                return;
            }

            AI_UseCrafter ai = (AI_UseCrafter)EClass.pc?.ai;

            if (ai == null)
            {
                return;
            }
            
            SourceRecipe.Row row = BatchProcessingUtils.GetSourceRow(ai: ai);

            if (row is null)
            {
                return;
            }
            
            int ingredientMultiplier = BatchProcessingUtils.GetSafeIngredientMultiplier(ai: ai);

            if (ingredientMultiplier == 1)
            {
                return;
            }
            
            __result *= ingredientMultiplier;
        }
    }
}