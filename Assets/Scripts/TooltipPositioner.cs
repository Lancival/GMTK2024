using UnityEngine;

public class Tooltip : MonoBehaviour {
  void Update() {
    RectTransform rt = GetComponent<RectTransform>();

    float leftOffset = transform.position.x + rt.rect.xMin;
    if (leftOffset < 0) {
      transform.position = new Vector2(transform.position.x - leftOffset, transform.position.y);
    }

    float rightOffset = transform.position.x + rt.rect.xMax;
    if (rightOffset > Screen.width) {
      transform.position = new Vector2(transform.position.x - rightOffset + Screen.width, transform.position.y);
    }
    this.enabled = false;
  }
}
