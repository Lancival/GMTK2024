using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(UIOnHover))]
public class ShowTooltipOnHover : MonoBehaviour {
  private UIOnHover onHover;
  [SerializeField] private GameObject tooltip;

  void Start() {
    onHover = GetComponent<UIOnHover>();
    onHover.startHover.AddListener(Show);
    onHover.stopHover.AddListener(Hide);
  }

  void Show(GameObject invoker) {
    tooltip.gameObject.SetActive(true);
    Update();
  }

  void Hide(GameObject invoker) {
    tooltip.gameObject.SetActive(false);
  }

  void Update() {
    // TODO: Update text on tooltip
  }
}
