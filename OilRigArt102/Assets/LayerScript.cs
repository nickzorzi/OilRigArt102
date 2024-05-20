using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerScript : MonoBehaviour
{

    public GameObject[] hideThis;

    private KeyCode[] InputCodes =
    {
        KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4,
    };


    private void Update()
    {
        for (int i = 0; i < InputCodes.Length; i++)
        {
            if (Input.GetKeyUp(InputCodes[i])) hideThis[i].SetActive(!hideThis[i].activeSelf);
        }
    }

}
