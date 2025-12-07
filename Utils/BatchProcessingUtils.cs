using System.Collections.Generic;
using UnityEngine;

namespace BatchProcessing
{
    internal static class BatchProcessingUtils
    {
        internal static int CachedIngredientMultiplier = 1;

        internal static int CalculateAutoMaxBatchMultiplier(AI_UseCrafter ai)
        {
            int stamina = EClass.pc?.stamina?.value ?? 0;
            if (stamina <= 0)
            {
                return 1;
            }

            if (ai is null ||
                ai.layer is null ||
                ai.crafter is null)
            {
                return 1;
            }

            int available = GetAvailableTargetMin(ai: ai);
            if (available <= 0)
            {
                return 1;
            }

            SourceRecipe.Row row = GetSourceRow(ai: ai);
            if (row is null)
            {
                return 1;
            }

            int costSp = Mathf.Max(a: 1, b: row.sp);
            int maxByStamina = Mathf.Max(a: 1, b: stamina / costSp);
            int maxByInventory = Mathf.Max(a: 1, b: available);

            return Mathf.Clamp(
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
                row?.type == "RuneMold" ||
                row?.type == "Fortune")
            {
                return null;
            }

            return row;
        }

        internal static int GetAvailableTargetMin(AI_UseCrafter ai)
        {
            if (ai == null ||
                ai.layer == null)
            {
                return 0;
            }

            List<Thing> targets = ai.layer.GetTargets();

            if (targets is null ||
                targets.Count == 0)
            {
                return 0;
            }

            int available = int.MaxValue;

            for (int i = 0; i < targets.Count; i++)
            {
                Thing target = targets[i];
                int num = target?.Num ?? 0;
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
            if (ai == null)
            {
                return 1;
            }

            int baseMultiplier = Mathf.Max(
                a: 1,
                b: BatchProcessingConfig.IngredientMultiplier?.Value ?? 1
            );

            bool enableAutoMaxBatchMultiplier = BatchProcessingConfig.EnableAutoMaxBatchMultiplier?.Value ?? false;

            int available = GetAvailableTargetMin(ai: ai);

            if (available <= 0)
            {
                return 1;
            }

            if (enableAutoMaxBatchMultiplier == true)
            {
                int autoMax = CalculateAutoMaxBatchMultiplier(ai: ai);
                return autoMax;
            }

            int safeMultiplier = Mathf.Clamp(
                value: baseMultiplier,
                min: 1,
                max: available
            );

            return safeMultiplier;
        }
    }
}