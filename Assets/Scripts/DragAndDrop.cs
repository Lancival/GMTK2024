using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class DragAndDrop : MonoBehaviour {
  public bool dragging = true;
  private Camera cam;

  void Start() => cam = Camera.main;

  void Update() {
    if (dragging) {
      Mouse mouse = Mouse.current;
      transform.position = CalculateDropPosition(mouse.position.ReadValue());
      if (mouse.leftButton.wasReleasedThisFrame) {
        dragging = false;
        // Return to inventory if invalid drop position
      }
    }
  }

  void OnMouseDown() => dragging = true;

  private Vector2 CalculateDropPosition(Vector2 mousePosition) => cam.ScreenToWorldPoint(mousePosition);
}
