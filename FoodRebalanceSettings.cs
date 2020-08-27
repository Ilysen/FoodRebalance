using ModSettings;

namespace Ilysen.FoodRebalance
{
	internal class FoodRebalanceSettings : JsonModSettings
	{
		internal static FoodRebalanceSettings Instance;

		[Name("Calorie Rebalance")]
		[Description("Rebalances calorie values on various food items. WARNING - Do not change while in-game.")]
		public bool adjustCalories = true;

		[Name("Efficient Can Smashing")]
		[Description("Whether cans should always be opened without a tool if there's no calorie loss from smashing them open.")]
		public bool efficientSmashing = true;
	}
}
