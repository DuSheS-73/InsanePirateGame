using UnityEngine;

public class WavesManager : MonoBehaviour
{
    [SerializeField] private Vector4 _waveA = new Vector4(1f, 1f, .25f, 60f);
    [SerializeField] private Vector4 _waveB = new Vector4(1f, .6f, .25f, 31f);
    [SerializeField] private Vector4 _waveC = new Vector4(1f, 1.3f, .25f, 18f);

    [SerializeField] private Transform _wavesHolder; // TEMP name

    private Material _wavesMaterial;
    /*
float3 GerstnerWave (
            float4 wave, float3 p, inout float3 tangent, inout float3 binormal
        ) {
            float steepness = wave.z;
            float wavelength = wave.w;
            float k = 2 * UNITY_PI / wavelength;
            float c = sqrt(9.8 / k);
            float2 d = normalize(wave.xy);
            float f = k * (dot(d, p.xz) - c * _Time.y);
            float a = steepness / k;

            tangent += float3(
                -d.x * d.x * (steepness * sin(f)),
                d.x * (steepness * cos(f)),
                -d.x * d.y * (steepness * sin(f))
            );
            binormal += float3(
                -d.x * d.y * (steepness * sin(f)),
                d.y * (steepness * cos(f)),
                -d.y * d.y * (steepness * sin(f))
            );
            return float3(
                d.x * (a * cos(f)),
                a * sin(f),
                d.y * (a * cos(f))
            );
        }

        void vert (inout appdata_full vertexData)
        {
            float3 gridPoint = vertexData.vertex.xyz;
            float3 tangent = float3(1, 0, 0);
            float3 binormal = float3(0, 0, 1);
            float3 p = gridPoint;
            p += GerstnerWave(_WaveA, gridPoint, tangent, binormal);
            p += GerstnerWave(_WaveB, gridPoint, tangent, binormal);
            p += GerstnerWave(_WaveC, gridPoint, tangent, binormal);
            float3 normal = normalize(cross(binormal, tangent));
            vertexData.vertex.xyz = p;
            vertexData.normal = normal;
        }
    */

    private void Start()
    {
        SetVariables();
    }

    private void SetVariables()
    {
        _wavesMaterial = _wavesHolder.GetComponent<Renderer>().sharedMaterial;
    }

    public float GetWaveHeight(Vector3 point)
    {
        // Debug.Log(transform.InverseTransformPoint(point));
        // var p = transform.InverseTransformPoint(point);

        float y = point.y;
        y += GerstnerWave(_waveA, point);
        y += GerstnerWave(_waveB, point);
        y += GerstnerWave(_waveC, point);

        return y;
    }

    private float GerstnerWave(Vector4 wave, Vector3 point)
    {
        float steepness = wave.z;
        float waveLength = wave.w;
        float k = 2 * Mathf.PI / waveLength;
        float c = Mathf.Sqrt(9.8f / k);
        Vector2 d = new Vector2(wave.x, wave.y).normalized;
        float f = k * (Vector2.Dot(d, new Vector2(point.x, point.z)) - c * Time.time);
        float a = steepness / k;

        // return new Vector3(
        //     d.x * (a * Mathf.Cos(f)), 
        //     a * Mathf.Sin(f),
        //     d.y * (a * Mathf.Cos(f))
        // );

        return a * Mathf.Sin(f);
    }

    private void OnValidate()
    {
        if (!_wavesMaterial)
            SetVariables();

        UpdateMaterial();
    }

    private void UpdateMaterial()
    {
        _wavesMaterial.SetVector("_WaveA", _waveA);
        _wavesMaterial.SetVector("_WaveB", _waveB);
        _wavesMaterial.SetVector("_WaveC", _waveC);
    }
}
