using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float dampTime = 1f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    public Camera cam;
    public float minHeight;
    private float minWidth;
    private HordeController horde;

    private void Start()
    {
        horde = FindObjectOfType<HordeController>();
        minWidth = 0.2f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (horde.getActiveFollowers().Count > 3 && horde.getActiveFollowers().Count < 7)
        {
            minWidth = 0.3f;
        }
        else if (horde.getActiveFollowers().Count > 7 && horde.getActiveFollowers().Count < 12)
        {
            minWidth = 0.4f;
        }
        else if (horde.getActiveFollowers().Count > 12)
        {
            minWidth = 0.5f;
        }


        if (target)
        {
            Vector3 point = cam.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - cam.ViewportToWorldPoint(new Vector3(minWidth, 0.4f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);

            if(transform.position.y < minHeight)
            {
                transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
            }
        }

    }
}