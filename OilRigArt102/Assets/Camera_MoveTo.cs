using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Camera_MoveTo : MonoBehaviour
{
    [Range(0.1f,10f)]
    public float turnSpeed = 0.1f;
    public float speed;
    public bool startFollow;
    [Space(10)]
    public bool StartLook;
    public bool ContinuousTrack;
    public bool slowLook;
    [Space(10)]
    public Transform Tracker;
    public Transform Follower;

    private void Start()
    {
        if(StartLook) transform.LookAt(Tracker);
    }

    private void FixedUpdate()
    {
        if(startFollow) transform.position = Vector3.MoveTowards(transform.position, Follower.position, speed*Time.deltaTime);
        
        if(slowLook)
        {
            Vector3 relativePos = Tracker.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.time * (turnSpeed/10000));
        }
        else if(ContinuousTrack) transform.LookAt(Tracker);

    }

}
