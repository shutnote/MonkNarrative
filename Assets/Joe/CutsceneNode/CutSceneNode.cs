using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutSceneNode : MonoBehaviour
{

    [SerializeField] private CutSceneNode _NextNode;
    [SerializeField] private int _Tick;

    [SerializeField] private UnityEvent _Calls;
    [SerializeField] private bool _PauseOnReach;

    private CutSceneManager _Manager;

    // Start is called before the first frame update
    void Start()
    {
        _Manager = transform.parent.GetComponent<CutSceneManager>();
    }

    public int GetTick()
    {
        return _Tick;
    }

    public CutSceneNode GetNextNode()
    {
        return _NextNode;
    }

    public void CallFunctions()
    {
        _Calls.Invoke();
    }

    public bool GetPauseOnReach()
    {
        return _PauseOnReach;
    }
}
