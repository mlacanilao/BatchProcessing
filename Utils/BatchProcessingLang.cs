using System.Collections.Generic;

namespace BatchProcessing
{
    internal static class BatchProcessingLang
    {
        private static readonly Dictionary<string, Dictionary<string, string>> Data =
            new Dictionary<string, Dictionary<string, string>>
            {
                {
                    "EN",
                    new Dictionary<string, string>
                    {
                        { "bp_menu_title", "Batch Processing" },
                        { "bp_auto_max", "Enable Auto Max Batch Multiplier" },
                        { "bp_mult", "Ingredient Multiplier" },
                    }
                },
                {
                    "JP",
                    new Dictionary<string, string>
                    {
                        { "bp_menu_title", "一括加工" },
                        { "bp_auto_max", "自動最大バッチ倍率を有効化" },
                        { "bp_mult", "材料倍率" },
                    }
                },
                {
                    "CN",
                    new Dictionary<string, string>
                    {
                        { "bp_menu_title", "批量处理" },
                        { "bp_auto_max", "启用自动最大批量倍数" },
                        { "bp_mult", "材料倍数" },
                    }
                },
            };

        public static string T(string key)
        {
            string lang = EClass.core.config.lang;
            if (!Data.ContainsKey(key: lang))
                lang = "EN";

            string value;
            if (Data[key: lang].TryGetValue(key: key, value: out value))
                return value;

            return key;
        }
    }
}