namespace ProgressionAgriculture
{
	using UnityEngine;
	using Verse;

	public class ProgressionAgricultureModSettings : ModSettings
	{
		public static float RecipeDefIngredientCount = 10f;
		public override void ExposeData()
		{
			base.ExposeData();
			Scribe_Values.Look(ref RecipeDefIngredientCount, "RecipeDefIngredientCount", 10f);
		}

		public void DoSettingsWindowContents(Rect inRect)
		{
			Listing_Standard listingStandard = new Listing_Standard();
			listingStandard.Begin(inRect);
			RecipeDefIngredientCount = listingStandard.SliderLabeled("PA.SeedBundleIngredientCount".Translate
				(RecipeDefIngredientCount), RecipeDefIngredientCount, 1f, 100f);
			listingStandard.End();
		}
	}
}