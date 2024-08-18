using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class UIOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
  [Tooltip("Time cursor must hover before tooltip will appear")]
  [Range(0f, 1f)]
  [SerializeField] private float hoverTime = 0.5f;

  public UnityEvent<GameObject> startHover;
  public UnityEvent<GameObject> stopHover;

  private Coroutine detectHover;
  private float enterTime = 0f;

  public void OnPointerEnter(PointerEventData eventData) {
    enterTime = Time.time;
    detectHover = StartCoroutine(DetectHover());
  }

  public void OnPointerExit(PointerEventData eventData) {
    if (detectHover != null) {
      StopCoroutine(detectHover);
    }
    detectHover = null;
    stopHover.Invoke(gameObject);
  }

  private IEnumerator DetectHover() {
    while (Time.time - enterTime < hoverTime) {
      yield return null;
    }
    startHover.Invoke(gameObject);
  }
}
