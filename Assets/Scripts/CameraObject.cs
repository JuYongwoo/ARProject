using UnityEngine;

public class CameraObject : MonoBehaviour
{

    public static GameObject GameARCamera;
    // Start is called before the first frame update
    void Awake()
    {
        GameARCamera = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
