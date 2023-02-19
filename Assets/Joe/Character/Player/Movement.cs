using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody _RigidBody;

    // Start is called before the first frame update
    void Start()
    {
        _RigidBody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        //Gets input from WSAD
        Vector3 Delta = new Vector3(Input.GetAxis("Horizontal") * 3, 0, Input.GetAxis("Vertical") * 3);

        //Gets speed of player
        float Speed = _RigidBody.velocity.magnitude;

        //Makes sure that the speed is greater than 0
        float DeltaSpeed = Mathf.Max(5.0f - Speed, 0.0f);

        //Applies the speed
        _RigidBody.AddRelativeForce(Delta * DeltaSpeed);
    }
}
