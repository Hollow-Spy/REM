using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchScreenController : MonoBehaviour
{

    [SerializeField] Material Mat;
    public float noiseAmount;
    public float glitchPower;
    public float scanLinesPower;

    private void OnDisable()
    {
        Mat.SetFloat("_NoiseAmount", 0);
        Mat.SetFloat("_GlitchPower", 0);
        Mat.SetFloat("_ScanLinesPower", 0);
    }

    // Update is called once per frame
    void Update()
    {
        Mat.SetFloat("_NoiseAmount", noiseAmount);
        Mat.SetFloat("_GlitchPower", glitchPower);
        Mat.SetFloat("_ScanLinesPower", scanLinesPower);


    }
}
