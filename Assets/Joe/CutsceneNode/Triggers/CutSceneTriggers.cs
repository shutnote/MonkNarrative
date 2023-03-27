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

    [SerializeField] private bool _OnTimer;
    [SerializeField] private float _Countdown;

    private float _StartTimer;

    private bool _IsTriggered;

    public void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        _StartTimer = -1;
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

    public void FixedUpdate()
    {
        if(_OnTimer && _StartTimer > 0)
        {
            Debug.Log(_StartTimer);
            _StartTimer -= Time.deltaTime;
            if (_StartTimer < 0)
            {
                _StartTimer = -1;
                _IsTriggered = true;
                _TriggerFunctions.Invoke();
                _NumOfTimesCanTrigger--;
            }
        }
    }

    public void Trigger(bool Trigger)
    {
        if (_NumOfTimesCanTrigger > 0)
        {
            
            if (Trigger)
            {
                if (_OnTimer)
                {
                    Debug.Log(this.name + ": Started Countdown");
                    _StartTimer = _Countdown;
                    return;
                }
                else
                {
                    _IsTriggered = Trigger;
                    _TriggerFunctions.Invoke();
                    _NumOfTimesCanTrigger--;
                }

                
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
