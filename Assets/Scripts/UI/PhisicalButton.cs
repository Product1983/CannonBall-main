using System.Collections;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Animator))]
public class PhisicalButton : MonoBehaviour
{
    public UnityEvent _onClickEvent;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnMouseEnter()
    {
        _animator.SetBool("IsMouseover", true);
    }
    public void OnMouseDown()
    {
        _onClickEvent.Invoke();
    }
    public void OnMouseExit()
    {
        _animator.SetBool("IsMouseOver", false);
    }
   
}
