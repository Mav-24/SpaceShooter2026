using UnityEngine;

public class Game : MonoBehaviour {
  // set in inspector
  public float enemySpawnDelay;
  public float switchDelay;
  public float despawnTime;
  public GameObject[] enemyPrefabs;
  public GameObject powerupPrefab;
  public BoxCollider2D spawnRangeRight;
  public BoxCollider2D spawnRangeLeft;
  public Score gameScore;
  public GameObject pauseScreen;
  public UI ui;

  // private fields
  private float powerUpDelay;
  private float enemySpawnTimer;
  private float powerupSpawnTimer;
  private BoxCollider2D spawnRange;
  private float switchTimer;
  private Quaternion spawnDirection;
  private int threshold = 1000;

  private void Start() {
    powerUpDelay = Random.Range(5f, 10f);
    //pauseScreen = GameObject.ge
    powerupSpawnTimer = 0;
    spawnRange = spawnRangeRight;
  }

  private void SpawnEnemy() {
    Vector3 enemySpawnPt = new Vector3(
        Random.Range(spawnRange.bounds.min.x, spawnRange.bounds.max.x),
        Random.Range(spawnRange.bounds.min.y, spawnRange.bounds.max.y),
        0);
    GameObject enemy = Instantiate(enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)], enemySpawnPt, spawnDirection);
    Destroy(enemy, despawnTime);
    }
  private void SpawnPowerup() {
    Vector3 powerupSpawnPt = new Vector3(
        Random.Range(spawnRange.bounds.min.x, spawnRange.bounds.max.x),
        Random.Range(spawnRange.bounds.min.y, spawnRange.bounds.max.y),
        0);
    GameObject shieldBoost = Instantiate(powerupPrefab, powerupSpawnPt, spawnDirection);
    Destroy(shieldBoost, despawnTime);
    }
    private void levelIncrease()
    {
        enemySpawnDelay -= 0.1f;
    }
  void Update() {
    if (!ui.IsReady) {
      return;
    }

    // check spawn enemy
    enemySpawnTimer += Time.deltaTime;
    if (enemySpawnTimer >= enemySpawnDelay) {
      SpawnEnemy();
      enemySpawnTimer = 0.0f;
    }

    // check spawn powerup
    powerupSpawnTimer += Time.deltaTime;
    if (powerupSpawnTimer >= powerUpDelay) {
      SpawnPowerup();
      powerUpDelay = Random.Range(5, 10);
      powerupSpawnTimer = 0.0f;
    }

    switchTimer += Time.deltaTime;
    if (switchTimer > switchDelay)
        {
            Debug.Log("15 seconds passed");
            if (spawnRange == spawnRangeRight)
            {
                spawnRange = spawnRangeLeft;
                spawnDirection = Quaternion.Euler(0f, 180f, 0f);
            } else
            {
                spawnRange = spawnRangeRight;
                spawnDirection = Quaternion.identity;
            }
            switchTimer = 0;
        }

    if (gameScore.score >= threshold)
    {
        levelIncrease();
        threshold += 1000;
    }
    }
}
