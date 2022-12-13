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
    [SerializeField] float _enterScale = 1.2f;
    [SerializeField] float _exitScale = 1f;
    [SerializeField] float _downScale  = 1.3f;
    [SerializeField] float _upScale  = 1f;
    [SerializeField] Color _upColor  = Color.white;
    [SerializeField] Color _downColor  = new Color(0.8f, 0.8f, 0.8f,1);
    [SerializeField] Image _imageToResize;

    [SerializeField] Ease.Type _easeType = Ease.Type.OutBack;
    [SerializeField] Ease.Power _easePower = Ease.Power.Quart;
    Ease.Function _easeFunction;



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
        //So that it only creates the listener once, when the component is dragged on, not when the scene is loaded.
        if(_pointerEnterEvent == null)
        {
            _pointerEnterEvent = new UnityEvent ();
            UnityEventTools.AddVoidPersistentListener(_pointerEnterEvent, EnterScaleAnimation);
            UnityEventTools.AddVoidPersistentListener(_pointerEnterEvent, UpTintAnimation);
            UnityEventTools.AddIntPersistentListener(_pointerEnterEvent, PlaySound, 0);
        }
        if(_pointerExitEvent == null)
        {
            _pointerExitEvent = new UnityEvent ();
            UnityEventTools.AddVoidPersistentListener(_pointerExitEvent, ExitScaleAnimation);
        }
        if(_pointerDownEvent == null)
        {
            _pointerDownEvent = new UnityEvent ();
            UnityEventTools.AddVoidPersistentListener(_pointerDownEvent, DownScaleAnimation);
            UnityEventTools.AddVoidPersistentListener(_pointerDownEvent, DownTintAnimation);
        }
        if(_pointerUpEvent == null)
        {
            _pointerUpEvent = new UnityEvent ();
            UnityEventTools.AddVoidPersistentListener(_pointerUpEvent, UpScaleAnimation);
            UnityEventTools.AddVoidPersistentListener(_pointerUpEvent, UpTintAnimation);
        }
        if(_pointerClickEvent == null)
        {
            _pointerClickEvent = new UnityEvent ();
            UnityEventTools.AddIntPersistentListener(_pointerClickEvent, PlaySound, 0);
        }
        if(_imageToResize == null)
        {
            _imageToResize = GetComponent<Image>();
            _upColor = _imageToResize.color;
        }
    }
    void OnValidate() => _easeFunction = Ease.GetEase(_easeType, _easePower);
#endif

    void Start() => _easeFunction = Ease.GetEase(_easeType, _easePower);
    public void PlaySound(AudioClip audioClip)
    {
        Singleton.Instance.Audio.PlaySound(audioClip);
    }
    public void PlaySound(int index)
    {
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
        if(_currentCursorState == CursorState.Inside)_pointerEnterEvent.Invoke();
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

    public void DownTintAnimation(){StartCoroutine(TweenTint(_imageToResize, _downColor));}
    public void UpTintAnimation(){StartCoroutine(TweenTint(_imageToResize, _upColor));}

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
            img.color = Color.LerpUnclamped(startColor, endColor, _easeFunction(t));
            t += Time.unscaledDeltaTime / _duration;
            yield return null;
        }
        if(requirement == _keyTint)img.color = endColor;//if the key didn't change then get into endColor
    }

}
