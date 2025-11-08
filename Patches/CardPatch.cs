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
            
            BatchProcessing.Log(payload: $"ModChargePrefix");
            BatchProcessing.Log(payload: $"__instance: {__instance}");
            BatchProcessing.Log(payload: $"__instance.trait: {__instance.trait}");
            BatchProcessing.Log(payload: $"a: {a}");
            BatchProcessing.Log(payload: $"EClass.pc?.ai: {EClass.pc?.ai}");
            BatchProcessing.Log(payload: $"(EClass.pc?.ai as AI_UseCrafter).crafter: {(EClass.pc?.ai as AI_UseCrafter).crafter}");
            BatchProcessing.Log(payload: $"ReferenceEquals(__instance.trait, (EClass.pc?.ai as AI_UseCrafter).crafter: {ReferenceEquals(objA: __instance.trait, objB: (EClass.pc?.ai as AI_UseCrafter).crafter)}");
            
            
            
            SourceRecipe.Row row = BatchProcessingUtils.GetSourceRow(ai: ai);
            BatchProcessing.Log(payload: $"row: {row}");
            
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
            BatchProcessing.Log(payload: $"a: {a}");
        }
    }
}