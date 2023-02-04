using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;


public class SpawnEnemyZone : MonoBehaviour
{
    [SerializeField] private HeartZone heartZone;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private float stamina;
    [SerializeField] private float staminaRegenRate;
    [SerializeField] private float maxStamina;
    [SerializeField] private Scrollbar staminaBar;
    [SerializeField] private List<EnemyData> enemyDataList;
    [SerializeField] private List<Image> skillImages;
    [SerializeField] private TimeSystem timeSystem;
    public int unitAmount;
    private int difficultLevel;
    private Color tempColor;

    public void AssignData(GameSystemData gameSystemData)
    {
        difficultLevel = gameSystemData.level;
        unitAmount = gameSystemData.unitAmount;
        staminaRegenRate = gameSystemData.badPlayerStaminaRegen;
    }
    void Start()
    {
        
    }

    public void SetUp()
    {
        InvokeRepeating(nameof(StaminaRegen) , 0, 0.1f);
        tempColor = skillImages[0].color;
        tempColor.a = 0.1f;

        switch (difficultLevel)
        {
            case 1:
                skillImages[3].color = tempColor;
                skillImages[2].color = tempColor;
                break;
            case 2:
                skillImages[3].color = tempColor;
                break;
        }
    }
    
    void Update()
    {
        if (!timeSystem.gameStart) return;
        KeyDetect();
        StaminaBarUpdate();
    }

    private void KeyDetect()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (enemyDataList[0].cost <= stamina)
            {
                CreateEnemy(enemyDataList[0]);
                skillImages[0].color = Color.gray;
            }
            else
            {
                //skillImages[0].color = Color.red;
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (enemyDataList[1].cost <= stamina)
            {
                CreateEnemy(enemyDataList[1]);
                skillImages[1].color = Color.gray;
            }
            else
            {
                //skillImages[0].color = Color.red;
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && difficultLevel > 1)
        {
            if (enemyDataList[2].cost <= stamina)
            {
                Debug.Log("E");
                CreateEnemy(enemyDataList[2]);
                skillImages[2].color = Color.gray;
            }
            else
            {
                //skillImages[2].color = Color.red;
            }
        }
        else if (Input.GetKeyDown(KeyCode.R) && difficultLevel > 2)
        {
            if (enemyDataList[3].cost <= stamina)
            {
                CreateEnemy(enemyDataList[3]);
                skillImages[3].color = Color.gray;
            }
            else
            {
                //skillImages[3].color = Color.red;
            }
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            skillImages[0].color = Color.white;
        }

        else if (Input.GetKeyUp(KeyCode.W))
        {
            skillImages[1].color = Color.white;
        }

        else if (Input.GetKeyUp(KeyCode.E) && difficultLevel > 1)
        {
            skillImages[2].color = Color.white;
        }

        else if (Input.GetKeyUp(KeyCode.R) && difficultLevel > 2)
        {
            skillImages[3].color = Color.white;
        }
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
