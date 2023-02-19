using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum POTTRIGGERS { BUTTON, ZONE };

public class CutSceneTriggers : MonoBehaviour
{


    [SerializeField] private POTTRIGGERS _Trigger;

    [SerializeField] private string _ButtonInput;

    [SerializeField] private UnityEvent _TriggerFunctions;

    private bool _IsTriggered;

    public bool IsTriggered()
    {
        switch (_Trigger)
        {
            case POTTRIGGERS.BUTTON:
                return Input.GetButton(_ButtonInput);
            case POTTRIGGERS.ZONE:
                return _IsTriggered;
        }

        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _TriggerFunctions.Invoke();
            _IsTriggered = true;
        }
        else
        {
            _IsTriggered = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _IsTriggered = false;
        }
    }
}
