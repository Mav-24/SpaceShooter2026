using UnityEngine;

public class Boss : MonoBehaviour
{
    private int bossHp;
    private float shootTimer;
    private Transform bulletDirection;

    public float shootDelay;
    public GameObject expoPrefab;
    public BoxCollider2D bulletSpawn;
    public GameObject bossBulletPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bossHp = 300;
        shootTimer = 0;
        bulletDirection = bulletSpawn.transform;
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootDelay)
        {
            Vector3 bulletSpawnPt = new Vector3(
            Random.Range(bulletSpawn.bounds.min.x, bulletSpawn.bounds.max.x),
            Random.Range(bulletSpawn.bounds.min.y, bulletSpawn.bounds.max.y),
            0);
            GameObject bossBullet = Instantiate(bossBulletPrefab, bulletSpawnPt, Quaternion.Euler(0f, 180f, 0f));
            shootTimer = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.CompareTag("Bullet"))
        {
            if (bossHp > 0)
            {
                bossHp -= 15;
                Destroy(c.gameObject);
            }
            else if (bossHp <= 0)
            {
                var expoObj = Instantiate(expoPrefab, transform.position, Quaternion.identity);
                Destroy(expoObj, expoObj.GetComponent<ParticleSystem>().main.duration);
                Destroy(gameObject);
                Destroy(c.gameObject);
                Score.Instance.HitEnemy();
            }
        }
    }
}
