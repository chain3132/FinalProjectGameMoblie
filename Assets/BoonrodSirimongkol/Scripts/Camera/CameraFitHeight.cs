using UnityEngine;

public class CameraFitHeight : MonoBehaviour
{
    public float designHeight = 1080f;

    void Start()
    {
        float targetAspect = designHeight / Screen.height;
        Camera.main.orthographicSize *= targetAspect;
    }
}
