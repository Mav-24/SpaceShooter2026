using UnityEngine;

public class Enemy : MonoBehaviour {
  // set in inspector
  public float speed;
<<<<<<< Updated upstream
=======
  public bool isHoming;
  public bool isBobber;
  public GameObject expoPrefab;
>>>>>>> Stashed changes

  private Transform player;
  private float height = 0.05f;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.transform;
    }

    void Update() {
    if (!isHoming && !isBobber)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    else if (isHoming)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    else if (isBobber)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y + (Mathf.Sin(Time.time * speed) * height), transform.position.z);
        }
  }

  private void OnCollisionEnter2D(Collision2D c) {
    if (c.gameObject.CompareTag("Bullet")) {
      Destroy(gameObject);
      Destroy(c.gameObject);
      Score.Instance.HitEnemy();
    }
    else if (c.gameObject.CompareTag("Player")) {
      Destroy(gameObject);
      c.gameObject.GetComponent<Player>().DamageFromEnemy();
    }
  }
}
