using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

#if UNITY_EDITOR 
using UnityEditor.Events;
[ExecuteInEditMode]
#endif
public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] float _duration = 0.1f;
    [SerializeField] Image _imageToResize;
    [SerializeField] TMPro.TextMeshProUGUI _textToTint;
    [SerializeField] Ease.Type _easeType = Ease.Type.OutBack;
    [SerializeField] Ease.Power _easePower = Ease.Power.Quart;
    Ease.Function _easeFunction;

    [Space]
    [SerializeField] float _enterScale = 1.2f;
    [SerializeField] float _exitScale = 1f;
    [SerializeField] float _downScale  = 1.3f;
    [SerializeField] float _upScale  = 1f;

    [Space]
    [SerializeField] Color _enterTint  = Color.white;
    [SerializeField] Color _exitTint  = Color.white;
    [SerializeField] Color _downTint  = new Color(0.8f, 0.8f, 0.8f,1);
    [SerializeField] Color _upTint  = Color.white;

    [Space]
    [SerializeField] Color _textEnterTint  = Color.white;
    [SerializeField] Color _textExitTint  = Color.white;
    [SerializeField] Color _textDownTint  = Color.white;
    [SerializeField] Color _textUpTint  = Color.white;
    
    



    [SerializeField] UnityEvent _pointerClickEvent;
    [SerializeField] UnityEvent _pointerEnterEvent;
    [SerializeField] UnityEvent _pointerExitEvent;
    [SerializeField] UnityEvent _pointerDownEvent;
    [SerializeField] UnityEvent _pointerUpEvent;

    enum CursorState
    {
        Inside, Outside
    }
    CursorState _currentCursorState;

#if UNITY_EDITOR 
    void Awake()
    {
        if(_imageToResize == null)
        {
            _imageToResize = GetComponent<Image>();
            if(_imageToResize == null)_imageToResize = GetComponentInChildren<Image>();

            if(_imageToResize != null)
            {
                _enterTint = _imageToResize.color;
                _exitTint  = _imageToResize.color;
                // _downColor  = _imageToResize.color;
                _upTint    = _imageToResize.color;
            }
                
        }
        if(_textToTint == null)
        {
            _textToTint = transform.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            if(_textToTint != null)
            {
                _textEnterTint = _textToTint.color;
                _textExitTint  = _textToTint.color;
                _textDownTint  = _textToTint.color;
                _textUpTint    = _textToTint.color;
            }
        }

        //So that it only creates the listener once, when the component is dragged on, not when the scene is loaded.
        if(_pointerEnterEvent == null)
        {
            _pointerEnterEvent = new UnityEvent ();
            UnityEventTools.AddVoidPersistentListener(_pointerEnterEvent, EnterScaleAnimation);
            if(_imageToResize != null)UnityEventTools.AddVoidPersistentListener(_pointerEnterEvent, EnterTintAnimation);
            if(_textToTint != null)UnityEventTools.AddVoidPersistentListener(_pointerEnterEvent, EnterTextTintAnimation);
            UnityEventTools.AddIntPersistentListener(_pointerEnterEvent, PlaySound, 0);
        }
        if(_pointerExitEvent == null)
        {
            _pointerExitEvent = new UnityEvent ();
            UnityEventTools.AddVoidPersistentListener(_pointerExitEvent, ExitScaleAnimation);
            if(_imageToResize != null)UnityEventTools.AddVoidPersistentListener(_pointerExitEvent, ExitTintAnimation);
            if(_textToTint != null)UnityEventTools.AddVoidPersistentListener(_pointerExitEvent, ExitTextTintAnimation);
        }
        if(_pointerDownEvent == null)
        {
            _pointerDownEvent = new UnityEvent ();
            UnityEventTools.AddVoidPersistentListener(_pointerDownEvent, DownScaleAnimation);
            if(_imageToResize != null)UnityEventTools.AddVoidPersistentListener(_pointerDownEvent, DownTintAnimation);
            if(_textToTint != null)UnityEventTools.AddVoidPersistentListener(_pointerDownEvent, DownTextTintAnimation);
            UnityEventTools.AddIntPersistentListener(_pointerDownEvent, PlaySound, 0);
        }
        if(_pointerUpEvent == null)
        {
            _pointerUpEvent = new UnityEvent ();
            UnityEventTools.AddVoidPersistentListener(_pointerUpEvent, UpScaleAnimation);
            if(_imageToResize != null)UnityEventTools.AddVoidPersistentListener(_pointerUpEvent, UpTintAnimation);
            if(_textToTint != null)UnityEventTools.AddVoidPersistentListener(_pointerUpEvent, UpTextTintAnimation);
        }
        if(_pointerClickEvent == null)
        {
            _pointerClickEvent = new UnityEvent ();
            UnityEventTools.AddIntPersistentListener(_pointerClickEvent, PlaySound, 0);

            UnityEventTools.AddVoidPersistentListener(_pointerClickEvent, EnterScaleAnimation);
            UnityEventTools.AddVoidPersistentListener(_pointerClickEvent, EnterTextTintAnimation);
            UnityEventTools.AddVoidPersistentListener(_pointerClickEvent, EnterTintAnimation);
        }
        
    }
    void OnValidate() => _easeFunction = Ease.GetEase(_easeType, _easePower);
