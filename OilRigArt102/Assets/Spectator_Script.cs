using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectator_Script : MonoBehaviour
{
    public float speed;
    public float sprint = 1.5f;


    private void Start()
    {
        sprint *= speed;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("UpDown"), Input.GetAxisRaw("Vertical"));
        


        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ) transform.Translate(movement.normalized * sprint * Time.deltaTime);
        transform.Translate(movement.normalized * speed * Time.deltaTime);

    }

}
