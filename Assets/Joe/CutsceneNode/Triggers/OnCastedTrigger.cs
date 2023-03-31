using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCastedTrigger : MonoBehaviour
{

    [SerializeField] private UnityEvent _TriggerFunctions;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCasted()
    {
        Debug.Log("I've been casteded");
        _TriggerFunctions.Invoke();
    }
}