#endif

    void Start() => _easeFunction = Ease.GetEase(_easeType, _easePower);
    public void PlaySound(AudioClip audioClip)
    {
        if(Singleton.Instance != null)
        Singleton.Instance.Audio.PlaySound(audioClip);
    }
    public void PlaySound(int index)
    {
        if(Singleton.Instance != null)
        Singleton.Instance.Audio.PlaySound(index);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _pointerEnterEvent.Invoke();
        _currentCursorState = CursorState.Inside;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _pointerExitEvent.Invoke();
        _currentCursorState = CursorState.Outside;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _pointerDownEvent.Invoke();
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if(_currentCursorState == CursorState.Inside){}// _pointerEnterEvent.Invoke();
        else if(_currentCursorState == CursorState.Outside)_pointerUpEvent.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _pointerClickEvent.Invoke();
    }

    public void EnterScaleAnimation(){StartCoroutine(TweenScale(_imageToResize.transform, _enterScale));}
    public void ExitScaleAnimation(){StartCoroutine(TweenScale(_imageToResize.transform, _exitScale));}
    public void DownScaleAnimation(){StartCoroutine(TweenScale(_imageToResize.transform, _downScale));}
    public void UpScaleAnimation(){StartCoroutine(TweenScale(_imageToResize.transform, _upScale));}

    public void EnterTintAnimation(){StartCoroutine(TweenTint(_imageToResize, _enterTint));}
    public void ExitTintAnimation(){StartCoroutine(TweenTint(_imageToResize, _exitTint));}
    public void DownTintAnimation(){StartCoroutine(TweenTint(_imageToResize, _downTint));}
    public void UpTintAnimation(){StartCoroutine(TweenTint(_imageToResize, _upTint));}

    public void EnterTextTintAnimation(){StartCoroutine(TweenTextTint(_textToTint, _textEnterTint));}
    public void ExitTextTintAnimation(){StartCoroutine(TweenTextTint(_textToTint, _textExitTint));}
    public void DownTextTintAnimation(){StartCoroutine(TweenTextTint(_textToTint, _textDownTint));}
    public void UpTextTintAnimation(){StartCoroutine(TweenTextTint(_textToTint, _textUpTint));}

    ushort _key;
    //Value will keep changing so that everytime a new TweenScale() coroutine is called,
    //the previous coroutine will be stopped and the new Scaling animation will be executed
    //without interuption.
    
    IEnumerator TweenScale(Transform trans, float endScale)
    {
        _key++;
        ushort requirement = _key;
        float startScale = trans.localScale.x;
        float t = 0;
        while (t <= 1 && requirement == _key)
        {
            trans.localScale = Vector3.one * Mathf.LerpUnclamped(startScale, endScale, _easeFunction(t));
            t += Time.unscaledDeltaTime / _duration;
            yield return null;
        }
        if(requirement == _key)trans.localScale = Vector3.one * endScale;//if the key didn't change then get into endScale
    }

    ushort _keyTint;
    IEnumerator TweenTint(Image img, Color endColor)
    {
        _keyTint++;
        ushort requirement = _keyTint;
        Color startColor = img.color;
        float t = 0;
        while (t <= 1 && requirement == _keyTint)
        {
            img.color = Color.Lerp(startColor, endColor, _easeFunction(t));
            t += Time.unscaledDeltaTime / _duration;
            yield return null;
        }
        if(requirement == _keyTint)img.color = endColor;//if the key didn't change then get into endColor
    }

    ushort _keyTextTint;
    IEnumerator TweenTextTint(TMPro.TextMeshProUGUI text, Color endColor)
    {
        _keyTextTint++;
        ushort requirement = _keyTextTint;
        Color startColor = text.color;
        float t = 0;
        while (t <= 1 && requirement == _keyTextTint)
        {
            text.color = Color.Lerp(startColor, endColor, _easeFunction(t));
            t += Time.unscaledDeltaTime / _duration;
            yield return null;
        }
        if(requirement == _keyTextTint)text.color = endColor;//if the key didn't change then get into endColor
    }

