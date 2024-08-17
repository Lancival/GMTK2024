using UnityEngine;
using UnityEngine.EventSystems;

public class MockUI : MonoBehaviour, IPointerDownHandler {
  [SerializeField] private GameObject prefab;

  public void OnPointerDown(PointerEventData pointerEventData) {
    Instantiate(prefab, Vector3.zero, Quaternion.identity);
    Destroy(this.gameObject);
  }
}
