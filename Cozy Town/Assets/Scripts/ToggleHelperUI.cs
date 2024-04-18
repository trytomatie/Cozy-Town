using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
[RequireComponent(typeof(Animator))]
public class ToggleHelperUI : MonoBehaviour
{
    public UnityEvent onToggleOn;
    private Animator anim;
    // Use this for initialization
    void Awake()
    {
        Toggle toggle = GetComponent<Toggle>();
        anim = GetComponent<Animator>();
        toggle.onValueChanged.AddListener(OnToggleOn);
    }

    private void OnToggleOn(bool value)
    {
        if(value)
        {
            onToggleOn.Invoke();
        }
        anim.SetBool("IsOn", value);
    }

}