#if UNITY_EDITOR
    [UnityEditor.MenuItem("GameObject/UI/Create ButtonUI")]
    static void CreateButtonUI(UnityEditor.MenuCommand menuCommand)
    {
        GameObject selected = UnityEditor.Selection.activeGameObject;
        GameObject createdGo = new GameObject("ButtonUI");
        GameObject imgGo = new GameObject("Image");
        GameObject textGo = new GameObject("Text");
        UnityEditor.GameObjectUtility.SetParentAndAlign(createdGo, selected);
        UnityEditor.GameObjectUtility.SetParentAndAlign(imgGo, createdGo);
        UnityEditor.GameObjectUtility.SetParentAndAlign(textGo, imgGo);

        Image img = imgGo.AddComponent<Image>();
        img.color = Color.white;
        RectTransform imgRect = imgGo.GetComponent<RectTransform>();
        imgRect.anchorMin = new Vector2(0, 0);
        imgRect.anchorMax = new Vector2(1, 1);
        imgRect.sizeDelta = Vector2.zero;
        
        TMPro.TextMeshProUGUI text = textGo.AddComponent<TMPro.TextMeshProUGUI>();
        text.text = "Button";
        text.color = Color.black;
        text.alignment = TMPro.TextAlignmentOptions.Center;
        text.fontSize = 45;
        text.fontStyle = TMPro.FontStyles.Bold;
        text.verticalAlignment = TMPro.VerticalAlignmentOptions.Middle;
        RectTransform textRect = textGo.GetComponent<RectTransform>();
        textRect.anchorMin = new Vector2(0, 0);
        textRect.anchorMax = new Vector2(1, 1);
        textRect.sizeDelta = Vector2.zero;
        
        createdGo.AddComponent<ButtonUI>();
        RectTransform createdRect = createdGo.AddComponent<RectTransform>(); 
        createdRect.sizeDelta = new Vector2(400, 100);

        createdGo.GetComponent<ButtonUI>()._imageToResize = img;
        createdGo.GetComponent<ButtonUI>()._textToTint = text;

        UnityEditor.Undo.RegisterCreatedObjectUndo(createdGo, "Created +"+createdGo.name);
        UnityEditor.Undo.RegisterCreatedObjectUndo(imgGo, "Created +"+imgGo.name);
        UnityEditor.Undo.RegisterCreatedObjectUndo(textGo, "Created +"+textGo.name);
    }
#endif

}
