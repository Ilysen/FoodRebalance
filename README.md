# Food Rebalance

This is a mod for *The Long Dark* that changes the calorie values of meat and fish to more closely resemble their nutrition in real life. Food items in TLD trend towards being less nutritious in-game than they should be; this is most apparent in rabbits, and most especially fish, some of which are over three times less filling than they would normally be for their size.

It also implements a feature that blocks tool use on opening cans if the player's cooking skill has advanced to a point where they would not lose any calories from smashing open the can instead of using a tool, which potentially saves durability in the long-term instead of using tools when there's no longer a need to do so.

## Why? Doesn't this make the game a lot easier?

Short answer: Because a 4kg wild Coho salmon is worth, like, 3,000 calories after preparation, instead of the 1,200 the game gives you.

Long answer: Mostly for fun. The game is indeed made much easier with this mod, and the game balance is broken with it. Nonetheless, it felt a little offensive how ridiculous how few calories meat items nourished you for in a game that strives to be realistic in many areas. That last part's not from my own mouth, it's literally in the game's splash screen. None of the meats in the game would offer less than a thousand calories a real-world scenario, and good-sized fish are very filling, even after filleting.

## How'd you get these values?

I used data from the [Alaska Department of Fish and Game](https://www.adfg.alaska.gov/index.cfm?adfg=hunting.eating) where applicable for game meats. For deer, I used the statistics for caribou based on the fact that all deer in *The Long Dark* have antlers. Wolf meat is currently unchanged from a calorie perspective.

For fish, I used the [US Department of Agriculture](https://ndb.nal.usda.gov/fdc-app.html) food data repository to determine the calorie density, and then adjusted that based on the percentage of yield you get from the actual fish when filleting it. Yield data was harder to pin down, so I got it from various different sources. Fish data may not be entirely accurate (since the fish you catch isn't entirely composed of fillets) but it should be better than it currently is.

Values were also rounded down in all cases for simplicity's sake, and so we can at least pretend that we're trying to maintain any semblance of balance.

## Does this work on existing saves?

Probably. It should work for fish, because the values that determine their calorie calculation are changed directly. Non-fish items, however, have calorie values assigned directly. It may behave weirdly with meat items in an existing save, but your mileage may vary. You should back up your save just in case.

## I want one part of the mod, but not the other part. Can I do that?

Sure! The mod's settings tab allows you to enable or disable any of the mod's features. You can use the calorie tweaks without the can opening changes, or you can smash open cans without changing the calorie values of any food. Both settings are enabled by default, but you're free to disable either of them as you please.

## Installation

This mod requires the [ModSettings](https://github.com/zeobviouslyfakeacc/ModSettings) mod as a prerequisite.

1. Install the latest version of the [MelonLoader](https://github.com/HerpDerpinstine/MelonLoader/releases/tag/v0.2.6) mod loader.
2. Download and install the latest release of [ModSettings](https://github.com/zeobviouslyfakeacc/ModSettings).
3. Download `FoodRebalance.dll` from the [latest release](https://github.com/Ilysen/FoodRebalance/releases/latest).
4. Move `FoodRebalance.dll` into the Mods folder in your TLD install directory.

## Legal Stuff

*The Long Dark* is created and owned by Hinterland Studio; I do not own any part of the game. Food Rebalance is licensed under the MIT License.

## Calorie Edit List

These values are on a per-kilogram basis. Two kg of fish will have twice as many listed calories, and so on.

**Meats**

* **Raw Venison** - 900 calories/kg -> 1200 calories/kg
* **Cooked Venison** - 800 calories/kg -> 1100 calories/kg
* **Raw Rabbit** - 500 calories/kg -> 1100 calories/kg
* **Cooked Rabbit** - 450 calories/kg -> 1050 calories/kg
* **Bear Meat (raw and cooked)** - 900 calories/kg -> 1600 calories/kg
* **Moose Meat (raw and cooked)** - 900 calories/kg -> 1300 calories/kg

**Fish**

* **Rainbow Trout (raw and cooked)** - 250 calories/kg -> 770 calories/kg
* **Lake Whitefish (raw and cooked)** - 250 calories/kg -> 780 calories/kg
* **Smallmouth Bass (raw and cooked)** - 300 calories/kg -> 440 calories/kg
* **Coho Salmon (raw and cooked)** - 300 calories/kg -> 770 calories/kg