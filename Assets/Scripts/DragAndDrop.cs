using UnityEngine;
using UnityEngine.InputSystem;

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

        // Added to tank successfully
        if (IsInTank(mousePos, out Tank tank) && tank.Add(gameObject)) 
        {
          return;
        }
        
        Destroy(gameObject);
      }
    }
  }

  void OnMouseDown() 
  {
    dragging = true;
    Vector2 mousePos = Mouse.current.position.ReadValue();
    if (IsInTank(mousePos, out Tank tank)) 
    {
      tank.Remove(gameObject);
    }
  } 
  
  bool IsInTank(Vector2 mousePosition, out Tank tank) 
  {
    tank = null;
    
    Ray ray = cam.ScreenPointToRay(mousePosition);
    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100, tankLayer);
    if (hit.collider != null && hit.collider.TryGetComponent(out Tank t)) {
      tank = t;
      return true;
    }
    
    return hit.collider != null;
  }


  private Vector2 CalculateDropPosition(Vector2 mousePosition) => cam.ScreenToWorldPoint(mousePosition);
}
