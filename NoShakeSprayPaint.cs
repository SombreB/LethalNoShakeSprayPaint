using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace NoShakeSprayPaint
{
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    public class NoShake : BaseUnityPlugin
    {
        private const string PLUGIN_GUID = "Sombre.NoShakeSprayPaint";
        private const string PLUGIN_NAME = "No Shake Spray Paint";
        private const string PLUGIN_VERSION = "1.0.0";

        private readonly Harmony harmony = new Harmony(PLUGIN_GUID);

        private static NoShake Instance;

        internal ManualLogSource mls;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource(PLUGIN_GUID);

            mls.LogInfo($"------{PLUGIN_NAME} done.------");

            harmony.PatchAll(typeof(SprayPaintItemPatch));
        }
    }

    [HarmonyPatch(typeof(SprayPaintItem))]
    internal class SprayPaintItemPatch
    {
        [HarmonyPatch("LateUpdate")]
        [HarmonyPostfix]
        static void LateUpdatePatch(ref float ___sprayCanTank, ref float ___sprayCanShakeMeter)
        {
            ___sprayCanTank = 1f;
            ___sprayCanShakeMeter = 1f;
        }
    }
}
