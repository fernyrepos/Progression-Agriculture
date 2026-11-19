using Verse;
using System.Collections.Generic;
using RimWorld;
using LudeonTK;
using System.Linq;

namespace ProgressionAgriculture
{
	public class GameComponent_UnlockedCrops : GameComponent
	{
		private HashSet<ThingDef> unlockedCrops = new HashSet<ThingDef>();
		private bool seedBundleTipShown;
		public static GameComponent_UnlockedCrops Instance;
		public GameComponent_UnlockedCrops()
		{
			Instance = this;
		}

		public GameComponent_UnlockedCrops(Game game)
		{
			Instance = this;
		}

		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Collections.Look(ref unlockedCrops, "unlockedCrops", LookMode.Def);
			Scribe_Values.Look(ref seedBundleTipShown, "seedBundleTipShown", false);
			Instance = this;
		}

		public bool SeedBundleTipShown => seedBundleTipShown;

		public void MarkSeedBundleTipShown()
		{
			seedBundleTipShown = true;
		}

		public bool IsCropUnlocked(ThingDef cropDef)
		{
			if (DefGenerator_GenerateImpliedDefs_PreResolve_Patch.sowableCrops.Contains(cropDef) is false) return true;
			return unlockedCrops.Contains(cropDef);
		}

		[DebugAction("General", "Unlock all crops", actionType = DebugActionType.Action)]
		public static void UnlockAllCrops()
		{
			foreach (ThingDef cropDef in DefGenerator_GenerateImpliedDefs_PreResolve_Patch.sowableCrops)
			{
				if (Instance.IsCropUnlocked(cropDef) is false)
				{
					Instance.unlockedCrops.Add(cropDef);
				}
			}
		}

		public void UnlockCrop(ThingDef cropDef)
		{
			unlockedCrops.Add(cropDef);
			Find.LetterStack.ReceiveLetter("PA.CropUnlocked".Translate(cropDef.LabelCap),
			"PA.CropUnlockedDesc".Translate(cropDef.label), LetterDefOf.PositiveEvent);
		}
	}
}
