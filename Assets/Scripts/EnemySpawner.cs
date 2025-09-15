using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> enemies;

    private float duration = 3;
    private float timer = 0;

    [SerializeField]
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >duration)
        {
            timer = 0;
            duration = Random.Range(3, 5);

            var enemyCount = Random.Range(1,(playerController.Level/5)+1);

            for (int i = 0; i < enemyCount; i++)
            {
                Instantiate(enemies[Random.Range(0,enemies.Count)],transform.position + new Vector3(Random.Range(-10,10),Random.Range(-10,10)),Quaternion.identity);
            }

        }
        timer += Time.deltaTime;
    }

    public GameObject enemyPrefab;
}

