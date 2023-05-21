using UnityEngine;

public class Floater : MonoBehaviour
{
    [SerializeField] private WavesManager _wavesManager;

    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _depthBeforeSubmerge = 1f;
    [SerializeField] private float _displacementAmount = 3f;
    [SerializeField] private int _floaterCount = 1;
    [SerializeField] private float _waterDrag = .99f;
    [SerializeField] private float _waterAngularDrag = .5f;

    private void FixedUpdate()
    {
        // var y = _wavesManager.GetWaveHeight(transform.position);
        // transform.position = new Vector3(transform.position.x, y, transform.position.z);

        //transform.position = _wavesManager.GetWaveHeight(transform.position);

        _rb.AddForceAtPosition(Physics.gravity / _floaterCount, transform.position, ForceMode.Acceleration);

        float waveHeight = _wavesManager.GetWaveHeight(transform.position);

        if (transform.position.y < waveHeight)
        {
            float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / _depthBeforeSubmerge) * _displacementAmount;
            _rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
            _rb.AddForce(displacementMultiplier * -_rb.velocity * _waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            _rb.AddTorque(displacementMultiplier * -_rb.angularVelocity * _waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
