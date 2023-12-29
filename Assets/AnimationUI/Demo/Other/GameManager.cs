using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    void OnEnable()
    {
        AnimationUI.OnSetActiveAllInput += SetActiveAllInput;
    }
    void OnDisable()
    {
        AnimationUI.OnSetActiveAllInput -= SetActiveAllInput;
    }
    public void SetActiveAllInput(bool isActive)
    {
        transform.GetChild(0).gameObject.SetActive(!isActive);
    }
}
