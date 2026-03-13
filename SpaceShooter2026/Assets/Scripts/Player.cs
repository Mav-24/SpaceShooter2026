using UnityEngine;

public class Player : MonoBehaviour {
  private SpaceShooterInputActions inputActions;
  public float speed = 0.1f;
  private const float Y_LIMIT = 4.6f;

  private void Start() {
    inputActions = new();
    inputActions.Enable();
    inputActions.Standard.Enable();
  }

  private void Update() {
    if (inputActions.Standard.MoveUp.IsPressed()) {
      this.transform.Translate(Vector3.up * speed);
    }
    else if (inputActions.Standard.MoveDown.IsPressed()) {
      this.transform.Translate(Vector3.down * speed);
    }
    if (this.transform.position.y > Y_LIMIT) {
      this.transform.position = new Vector3(transform.position.x, Y_LIMIT);
    }
    else if (this.transform.position.y < -Y_LIMIT) {
      this.transform.position = new Vector3(transform.position.x, -Y_LIMIT);
    }
  }
}
