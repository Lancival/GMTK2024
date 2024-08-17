using UnityEngine;
using UnityEngine.EventSystems;

public class UITooltipOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
  [Tooltip("Time cursor must hover before tooltip will appear")]
  [Range(0f, 1f)]
  [SerializeField] private float hoverTime = 0.5f;

  [Tooltip("Tooltip prefab")]
  [SerializeField] private GameObject tooltipPrefab;

  private bool hovering = false;
  private float enterTime = 0f;
  private GameObject tooltip;

  public void OnPointerEnter(PointerEventData eventData) {
    hovering = true;
    enterTime = Time.time;
  }

  public void OnPointerExit(PointerEventData eventData) {
    hovering = false;
    Destroy(tooltip);
    tooltip = null;
  }

  void Update() {
    if (hovering && !tooltip && Time.time - enterTime > hoverTime) {
      tooltip = Instantiate(tooltipPrefab, transform);
      // TODO: Update tooltip text
    }
  }

}
