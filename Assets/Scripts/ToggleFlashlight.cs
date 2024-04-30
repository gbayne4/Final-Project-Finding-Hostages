using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleFlashlight : MonoBehaviour
{
    public static bool flashOn = true;

    private Light flash;

    private int timer=0;
    // Update is called once per frame
    private void Start()
    {
        flash = GetComponent<Light>();
    }
    void Update()
    {
        timer++;
        if ((Input.GetKey(KeyCode.R)) && flashOn && timer > 100) {flashOn = false; timer = 0; Debug.Log("Flash Off"); }
        if ((Input.GetKey(KeyCode.R)) && !flashOn && timer > 100) { flashOn = true;timer = 0; }

        if (flashOn) { flash.enabled = true; }
        else { flash.enabled = false; }
    }
}
