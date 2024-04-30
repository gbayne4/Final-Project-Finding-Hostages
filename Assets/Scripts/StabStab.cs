using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StabStab : MonoBehaviour
{
    private float rotateKnife = 10;
    private float maxRotation = 80;
    private bool rotate = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.F))
        {
            rotate = true;
        }

        if (rotate) //roating the knife, but only to its max
        {
            rotateKnife += 1;

            if (rotateKnife > maxRotation) rotate = false;
        }

        if (!rotate) //when not rotating, it rotates back
        {
            rotateKnife -= 1;

            if (rotateKnife <= 10) rotateKnife = 10;
        }

        //updating rotations
        gameObject.transform.eulerAngles = new Vector3(
        rotateKnife,
        gameObject.transform.eulerAngles.y,
        gameObject.transform.eulerAngles.z
        );
    }
}
