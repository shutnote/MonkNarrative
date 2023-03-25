using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaypointPrompt : MonoBehaviour
{
    public Transform waypoint; 
    public float distanceThreshold = 5f; 
    public Text promptText; 

    private bool promptActive = false;

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, waypoint.position);

        if (distance > distanceThreshold && !promptActive)
        {
            promptText.gameObject.SetActive(true);
            promptActive = true;
        }
        else if (distance <= distanceThreshold && promptActive)
        {
            promptText.gameObject.SetActive(false);
            promptActive = false;
        }
    }
}
