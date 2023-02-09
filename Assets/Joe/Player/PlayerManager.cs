using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] private bool _FirstPerson;
    [SerializeField] private Camera _Camera;

    private GameObject FirstPerson;
    private GameObject ThirdPerson;

    // Start is called before the first frame update
    void Start()
    {
        FirstPerson = transform.GetChild(0).gameObject;
        ThirdPerson = transform.GetChild(1).gameObject;
        FirstPerson.SetActive(true);
        ThirdPerson.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Perspective Stuff
        switch (_FirstPerson)
        {
            case true:
                Debug.Log("HJEPFJW");
                _Camera.transform.position = FirstPerson.transform.GetChild(0).position;
                _Camera.transform.rotation = Quaternion.Euler(_Camera.transform.rotation.eulerAngles + new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0));
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, Input.GetAxis("Mouse X"), 0));
                FirstPerson.transform.rotation = _Camera.transform.rotation;
                break;
            case false:
                _Camera.transform.position = ThirdPerson.transform.GetChild(0).position;
                _Camera.transform.rotation = transform.rotation;
                break;
        }

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
        
    }
}
