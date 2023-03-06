using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum POTTRIGGERS { BUTTON, ZONE, ONLOADED };

public class CutSceneTriggers : MonoBehaviour
{


    [SerializeField] private POTTRIGGERS _Trigger;

    [SerializeField] private string _ButtonInput;
    [SerializeField] private string _ZoneTag;
    [SerializeField] private int _NumOfTimesCanTrigger;

    [SerializeField] private UnityEvent _TriggerFunctions;

    private bool _IsTriggered;

    public void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        
    }

    public void Awake()
    {
        switch (_Trigger)
        {
            case POTTRIGGERS.ONLOADED:
                Trigger(true);
                break;
        }

    }

    public void Trigger(bool Trigger)
    {
        if (_NumOfTimesCanTrigger > 0)
        {
            _IsTriggered = Trigger;
            if (Trigger)
            {
                _TriggerFunctions.Invoke();
                _NumOfTimesCanTrigger--;
            }
            
            return;
        }
        else
        {
            _IsTriggered = false;
        }
    }

    public bool IsTriggered()
    {
        switch (_Trigger)
        {
            case POTTRIGGERS.BUTTON:
                Trigger(Input.GetButton(_ButtonInput));
                break;
            case POTTRIGGERS.ZONE:
                Trigger(true);
                break;
        }

        return _IsTriggered;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == _ZoneTag)
        {
            switch (_Trigger)
            {
                case POTTRIGGERS.ZONE:
                    Trigger(true);
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
        if (other.tag == _ZoneTag)
        {
            switch (_Trigger)
            {
                case POTTRIGGERS.BUTTON:
                    Trigger(Input.GetButton(_ButtonInput));
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
        if (other.tag == _ZoneTag)
        {
            Trigger(false);
        }
    }
}
