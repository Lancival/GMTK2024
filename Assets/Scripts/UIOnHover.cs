using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
  [Tooltip("Time cursor must hover before tooltip will appear")]
  [Range(0f, 1f)]
  [SerializeField] private float hoverTime = 0.5f;

  public UnityEvent<GameObject> startHover;
  public UnityEvent<GameObject> stopHover;

  private bool hovering = false;
  private float enterTime = 0f;
  private bool invoked = false;

  public void OnPointerEnter(PointerEventData eventData) {
    hovering = true;
    enterTime = Time.time;
  }

  public void OnPointerExit(PointerEventData eventData) {
    hovering = false;
    stopHover.Invoke(gameObject);
    invoked = false;
  }

  void Update() {
    if (hovering && !invoked && Time.time - enterTime > hoverTime) {
      startHover.Invoke(gameObject);
      invoked = true;
    }
  }

}
