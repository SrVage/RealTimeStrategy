using UnityEngine;

namespace Code.ControlSystem
{
    public class CameraMove:MonoBehaviour
    {
        private const float MoveSpeed = 2f;
        private const float MaxBorder = 0.95f;
        private const float MinBorder = 0.05f;
        
        private Camera _camera;
        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.mousePosition.x > MaxBorder * Screen.width)
                _camera.transform.position += new Vector3(MoveSpeed, 0, 0)*Time.deltaTime;
            if (Input.mousePosition.x < MinBorder * Screen.width)
                _camera.transform.position += new Vector3(-MoveSpeed, 0, 0)*Time.deltaTime;
            if (Input.mousePosition.y > MaxBorder * Screen.height)
                _camera.transform.position += new Vector3(0, 0, MoveSpeed)*Time.deltaTime;
            if (Input.mousePosition.y < MinBorder * Screen.height)
                _camera.transform.position += new Vector3(0, 0, -MoveSpeed)*Time.deltaTime;
            
        }
    }
}