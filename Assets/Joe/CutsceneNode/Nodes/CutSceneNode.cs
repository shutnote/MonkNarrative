using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum CUTSCENENODETYPES { CAMERA, ACTOR };
public class CutSceneNode : MonoBehaviour
{

    [SerializeField] private CutSceneNode _NextNode;
    [SerializeField] private int _Tick;

    [SerializeField] private CUTSCENENODETYPES _Type;

    [SerializeField] private UnityEvent _Calls;
    [SerializeField] private CutSceneTriggers _Trigger;

    [SerializeField] private bool _PauseOnReach;
    [SerializeField] private GameObject _Actor;

    //[SerializeField] private Animation _Animation;
    [SerializeField] private string _AnimationStringToPlay;

    private CutSceneManager _Manager;

    // Start is called before the first frame update
    void Start()
    {
        _Manager = transform.parent.GetComponent<CutSceneManager>();
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
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

    public void PlayAnimation()
    {
        if (_Actor.GetComponent<Animator>())
        {
            _Actor.GetComponent<Animation>().Stop();
            _Actor.GetComponent<Animator>().Play(_AnimationStringToPlay);

        }
    }

    public bool CanContinue()
    {
        if (!_Trigger)
        {
            return true;
        }
        return _Trigger.IsTriggered();

    }

    public bool GetPauseOnReach()
    {
        return _PauseOnReach;
    }

    public GameObject GetActor()
    {
        return _Actor;
    }

    public void SetActor(GameObject Actor)
    {
        _Actor = Actor;
    }

    public string GetAnimationString()
    {
        return _AnimationStringToPlay;
    }
}