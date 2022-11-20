using UnityEngine;

public class SwerveInputSystem : MonoBehaviour
{
    private float _lastFrameFingerPositionX;
    private float _moveFactoryX;

    public float MoveFactoryX { get => _moveFactoryX; }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(button: 0))
        {
            _lastFrameFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(button: 0))
        {
            Vector3 currentMousePosition = Input.mousePosition;

            _moveFactoryX = currentMousePosition.x - _lastFrameFingerPositionX;

            _lastFrameFingerPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(button: 0))
        {
            _moveFactoryX = 0f;
        }
    }
}
