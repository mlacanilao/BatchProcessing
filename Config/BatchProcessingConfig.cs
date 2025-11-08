using BepInEx.Configuration;

namespace BatchProcessing
{
    internal static class BatchProcessingConfig
    {
        internal static ConfigEntry<int> IngredientMultiplier;
        internal static ConfigEntry<bool> EnableAutoMaxBatchMultiplier;
        
        internal static void LoadConfig(ConfigFile config)
        {
            
            IngredientMultiplier = config.Bind(
                section: ModInfo.Name,
                key: "Ingredient Multiplier",
                defaultValue: 2,
                description:
                "Set the multiplier for required ingredients when crafting. Ignored if Auto Max Batch Multiplier is enabled.\n" +
                "Must be an integer value.\n" +
                "クラフト時に必要な材料数の倍率を設定します。自動最大バッチ倍率が有効な場合は無視されます。\n" +
                "整数値で指定します。\n" +
                "设置制作时所需材料的倍增值。启用自动最大批量倍增时将被忽略。\n" +
                "必须为整数值。"
            );
            
            EnableAutoMaxBatchMultiplier = config.Bind(
                section: ModInfo.Name,
                key: "Enable Auto Max Batch Multiplier",
                defaultValue: false,
                description:
                "Automatically calculates the batch multiplier so one craft consumes up to current stamina and available ingredients.\n" +
                "１回のクラフトが現在のスタミナ内に収まるよう倍率を自動計算し、必要素材の所持数を上限として適用します。\n" +
                "自动计算批量倍数，使单次制作的消耗不超过当前体力，并受可用材料数量限制。"
            );
        }
    }
}