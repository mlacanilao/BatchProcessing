using System;
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
        
        [HarmonyPrefix]
        [HarmonyPatch(declaringType: typeof(Card), methodName: nameof(Card.ModCharge))]
        public static void CardModCharge(Card __instance, ref int a)
        {
            CardPatch.ModChargePrefix(__instance: __instance, a: ref a);
        }
        
        [HarmonyPrefix]
        [HarmonyPatch(declaringType: typeof(ActPlan), methodName: nameof(ActPlan.ShowContextMenu))]
        public static void ActPlanShowContextMenu(ActPlan __instance)
        {
            ActPlanPatch.ShowContextMenuPrefix(__instance: __instance);
        }
        
        [HarmonyPrefix]
        [HarmonyPatch(declaringType: typeof(ActPlan.Item), methodName: nameof(ActPlan.Item.Perform))]
        public static bool ActPlanItemPerform(ActPlan.Item __instance)
        {
            if (__instance?.act is DynamicAct dynamicAct == false)
            {
                return true;
            }
            
            if (dynamicAct?.id != BatchProcessingLang.T(key: "bp_menu_title"))
            {
                return true;
            }
            
            dynamicAct.Perform();
            return false;
        }
    }
}