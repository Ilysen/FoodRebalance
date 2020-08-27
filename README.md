# FoodRebalance

This is a mod for The Long Dark that changes the calorie values of meat and fish to more closely resemble their nutrition in real life. Food items in TLD trend towards being less nutritious in-game than they should be; this is most apparent in rabbits, and most especially fish, some of which are over three times less filling than they would normally be for their size.

## Why? Doesn't this make the game a lot easier?

Short answer: Because a 4kg wild Coho salmon is worth, like, 3,000 calories after preparation, instead of the 1,200 the game gives you.

Long answer: Mostly for fun. The game is indeed made much easier with this mod, and it probably breaks intended game balance, but it still felt a little offensive how ridiculous how few calories meat items nourished you for in a game that strives to be realistic in many areas. That's not from my own mouth, it's literally in the game's splash screen! None of the meats in the game would offer less than a thousand calories a real-world scenario, and good-sized fish are very filling, even after filleting.

## How'd you get these values?

I used data from the [Alaska Department of Fish and Game](https://www.adfg.alaska.gov/index.cfm?adfg=hunting.eating) where applicable for game meats. For deer, I used the statistics for caribou based on the fact that all deer in The Long Dark have antlers. For fish, I used the [US Department of Agriculture](https://ndb.nal.usda.gov/fdc-app.html) food data repository to determine the calorie density, and then adjusted that based on the percentage of yield you get from the actual fish when filleting it. Yield data was harder to pin down, so I got it from various different sources. Fish data may not be entirely accurate (since the fish you catch isn't entirely composed of fillets) but it should be better than it currently is.

Values were also rounded down in all cases for simplicity's sake, and so we can at least pretend that we're attempting to maintain any semblance of balance.

## Does this work on existing saves?

Probably. It should work for fish, because the values that determine their calorie calculation are changed directly. Non-fish items, however, have calorie values assigned directly. It may behave weirdly with meat items in an existing save, but your mileage may vary. You should back up your save just in case.

## Installation

1. Download and install the latest release of [MelonLoader](https://github.com/HerpDerpinstine/MelonLoader/releases/tag/v0.2.6).
2. Download the latest release version.
3. Move `FoodRebalance.dll` into the Mods folder in your TLD install directory.

## Calorie Change List

These values are on a per-kilogram basis. Two kg of fish will have twice as many listed calories, and so on.

**Meats**

* **Raw Venison** - 900 calories -> 1200 calories
* **Cooked Venison** - 800 calories -> 1100 calories
* **Raw Rabbit** - 500 calories -> 1100 calories
* **Cooked Rabbit** - 450 calories -> 1050 calories
* **Bear Meat (raw and cooked)** - 900 calories -> 1600 calories
* **Moose Meat (raw and cooked)** - 900 calories -> 1300 calories

**Fish**

* **Rainbow Trout (raw and cooked)** - 250 calories -> 770 calories
* **Lake Whitefish (raw and cooked)** - 250 calories -> 780 calories
* **Smallmouth Bass (raw and cooked)** - 300 calories -> 440 calories
* **Coho Salmon (raw and cooked)** - 300 calories -> 770 calories