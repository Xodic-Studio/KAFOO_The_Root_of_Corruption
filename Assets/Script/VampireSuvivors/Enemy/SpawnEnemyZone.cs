using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class SpawnEnemyZone : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private float stamina;
    [SerializeField] private float staminaRegenRate;
    [SerializeField] private float maxStamina;
    [SerializeField] private Scrollbar staminaBar;

    private int randomSpawnIndex;
    void Start()
    {
        StartCoroutine(staminaRegen());
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CreateEnemy(1);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            CreateEnemy(2);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            CreateEnemy(3);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            CreateEnemy(4);
        }
        
        StaminaBarUpdate();
    }

    public void StaminaBarUpdate()
    {
        staminaBar.size = stamina / maxStamina;
    }
    
    public void CreateEnemy(int level)
    {
        randomSpawnIndex = Random.Range(0, transform.childCount);
        enemy.level = level;
        stamina -=  enemy.cost;
        Debug.Log("Cost: "+ enemy.cost);
        
        if (stamina > 0)
        {
            Instantiate(enemy, (Vector2)transform.GetChild(randomSpawnIndex).position,Quaternion.Euler(0,0,0));
        }
    }
    
    IEnumerator staminaRegen()
    {
        while (true)
        {
            stamina += staminaRegenRate;
            stamina = Mathf.Clamp(stamina, 0, maxStamina);
            yield return new WaitForSeconds(1);
        }
    }
}
