using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera main_cam;

    private void Start()
    {
        main_cam = Camera.main;
    }

    private void Update()
    {
        transform.LookAt(main_cam.transform);
    }
}
