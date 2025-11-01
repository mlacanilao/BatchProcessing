using HarmonyLib;

namespace BatchProcessing
{
    internal class Patcher
    {
        [HarmonyPostfix]
        [HarmonyPatch(declaringType: typeof(LayerBaseCraft), methodName: nameof(LayerBaseCraft.GetReqIngredient))]
        public static void LayerBaseCraftGetReqIngredient(LayerBaseCraft __instance, int index, ref int __result)
        {
            LayerBaseCraftPatch.GetReqIngredientPostfix(__instance: __instance, index: index, __result: ref __result);
        }
        
        [HarmonyPostfix]
        [HarmonyPatch(declaringType: typeof(TraitCrafter), methodName: nameof(TraitCrafter.Craft))]
        public static void TraitCrafterCraft(TraitCrafter __instance, AI_UseCrafter ai, ref Thing __result)
        {
            TraitCrafterPatch.CraftPostfix(__instance: __instance, ai: ai, __result: ref __result);
        }
        
        [HarmonyPostfix]
        [HarmonyPatch(declaringType: typeof(TraitCrafter), methodName: nameof(TraitCrafter.GetCostSp))]
        public static void TraitCrafterGetCostSp(TraitCrafter __instance, AI_UseCrafter ai, ref int __result)
        {
            TraitCrafterPatch.GetCostSpPostfix(__instance: __instance, ai: ai, __result: ref __result);
        }
        
        [HarmonyPostfix]
        [HarmonyPatch(declaringType: typeof(AI_UseCrafter), methodName: nameof(AI_UseCrafter.OnSuccess))]
        public static void AI_UseCrafterOnSuccess(AI_UseCrafter __instance)
        {
            AI_UseCrafterPatch.OnSuccessPostfix(__instance: __instance);
        }
    }
}