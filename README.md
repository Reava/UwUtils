# Reava_'s Udon UwUtils
I makes Udon stuff...
You'll find all sorts of niche scripts made in U# by myself for different projects, revisited & cleaned for everyone's use.
By all means you are very welcome to pick and choose parts of my code to make your own scripts! These serve as a handy toolbox for everyone.

**WARNING**: Most of those aren't synced! Download the version for VCC if you use UdonSharp 1.0 or higher

- **[FadingTP](https://github.com/Reava/ReavaUwUtils/tree/main/FadingTP)**:
A small prefab that allows you to setup an infinite amount of teleports with Fade In/Out blackout effects! (Can change the fade speed per door, super lightweight)
- **[SpawnFade](https://github.com/Reava/ReavaUwUtils/tree/main/SpawnFade)**:
A small prefab for fading into a world when you join, can toggle to also fade in when respawning! (Toggleable on runtime, can change the fade speed)
- **[iTP](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/iTP.cs)**:
Literally just TPs you on interact, that's it.
- **[iState](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/iState.cs)**:
Switches the state of an array of GameObjects. Can receive events to either invert the state, toggle all on, or toggle all off. (_InvertState,_Disable,_Enable)
- **[iStateSet](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/iStateSet.cs)**:
On interact, sets the state of an array of gameObjects. Does NOT revert once triggered again, it SETS the state and is NOT synced. (Events: _Invert to do the opposite, _Switch to swap all states)
- **[TagAssigner](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/TagAssigner.cs)**:
Functions as a whitelist with functions, assigns a Tag to anyone who matches their username to the user Array of the behavior on world join. Local, allows toggling specific objects if user matches. Has a toggle to empower the user if they just created the instance regardless of whitelist matching.
- **[TagTP](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/TagTP.cs)**:
If you got the correct Tag to your name on interact with the behavior, teleports you to the target, if not, teleports you to the second target (or doesn't if empty / disabled)
- **[reflectionprobeiscool](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/reflectionprobeiscool.cs)**:
RelfectionProbes are cool! make them real time, scripted and add this script to change the frequency they refresh at !
- **[Spinny](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/Spinny.cs)**:
A script to rotate things on any axis, at any speed, and even at weird update speeds (like 30 degrees but only once a second). You should do that with animators, but this might be useful idk.
- **[UnityFogToggle](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/UnityFogToggle.cs)**:
Just an interact toggle that toggles ON/OFF Unity's fog... that's it. Call it with a trigger or a UI button, it'll work.
- **[SceneInitializer](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/SceneInitializer.cs)**:
Want to have things enabled for the first few seconds an user enters your world then disable ? the opposite ? both ? Just use that, ezpz
- **[tagSetter](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/tagSetter.cs)**:
Set a pre determined tag to the local user on interact. that's it.
- **[TagDebugger](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/TagDebugger.cs)**:
Handy tool to display the local user's tag and output it to the debugLogs or text (Compatible with UnityUI, TMP & TMP GUI), updates on Interact & on Start.
- **[TagArrayTP](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/TagArrayTP.cs)**:
Have a lot of tags & want each one to TP the user to a different spot ? Well... this does it all for ya! Even has a fallback target when the user doesn't have a tag (can be disabled to disallow TPing when no matching ranks are found)
- **[ActionRelay](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/ActionRelay.cs)**:
Wanna use a button to push another button ? Do the same as UI can do ? Yup, just type the event name (like "\_interact"), if you want a delay or not & for how many seconds.... and you're good to go! You can also check the state of another object to ignore the delay if that object is on / off. Will support UdonBehavior Arrays on for the UdonSharp1.0 update soon
- **[UdonKeybinds](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/UdonKeybinds.cs)**:
Send an event call to 6 different udon behaviors based on keybinds, serves either for RollTheRed's Camera System or as a code template. Press CTRL + 1 to 6 to trigger changes. CTRL + 0 to toggle keybinds ON/OFF, defaults to ON unless changed. Will support UdonBehavior Arrays on for the UdonSharp1.0 update soon
- **[AnimatorDriver](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/AnimatorDriver.cs)**:
Inverts a boolean on an animator on interact... and that's it
- **[TriggerRelay](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/TriggerRelay.cs)**:
Assign trigger colliders, and assign an Udon Behavior to send an event to either on Enter or Exit, super simple stuff! Supports UdonBehaviorArrays for U# 1.x version of the scripts.
- **[PlayercountToAnimator](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/PlayercountToAnimator.cs)**:
Enables driving an Animator's parameter (one parameter per Behavior, multiple Animators at once supported) between two values (Min/Max) depending on the player count in the instance. Can set the player count cap to reach max value.
- **[JoinBell](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/JoinBell.cs)**:
Pretty straightforward, just tap in an AudioSource & a clip for Join/Leave and enjoy
- **[ToggleCanvas](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/ToggleCanvas.cs)**:
Same as iState, but for Canvas components
- **[MeshRendererSwapper](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/MeshRendererSwapper.cs)**:
Enables swapping between two Groups of Mesh Renderers at runtime, setting between 1 & 2 as default, and which group by default on Quest. practical for optimization toggles. Supports events (_switchGroup, _enableOne, _enableTwo)
- **[InstanceCreatorRelay](https://github.com/Reava/ReavaUwUtils/blob/main/Scripts/InstanceCreatorRelay.cs)**:
Sends a custom event of your choice to Udon Behaviors if the local user just created the instance

# Scripts that require some more work before release:
- **InstanceTimeActions**: > NOT READY <
Enables performing actions based on Instance Time (segmented), synced for late joiners.

## **Requirements**
Check updates before reporting issues.

- **[Unity](https://docs.vrchat.com/docs/current-unity-version)** (Tested: v2019.4.31f1)
- **[VRChat Worlds SDK3](https://vrchat.com/home/download)** (Tested: v2022.1.1)
- **[UdonSharp](https://github.com/vrchat-community/UdonSharp)** (Tested: v1.15+)
- **Text Mesh Pro** is very recommended but won't break things, can be imported anytime.

## **Extras**
- **UwUtils_AxisGuides**
A package containing a Blender & Unity Axis model for debugging or display, <1kb texture & 1 Meter scale (bounds)

## **Links**
Get support & support me here:
- https://discord.gg/TxYwUFKbUS
- https://patreon.com/Reava

Some tutorials might be posted on my Youtube: https://www.youtube.com/channel/UCm3RYWUql-2yGt8K2u9eFEg/
