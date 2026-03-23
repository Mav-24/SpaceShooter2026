using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
  // set in inspector
  public float speed = 0.1f;
  public GameObject bulletPrefab;
  public Transform bulletSpawnPoint;
  public Slider sliderHealth;
  public Shield shield;
  //true for right, false for left
  public bool direction = true;

  // private fields
  private float health;
  private const float Y_LIMIT = 2.7f;

  private void Start() {
    health = 1.0f;
  }

  private void Update() {
    sliderHealth.value = health;

    if (SpaceShooterInput.Instance.input.Fire.WasPressedThisFrame()) {
      GameObject bulletObj = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }

    var vertMove = SpaceShooterInput.Instance.input.MoveVertically.ReadValue<float>();
    this.transform.Translate(Vector3.up * speed * Time.deltaTime * vertMove);

    if (this.transform.position.y > Y_LIMIT) {
      this.transform.position = new Vector3(transform.position.x, Y_LIMIT);
    }
    else if (this.transform.position.y < -Y_LIMIT) {
      this.transform.position = new Vector3(transform.position.x, -Y_LIMIT);
    }

    if (SpaceShooterInput.Instance.input.TurnLeft.WasPressedThisFrame() && direction!=false)
    {
        transform.Rotate(0f,180f,0f);
        direction = false;
    }
    if (SpaceShooterInput.Instance.input.TurnRight.WasPressedThisFrame() && direction != true)
    {
        transform.Rotate(0f,180f,0f);
        direction = true;
    }

    }

  public void DamageFromEnemy() {
    if (!shield.IsActive) {
      health -= 0.25f;
    }
  }

  public void RefillShield() {
    shield.FullRefill();
  }
}
