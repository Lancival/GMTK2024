using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour {
    [Tooltip("Scaling factor for X camera shift from mouse")]
    [SerializeField] [Range(0f, 10f)] float maxShiftOffsetX = 1f;
    [Tooltip("Scaling factor for Y camera shift from mouse")]
    [SerializeField] [Range(0f, 10f)] float maxShiftOffsetY = 0.5f;
    [SerializeField] float smoothSpeed = 5f;
    
    [Tooltip("Min X camera position")]
    [SerializeField] float minPosOffsetX = -5f;
    [Tooltip("Max X camera position")]
    [SerializeField] float maxPosOffsetX = 5f;
    [SerializeField] float scrollSpeed = 5f;

    [Tooltip("Dead zone width as a percentage of screen width")]
    [SerializeField] [Range(0f, 1f)] float deadZoneWidthPercentage = 0.75f;
    [Tooltip("Dead zone height as a percentage of screen height")]
    [SerializeField] [Range(0f, 1f)] float deadZoneHeightPercentage = 0.75f;

    float halfScreenWidth;
    float halfScreenHeight;
    float deadZoneHalfWidth;
    float deadZoneHalfHeight;

    Transform cameraBase;

    void Awake() {
        halfScreenWidth = Screen.width / 2f;
        halfScreenHeight = Screen.height / 2f;

        deadZoneHalfWidth = halfScreenWidth * deadZoneWidthPercentage;
        deadZoneHalfHeight = halfScreenHeight * deadZoneHeightPercentage;

        cameraBase = transform.parent;
        scrollTargetPosition = cameraBase.transform.position;
    }

    void Update() {
        CameraShift();
        SideScroll();
    }

    // Shift camera based on mouse position
    void CameraShift() {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 targetPosition;

        Vector2 offsetFromCenter = new Vector2(
            (mousePos.x - halfScreenWidth) / halfScreenWidth,
            (mousePos.y - halfScreenHeight) / halfScreenHeight
        );
        
        float curOffsetX = Mathf.Clamp(offsetFromCenter.x * maxShiftOffsetX, -maxShiftOffsetX, maxShiftOffsetX);
        float curOffsetY = Mathf.Clamp(offsetFromCenter.y * maxShiftOffsetY, -maxShiftOffsetY, maxShiftOffsetY);

        // Mouse is in deadzone
        if (Mathf.Abs(mousePos.x - halfScreenWidth) <= deadZoneHalfWidth && Mathf.Abs(mousePos.y - halfScreenHeight) <= deadZoneHalfHeight) {
            targetPosition = new Vector3(0, 0, transform.localPosition.z);
        } else {
            targetPosition = new Vector3(curOffsetX, curOffsetY, transform.localPosition.z);
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, smoothSpeed * Time.deltaTime);
    }

    Vector3 scrollTargetPosition;
    void SideScroll() {
        Vector2 mousePos = Mouse.current.position.ReadValue();

        if (mousePos.x <= 15) { // left
            scrollTargetPosition += new Vector3(-scrollSpeed * Time.deltaTime, 0, 0);
        }
        else if (mousePos.x >= Screen.width - 15) { // right
            scrollTargetPosition += new Vector3(scrollSpeed * Time.deltaTime, 0, 0);
        }

        scrollTargetPosition.x = Mathf.Clamp(scrollTargetPosition.x, minPosOffsetX, maxPosOffsetX);

        cameraBase.transform.position = Vector3.Lerp(cameraBase.transform.position, scrollTargetPosition, smoothSpeed * Time.deltaTime);
    }
}
