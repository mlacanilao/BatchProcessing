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

            AI_UseCrafter ai = (AI_UseCrafter)EClass.pc.ai;

            if (ai == null)
            {
                return;
            }

            SourceRecipe.Row row = BatchProcessingUtils.GetSourceRow(ai: ai);
            if (row is null)
            {
                return;
            }

            if (index == 0)
            {
                int computedMultiplier = BatchProcessingUtils.GetSafeIngredientMultiplier(ai: ai);
                BatchProcessingUtils.CachedIngredientMultiplier = Mathf.Max(
                    a: 1,
                    b: computedMultiplier
                );
            }

            int ingredientMultiplier = Mathf.Max(
                a: 1,
                b: BatchProcessingUtils.CachedIngredientMultiplier
            );

            if (ingredientMultiplier == 1)
            {
                return;
            }

            __result *= ingredientMultiplier;
        }
    }
}