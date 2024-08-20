using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class DragAndDrop : MonoBehaviour {
  public bool dragging = true;
  [SerializeField] LayerMask tankLayer;
  
  private Camera cam;

  private SFXItems AudioPlayer;
    private bool _isInTank = false;
    private bool oldIsInTank;
    private FMOD.Studio.EventInstance waterSFX;
    private Vector2 oldMousePos;

    void Start()
    {
        oldIsInTank = _isInTank;
        AudioPlayer = GetComponent<SFXItems>();
        AudioPlayer.SFXPlaySelect();
        cam = Camera.main;
        //waterSFX.setParameterByName("Speed", 1);
        oldMousePos = Mouse.current.position.ReadValue();
    }

  void Update() {
    if (dragging) {
      Mouse mouse = Mouse.current;
      Vector2 mousePos = mouse.position.ReadValue();
      transform.position = CalculateDropPosition(mousePos);

      _isInTank = IsInTank(mousePos, out Tank tank);
      if(oldIsInTank != _isInTank)
            if(_isInTank)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Water_Enter");
                waterSFX = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/WaterMovement");
                waterSFX.start();
                oldIsInTank = _isInTank;
            }
            else
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Water_Exit");
                oldIsInTank = _isInTank;
                waterSFX.release();
            }

      waterSFX.setParameterByName("Speed", ((GetMouseSpeed(out float speed)/10000) + 0.05f));

      if (mouse.leftButton.wasReleasedThisFrame) {
        dragging = false;
        waterSFX.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        waterSFX.release();

        // Added to tank successfully
        if (_isInTank && tank.Add(gameObject)) 
        {
            AudioPlayer.SFXPlayPlace();
            return;
        }
        AudioPlayer.SFXPlayReturn();
        Destroy(gameObject);
      }
    }
  }

  void OnMouseDown() 
  {
    AudioPlayer.SFXPlaySelect();
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

    float GetMouseSpeed(out float speed)
    {
        Vector2 currentMousePos = Mouse.current.position.ReadValue();
        speed = Vector2.Distance(currentMousePos, oldMousePos) / Time.deltaTime;
        oldMousePos = currentMousePos;
        return speed;
    }
}


