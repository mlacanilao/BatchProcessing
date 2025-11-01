using UnityEngine;

namespace BatchProcessing
{
    internal static class AI_UseCrafterPatch
    {
        internal static void OnSuccessPostfix(AI_UseCrafter __instance)
        {
            int ingredientMultiplier = Mathf.Max(a: 1, b: BatchProcessingConfig.IngredientMultiplier?.Value ?? 1);
            
            BatchProcessing.Log(payload: $"OnSuccessPostfix");
            BatchProcessing.Log(payload: $"__instance: {__instance}");
            BatchProcessing.Log(payload: $"__instance.owner: {__instance.owner}");
            
            int costSp = __instance.crafter.GetCostSp(ai: __instance);
            int duration = __instance.crafter.GetDuration(ai: __instance, costSp: costSp);
            Element orCreateElement = __instance?.owner.elements.GetOrCreateElement(alias: __instance.crafter.IDReqEle(r: __instance.recipe?.source ?? null));
            
            __instance?.owner?.elements?.ModExp();
        }
    }
}