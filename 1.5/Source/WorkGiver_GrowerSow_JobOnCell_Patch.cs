using HarmonyLib;
using RimWorld;
using Verse;
using Verse.AI;

namespace ProgressionAgriculture
{
    [HarmonyPatch(typeof(WorkGiver_GrowerSow), "JobOnCell")]
    public static class WorkGiver_GrowerSow_JobOnCell_Patch
    {

        public static void Postfix(ref Job __result, Pawn pawn)
        {
            if (__result != null && WorkGiver_Grower.wantedPlantDef != null &&
                GameComponent_UnlockedCrops.Instance.IsCropUnlocked(WorkGiver_Grower.wantedPlantDef) is false)
            {
                __result = null;
            }
        }
    }
}
