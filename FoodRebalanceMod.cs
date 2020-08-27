//#define FRM_DEBUG
// Uncomment the above line to enable detailed info in the game's output log.

using UnityEngine;
using MelonLoader;
using System;
using System.Collections.Generic;

namespace Ilysen.FoodRebalance
{
	internal class FoodRebalanceMod : MelonMod
	{
		/// <summary>
		/// A <c><see cref="Dictionary{TKey, TValue}"/></c> that uses the names of game objects
		/// to determine the amount of calories per KG an item should have.
		/// <br/><br/>
		/// The game's logic regarding food calories is very janky.
		/// Fish items have their caloric value automatically determined by a calories/kg value,
		/// which we hook into for them by overriding that value.
		/// Other food items do not have this amount (for some reason) and instead have their own direct values.
		/// We add calories to those directly, assuming that their weight is going to be 1 kg.
		/// <br/><br/>
		/// These amounts are source from the Alaska Department of Fish and Game (ADFG) when possible,
		/// or the USDA. Venison is based on caribou, because all deer in Long Dark have antlers.
		/// </summary>
		private static Dictionary<string, float> newKcalPerKg = new Dictionary<string, float>()
		{
			// These are direct calorie ADDITIONS.
			{ "GEAR_RawMeatRabbit", 600 },
			{ "GEAR_CookedMeatRabbit", 600 },
			{ "GEAR_RawMeatDeer", 300 },
			{ "GEAR_CookedMeatDeer", 300 },
			{ "GEAR_RawMeatBear", 700 },
			{ "GEAR_CookedMeatBear", 700 },
			{ "GEAR_RawMeatMoose", 400 },
			{ "GEAR_CookedMeatMoose", 400 },
			
			// Fish yields are a bit harder because a lot of the fish is lost during preparation.
			// These values are based on USDA sources and then percentage is adjusted based on
			// the amount lost per pound during skin-off head-on gutting, which is taken from the in-game model.
			// Raw fish use the same calorie count as cooked fish for simplicity's sake.

			// These are calorie/kg OVERRIDES.
			{ "GEAR_RawRainbowTrout", 770 }, // ~1100 cal/kg, ~70% yield (chefs-resources)
			{ "GEAR_CookedRainbowTrout", 770 },
			{ "GEAR_RawLakeWhiteFish", 780 }, // 1300 cal/kg, 60% yield (could not find a source!)
			{ "GEAR_CookedLakeWhiteFish", 780 },
			{ "GEAR_RawSmallMouthBass", 440 }, // ~1100 cal/kg, 40% yield (clovegarden)
			{ "GEAR_CookedSmallMouthBass", 440 },
			{ "GEAR_RawCohoSalmon", 770 }, // ~1400 cal/kg, 55% yield (chefs-resources)
			{ "GEAR_CookedCohoSalmon", 770 },
		};

		/// <summary>
		/// Processes the <c><see cref="GameObject"/></c> name of an object and its <c><see cref="GearItem"/></c>
		/// to determine how its calories will be modified.
		/// </summary>
		/// <param name="itemName">The name of the provided item in the scene, such as <c>GEAR_RawMeatDeer</c>.</param>
		/// <param name="foodItem">The <c><see cref="GearItem"/></c> component of the item to process.</param>
		/// <returns>If the item has a <c><see cref="FoodWeight"/></c>, returns its new calories per kilogram.
		/// If not, returns an amount of calories to add or remove from the object.</returns>
		public static float ChangedCaloriesForItem(string itemName, GearItem foodItem)
		{
			itemName = itemName.Replace("(Clone)", "").Trim();
			DebugLog($"Checking calories for item named {itemName}...");
			if (newKcalPerKg.ContainsKey(itemName))
			{
				try
				{
					float changedCalories = 0;
					if (foodItem.m_FoodWeight) // Just in case we return nothing
						changedCalories = foodItem.m_FoodWeight.m_CaloriesPerKG;

					DebugLog("This item has a modified calorie count. Processing...");
					if (newKcalPerKg.TryGetValue(itemName, out float cal))
					{
						changedCalories = cal;
					}

					if (foodItem.m_FoodWeight)
						DebugLog($"Item's kcal/KG will be set to {changedCalories} cal/kg.");
					else
						DebugLog($"Item nutrition will be adjusted by {changedCalories} calories.");

					return changedCalories;
				}
				catch (Exception e)
				{
					Debug.LogError($"[FoodRebalance] Caught exception: {e.Message}\nStack trace:\n{e.StackTrace})");
					return foodItem.m_FoodWeight != null ? foodItem.m_FoodWeight.m_CaloriesPerKG : 0;
				}
			}
			DebugLog("No changed calories for this item.");
			return 0;
		}

		/// <summary>
		/// A wrapper for <c><see cref="Debug.Log(Il2CppSystem.Object)"/></c> that only displays a message if
		/// <c>FRM_DEBUG</c> is defined as a preprocessor symbol.
		/// </summary>
		/// <param name="textToLog">The message to log.</param>
		public static void DebugLog(string textToLog)
		{
#if FRM_DEBUG
			Debug.Log($"[FoodRebalance] {textToLog}");
#endif
		}

		public override void OnApplicationStart()
		{
			Debug.Log($"{InfoAttribute.Name} version {InfoAttribute.Version} loaded.");
#if FRM_DEBUG
			MelonLogger.Log($"{InfoAttribute.Name} version {InfoAttribute.Version} loaded.");
#endif
		}
	}
}
