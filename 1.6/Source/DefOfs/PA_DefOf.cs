using RimWorld;
using Verse;
using VFETribals;

namespace ProgressionAgriculture
{
    [DefOf]
    public static class PA_DefOf
    {
        public static TraderKindDef AgriculturalTrader;
        public static JobDef UnlockCrop;
        public static ThingDef SeedPackingBench;
        public static ThingDef SeedPackingCraftingSpot;
        public static ThingCategoryDef SeedBundles;

        public static ScenPartDef Ferny_StartingSeeds;

        [MayRequire("OskarPotocki.VFE.Tribals")]
        public static TribalResearchProjectDef VFET_Agriculture;
    }
}