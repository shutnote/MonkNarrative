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
    [SerializeField] private float transitionWeight;
    [SerializeField] bool blendAnim;

    private CutSceneManager _Manager;

    private float animSpeedBeforePause;


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
            if (_AnimationStringToPlay == "") return;

            if(blendAnim)
            {
                _Actor.GetComponent<Animator>().CrossFade(_AnimationStringToPlay, transitionWeight==0? 0.2f:transitionWeight, 0);
                
            }
            else
            {
                _Actor.GetComponent<Animator>().StopPlayback();
                _Actor.GetComponent<Animator>().Play(_AnimationStringToPlay);
            }

        }
    }

    public void PauseAnimation()
    {
        _Actor.GetComponent<Animator>().speed = 0;
    }
    
    public void ResumeAnimation()
    {
        _Actor.GetComponent<Animator>().speed = 1;
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