using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutSceneActorNode : MonoBehaviour
{
    [SerializeField] private CutSceneNode _NextNode;
    [SerializeField] private int _Tick;

    [SerializeField] private UnityEvent _Calls;
    [SerializeField] private bool _PauseOnReach;

    private CutSceneManager _Manager;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
