namespace CloverPitExampleMod.Patches;

[HarmonyPatch]
internal class ExamplePatch
{
    
    // Example of a postfix patch
    // that modifies the text displayed for total amount won (when 2 or more patterns are scored).
    /*
    [HarmonyPatch(typeof(SlotMachineScript), nameof(SlotMachineScript.SpinWinSetText))]
    [HarmonyPostfix]
    internal static void SlotMachineScript_SpinWinSetText_Postfix(SlotMachineScript __instance)
    {
        __instance.textSpinWin.text += "\n<color=yellow>Remember to like and subscribe!</color>";
    }
    */
}