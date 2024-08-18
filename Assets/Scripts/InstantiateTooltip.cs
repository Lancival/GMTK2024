using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UIOnHover))]
public class InstantiateTooltip : MonoBehaviour {
  [SerializeField] private GameObject tooltipPrefab;
  private UIOnHover onHover;
  private GameObject tooltip;

  void Start() {
    onHover = GetComponent<UIOnHover>();
    onHover.startHover.AddListener(Instantiate);
    onHover.stopHover.AddListener(Destroy);
  }

  void Instantiate(GameObject invoker) {
    tooltip = GameObject.Instantiate(tooltipPrefab, invoker.transform);
    // TODO: Update tooltip text
  }

  void Destroy(GameObject invoker) {
    if (tooltip) {
      GameObject.Destroy(tooltip);
    }
    tooltip = null;
  }
}
