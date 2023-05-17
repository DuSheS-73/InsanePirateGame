using UnityEngine;

public class FpsLimitter : MonoBehaviour
{
    [SerializeField] private int targetFrameRate = 30;
 
    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFrameRate;
    }
}
