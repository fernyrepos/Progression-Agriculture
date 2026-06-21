using HarmonyLib;
using VanillaPlantsExpandedFlowers;
using Verse;

namespace ProgressionAgriculture
{
    [HarmonyPatch(typeof(Designator_BloomingFlowerGrowingZone), "Visible", MethodType.Getter)]
    public static class ProgressionAgriculture_Designator_BloomingFlowerGrowingZone_Visible_Patch
    {
        public static void Postfix(ref bool __result)
        {
            if (ModLister.AnyModActiveNoSuffix(["OskarPotocki.VFE.Tribals"])) {
                if (!PA_DefOf.VFET_Agriculture.IsFinished)
                {
                    __result = DebugSettings.godMode;
                }
            } else {
                __result = true;
            }
        }
    }
}
