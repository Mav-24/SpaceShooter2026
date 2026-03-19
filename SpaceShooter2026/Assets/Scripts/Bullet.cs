using UnityEngine;

public class Bullet : MonoBehaviour {
  public float speed = 95f;

  private void Update() {
    this.transform.Translate(Vector3.right * speed * Time.deltaTime);
  }
}
