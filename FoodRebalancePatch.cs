﻿using Harmony;
using UnityEngine;

namespace Ilysen.FoodRebalance
{
	[HarmonyPatch(typeof(GearItem), "Awake")]
	internal static class FoodRebalanceAwakePatch
	{
		// Changes the calorie amount before the rest of the item's Awake call runs.
		// Items with a FoodWeight have their cal/KG changed, and the game's internal logic handles the rest.
		// Items without it have calories directly added to their value.
		private static void Prefix(GearItem __instance)
		{
			if (FoodRebalanceSettings.Instance.adjustCalories && __instance.m_FoodItem)
			{
				if (__instance.m_FoodWeight != null)
				{
					__instance.m_FoodWeight.m_CaloriesPerKG =
						FoodRebalanceMod.ChangedCaloriesForItem(__instance.name, __instance);
				}
				else
				{
					float changedCal = FoodRebalanceMod.ChangedCaloriesForItem(__instance.name, __instance);
					__instance.m_FoodItem.m_CaloriesRemaining += changedCal;
					__instance.m_FoodItem.m_CaloriesTotal += changedCal;
				}
			}
		}
	}

	[HarmonyPatch(typeof(GearItem), "Serialize")]
	internal static class FoodRebalanceSeerializePatch
	{
		// To prevent non-FoodWeight items from acting up across saves,
		// we revert the added calories before the item serializes.
		// This can put calories into the negatives for the saved item, but this is saved like normal
		// and then we fix that when the item loads/the save finishes.
		private static void Prefix(GearItem __instance)
		{
			if (FoodRebalanceSettings.Instance.adjustCalories && __instance.m_FoodItem && !__instance.m_FoodWeight)
			{
				float changedCal = FoodRebalanceMod.ChangedCaloriesForItem(__instance.name, __instance);
				__instance.m_FoodItem.m_CaloriesRemaining -= changedCal;
				__instance.m_FoodItem.m_CaloriesTotal -= changedCal;
			}
		}
		
		// Restore the calorie changes that were removed in Prefix.
		private static void Postfix(GearItem __instance)
		{
			if (FoodRebalanceSettings.Instance.adjustCalories &&__instance.m_FoodItem && !__instance.m_FoodWeight)
			{
				float changedCal = FoodRebalanceMod.ChangedCaloriesForItem(__instance.name, __instance);
				__instance.m_FoodItem.m_CaloriesRemaining += changedCal;
				__instance.m_FoodItem.m_CaloriesTotal += changedCal;
			}
		}
	}

	[HarmonyPatch(typeof(GearItem), "Deserialize")]
	internal static class FoodRebalanceDeserializePatch
	{
		// I'm unsure if this is necessary, but it works fine, and so better safe than sorry.
		private static void Prefix(GearItem __instance)
		{
			if (FoodRebalanceSettings.Instance.adjustCalories &&  __instance.m_FoodItem && __instance.m_FoodWeight)
			{
				__instance.m_FoodWeight.m_CaloriesPerKG =
					FoodRebalanceMod.ChangedCaloriesForItem(__instance.name, __instance);
			}
		}

		// Deserialize happens when an item is loaded from a save file,
		// meaning we'll need to adjust the calories again.
		// Any calorie changes that happen in Awake() will be overriden by the loaded item.
		// Running this method after the load completes will fix that issue.
		private static void Postfix(GearItem __instance)
		{
			if (FoodRebalanceSettings.Instance.adjustCalories &&  __instance.m_FoodItem)
			{
				if (__instance.m_FoodWeight)
				{
					__instance.m_FoodWeight.m_CaloriesPerKG =
						FoodRebalanceMod.ChangedCaloriesForItem(__instance.name, __instance);
				}
				else
				{
					float changedCal = FoodRebalanceMod.ChangedCaloriesForItem(__instance.name, __instance);
					__instance.m_FoodItem.m_CaloriesRemaining += changedCal;
					__instance.m_FoodItem.m_CaloriesTotal += changedCal;
				}
			}
		}
	}

	[HarmonyPatch(typeof(Panel_CanOpening), "OnOpen")]
	internal static class LemmeSmash
	{
		private static bool Prefix(Panel_CanOpening __instance)
		{
			FoodRebalanceSettings.CanSmashingMode smashMode = FoodRebalanceSettings.Instance.efficientSmashing;
			FoodRebalanceMod.DebugLog("Checking can smashing...");
			if (smashMode == FoodRebalanceSettings.CanSmashingMode.EfficientSmashing && GameManager.GetSkillCooking().NoCalorieLossWhenSmashingOpen() ||
				smashMode == FoodRebalanceSettings.CanSmashingMode.AlwaysSmash)
			{
				FoodRebalanceMod.DebugLog($"Settings inform us to smash the can. Efficient: " +
					$"{(smashMode == FoodRebalanceSettings.CanSmashingMode.EfficientSmashing).ToString()} Smashing open");
				__instance.OnSmash();
				return false; // Skip the rest of the method
			}
			if (smashMode == FoodRebalanceSettings.CanSmashingMode.CanOpenerOnly)
			{
				if (GameManager.GetInventoryComponent().GetBestGearItemWithName("GEAR_CanOpener") != null)
				{
					FoodRebalanceMod.DebugLog("Settings use a can opener only, and we have one. Opening normally");
					return true;
				}
				FoodRebalanceMod.DebugLog("Settings use a can opener only, but we do not have one. Smashing open");
				__instance.OnSmash();
				return false;
			}
			FoodRebalanceMod.DebugLog("Normal mode, or no logic fell through. Opening normally");
			return true; // Continue as normal
		}
	}
}
