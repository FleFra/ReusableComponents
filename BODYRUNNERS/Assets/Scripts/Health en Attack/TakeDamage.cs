using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TakeDamage : MonoBehaviour, IDamageable
{
    // serializefield zodat je elke guy met dit script andere health kan geven 
    [SerializeField] private float startingHealth;
    [SerializeField] TMP_Text healthTxt;
    public float health { get; set; }

    void Start()
    {
        // zet health naar starting health
        health = startingHealth;
    }

    public void RemoveHealth(float amount) 
    {
        gameObject.TryGetComponent<PlayerController>(out PlayerController playerController);

        // doe damage en check of de guy dood is
        health -= amount;
        if (playerController != null)
        {
            healthTxt.text = $"Health: {health} \r\n";
        }

        if (health <= 0)
        {
            CashManager.addCash(3);

            if (playerController != null)
            {
                SceneManager.LoadScene("LoseScene");
            }
            Destroy(gameObject);
        }
    }
}
