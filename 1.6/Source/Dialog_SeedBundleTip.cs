using Verse;

namespace ProgressionAgriculture
{
	public class Dialog_SeedBundleTip : Dialog_MessageBox
	{
		public Dialog_SeedBundleTip(TaggedString text) : base(text)
		{
		}

		public override void PostClose()
		{
			base.PostClose();
			DiscoveryQueue_TryShowNext_Patch.ResumeIfDeferred();
		}
	}
}
