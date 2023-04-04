using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] private Camera _Camera;
    [SerializeField] private bool _InControl;
    [SerializeField] private MagicManager _MagicManager;

    private GameObject FirstPerson;
    private GameObject ThirdPerson;



    // Start is called before the first frame update
    void Start()
    {
        FirstPerson = transform.GetChild(0).gameObject;
        _MagicManager = GetComponent<MagicManager>();
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Awake()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!_InControl) {
            return;
        }

        if (Input.GetButton("SpellMenu"))
        {
            _MagicManager.MagicUISelection();
            return;
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
        //Perspective Stuff
        _Camera.transform.position = FirstPerson.transform.GetChild(0).position;
        _Camera.transform.rotation = Quaternion.Euler(_Camera.transform.rotation.eulerAngles + new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0));
        transform.rotation = Quaternion.Euler(new Vector3(0, _Camera.transform.rotation.eulerAngles.y, 0));
        FirstPerson.transform.rotation = _Camera.transform.rotation;

        //Looking At
        if (Input.GetButtonDown("Interact"))
        {
            RaycastHit Hit;
            if (Physics.Raycast(transform.position, _Camera.transform.forward, out Hit, 1.0f))
            {
                if (Hit.collider.gameObject.tag == "Interactable")
                {
                    Hit.collider.gameObject.GetComponent<DoorManager>().Toggle();
                }
            }
        }

        _MagicManager.ToggleActive(Input.GetButton("Fire1") || Input.GetButtonUp("Fire1"));

    }

    public void ToggleControl(bool Control)
    {
        _InControl = Control;
        transform.GetChild(0).gameObject.SetActive(Control);
    }

    public void ResetVelocity()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }
}
