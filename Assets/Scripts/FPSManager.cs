using UnityEngine;
using UnityEngine.UI;

public class FPSManager : MonoBehaviour
{
    ///// Zeigt die FPS in der Szene an, damit wir die Performance besser optimieren koennen /////
    ///
    
    private string fps;

    void Update()
    {
        // hier wird die FPS berechnet und in der fps-Variable gespeichert
        fps = "FPS: " + (int) (1.0 / Time.smoothDeltaTime);
        DebugOculus.Instance.Log(fps);
    }
}
