# Reava_'s Udon UwUtils Toolkit

*You'll find all sorts of niche scripts made in U# by myself for different projects, revisited & cleaned for everyone's use.*
*By all means you are very welcome to pick and choose parts of my code to make your own scripts! These serve as a handy toolbox for everyone either for using directly in projects or to grow into new scripts of their own.*

**Info**: There are currently 31 Udon Scripts and 1 Tool in UwUtils! This will continue to expand as I find more ideas.

**Tips**: 
- You can easily add any of my scripts by simply typing it in the component window instead of adding a Udon behaviour first!
- A lot of my scripts work best when used with other UwUtils scripts, you can do a LOT by combining their efforts!
- A lot of variables have tooltips you can hover over to get more insight
- My scripts output proper errors and logs, you can search for "*Reava_/UwUtils/*" in your logs/console to see any issues or find more insight about their behaviour.
- If you find any issues, need support or think of a script you'd like to see, you can join my **[Discord](https://discord.gg/TxYwUFKbUS)** or open an issue on Github!

## **Script list**
- **[i TP](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/iTP.cs)**:
Literally just TPs you on interact, that's it.
- **[i State](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/iState.cs)**:
Switches the state of an array of GameObjects. Can receive events to either invert the state, toggle all on, or toggle all off. (_InvertState,_Disable,_Enable)
- **[i State Set](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/iStateSet.cs)**:
On interact, sets the state of an array of gameObjects. Does NOT revert once triggered again, it SETS the state and is NOT synced. (Events: _Invert to do the opposite, _Switch to swap all states)
- **[Tag Assigner](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/TagAssigner.cs)**:
Functions as a whitelist with functions, assigns a Tag to anyone who matches their username to the user Array of the behavior on world join. Local, allows toggling specific objects if user matches. Has a toggle to empower the user if they just created the instance regardless of whitelist matching. Supports adding users on the go and loading a remote string for updating the whitelist without updating the world!
- **[Tag TP](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/TagTP.cs)**:
If you got the correct Tag to your name on interact with the behavior, teleports you to the target, if not, teleports you to the second target (or doesn't if empty / disabled)
- **[reflectionprobeiscool](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/reflectionprobeiscool.cs)**:
RelfectionProbes are cool! make them real time, scripted and add this script to change the frequency they refresh at !
- **[Spinny](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/Spinny.cs)**:
A script to rotate things on any axis, at any speed, and even at weird update speeds (like 30 degrees but only once a second). You should do that with animators, but this might be useful idk.
- **[Unity Fog Toggle](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/UnityFogToggle.cs)**:
Just an interact toggle that toggles ON/OFF Unity's fog... that's it. Call it with a trigger or a UI button, it'll work.
- **[Scene Initializer](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/SceneInitializer.cs)**:
Want to have things enabled for the first few seconds an user enters your world then disable ? the opposite ? both ? Just use that, ezpz
- **[tag Setter](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/tagSetter.cs)**:
Set a pre determined tag to the local user on interact. that's it.
- **[Tag Debugger](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/TagDebugger.cs)**:
Handy tool to display the local user's tag and output it to the debugLogs or text (Compatible with UnityUI, TMP & TMP GUI), updates on Interact & on Start.
- **[Tag Array TP](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/TagArrayTP.cs)**:
Have a lot of tags & want each one to TP the user to a different spot ? Well... this does it all for ya! Even has a fallback target when the user doesn't have a tag (can be disabled to disallow TPing when no matching ranks are found)
- **[Event Relay](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/EventRelay.cs)**:
Wanna use a button to push another button ? Do the same as UI can do ? Yup, just type the event name (like "\_interact"), if you want a delay or not & for how many seconds.... and you're good to go! You can also check the state of another object to ignore the delay if that object is on / off. Will support UdonBehavior Arrays on for the UdonSharp1.0 update soon
- **[Udon Keybinds](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/UdonKeybinds.cs)**:
Send an event call to 6 different udon behaviors based on keybinds, serves either for RollTheRed's Camera System or as a code template. Press CTRL + 1 to 6 to trigger changes. CTRL + 0 to toggle keybinds ON/OFF, defaults to ON unless changed. Will support UdonBehavior Arrays on for the UdonSharp1.0 update soon
- **[Animator Driver](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/AnimatorDriver.cs)**:
Inverts a boolean on an animator on interact... and that's it
- **[Trigger Zone Relay](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/TriggerZoneRelay.cs)**:
Assign trigger colliders, and assign an Udon Behavior to send an event to either on Enter or Exit, super simple stuff! Supports UdonBehaviorArrays for U# 1.x version of the scripts.
- **[Playercount To Animator](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/PlayercountToAnimator.cs)**:
Enables driving an Animator's parameter (one parameter per Behavior, multiple Animators at once supported) between two values (Min/Max) depending on the player count in the instance. Can set the player count cap to reach max value.
- **[Join Bell](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/JoinBell.cs)**:
Pretty straightforward, just tap in an AudioSource & a clip for Join/Leave and enjoy
- **[Toggle Canvas](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/ToggleCanvas.cs)**:
Same as iState, but for Canvas components
- **[MeshRenderer Swapper](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/MeshRendererSwapper.cs)**:
Enables swapping between two Groups of Mesh Renderers at runtime, setting between 1 & 2 as default, and which group by default on Quest. practical for optimization toggles. Supports events (_switchGroup, _enableOne, _enableTwo)
- **[Instance Creator Relay](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/InstanceCreatorRelay.cs)**:
Sends a custom event of your choice to Udon Behaviors if the local user just created the instance
- **[Fading TP](https://github.com/Reava/ReavaUwUtils/tree/main/FadingTP)**:
A small prefab that allows you to setup an infinite amount of teleports with Fade In/Out blackout effects! (Can change the fade speed per door, super lightweight)
- **[Spawn Fade](https://github.com/Reava/ReavaUwUtils/tree/main/SpawnFade)**:
A small prefab for fading into a world when you join, can toggle to also fade in when respawning! (Toggleable on runtime, can change the fade speed)
- **[RemoteStringToText](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/RemoteStringToText.cs)**:
Allows loading a remote string from a URL and output to an array, or any type of text field (feel free to use as a code base for your own use!)
- **[Sequencial Toggle](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/.cs)**:
Toggles a set of objects in sequential order, can send a different event to toggle it completely and keep progress, interact to go through it.
- **[Advanced UI Toggle](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/AdvancedUIToggle.cs)**:
All the things done around a toggle packed in a single behavior, can be used with a UI button, physical event button or actual UI button to change anything Ui related + sound feedback, all toggleable.
- **[Collectible](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/[Collectible].cs)**:
WHen interacted with, can send a value to the [Collection System](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/[CollectionSystem].cs) to add to the balance of it
- **[Collection System](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/[CollectionSystem].cs)**:
The brain that receives events from [Collectibles](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/[Collectible].cs) and adds up all the values received, can output to mulitple text displays
- **[Toggle Hub](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/[ToggleHub].cs)**:
Links any number of toggles together and allows any of them to control the rest of them, and update a script when a new value is received
- **[Slider Hub](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/[SliderHub].cs)**:
Links any number of sliders together and allows any of them to control the rest of them, and update a script when a new value is received
- **[PostProcessing Controller](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/[PostProcessingController].cs)**:
Controls the post processing weight based on a slider or [Slider Hub](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/[SliderHub].cs)
- **[Instance Time Actions] > NOT READY <**
Enables performing actions based on Instance Time (segmented), synced for late joiners.

## **Tools**
- **[BakeryEditorAddons**:
Allows scaling any property of any bakery light in group by selecting multiple or their parents then right click > Bakery > scale x. Accepts any basic mathematical operation (ex: *2 or /5 or +5 etc)

## **Requirements**
Check updates before reporting issues.

- **[Unity](https://docs.vrchat.com/docs/current-unity-version)** (Tested: v2019.4.31f1)
- **[VRChat Worlds SDK3](https://vrchat.com/home/download)** (Tested: v3.1.11 VCC)
- **[UdonSharp](https://github.com/vrchat-community/UdonSharp)** (Tested: v1.17+)
- **Text Mesh Pro** is very recommended but won't break things, can be imported anytime.

## **Extras**
- **UwUtils_AxisGuides**
A package containing a Blender & Unity Axis model for debugging or display, <1kb texture & 1 Meter scale (bounds)

## **Links**
Get support & support me here:
- https://discord.gg/TxYwUFKbUS
- https://patreon.com/Reava

Some tutorials might be posted on my Youtube: https://www.youtube.com/channel/UCm3RYWUql-2yGt8K2u9eFEg/
