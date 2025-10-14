using BepInEx.Configuration;
using CloverPitExampleMod.Charms;
using System.IO;

namespace CloverPitExampleMod
{
    [BepInPlugin(PluginGuid, PluginName, PluginVer)]
    [BepInDependency("ModdingAPIs.cloverpit.CloverAPI")]
    [HarmonyPatch] // If you want to put harmony patches in this class, otherwise you can remove this line. It's recommended to put patches in their own file(s) for larger mods.
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "IngoH.cloverpit.CloverPitExampleMod";
        public const string PluginName = "CloverPit Example Mod";
        public const string PluginVer = "1.0.0";

        internal static ManualLogSource Log;
        internal readonly static Harmony Harmony = new(PluginGuid);

        internal static string PluginPath;
        internal const string MainContentFolder = "CloverPitExampleMod_Content";
        
        // When testing and sharing your mod, make sure to have your data and image files in these folders
        // It is recommended to use FileUtils.FindFile(Path.Combine(Plugin.DataPath, "yourfile")) to find your files
        public static string DataPath { get; private set; }
        public static string ImagePath { get; private set; }
        
        internal ConfigEntry<bool> SomeConfigOption;

        private void Awake()
        {
            Log = Logger;
            PluginPath = Path.GetDirectoryName(Info.Location);
            DataPath = Path.Combine(PluginPath, MainContentFolder, "Data");
            ImagePath = Path.Combine(PluginPath, MainContentFolder, "Images");

            MakeConfig();
            /*
            ExampleCharms.RegisterCharms();
            */
        }
        
        private void MakeConfig()
        {
            /*
            SomeConfigOption = Config.Bind("General", "SomeConfigOption", true, "An example config option.");
            */
        }


        private void OnEnable()
        {
            Harmony.PatchAll();
            LogInfo($"Loaded {PluginName}!");
        }

        private void OnDisable()
        {
            Harmony.UnpatchSelf();
            LogInfo($"Unloaded {PluginName}!");
        }

        private void Update()
        {
            // Runs every frame
        }
    }
}