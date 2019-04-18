![preview1](https://raw.githubusercontent.com/cptgrey/KK_Archetypes/master/Assets/KKAT_Preview_400.gif)

# KK_Archetypes

KK_Archetypes allows you to favorite different character parts and recombine them into new characters. 

## Requirements
- KKAPI v1.2
- KKABMX v3.0.1


## Features

### The Archetypes menu
<img src= "https://raw.githubusercontent.com/cptgrey/KK_Archetypes/master/Assets/KKAT1.gif">

The plugin adds a new subcategory to the Parameters menu (Heart Icon), where you can toggle which parts you want to save to your favorites or recombine to create a new character. There is also an advanced menu option for renaming your favorite parts, saving your favorites to disk for later use, and much more.


### The Quick Controls menu
<img src= "https://raw.githubusercontent.com/cptgrey/KK_Archetypes/master/Assets/KKAT2.gif">

In the Load Character and Load Outfit menu screens, you will find a Quick Access menu to the right side where you can quickly add items to your favorite list from the selected cards. The "Jump to Next" option increments the selected card for you, making it easier to quickly add parts from cards to your favorites collection. Outfits, with accessories, can be added from either Character cards or Coordinate cards. Or you could always add items from your current character from the Archetype submenu instead.


### Recombining new characters
<img src= "https://raw.githubusercontent.com/cptgrey/KK_Archetypes/master/Assets/KKAT3.gif">

When you have added enough parts to your favorite list, you can at any point recombine your favorite parts to a new character by going to the Archetype submenu, checking the parts you want to randomize, and clicking "Get Random From Favorite". If you want to add a specific part, you can find all the parts you have added to your list in the Advanced Controls menu.

## Menu Overview / Guide
<img src= "https://raw.githubusercontent.com/cptgrey/KK_Archetypes/master/Assets/Menu_Overview.png">

<img src= "https://raw.githubusercontent.com/cptgrey/KK_Archetypes/master/Assets/Quick_Overview.png">

## Saving / Reloading / Resetting Favorites
After creating a list of favorite parts, you can save your list for later with the advanced controls in the Archetypes menu. The list will be saved to `Koikatu/BepInEx/KKAT/KKAT_Data.xml` There are options for resetting / clearing the current list, and reloading the currently saved list in the Advanced Options menu.

## Parts Overview
### Hair
- Hair Style (Front, Back, Sides, Ahoge, Gloss)
- Hair Colors (Root, Tip, Outline, Gloss)
### Eyes
- Eyeliner (Top, Bottom, Color)
- Eyecolor / Iris (Colors, Type, Highlights, Sclera)
### Face
- Eyebrow (Uses color from characters hair color)
- Face (Face parameters + KKABMX Bones)
### Body
- Skin (Makeup, Skin type, Skin Color, etc...)
- Body (Body parameters + KKABMX Bones)
### Clothes
- Clothes, Accessories

## Compatibility / Bugs
- There are some issues with the Kiyase plugin and chest parameters, so disabling this is recommended.
- Because of how skirt bones are loaded into the Maker, you might experience some issues with Skirt Scale parameters when recombining characters. This is a known bug, and will (hopefully) be improved on later releases. For now this can be remedied by saving and reloading your card.

## Planned additions:
- Option for favoriting custom overlays (Skin, Clothes, Eyes, etc...)
- Options for managing different favorite lists
- Possibly move Load menu options into a custom System subcategory

## Feedback
Hit me up on the Koikatu discord if you have any suggestions / feedback on this mod. I apologize in advance for slow replies, as I'm quite busy with Uni for a while now, but I'll get back to you as soon as I can.

## Special thanks to:
- [ManlyMarco](https://github.com/ManlyMarco)
- [Anon11 (a.k.a. DeathWeasel1337)](https://github.com/DeathWeasel1337)
- [bbepis](https://github.com/bbepis)
- essu
- The community in general :)