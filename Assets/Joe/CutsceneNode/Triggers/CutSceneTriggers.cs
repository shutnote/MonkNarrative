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

    public void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

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
            switch (_Trigger)
            {
                case POTTRIGGERS.ZONE:
                    _TriggerFunctions.Invoke();
                    _IsTriggered = true;
                    break;
            }
            
        }
        else
        {
            _IsTriggered = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (_Trigger)
            {
                case POTTRIGGERS.BUTTON:
                    if (Input.GetButton(_ButtonInput))
                    {
                        Debug.Log("Hi tere");
                        _TriggerFunctions.Invoke();
                        _IsTriggered = true;
                    }
                    break;
            }
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
