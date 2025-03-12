using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Spaw_Enemies : MonoBehaviour
{
    [SerializeField] GameObject prefabEnemy;
    const int sizeofEnemies = 64;
    GameObject[] allEnemies = new GameObject[sizeofEnemies];
    float[] allPhases = new float[sizeofEnemies];
    float[] allSavedXPositions = new float[sizeofEnemies];
    SpriteRenderer[] allSprites = new SpriteRenderer[sizeofEnemies];
    int currentEnemy = 0;
    [SerializeField] private Transform player;
    private Move_Character characterMove;
    void Start()
    {
        characterMove = player.GetComponent<Move_Character>();
        StartCoroutine(SpawnEnemies());
        for (int i = 0; i < sizeofEnemies; i++)
        {
            allEnemies[i] = Instantiate(prefabEnemy);
            allEnemies[i].SetActive(false);
            allSprites[i] = allEnemies[i].GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        for (int i = 0; i < sizeofEnemies; i++)
        {
            if (!allEnemies[i].activeSelf)
            {
                continue;
            }
            float currentOffset = Mathf.Sin(allPhases[i]);
            allPhases[i] += Time.deltaTime;
            if (allPhases[i] > Mathf.PI * 2.0f)
            {
                allPhases[i] -= Mathf.PI * 2.0f;
            }
            allEnemies[i].transform.position = new Vector3(allSavedXPositions[i] + currentOffset, allEnemies[i].transform.position.y - Time.deltaTime, allEnemies[currentEnemy].transform.position.z);
            if (Mathf.Abs(allEnemies[i].transform.position.x) > Constants.WORLD_BOUNDARY_X)
            {
                allEnemies[i].transform.position = new Vector3(allEnemies[i].transform.position.x * -0.98f, allEnemies[i].transform.position.y, allEnemies[i].transform.position.z);
            }
            if (allEnemies[i].transform.position.y < -Constants.WORLD_BOUNDARY_Y)
            {
                allEnemies[i].SetActive(false);
                allEnemies[i].transform.position = new Vector3(allEnemies[i].transform.position.x, transform.position.y - Time.deltaTime, allEnemies[currentEnemy].transform.position.z);
            }
            Vector2 distanceToPlayer = allEnemies[i].transform.position - player.position;
            float distance = distanceToPlayer.magnitude;
            float playerRadius = player.localScale.x / 2;
            float enemyRadius = allEnemies[i].transform.localScale.x / 2;
            if (distance < playerRadius + enemyRadius)
            {
                if (allEnemies[i].CompareTag("Evil"))
                {
                    ++characterMove.lives;
                }
                else
                {
                    ++characterMove.points;
                }
            }
        }
    }
    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1.0f, 1.5f));
           if (currentEnemy >= sizeofEnemies - 1)
            {
                currentEnemy = 0;
            }
            allEnemies[currentEnemy].SetActive(true);
            allEnemies[currentEnemy].transform.position = new Vector3(Random.Range(-Constants.WORLD_BOUNDARY_X, Constants.WORLD_BOUNDARY_X), transform.position.y, allEnemies[currentEnemy].transform.position.z);
            allSavedXPositions[currentEnemy] = allEnemies[currentEnemy].transform.position.x;
            if (Random.Range(0, 9) > 2)
            {
                allSprites[currentEnemy].color = new Color32((byte)Random.Range(100, 255), 0, 0, 255);
                allSprites[currentEnemy].tag = "Evil";
            }
            else
            {
                allSprites[currentEnemy].color = new Color32(0, (byte)Random.Range(100, 255), 0, 255);
                allSprites[currentEnemy].tag = "Good";
            }
            float scale = Random.Range(0.3f, 1.0f);
            allEnemies[currentEnemy].transform.localScale = new Vector3(scale, scale, scale);
            ++currentEnemy;
        }
    }
}
