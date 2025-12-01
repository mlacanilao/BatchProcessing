using BepInEx;
using HarmonyLib;

namespace BatchProcessing
{
    internal static class ModInfo
    {
        internal const string Guid = "omegaplatinum.elin.batchprocessing";
        internal const string Name = "Batch Processing";
        internal const string Version = "1.3.0";
    }

    [BepInPlugin(GUID: ModInfo.Guid, Name: ModInfo.Name, Version: ModInfo.Version)]
    internal class BatchProcessing : BaseUnityPlugin
    {
        internal static BatchProcessing Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            BatchProcessingConfig.LoadConfig(config: Config);
            Harmony.CreateAndPatchAll(type: typeof(Patcher), harmonyInstanceId: ModInfo.Guid);
        }

        internal static void Log(object payload)
        {
            Instance?.Logger.LogInfo(data: payload);
        }
    }
}