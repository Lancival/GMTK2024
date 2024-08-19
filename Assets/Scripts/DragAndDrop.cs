using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class DragAndDrop : MonoBehaviour {
  public bool dragging = true;
  [SerializeField] LayerMask tankLayer;
  
  private Camera cam;

  void Start() => cam = Camera.main;

  void Update() {
    if (dragging) {
      Mouse mouse = Mouse.current;
      Vector2 mousePos = mouse.position.ReadValue();
      transform.position = CalculateDropPosition(mousePos);
      if (mouse.leftButton.wasReleasedThisFrame) {
        dragging = false;
        
        if (!IsInTank(mousePos)) 
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
    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100, tankLayer);
    return hit.collider != null;
  }


  private Vector2 CalculateDropPosition(Vector2 mousePosition) => cam.ScreenToWorldPoint(mousePosition);
}
