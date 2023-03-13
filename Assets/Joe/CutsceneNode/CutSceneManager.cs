using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ACTOR {
    public GameObject _Actor;
    public CutSceneNode _CurrentNode;
    public CutSceneNode _NextNode;
}



public class CutSceneManager : MonoBehaviour
{

    [SerializeField] private Camera _Camera;
    [SerializeField] private CutSceneNode _CurrentNode;
    [SerializeField] private bool _Paused;
    [SerializeField] private GameObject _PlayerPositionAtEnd;
    [SerializeField] private GameObject _Player;

    [SerializeField] private CutSceneNode[] _ActorNodes;
    [SerializeField] private int _EndingTick;
    private List<ACTOR> _Actors;

    private float _CurrentTick;
    private bool _AlreadyTriggered;
    private CutSceneNode _InitNode;
    private bool _ContinueCamera;

    [SerializeField] private bool _IsActive;

    // Start is called before the first frame update
    void Start()
    {
        _CurrentTick = 0;
        _InitNode = _CurrentNode;
        //_CurrentNode = transform.GetChild(0).GetChild(0).GetComponent<CutSceneNode>();
        _IsActive = false;
    }

    public void StartCutScene()
    {
        if (_IsActive) return;
        _CurrentTick = 0;
        if (_CurrentNode)
        {
            _CurrentNode = _InitNode;
            _ContinueCamera = true;
            //_CurrentNode = transform.GetChild(0).GetChild(0).GetComponent<CutSceneNode>();
            _CurrentNode.CallFunctions();
        }
        
        _IsActive = true;
        GameObject.Find("Player").GetComponent<PlayerManager>().ToggleControl(false);
        _Actors = new List<ACTOR> { };
        foreach(CutSceneNode ActorNodeParent in _ActorNodes)
        {
            ACTOR Actor = new ACTOR();
            Actor._Actor = ActorNodeParent.GetActor();
            Actor._CurrentNode = ActorNodeParent;
            Actor._NextNode = ActorNodeParent.GetNextNode();
            _Actors.Add(Actor);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!_IsActive || _Paused || (!_CurrentNode.CanContinue() && !_AlreadyTriggered))
        {
            return;
        }
        if(_CurrentTick > _EndingTick)
        {
            Debug.Log("Ending Cutscene");
            _IsActive = false;
        }
        Vector3 CurrentNode;
        Vector3 TargetNode;
        float NodeProgress;
        Vector3 Progress;
        Quaternion CurrentNodeR;
        Quaternion TargetNodeR;
        Quaternion ProgressR;

        _CurrentTick++;
        Debug.Log(_CurrentTick);
        _AlreadyTriggered = true;
        if (_ContinueCamera)
        {
            if (_CurrentTick >= _CurrentNode.GetNextNode().GetTick())
            {
                _CurrentNode = _CurrentNode.GetNextNode();
                _CurrentNode.CallFunctions();
                _AlreadyTriggered = false;

                if (!_CurrentNode.GetNextNode())
                {
                    _ContinueCamera = false;
                    _Player.GetComponent<PlayerManager>().ToggleControl(true);
                    _Player.transform.position = _PlayerPositionAtEnd.transform.position;
                    _Player.transform.rotation = _PlayerPositionAtEnd.transform.rotation;
                }

                _Paused = _CurrentNode.GetPauseOnReach();

            }

            //Gets the position of the node and next or "destination" node
            CurrentNode = _CurrentNode.transform.position;
            TargetNode = _CurrentNode.GetNextNode().transform.position;

            //Gets the progress the camera has made between ticks
            NodeProgress = (_CurrentNode.GetTick() - _CurrentTick) / (_CurrentNode.GetTick() - _CurrentNode.GetNextNode().GetTick());

            //Sets the position of the camera depending on the progress
            Progress = Vector3.LerpUnclamped(CurrentNode, TargetNode, NodeProgress);

            _Camera.transform.position = Progress;

            //Gets the position of the node and next or "destination" node
            CurrentNodeR = _CurrentNode.transform.rotation;
            TargetNodeR = _CurrentNode.GetNextNode().transform.rotation;

            //Sets the position of the camera depending on the progress
            ProgressR = Quaternion.LerpUnclamped(CurrentNodeR, TargetNodeR, NodeProgress);

            _Camera.transform.rotation = ProgressR;
        }
        
        
        for (int i = 0; i < _Actors.Count; i++)
        {
            ACTOR Actor = _Actors[i];
            if (_CurrentTick >= Actor._NextNode.GetTick())
            {
                if (!Actor._NextNode.GetNextNode())
                {
                    _Actors.Remove(Actor);
                    continue;
                }
                else
                {
                    ACTOR NewActor = new ACTOR();
                    NewActor._CurrentNode = Actor._NextNode;
                    NewActor._NextNode = Actor._NextNode.GetNextNode();
                    NewActor._Actor = Actor._Actor;
                    _Actors[i] = NewActor;
                    Actor = _Actors[i];
                }

            }

            NodeProgress = (Actor._CurrentNode.GetTick() - _CurrentTick) / (Actor._CurrentNode.GetTick() - Actor._CurrentNode.GetNextNode().GetTick());

            CurrentNode = Actor._CurrentNode.transform.position;
            TargetNode = Actor._NextNode.transform.position;
            Progress = Vector3.Lerp(CurrentNode, TargetNode, NodeProgress);
            Actor._Actor.transform.position = Progress;

            CurrentNodeR = Actor._CurrentNode.transform.rotation;
            TargetNodeR = Actor._NextNode.transform.rotation;

            ProgressR = Quaternion.Lerp(CurrentNodeR, TargetNodeR, NodeProgress);

            Actor._Actor.transform.rotation = ProgressR;
            
        }

    }

    public void TogglePause()
    {
        _Paused = !_Paused;
    }

    public void TogglePause(bool Pause)
    {
        _Paused = Pause;
    }
}
