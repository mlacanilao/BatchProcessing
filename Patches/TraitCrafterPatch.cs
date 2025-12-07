using System.Collections;
using Cwl.Helper.Unity;
using UnityEngine;

namespace BatchProcessing
{
    internal static class TraitCrafterPatch
    {
        internal static void CraftPostfix(TraitCrafter __instance, AI_UseCrafter ai, ref Thing __result)
        {
            if (EClass.pc?.ai is AI_UseCrafter == false)
            {
                return;
            }

            SourceRecipe.Row row = BatchProcessingUtils.GetSourceRow(ai: ai);
            if (row is null)
            {
                return;
            }

            int ingredientMultiplier = Mathf.Max(
                a: 1,
                b: BatchProcessingUtils.CachedIngredientMultiplier
            );

            if (ingredientMultiplier == 1)
            {
                return;
            }

            __result?.SetNum(a: __result.Num * ingredientMultiplier);
        }

        internal static void GetCostSpPostfix(TraitCrafter __instance, AI_UseCrafter ai, ref int __result)
        {
            if (EClass.pc?.ai is AI_UseCrafter == false)
            {
                return;
            }

            SourceRecipe.Row row = BatchProcessingUtils.GetSourceRow(ai: ai);
            if (row is null)
            {
                return;
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