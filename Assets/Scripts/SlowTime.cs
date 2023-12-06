using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTime : MonoBehaviour
{
    float playerSpeed = 0;
    bool startSlowMo = false;
    float originTimeScale = 0;
    private void Start()
    {
        playerSpeed = GetComponent<PlayerBasic>().speed;
        originTimeScale = Time.fixedDeltaTime;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            
            Time.timeScale = 0.3f;
            Time.fixedDeltaTime = Time.timeScale*0.01f;
            //if (!startSlowMo )
            //{
            //    GetComponent<PlayerBasic>().speed *= 0.01f;
            //    startSlowMo = true;
            //}

        }

        if (Input.GetMouseButtonUp(1))
        {
            Time.timeScale = 1f;
            // Time.fixedDeltaTime = originTimeScale;
            //GetComponent<PlayerBasic>().speed = playerSpeed;
            startSlowMo = false;
        }
    }
}
