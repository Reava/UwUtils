
<div align=left>
  <img alt="GitHub Workflow Status" src="https://img.shields.io/github/actions/workflow/status/reava/UwUtils/release.yml?style=for-the-badge">
  <a href="https://github.com/Reava/UwUtils/releases/latest/"><img alt="GitHub release (latest by date)" src="https://img.shields.io/github/v/release/reava/UwUtils?logo=unity&style=for-the-badge"></a>
  <a href="https://github.com/Reava/UwUtils/releases/latest/"><img alt="GitHub all releases" src="https://img.shields.io/github/downloads/reava/UwUtils/total?color=blue&style=for-the-badge"></a>
</div>

# 🧰 Reava_'s Udon UwUtils Toolkit

* You'll find all sorts of niche scripts made in U# by myself for different projects, revisited & cleaned for everyone's use.
* This Toolkit is made to use simple scripts in unison to create more in specific behaviors rather than highly specific or overengineered scripts!
* By all means you are very welcome to pick and choose parts of my code to make your own scripts! These serve as a handy toolbox for everyone either for using directly in projects or to grow into new scripts of their own.

**Info**: There are currently 31 Udon Scripts in UwUtils! This will continue to expand as I find more ideas.

## ℹ️ **Tips**: 
- You can easily add any of my scripts by simply typing it in the component window instead of adding a Udon behaviour first!
- A lot of my scripts work best when used with other UwUtils scripts, you can do a LOT by combining their efforts!
- A lot of variables have tooltips you can hover over to get more insight
- My scripts output proper errors and logs, you can search for "*Reava_/UwUtils/*" in your logs/console to see any issues or find more insight about their behaviour.
- If you find any issues, need support or think of a script you'd like to see, you can join my **[Discord](https://discord.gg/TxYwUFKbUS)** or open an issue on Github!

## 📥 Add it to VCC as a package ! [![AddToVCC](https://github.com/user-attachments/assets/4767cf84-e44b-4595-b818-faa6735d9180)](https://reava.github.io/UwUtils/)

