using System.Reflection;
using HarmonyLib;
using Verse;

namespace ProgressionAgriculture
{
	[HarmonyPatch]
	public static class DiscoveryQueue_TryShowNext_Patch
	{
		private static bool deferred;

		public static bool Prepare()
		{
			return DiscoveriesCompat.TryShowNextMethod != null;
		}

		public static MethodBase TargetMethod()
		{
			return DiscoveriesCompat.TryShowNextMethod;
		}

		public static bool Prefix()
		{
			if (Find.WindowStack?.WindowOfType<Dialog_SeedBundleTip>() == null) return true;
			deferred = true;
			return false;
		}

		public static void ResumeIfDeferred()
		{
			if (deferred is false) return;
			deferred = false;
			DiscoveriesCompat.TryShowNext();
		}
	}
}
