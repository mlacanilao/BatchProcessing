using UnityEngine;

namespace BatchProcessing
{
    internal static class LayerBaseCraftPatch
    {
        internal static void GetReqIngredientPostfix(LayerBaseCraft __instance, int index, ref int __result)
        {
            int ingredientMultiplier = Mathf.Max(a: 1, b: BatchProcessingConfig.IngredientMultiplier?.Value ?? 1);
            
            BatchProcessing.Log(payload: $"GetReqIngredientPostfix");
            BatchProcessing.Log(payload: $"index: {index}");
            BatchProcessing.Log(payload: $"__result: {__result}");
            
            __result *= ingredientMultiplier;
            
            BatchProcessing.Log(payload: $"__result: {__result}");
        }
    }
}