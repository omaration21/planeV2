using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavoiur : MonoBehaviour
{
    [Header("Speed")]
    public float speed = 2f;

    [Header("Disparo")]
    public GameObject prefabDisparo;
    public float disparoSpeed = 2f;
    public float shootingInterval = 6f;
    public float timeDisparoDestroy = 2f;
    public int enemyDamage = 10; 

    private float _shootingTimer;
    private float _lifeTimer;
    private bool isPaused = false;
    private float pauseDuration = 3.0f; // Duración de la pausa en segundos
    private float pauseTimer = 0.0f;
    public float maxLifeTime = 10.0f; // Tiempo de vida máximo en segundos

    public Transform weapon1;
    public Transform weapon2;

    void Start()
    {
        _shootingTimer = Random.Range(0f, shootingInterval + pauseDuration);
        _lifeTimer = maxLifeTime;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
    }

    void Update()
    {
        if (!isPaused)
        {
            StartFire();
        }
        else
        {
            pauseTimer += Time.deltaTime;
            if (pauseTimer >= pauseDuration)
            {
                isPaused = false;
                pauseTimer = 0.0f;
            }
        }

        _lifeTimer -= Time.deltaTime;
        if (_lifeTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void StartFire()
    {
        if (!isPaused)
        {
            _shootingTimer -= Time.deltaTime;
            if (_shootingTimer <= 0f)
            {
                _shootingTimer = shootingInterval;

                GameObject disparoInstance = Instantiate(prefabDisparo);
                disparoInstance.transform.SetParent(transform.parent);
                disparoInstance.transform.position = weapon1.position;
                disparoInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, disparoSpeed);
                Destroy(disparoInstance, timeDisparoDestroy);

                GameObject disparoInstance2 = Instantiate(prefabDisparo);
                disparoInstance2.transform.SetParent(transform.parent);
                disparoInstance2.transform.position = weapon2.position;
                disparoInstance2.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, disparoSpeed);
                Destroy(disparoInstance2, timeDisparoDestroy);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "disparoPlayer" || otherCollider.tag == "Player")
        {
            if (otherCollider.gameObject.GetComponent<PlayerManager>()!= null)
            {
                // Restar el daño al jugador
                otherCollider.gameObject.GetComponent<PlayerManager>().TakeDamage(enemyDamage);
            }

            gameObject.SetActive(false);

            // Pausar el modo de disparo
            isPaused = true;
        }
    }
}
