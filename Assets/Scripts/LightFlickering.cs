using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightFlickering : MonoBehaviour
{
    public Light2D light;
    public float minIntensity = 0f;
    public float maxIntensity = 1f;
    public float minSpeed = 0.1f;
    public float maxSpeed = 0.5f;
    public int smoothing = 5;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light2D>();
        StartCoroutine(run());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator run()
    {
        while (true)
        {
            light.enabled = true;
            light.intensity = Random.Range(minIntensity, maxIntensity);
            yield return new WaitForSeconds(Random.Range(minSpeed, maxSpeed));
            light.enabled = false;
            yield return new WaitForSeconds(Random.Range(minSpeed, maxSpeed));

        }
    }
}
