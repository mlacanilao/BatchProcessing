using System.Collections.Generic;
using UnityEngine;

namespace BatchProcessing
{
    internal static class BatchProcessingUtils
    {
        internal static int MaxBatchMultiplier = 1;

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
            
            List<Thing> targets = ai.layer.GetTargets();
            
            int available = int.MaxValue;
            for (int i = 0; i < targets.Count; i++)
            {
                int num = targets[index: i]?.Num ?? 0;
                available = Mathf.Min(a: available, b: num);
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
    }
}