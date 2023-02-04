using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class SpawnEnemyZone : MonoBehaviour
{
    [SerializeField] private HeartZone heartZone;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private float stamina;
    [SerializeField] private float staminaRegenRate;
    [SerializeField] private float maxStamina;
    [SerializeField] private Scrollbar staminaBar;
    [SerializeField] private List<EnemyData> enemyDataList;
    
    void Start()
    {
        InvokeRepeating(nameof(StaminaRegen) , 0, 0.1f);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && enemyDataList[0].cost <= stamina)
        {
            CreateEnemy(enemyDataList[0]);
        }
        else if (Input.GetKeyDown(KeyCode.W) && enemyDataList[1].cost <= stamina)
        {
            CreateEnemy(enemyDataList[1]);
        }
        else if (Input.GetKeyDown(KeyCode.E) && enemyDataList[2].cost <= stamina)
        {
            CreateEnemy(enemyDataList[2]);
        }
        else if (Input.GetKeyDown(KeyCode.R) && enemyDataList[3].cost <= stamina)
        {
            CreateEnemy(enemyDataList[3]);
        }

        StaminaBarUpdate();
    }

    private void CreateEnemy(EnemyData enemyData)
    {
        int randomSpawnIndex = Random.Range(0, transform.childCount);
        int randomHeartIndex = Random.Range(0, heartZone.transform.childCount);
        Debug.Log("Cost: "+ enemyData.cost);
        if (heartZone.transform.childCount <= 0)
        {
            Debug.Log("No Heart");
            return;
        }
        stamina -=  enemyData.cost;
        Enemy newEnemy = Instantiate(enemyPrefab, (Vector2)transform.GetChild(randomSpawnIndex).position,Quaternion.Euler(0,0,0));
        newEnemy.AssignEnemyData(enemyData, heartZone.transform.GetChild(randomHeartIndex).gameObject.GetComponent<Heart>(),heartZone);
    }
    
    
    
    
    public void StaminaBarUpdate()
    {
        staminaBar.size = stamina / maxStamina;
    }
    
    
    void StaminaRegen()
    {
        stamina += staminaRegenRate;
        stamina = Mathf.Clamp(stamina, 0, maxStamina);
    }
}
