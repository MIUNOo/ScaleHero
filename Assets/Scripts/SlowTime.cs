using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTime : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Time.timeScale = 0.3f;
            Time.fixedDeltaTime = Time.timeScale*0.01f;
        }

        if (Input.GetMouseButtonUp(1))
        {
            Time.timeScale = 1f;
        }
    }
}
