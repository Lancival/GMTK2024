using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class DragAndDrop : MonoBehaviour {
  public bool dragging = true;
  [SerializeField] LayerMask tankLayer;
  
  private Camera cam;

  private SFXItems AudioPlayer;

  void Start()
   {
    cam = Camera.main;
    AudioPlayer = GetComponent<SFXItems>();
   }

  void Update() {
    if (dragging) {
      Mouse mouse = Mouse.current;
      Vector2 mousePos = mouse.position.ReadValue();
      transform.position = CalculateDropPosition(mousePos);
      if (mouse.leftButton.wasReleasedThisFrame) {
        dragging = false;
        AudioPlayer.SFXPlayPlace();
        
        if (!IsInTank(mousePos)) 
        {
          AudioPlayer.SFXPlayReturn();
          Destroy(gameObject);
        }
      }
    }
  }

  void OnMouseDown()
  {
    dragging = true;
    AudioPlayer.SFXPlaySelect();
  }

  bool IsInTank(Vector2 mousePosition)
  {
    Ray ray = cam.ScreenPointToRay(mousePosition);
    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100, tankLayer);
    return hit.collider != null;
  }


  private Vector2 CalculateDropPosition(Vector2 mousePosition) => cam.ScreenToWorldPoint(mousePosition);
}
