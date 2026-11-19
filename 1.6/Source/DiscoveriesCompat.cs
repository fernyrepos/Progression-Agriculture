using System.Reflection;
using HarmonyLib;
using Verse;

namespace ProgressionAgriculture
{
	public static class DiscoveriesCompat
	{
		private static bool resolved;
		private static MethodInfo tryShowNext;

		public static MethodInfo TryShowNextMethod
		{
			get
			{
				if (resolved is false)
				{
					resolved = true;
					if (ModsConfig.IsActive("ferny.Discoveries"))
					{
						tryShowNext = AccessTools.Method("Discoveries.DiscoveryQueue:TryShowNext");
					}
				}
				return tryShowNext;
			}
		}

		public static void TryShowNext()
		{
			TryShowNextMethod?.Invoke(null, null);
		}
	}
}
