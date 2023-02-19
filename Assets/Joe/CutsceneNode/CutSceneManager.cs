using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CutSceneManager : MonoBehaviour
{

    [SerializeField] private Camera _Camera;
    [SerializeField] private CutSceneNode _CurrentNode;
    [SerializeField] private bool _Paused;

    [SerializeField] private GameObject[] _ActorNodes;

    private float _CurrentTick;
    private bool _AlreadyTriggered;

    [SerializeField] private bool _IsActive;

    // Start is called before the first frame update
    void Start()
    {
        _CurrentTick = 0;
        _CurrentNode = transform.GetChild(0).GetChild(0).GetComponent<CutSceneNode>();
        _IsActive = false;
    }

    public void StartCutScene()
    {
        _CurrentTick = 0;
        _CurrentNode = transform.GetChild(0).GetChild(0).GetComponent<CutSceneNode>();
        _CurrentNode.CallFunctions();
        _IsActive = true;
        GameObject.Find("Player").GetComponent<PlayerManager>().ToggleControl(false);

        foreach(GameObject ActorNodeParent in _ActorNodes)
        {
            Instantiate(ActorNodeParent.transform.GetChild(0).GetComponent<CutSceneNode>().GetActor(), ActorNodeParent.transform.GetChild(0).transform);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!_IsActive || _Paused || (!_CurrentNode.CanContinue() && !_AlreadyTriggered)) return;
        _CurrentTick++;
        _AlreadyTriggered = true;
        if (_CurrentTick>=_CurrentNode.GetNextNode().GetTick())
        {
            _CurrentNode = _CurrentNode.GetNextNode();
            _CurrentNode.CallFunctions();
            _AlreadyTriggered = false;

            if (!_CurrentNode.GetNextNode())
            {
                _IsActive = false;
                GameObject.Find("Player").GetComponent<PlayerManager>().ToggleControl(false);
                return;
            }

            _Paused = _CurrentNode.GetPauseOnReach();

        }
        
        //Gets the position of the node and next or "destination" node
        Vector3 CurrentNode = _CurrentNode.transform.position;
        Vector3 TargetNode = _CurrentNode.GetNextNode().transform.position;

        //Gets the progress the camera has made between ticks
        float NodeProgress = (_CurrentNode.GetTick() - _CurrentTick) / (_CurrentNode.GetTick() - _CurrentNode.GetNextNode().GetTick());

        //Sets the position of the camera depending on the progress
        Vector3 Progress = Vector3.LerpUnclamped(CurrentNode, TargetNode, NodeProgress);

        _Camera.transform.position = Progress;

        //Gets the position of the node and next or "destination" node
        Quaternion CurrentNodeR = _CurrentNode.transform.rotation;
        Quaternion TargetNodeR = _CurrentNode.GetNextNode().transform.rotation;

        //Sets the position of the camera depending on the progress
        Quaternion ProgressR = Quaternion.LerpUnclamped(CurrentNodeR, TargetNodeR, NodeProgress);

        _Camera.transform.rotation = ProgressR;
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
