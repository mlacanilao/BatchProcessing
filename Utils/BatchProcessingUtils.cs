using System.Collections.Generic;
using UnityEngine;

namespace BatchProcessing
{
    internal static class BatchProcessingUtils
    {
        internal static int MaxBatchMultiplier = 1;
        internal static bool HasEnoughForConfiguredMultiplier = true;

        internal static void CalculateAutoMaxBatchMultiplier(AI_UseCrafter ai)
        {
            MaxBatchMultiplier = 1;
            
            int stamina = EClass.pc?.stamina?.value ?? 0;
            
            if (stamina <= 0)
            {
                return;
            }

            if (ai is null ||
                ai.layer is null ||
                ai.crafter is null)
            {
                return;
            }
            
            int available = GetAvailableTargetMin(ai: ai);
            
            if (available <= 0)
            {
                return;
            }
            
            SourceRecipe.Row row = BatchProcessingUtils.GetSourceRow(ai: ai);

            if (row is null)
            {
                return;
            }
            
            int costSp = Mathf.Max(a: 1, b: row.sp);
            
            int maxByStamina = Mathf.Max(a: 1, b: stamina / costSp);
            
            int maxByInventory = Mathf.Max(a: 1, b: available);
            
            MaxBatchMultiplier = Mathf.Clamp(
                value: maxByStamina,
                min: 1,
                max: maxByInventory
            );
        }
        
        internal static SourceRecipe.Row GetSourceRow(AI_UseCrafter ai)
        {
            if (ai == null || 
                ai.crafter == null || 
                ai.layer == null)
            {
                return null;
            }

            var targets = ai.layer.GetTargets();
            if (targets == null || 
                targets.Count == 0)
            {
                return null;
            }

            var tempAi = new AI_UseCrafter
            {
                crafter = ai.crafter,
                layer   = ai.layer,
                recipe  = ai.recipe,
                num     = ai.num,
                ings    = targets
            };

            SourceRecipe.Row row = ai.crafter.GetSource(ai: tempAi);
            
            if (row?.type == "Grind" ||
                row?.type == "Incubator" ||
                row?.type == "RuneMold")
            {
                return null;
            }
            
            return row;
        }
        
        internal static int GetAvailableTargetMin(AI_UseCrafter ai)
        {
            if (ai?.layer is null)
            {
                HasEnoughForConfiguredMultiplier = false;
                return 0;
            }

            List<Thing> targets = ai.layer.GetTargets();

            if (targets is null || targets.Count == 0)
            {
                HasEnoughForConfiguredMultiplier = false;
                return 0;
            }

            int available = int.MaxValue;
            for (int i = 0; i < targets.Count; i++)
            {
                int num = targets[index: i]?.Num ?? 0;
                available = Mathf.Min(a: available, b: num);
            }

            if (available == int.MaxValue)
            {
                available = 0;
            }

            return available;
        }
        
        internal static int GetSafeIngredientMultiplier(AI_UseCrafter ai)
        {
            int baseMultiplier = Mathf.Max(
                a: 1,
                b: BatchProcessingConfig.IngredientMultiplier?.Value ?? 1
            );

            bool enableAutoMaxBatchMultiplier = BatchProcessingConfig.EnableAutoMaxBatchMultiplier?.Value ?? false;

            int available = GetAvailableTargetMin(ai: ai);

            if (available <= 0)
            {
                HasEnoughForConfiguredMultiplier = false;
                return 1;
            }

            if (enableAutoMaxBatchMultiplier == true)
            {
                CalculateAutoMaxBatchMultiplier(ai: ai);
                baseMultiplier = MaxBatchMultiplier;
                HasEnoughForConfiguredMultiplier = true;
                return baseMultiplier;
            }

            int safeMultiplier = Mathf.Clamp(
                value: baseMultiplier,
                min: 1,
                max: available
            );

            HasEnoughForConfiguredMultiplier = baseMultiplier <= available;

            return safeMultiplier;
        }
    }
}