using System.Linq;
using UnityEngine;

namespace BatchProcessing
{
    internal static class ActPlanPatch
    {
        private static bool OpenBatchSettingsMenu()
        {
            UIContextMenu uicontextMenu = EClass.ui.CreateContextMenu(cid: "ContextMenu");
            
            uicontextMenu.AddToggle(
                idLang: BatchProcessingLang.T(key: "bp_auto_max"), 
                isOn: BatchProcessingConfig.EnableAutoMaxBatchMultiplier.Value, 
                action: (bool v) =>
                {
                    BatchProcessingConfig.EnableAutoMaxBatchMultiplier.Value = v;
                });
            
            uicontextMenu.AddSlider(
                text: BatchProcessingLang.T(key: "bp_mult"),
                textFunc: (float v) =>
                {
                    int iv = Mathf.Clamp(value: Mathf.RoundToInt(f: v), min: 1, max: EClass.pc._maxStamina);
                    return iv.ToString();
                },
                value: Mathf.Clamp(value: BatchProcessingConfig.IngredientMultiplier.Value, min: 1, max: EClass.pc._maxStamina),
                action: (float v) =>
                {
                    int iv = Mathf.Clamp(value: Mathf.RoundToInt(f: v), min: 1, max: EClass.pc._maxStamina);
                    BatchProcessingConfig.IngredientMultiplier.Value = iv;
                },
                min: 1f,
                max: EClass.pc._maxStamina,
                isInt: true,
                hideOther: false,
                useInput: false);
            
            uicontextMenu.Show();
            return false;
        }
        
        internal static void ShowContextMenuPrefix(ActPlan __instance)
        {
            bool hasCrafter = __instance.pos
                .ListCards(includeMasked: false)
                .Any(predicate: (Card c) => c.isThing && c.trait is TraitCrafter && c.sourceCard.category == "processor");

            if (hasCrafter == false)
            {
                return;
            }

            DynamicAct act = new DynamicAct(_id: BatchProcessingLang.T(key: "bp_menu_title"), _onPerform: OpenBatchSettingsMenu, _closeLayers: false);
            
            __instance.list.Add(item: new ActPlan.Item
            {
                act = act
            });
        }
    }
}