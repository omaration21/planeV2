using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public int playerMaxLife = 100;
    public int enemyMaxLife = 50;

    private int playerCurrentLife;
    private int enemyCurrentLife;

    private void Start()
    {
        playerCurrentLife = playerMaxLife;
        enemyCurrentLife = enemyMaxLife;
    }

    public void TakeDamagePlayer(int damage)
    {
        playerCurrentLife -= damage;
        if (playerCurrentLife <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void TakeDamageEnemy(int damage)
    {
        enemyCurrentLife -= damage;
        if (enemyCurrentLife <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
