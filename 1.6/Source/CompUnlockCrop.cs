using Verse;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse.AI;

namespace ProgressionAgriculture
{
	public class CompUnlockCrop : ThingComp
	{
		public CompProperties_UnlockCrop Props => (CompProperties_UnlockCrop)props;
		public override IEnumerable<FloatMenuOption> CompFloatMenuOptions(Pawn pawn)
		{
			if (GameComponent_UnlockedCrops.Instance.IsCropUnlocked(Props.cropDef) is false)
			{
				yield return new FloatMenuOption("PA.UnlockCrop".Translate(Props.cropDef.label), () => QueueOpenJob(pawn));
			}
			else
			{
				yield return new FloatMenuOption("PA.CropAlreadyUnlocked".Translate(Props.cropDef.label).CapitalizeFirst(), null);
			}
		}

		public override IEnumerable<Gizmo> CompGetGizmosExtra()
		{
			foreach (Gizmo gizmo in base.CompGetGizmosExtra())
			{
				yield return gizmo;
			}

			Command_Action command = new Command_Action
			{
				defaultLabel = "PA.UnlockCropGizmo".Translate(Props.cropDef.label),
				defaultDesc = "PA.UnlockCropGizmoDesc".Translate(Props.cropDef.label),
				icon = ContentFinder<Texture2D>.Get("SeedBundle"),
				action = ShowPawnSelectionMenu
			};

			if (GameComponent_UnlockedCrops.Instance.IsCropUnlocked(Props.cropDef))
			{
				command.Disable("PA.CropAlreadyUnlocked".Translate(Props.cropDef.label).CapitalizeFirst());
			}
			else if (parent.Map == null)
			{
				command.Disable("PA.SeedBundleNotOnMap".Translate());
			}

			yield return command;
		}

		private void ShowPawnSelectionMenu()
		{
			List<FloatMenuOption> options = new List<FloatMenuOption>();
			if (parent.Map == null)
			{
				options.Add(new FloatMenuOption("PA.SeedBundleNotOnMap".Translate(), null));
				Find.WindowStack.Add(new FloatMenu(options));
				return;
			}

			foreach (Pawn pawn in parent.Map.mapPawns.FreeColonistsSpawned)
			{
				Pawn localPawn = pawn;
				string label = localPawn.LabelShortCap;
				if (localPawn.Downed)
				{
					options.Add(new FloatMenuOption("PA.CannotOpenSeedBundleDowned".Translate(label), null));
				}
				else if (!localPawn.CanReserveAndReach(parent, PathEndMode.ClosestTouch, Danger.Deadly))
				{
					options.Add(new FloatMenuOption("PA.CannotOpenSeedBundleUnreachable".Translate(label), null));
				}
				else
				{
					options.Add(new FloatMenuOption(label, () => QueueOpenJob(localPawn)));
				}
			}

			if (options.Count == 0)
			{
				options.Add(new FloatMenuOption("PA.NoColonistsCanOpenSeedBundle".Translate(), null));
			}

			Find.WindowStack.Add(new FloatMenu(options));
		}

		private void QueueOpenJob(Pawn pawn)
		{
			Job job = JobMaker.MakeJob(PA_DefOf.UnlockCrop, parent);
			pawn.jobs.TryTakeOrderedJob(job);
		}

		public void Unlock()
		{
			GameComponent_UnlockedCrops.Instance.UnlockCrop(Props.cropDef);
			parent.Destroy();
		}

        public override string TransformLabel(string label)
        {
			if (GameComponent_UnlockedCrops.Instance.IsCropUnlocked(Props.cropDef)) {
				return "PA.UnlockedSeedLabel".Translate(label);
			}
            return base.TransformLabel(label);
        }
	}
}
