using HarmonyLib;
using Unity.Burst.Intrinsics;
using UnityEngine;
using VanillaPlantsExpandedFlowers;
using Verse;

namespace ProgressionAgriculture
{
	public class ProgressionAgricultureMod : Mod
	{
		public static ProgressionAgricultureModSettings settings;
		public static Harmony harmony;
		public ProgressionAgricultureMod(ModContentPack pack) : base(pack)
		{
			harmony = new Harmony("ProgressionAgricultureMod");
			harmony.PatchAll();
			if (ModLister.AnyModActiveNoSuffix(["VanillaExpanded.VPEFlowers"]))
			{              
               harmony.Patch(AccessTools.PropertyGetter((AccessTools.TypeByName("VanillaPlantsExpandedFlowers.Designator_BloomingFlowerGrowingZone")), "Visible"),
              postfix: new HarmonyMethod(typeof(ProgressionAgriculture_Designator_BloomingFlowerGrowingZone_Visible_Patch), "Postfix"));
            }
            
            settings = GetSettings<ProgressionAgricultureModSettings>();
		}

		public override void DoSettingsWindowContents(Rect inRect)
		{
			base.DoSettingsWindowContents(inRect);
			settings.DoSettingsWindowContents(inRect);
		}

        public override string SettingsCategory()
        {
            return Content.Name;
        }
	}
}
