using UnityEngine;

namespace BatchProcessing
{
    internal static class CardPatch
    {
        internal static void ModChargePrefix(Card __instance, ref int a)
        {
            if (a >= 0 ||
                __instance.trait is TraitCrafter crafter == false ||
                __instance.sourceCard?.category != "processor" ||
                EClass.pc?.ai is AI_UseCrafter ai == false ||
                ReferenceEquals(objA: ai.crafter, objB: crafter) == false)
            {
                return;
            }
            
            SourceRecipe.Row row = BatchProcessingUtils.GetSourceRow(ai: ai);
            
            if (row is null)
            {
                return;
            }
            
            int ingredientMultiplier = Mathf.Max(a: 1, b: BatchProcessingConfig.IngredientMultiplier?.Value ?? 1);
            bool enableAutoMaxBatchMultiplier = BatchProcessingConfig.EnableAutoMaxBatchMultiplier?.Value ?? false;

            if (enableAutoMaxBatchMultiplier == true)
            {
                ingredientMultiplier = BatchProcessingUtils.MaxBatchMultiplier;
            }

            if (ingredientMultiplier == 1)
            {
                return;
            }
            
            a *= ingredientMultiplier;
        }
    }
}