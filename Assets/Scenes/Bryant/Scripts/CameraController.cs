using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {
    [Tooltip("Scaling factor for X camera shift from mouse")]
    [SerializeField] [Range(0f, 10f)] float maxOffsetX = 1f;
    [Tooltip("Scaling factor for Y camera shift from mouse")]
    [SerializeField] [Range(0f, 10f)] float maxOffsetY = 0.5f;

    [Tooltip("Dead zone width as a percentage of screen width")]
    [SerializeField] [Range(0f, 1f)] float deadZoneWidthPercentage = 0.75f;
    [Tooltip("Dead zone height as a percentage of screen height")]
    [SerializeField] [Range(0f, 1f)] float deadZoneHeightPercentage = 0.75f;

    [SerializeField] [Range(0f, 10f)] float smoothSpeed = 5f;

    float halfScreenWidth;
    float halfScreenHeight;
    float deadZoneHalfWidth;
    float deadZoneHalfHeight;
    Vector3 targetPosition;

    void Awake() {
        halfScreenWidth = Screen.width / 2f;
        halfScreenHeight = Screen.height / 2f;

        deadZoneHalfWidth = halfScreenWidth * deadZoneWidthPercentage;
        deadZoneHalfHeight = halfScreenHeight * deadZoneHeightPercentage;
        
        targetPosition = transform.position;
    }

    void Update() { CameraShift(); }

    // Shift camera based on mouse position
    void CameraShift() {
        Vector2 mousePos = Mouse.current.position.ReadValue();

        Vector2 offsetFromCenter = new Vector2(
            (mousePos.x - halfScreenWidth) / halfScreenWidth,
            (mousePos.y - halfScreenHeight) / halfScreenHeight
        );
        
        float curOffsetX = Mathf.Clamp(offsetFromCenter.x * maxOffsetX, -maxOffsetX, maxOffsetX);
        float curOffsetY = Mathf.Clamp(offsetFromCenter.y * maxOffsetY, -maxOffsetY, maxOffsetY);

        // Mouse is in deadzone
        if (Mathf.Abs(mousePos.x - halfScreenWidth) <= deadZoneHalfWidth && Mathf.Abs(mousePos.y - halfScreenHeight) <= deadZoneHalfHeight) {
            targetPosition = new Vector3(0, 0, transform.position.z);
        } else {
            targetPosition = new Vector3(curOffsetX, curOffsetY, transform.position.z);
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
