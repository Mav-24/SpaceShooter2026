using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // set in inspector
    public float speed = 0.1f;
    public GameObject bulletPrefab;
    public GameObject expoPrefab;
    public Transform bulletSpawnPoint;
    public Slider sliderHealth;
    public Shield shield;
    public UI ui;
    public GameObject pauseMenu;
    //true for right, false for left
    public bool direction = true;
    //for sfx
    public AudioClip sfxFire;
    public AudioClip sfxHit;
    public AudioClip sfxShieldRefill;
    public AudioClip sfxDie;


    // private fields
    private float health;
    private const float Y_LIMIT = 2.75f;
    private AudioSource playerSfx;
    private bool isPaused = false;

    private void Start()
    {
        playerSfx = GetComponent<AudioSource>();
        health = 1.0f;
    }

    private void Update()
    {
        sliderHealth.value = health;

        if (SpaceShooterInput.Instance.input.Fire.WasPressedThisFrame())
        {
            GameObject bulletObj = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            playerSfx.clip = sfxFire;
            playerSfx.Play();
        }

        var vertMove = SpaceShooterInput.Instance.input.MoveVertically.ReadValue<float>();
        this.transform.Translate(Vector3.up * speed * Time.deltaTime * vertMove);

        if (this.transform.position.y > Y_LIMIT)
        {
            this.transform.position = new Vector3(transform.position.x, Y_LIMIT);
        }
        else if (this.transform.position.y < -Y_LIMIT)
        {
            this.transform.position = new Vector3(transform.position.x, -Y_LIMIT);
        }

        if (SpaceShooterInput.Instance.input.TurnLeft.WasPressedThisFrame() && direction != false)
        {
            transform.Rotate(0f, 180f, 0f);
            direction = false;
        }
        if (SpaceShooterInput.Instance.input.TurnRight.WasPressedThisFrame() && direction != true)
        {
            transform.Rotate(0f, 180f, 0f);
            direction = true;
        }

    }

    public void DamageFromEnemy()
    {
        playerSfx.clip = sfxHit;
        playerSfx.Play();

        if (!shield.IsActive)
        {
            health -= 0.2f;

            if (health <= 0)
            {
                playerSfx.clip = sfxDie;
                var expoObj = Instantiate(expoPrefab, transform.position, Quaternion.identity);
                Destroy(expoObj, expoObj.GetComponent<ParticleSystem>().main.duration);
                playerSfx.Play();
                ui.ShowGameOver();
            }

        }
    }

    public void RefillShield()
    {
        shield.FullRefill();
        playerSfx.clip = sfxShieldRefill;
        playerSfx.Play();
    }



    public void TogglePause()
    { 
        if (isPaused)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            isPaused = true;
        }
    }
}