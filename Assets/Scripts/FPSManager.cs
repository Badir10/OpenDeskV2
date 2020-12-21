using UnityEngine;
using UnityEngine.UI;

public class FPSManager : MonoBehaviour
{
    private string fps;

    void Update()
    {
        fps = "FPS: " + (int) (1.0 / Time.smoothDeltaTime);
        DebugOculus.Instance.Log(fps);
    }
}
