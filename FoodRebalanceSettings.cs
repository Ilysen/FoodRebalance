using ModSettings;

namespace Ilysen.FoodRebalance
{
	internal class FoodRebalanceSettings : JsonModSettings
	{
		internal enum CanSmashingMode
		{
			Normal,
			CanOpenerOnly,
			EfficientSmashing,
			AlwaysSmash
		}

		internal static FoodRebalanceSettings Instance;

		[Name("Calorie Rebalance")]
		[Description("Rebalances calorie values on various food items. WARNING - Do not change while in-game.")]
		public bool adjustCalories = true;

		[Name("Can Opening")]
		[Description("Changes how canned food items are opened. Default behaviour (Normal) is identical to vanilla. " +
			"'Efficient Smashing' will always smash open cans as long as there's no calorie loss.")]
		public CanSmashingMode efficientSmashing = CanSmashingMode.Normal;
	}
}
