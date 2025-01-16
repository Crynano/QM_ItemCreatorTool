# QM_ItemCreatorTool

Guide to create mod with the tool

· First, subscribe to the mod [0.8.5] Item Importer in the workshop.
· Open the tool and add some weapons
· When you've finished, click "Create Mod" in the "General" Tab
	- Select the workshop folder as directory
· Start the game and use the console to spawn the new weapon!

##[IMAGES AND SOUND]
· Sound is not implemented in the tool, even though you can see the variables there
· For the paths to images, please copy & paste the full path to the image you want to use, the program will take care of copying and renaming stuff.

##[CREATE MOD]
To create the mod files, press the "Create Mod" in the "General" tab
A window will open, you must navigate to the folder where the base config file will be created.
From there, note that many folders and subfolders will be created, so my recommendation is an **empty folder**, or your** own mod folder**
**Note that "Create Mod" will OVERRIDE all previous configuration files (weapons/descriptors etc) that have **

Because you'll be using the Weapon Importer library, you must navigate to the workshop path of your computer
your result path should be something along the lines of:

*(Where you have installed steam, does not matter, the important part is: **workshop/content/2059170/3407014011)***
C:/ProgramFiles/Steam/steamapps/workshop/content/2059170/3407014011

##[TESTING]
And that's it, open the console in the inventory or in a mission and type: item "YourWeaponID"
If it spawns, success! If it doesn't, send me the logfile located in the mod folder

##[CREDITS]
· Crynano (https://github.com/Crynano)

##[PACKAGES CREDITS]
· Multiselect Combobox - https://github.com/RWS/Multiselect-ComboBox

