using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPushed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPush()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 10000));
    }
}
