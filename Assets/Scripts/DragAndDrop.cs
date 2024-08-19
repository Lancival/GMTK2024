using UnityEngine;
using UnityEngine.InputSystem;

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
        
        if (!IsInTank(Mouse.current.position.ReadValue())) 
        {
          Destroy(gameObject);
        }
      }
    }
  }

  void OnMouseDown() => dragging = true;
  
  bool IsInTank(Vector2 mousePosition)
  {
    Ray ray = cam.ScreenPointToRay(mousePosition);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, 100))
    {
      if (hit.collider.CompareTag("Tank")) 
      {
        return true;
      }
    }

    return false;
  }

  private Vector2 CalculateDropPosition(Vector2 mousePosition) => cam.ScreenToWorldPoint(mousePosition);
}
