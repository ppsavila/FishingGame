using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    
    public Light EnviromentLight;
    public Gradient Gradient;

    public Transform LightTransform;

    private void FixedUpdate()
    {
        float t = Mathf.PingPong(Time.deltaTime, 10f) / 10f;
        EnviromentLight.color = Gradient.Evaluate(Mathf.Lerp(0f,1f, t));
        LightTransform.Rotate(Mathf.Lerp(0f,180f, t), 0.0f, 0.0f, Space.World);
    }
}
