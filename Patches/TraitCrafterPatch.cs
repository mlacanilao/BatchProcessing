using UnityEngine;

namespace BatchProcessing
{
    public class TraitCrafterPatch
    {
        internal static void CraftPostfix(TraitCrafter __instance, AI_UseCrafter ai, ref Thing __result)
        {
            int ingredientMultiplier = Mathf.Max(a: 1, b: BatchProcessingConfig.IngredientMultiplier?.Value ?? 1);
            
            SourceRecipe.Row source = __instance.GetSource(ai: ai);
            TraitCrafter.MixType mixType = source.type.ToEnum<TraitCrafter.MixType>(ignoreCase: true);
            string text = source.thing;
            
            BatchProcessing.Log(payload: $"CraftPostfix");
            BatchProcessing.Log(payload: $"ai.ings[0]: {ai.ings[index: 0]}");
            BatchProcessing.Log(payload: $"__instance.numIng: {__instance.numIng}");
            BatchProcessing.Log(payload: $"source.id: {source.id}");
            BatchProcessing.Log(payload: $"mixType: {mixType}");
            BatchProcessing.Log(payload: $"text: {text}");
            BatchProcessing.Log(payload: $"__result: {__result}");
            BatchProcessing.Log(payload: $"__result.Num: {__result.Num}");

            __result?.SetNum(a: __result.Num * ingredientMultiplier);
            
            BatchProcessing.Log(payload: $"__result: {__result}");
        }

        internal static void GetCostSpPostfix(TraitCrafter __instance, AI_UseCrafter ai, ref int __result)
        {
            int ingredientMultiplier = Mathf.Max(a: 1, b: BatchProcessingConfig.IngredientMultiplier?.Value ?? 1);
            
            BatchProcessing.Log(payload: $"GetCostSpPostfix");
            BatchProcessing.Log(payload: $"ai: {ai}");
            BatchProcessing.Log(payload: $"__result: {__result}");
            
            __result *= ingredientMultiplier;
                
            BatchProcessing.Log(payload: $"__result: {__result}");
            
        }
    }
}