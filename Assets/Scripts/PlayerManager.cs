using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Speed")]
    public float speed = 2f;
    public float speedY = 2f;
    [Header("Limites")]
    public float limiteX = 4f;
    public int playerCurrentLife = 100; // Vida actual del jugador
    public int playerMaxLife = 100; // Vida máxima del jugador

    [Header("Disparo")]
    public GameObject prefabDisparo;
    public float disparoSpeed = 2f;
    public float timeDisparoDestroy = 2f;

    public Transform weapon1;
    public Transform weapon2;
    private bool isFire = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MovePlayer();
        StartFire();
    }

    public void MovePlayer()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, speedY);
        if (transform.position.x > limiteX)
        {
            transform.position = new Vector2(limiteX, transform.position.y);
        }
        else if (transform.position.x < -limiteX)
        {
            transform.position = new Vector2(-limiteX, transform.position.y);
        }
    }

    public void StartFire()
    {
        if (Input.GetAxis("Fire1") == 1f)
        {
            if (!isFire)
            {
                isFire = true;
                GameObject disparoInstance = Instantiate(prefabDisparo);
                disparoInstance.transform.SetParent(transform.parent);

                disparoInstance.transform.position = weapon1.position;
                disparoInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, disparoSpeed);
                Destroy(disparoInstance, timeDisparoDestroy);

                GameObject disparoInstance2 = Instantiate(prefabDisparo);
                disparoInstance2.transform.SetParent(transform.parent);
                disparoInstance2.transform.position = weapon2.position;
                //Move
                disparoInstance2.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, disparoSpeed);
                Destroy(disparoInstance2, timeDisparoDestroy);
            }
            else
            {
                isFire = false;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        // Restar el daño de la vida del jugador
        playerCurrentLife -= damage;

        if (playerCurrentLife <= 0)
        {
            // El jugador ha perdido
        }
    }
}
