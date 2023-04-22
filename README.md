![Logo](Images/AnimationUI.png)
<h1 align="center">AnimationUI</h1>

AnimationUI is a unity tool to create UI animation easily with no code. You can simply drag and drop to create some smooth UI animation. There are some option that you can set such as the easing, duration, etc. It basically consist of a component that has an array of sequence that has an array of task. This may be special for UI, but it's also possible to animate values for non UI component like transform. You can also make a custom sequence for non UI component with UnityEvent including the dynamic one. Adding function to call at certain time or at the end of animation is also possible with code.


## 🕹️ Demo Preview

<p align="center" width="100%">
    <img width="49%" src="./Images/PreviewAnimationUI.gif"> 
    <img width="49%" src="./Images/PreviewSettings.gif"> 
</p>
<p align="center" width="100%">
    <img width="49%" src="./Images/PreviewUpgrade.gif"> 
    <img width="49%" src="./Images/PreviewStart.gif"> 
</p>


## ✨ Features

- Sequence for Animating values of RectTransform, Image, Camera, CanvasGroup, Transform, and Dynamic UnityEvent.
- Sequence for instant method such as Set Active All Input, Play SFX, Wait before executing next sequence, Set Active GameObject, Loading scene, and UnityEvent
- Preview animation in edit mode with Progress bar both globally and in each sequence.
- Reorderable sequences.
- Addable function to call at the end of animation or at certain determined time.
- Public variables for every sequence.
- Custom ButtonUI as bonus.
- Demo.
- Others.


## 📘 Instruction

![Instruction 1](Images/1.gif)
- Right click -> UI -> Create AnimationUI, or you can just add the AnimationUI Component to a gameObject
- Choose the kind of sequence you want.
- If you choose animation, assign the kind of component you want to animate to the inspector of the AnimationUI component.
<br/>

![Instruction 2](Images/2.gif)
- It's recomended to to to lock the inspector so that animating the values is easier.
- You can capture the start values by clicking the set start button.
- You can also capture the end value by changing the value, then clicking the set end button.
<br/>

![Instruction 3](Images/3.gif)
- Drag the progress bar to see how would the animation look like.
- You can also play the animation in edit mode, but make sure the scene view is open or the animation might have some lag.
<br/>

![Instruction 4](Images/4.gif)
- Try looking at the demo for examples. But make sure the "Level1" scene is added in the build settings or you can't load the "Level1" scene
- There are also progress bars for eace sequence in the left side of the sequence.
<br/>

![Instruction 5](Images/5.gif)
- Try comparing the upgrade menu and the settings menu of the demo.
- Notice that those settings position is always relative to the left side or the right side, but then it becomes relative to the middle of the screen.
- In the upgrade menu of the demo, there's also a similiar scenario with the settings menu.
- if you're not sure how to create this, you can always capture all variable with the set start or the set end button. One example is when you want to animate Rect Transform with Anchor Presets of stretch. just try enabling all variable and set everything, you might be able to produce a similiar result with the settings or upgrade menu in the demo.
<br/>

![Instruction 6](Images/6.gif)
- You can create the animation in a short time by utilizing some tricks with Unity built in list in the inspector. For example adding a new sequence will automatically copy the previous, it can be quick if the sequence is similiar with the other. Or doing something like creating many copies of wait sequence before using them.
<br/>

![Instruction 7](Images/7.png)
- With this tool, you can also create sequence that set active all input, play sfx, wait before executing next sequence, set active gameObject, loading scene, and do custom things with UnityEvent with each of them having different color.

## 🔍 API Reference

Get the reference by

```csharp
AnimationUI _animationUI;
_animationUI.MyMethodName();
```

### 🔗 Syntax

| Method                            | Description                        |
|:--------                          | :------------------------------    |
|`Play()`                           | Play the animation |
|`PlayReversed()`                   | Play the animation but reversed. Usefull to go back from a certain menu quickly.|
|`AddFunctionAt(float time, delegate func)`| Add a function to be called at a certain time after the AnimationUI.Play() is called|
|`AddFunctionAtEnd(delegate func)`  |Add a function to be called when the latest wait sequence is finished. It's intended like this so that you also have an option for this case and not just at the very end of the whole sequences. If you want to make it get called at the end of the whole sequences, you can either call `AddFunctionAt(_animationUI.TotalDuration, func)` or just add another wait sequence as the last sequence in the inspector|

Most of the variable in the Sequence class is modifiedable, so it's possible to change the values of `_animationUI.AnimationSequence[MyIndex].MyVariableName` on runtime.

### 📖 Examples

Play the animation, call `LoadSceneWithLoadingBar()` after animation finished.
```csharp
_animationUI.AddFunctionAtEnd(LoadSceneWithLoadingBar);
_animationUI.Play();
```

## 📃 Note
- There's a bonus component for ButtonUI
- There's also reverse sequence button usefull to go back from other menu.
- Make sure to press the preview start because you may accidentally do something like disabling all input
- Theres progress indicator individually in the left side of the sequences.
- Toggling PlayOnStart to true is usefull for transition to a new scene.
- Make sure the Singleton prefab exist in the resources folder. Don't move it outside.
- There's still no proper way to disable all input so if you also want to disable input other than mouse and touch, please modify line 9, 14, and 19 of Customizable.cs
- readme to explain others category for custom button demos, etc.


## 📝 License
[MIT](https://choosealicense.com/licenses/mit/)


