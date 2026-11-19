using HarmonyLib;
using RimWorld;
using Verse;

namespace ProgressionAgriculture
{
	[HarmonyPatch(typeof(Selector), "Select")]
	public static class Selector_Select_Patch
	{
		[HarmonyBefore("ferny.Discoveries")]
		public static void Postfix(object obj)
		{
			GameComponent_UnlockedCrops component = GameComponent_UnlockedCrops.Instance;
			if (component == null || component.SeedBundleTipShown) return;
			if (!(obj is Thing thing) || thing.TryGetComp<CompUnlockCrop>() == null) return;

			component.MarkSeedBundleTipShown();
			Find.WindowStack.Add(new Dialog_SeedBundleTip("PA.SeedBundleFirstSelectTip".Translate()));
		}
	}
}
