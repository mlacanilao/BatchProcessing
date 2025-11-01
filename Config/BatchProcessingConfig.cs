using BepInEx.Configuration;

namespace BatchProcessing
{
    internal static class BatchProcessingConfig
    {
        internal static ConfigEntry<int> IngredientMultiplier;
        
        internal static void LoadConfig(ConfigFile config)
        {
            IngredientMultiplier = config.Bind(
                section: ModInfo.Name,
                key: "Ingredient Multiplier",
                defaultValue: 2,
                description:
                "Set the multiplier for required ingredients when crafting.\n" +
                "Must be an integer value (e.g., 2 for double consumption).\n" +
                "クラフト時に必要な材料数の倍率を設定します。\n" +
                "整数値で指定します（例: 2 = 材料が2倍消費されます）。\n" +
                "设置制作时所需材料的倍增值。\n" +
                "必须为整数值（例如 2 表示消耗材料翻倍）。"
            );
        }
    }
}