using UnityEngine;
using TMPro;
using GLTFast.Schema;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    int startingEnemies;
    [SerializeField] TMP_Text ghoulsTxt;


    private void Start()
    {
        EnemyController[] enemies = FindObjectsByType<EnemyController>();
        startingEnemies = enemies.Length;
    }


    private void Update()
    {
        EnemyController[] enemies = FindObjectsByType<EnemyController>();
        int enemyCount = enemies.Length;

        if (enemyCount == 0)
        {
            SceneManager.LoadScene("WinScene");
        }
        

        ghoulsTxt.text = $"Ghouls remaining:  {startingEnemies - enemyCount} / {startingEnemies} \r\n";
    }
}
