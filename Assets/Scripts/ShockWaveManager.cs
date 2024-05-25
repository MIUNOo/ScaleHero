using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Logger;

public class ShockWaveManager : MonoBehaviour
{
    public float _shockWaveTime = 0.75f;

    private UniTask _shockWaveTask;

    private Material _material;

    private static int _waveDistanceFromCenter = Shader.PropertyToID("_WaveDistanceFromCentre");


    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            _ = ShockWaveAction(-.1f, 1f);
            //CallShockWave();
        }
    }



    public async UniTask ShockWaveAction(float startPos, float endPos)
    {

        LOG("CLicked");

        _material.SetFloat(_waveDistanceFromCenter, startPos);

        float lerpedAmount = 0f;

        var elapsedTime = 0f;
        while (elapsedTime < _shockWaveTime)
        {
            elapsedTime += Time.deltaTime;

            lerpedAmount = Mathf.Lerp(startPos, endPos, (elapsedTime / _shockWaveTime));
            _material.SetFloat(_waveDistanceFromCenter, lerpedAmount);

            await UniTask.Yield();
        }

        // Ensure the final value is set
        //_material.SetFloat(_waveDistanceFromCenter, endPos);

    }

    //IEnumerator ShockWaveAction(float startPos, float endPos)
    //{

    //    LOG("CLicked");

    //    _material.SetFloat(_waveDistanceFromCenter, startPos);

    //    float lerpedAmount = 0f;

    //    var elapsedTime = 0f;
    //    while (elapsedTime < _shockWaveTime)
    //    {
    //        elapsedTime += Time.deltaTime;

    //        lerpedAmount = Mathf.Lerp(startPos, endPos, (elapsedTime / _shockWaveTime));
    //        _material.SetFloat(_waveDistanceFromCenter, lerpedAmount);

    //        yield return null;
    //    }

    //    // Ensure the final value is set
    //    //_material.SetFloat(_waveDistanceFromCenter, endPos);

    //}



    //public void CallShockWave()
    //{
    //    StartCoroutine(ShockWaveAction(-.1f, 1f));
    //}



}
