![Logo](Assets/Logo/AnimationUI.png)
<h1 align="center">AnimationUI</h1>

AnimationUI is a unity tool to create UI animation easily with no code. You can simply drag and drop to create smooth UI animation. It basically consist of a component that has an array of sequence that has an array of task. You can also make a custom sequence with UnityEvent including the dynamic one.


## ✨ Features

- Animate values of RectTransform, Image, Camera, CanvasGroup, Transform, and Dynamic UnityEvent.
- Preview animation in edit mode with Progress bar both globally and in each sequence.
- Set Active All Input 
- Play SFX
- Wait before executing next sequence
- Set Active GameObject
- UnityEvent
- Others


## 📖 Instruction
- Right click -> UI -> Create AnimationUI
- Or you can just add the AnimationUI Component to a gameObject



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
|`AddFunctionAtEnd(delegate func)`  |Add a function to be called at a certain time after the animation is finished |

Most of the variable in the Sequence class is modifiedable, so it's possible to change the values of `_animationUI.AnimationSequence.MyVariableName` on runtime.

#### 📖 Examples

Play the animation, call `LoadSceneWithLoadingBar()` after animation finished.
```csharp
_animationUI.Play();
_animationUI.AddFunctionAtEnd(LoadSceneWithLoadingBar);
```


## 📝 License
[MIT](https://choosealicense.com/licenses/mit/)