## 📋 **Script list**
<details>
<Summary>Expand me!</summary>
- **[Player Teleporter](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/PlayerTeleporter.cs)**:
Literally just TPs you on interact, that's it.
- **[Objects Toggle](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/ObjectsToggle.cs)**:
Toggles the state of an array of GameObjects & is persistence ready!
- **[Object State Setter](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/ObjectStateSetter.cs)**:
On interact, sets the state of an array of gameObjects. Does NOT revert once triggered again, it SETS the state and is NOT synced. (Events: _Invert to do the opposite, _Switch to swap all states)
- **[Tag Assigner](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/TagAssigner.cs)**:
Functions as a whitelist with functions, assigns a Tag to anyone who matches their username to the user Array of the behavior on world join. Local, allows toggling specific objects if user matches. Has a toggle to empower the user if they just created the instance regardless of whitelist matching. Supports adding users on the go and loading a remote string for updating the whitelist without updating the world!
- **[Tag TP](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/TagTP.cs)**:
If you got the correct Tag to your name on interact with the behavior, teleports you to the target, if not, teleports you to the second target (or doesn't if empty / disabled)
- **[ReflectionProbeController](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/ReflectionProbeController.cs)**:
RelfectionProbes are cool! make them real time, scripted and add this script to change the frequency they refresh at ! Use ToggleLoop() to toggle if it refreshes in a loop or stops until enabled again
- **[Spinny](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/Spinny.cs)**:
A script to rotate things on any axis, at any speed, and even at weird update speeds (like 30 degrees but only once a second). You should do that with animators, but this might be useful idk.
- **[Unity Fog Toggle](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/UnityFogToggle.cs)**:
Just an interact toggle that toggles ON/OFF Unity's fog... that's it. Call it with a trigger or a UI button, it'll work.
- **[Scene Initializer](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/SceneInitializer.cs)**:
Want to have things enabled for the first few seconds an user enters your world then disable ? the opposite ? both ? Just use that, ezpz
- **[tag Setter](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/tagSetter.cs)**:
Set a pre determined tag to the local user on interact. that's it.
- **[Tag Debugger](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/TagDebugger.cs)**:
Handy tool to display the local user's tag and output it to the debugLogs or text (Compatible with UnityUI, TMP & TMP GUI), updates on Interact & on Start.
- **[Tag Array TP](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/TagArrayTP.cs)**:
Have a lot of tags & want each one to TP the user to a different spot ? Well... this does it all for ya! Even has a fallback target when the user doesn't have a tag (can be disabled to disallow TPing when no matching ranks are found)
- **[Event Relay](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/EventRelay.cs)**:
Wanna use a button to push another button ? Do the same as UI can do ? Yup, just type the event name (like "\_interact"), if you want a delay or not & for how many seconds.... and you're good to go! You can also check the state of another object to ignore the delay if that object is on / off. Will support UdonBehavior Arrays on for the UdonSharp1.0 update soon
- **[Udon Keybinds](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/UdonKeybinds.cs)**:
Send an event call to 6 different udon behaviors based on keybinds, serves either for RollTheRed's Camera System or as a code template. Press CTRL + 1 to 6 to trigger changes. CTRL + 0 to toggle keybinds ON/OFF, defaults to ON unless changed. Will support UdonBehavior Arrays on for the UdonSharp1.0 update soon
- **[Animator Driver](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/AnimatorDriver.cs)**:
Inverts a boolean on an animator on interact... and that's it (Persistence ready!)
- **[Trigger Zone Relay](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/TriggerZoneRelay.cs)**:
Assign trigger colliders, and assign an Udon Behavior to send an event to either on Enter or Exit, super simple stuff! Supports UdonBehaviorArrays for U# 1.x version of the scripts.
- **[Playercount To Animator](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/PlayercountToAnimator.cs)**:
Enables driving an Animator's parameter (one parameter per Behavior, multiple Animators at once supported) between two values (Min/Max) depending on the player count in the instance. Can set the player count cap to reach max value.
- **[Join Bell](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/JoinBell.cs)**:
Pretty straightforward, just tap in an AudioSource & a clip for Join/Leave and enjoy (bell sound can be toggled by UI or eventRelay using "_JoinToggle" event)
- **[Toggle Canvas](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/ToggleCanvas.cs)**:
Same as iState, but for Canvas components
- **[MeshRenderer Swapper](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/MeshRendererSwapper.cs)**:
Enables swapping between two Groups of Mesh Renderers at runtime, setting between 1 & 2 as default, and which group by default on Quest. practical for optimization toggles. Supports events (_switchGroup, _enableOne, _enableTwo)
- **[Instance Creator Relay](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/InstanceCreatorRelay.cs)**:
Sends a custom event of your choice to Udon Behaviors if the local user just created the instance
- **[Fading TP](https://github.com/Reava/ReavaUwUtils/tree/main/FadingTP)**:
A small prefab that allows you to setup an infinite amount of teleports with Fade In/Out blackout effects! (Can change the fade speed per door, super lightweight)
- **[Spawn Fade](https://github.com/Reava/ReavaUwUtils/tree/main/SpawnFade)**:
A small prefab for fading into a world when you join, can toggle to also fade in when respawning! (Toggleable on runtime, can change the fade speed)
- **[Remote String To Text](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/RemoteStringToText.cs)**:
Allows loading a remote string from a URL and output to an array, or any type of text field (feel free to use as a code base for your own use!)
- **[Sequencial Toggle](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/.cs)**:
Toggles a set of objects in sequential order, can send a different event to toggle it completely and keep progress, interact to go through it.
- **[Advanced UI Toggle](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/AdvancedUIToggle.cs)**:
All the things done around a toggle packed in a single behavior, can be used with a UI button, physical event button or actual UI button to change anything Ui related + sound feedback, all toggleable.
- **[Collectible](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/Collectible.cs)**:
WHen interacted with, can send a value to the [Collection System](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/CollectionSystem.cs) to add to the balance of it
- **[Collection System](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/CollectionSystem.cs)**:
The brain that receives events from [Collectibles](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/Collectible.cs) and adds up all the values received, can output to mulitple text displays
- **[Multi UI Toggle Manager](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/MultiUIToggleManager.cs)**:
Links any number of toggles together and allows any of them to control the rest of them, and update a script when a new value is received, allowing to control a single script with multiple toggles for example.
- **[Multi UI Slider Manager](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/MultiUISliderManager.cs)**:
Links any number of sliders together and allows any of them to control the rest of them, and update a script when a new value is received, allowing to control a single thing with multiple sliders.
- **[PostProcessing Controller](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/PostProcessingController.cs)**:
Controls the post processing weight based on a slider or [Slider Manager](https://github.com/Reava/ReavaUwUtils/blob/main/Runtime/Scripts/MultiUISliderManager.cs) for multiple sliders controlling the same value.
- **[Instance Time Actions] > NOT READY <**
Enables performing actions based on Instance Time (segmented), synced for late joiners.
</details>

## ⚠️ **Requirements**
Check updates before reporting issues.

- **[Unity](https://docs.vrchat.com/docs/current-unity-version)** (Tested: v2022.3.22f1)
- **[VRChat Worlds SDK3](https://vrchat.com/home/download)** (Tested: v3.8.0)
- **Text Mesh Pro** is used by some scripts and generally used widely, import it.

## 🔗 **Links**
Get support & support me here:
- https://discord.gg/TxYwUFKbUS
- https://patreon.com/Reava

Some tutorials might be posted on my Youtube: https://www.youtube.com/channel/UCm3RYWUql-2yGt8K2u9eFEg/
