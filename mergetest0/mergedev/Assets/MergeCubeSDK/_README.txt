MergeCubeSDK V1.1.2
====================================================================================================================================
Merge User Account:
	Merge User Account is optional to Use.
    Add documents for user account system.
    Add debug mode so user account will not popup all the time.
    Add user account documentation to explane how to use user account.
GazeCaster:
    Change when it is in full screen you can tap anywhere to "click".   
MergeMultiTarget: 
	Add option DoNothing and DisableSelect.
		DoNothing will let you have full control how you want to handle your objects when marker find/loss tracking.
		DisableSelect will disable the gameobjects you select(refenced in scene) when loss tracking, and enable them when find.

====================================================================================================================================

MergeCubeSDK V1.1.1
====================================================================================================================================
Fix core issue.
Fix Merge User Account signin issue.
Update Merge Development Key system.
====================================================================================================================================

MergeCubeSDK v1.1.0
====================================================================================================================================
Brand new SDK system.
Add Merge User Account.
Add Multi Cube Support.
Add Multi Headset Support.
New Tutorial.
New Intro Sequence.
====================================================================================================================================

====================================================================================================================================

Before releasing your app on the Apple App Store or the Google Play Store, 
please consider purchasing the "In-App Web Browser": https://www.assetstore.unity3d.com/en/#!/content/57532 

====================================================================================================================================

Input Tool Scripts:
How to Use:

GazeCaster:
- Add this Gaze Caster on your ARCamera in the scene.
- Set the lMask in the inspector to whichever layer you wish for gui collisions to occur.

GazeResponder:
- This script is the base interface that you may inherit from to allow your scripts to accept gaze events from GazeCaster.

BasicGazeButton:
- This is an example implementation of the GazeResponder. It behaves similar to a UnityEngine.UI Button. 
- Attach it and a box collider to an object, set its layer to the lMask defined by your GazeCaster, and then it will do anything you specify in the exposed Unity Events.

GazeSpriteButton:
- This is an example implementation of the GazeResponder. It behaves similar to a UnityEngine.UI Button, with added functionality to swap sprites of an expected Image component. 
- Attach it, a box collider, and a UnityEngine.UI Image component to an object, set its layer to the lMask defined by your GazeCaster, and then it will do anything you specify in the exposed Unity Events.
- Additionally, this script allows you to swap sprites based on your interaction with the button.

InputRelativeRotation:
- Attach this script to the ImageTarget. Then register to the OnRotationChange event to listen for changes in rotation.
- This event call will happen every update.

PointerControl:
- Attach sprite renderer object to ARCamera to ensure reticle stays in place on screen.
- Attach script to your sprite renderer reticle.

InputVelocity:
- Register to the OnVelocityReached() event to receive the average direction once terminal velocity is reached.

AndroidAutofocuser:
- Attach this script to the MultiTarget object that handles tracking the HoloCube.

BasicTrackableEventHandler:
- Attach this script to the MultiTarget object within the scene.

CubeOrientation:
- Call this script's OrientateToCamera function from another script by using the proper namespace:
	Merge.CubeOrientation.OrientateToCamera (Transform imageTargetLocation, Transform target);



