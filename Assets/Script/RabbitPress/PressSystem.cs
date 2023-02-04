using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PressSystem : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float changeKeyRate;
    [SerializeField] private int keyLimit;
    [SerializeField] private KeyCode key;
    [SerializeField] private List<Sprite> keyImage;
    [SerializeField] private List<KeyCode> keys;
    [SerializeField] public int pressGoal;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private GameObject[] particleObjects;
    [SerializeField] private int spawnCount;
    [SerializeField] private float[] xRange, yRange;
    [SerializeField] private bool comboParticlePlayed;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private AudioSource audioSource;
    [HideInInspector] public int pressCount;
    public TimeSystem timeSystem;
    
    private KeyCode keyBefore;
    [SerializeField] private List<KeyCode> otherKey;
    [SerializeField] bool pressAble = true;

    private void Start()
    {
        cam = Camera.main;
        otherKey = keys.ToList();
        otherKey.Remove(key);
    }

    public void SetKeyLimit(int limit)
    {
        math.clamp(limit, 0, keys.Count - 1);
        keyLimit = limit;
    }

    void Update()
    {
        if (!timeSystem.gameStart) return;
        if (!pressAble) return;
        foreach (var k in otherKey)
        {
            if (Input.GetKeyDown(k))
            {
                pressAble = false;
                Invoke(nameof(TurnOnPress), 1f);
                Debug.Log("Wrong Key");
                GetComponent<Image>().color = Color.red;
                return;
            }
        }
        if (Input.GetKeyDown(key))
        {
            GetComponent<Image>().color = Color.black;
            
            // change key & key sprite
            if (Random.Range(0, 100) < changeKeyRate)
            {
                Mathf.Lerp(0f,3f, keyLimit);

                while (CheckSameKey())
                {
                    Debug.Log("Random Same Key");
                }
                Debug.Log("Change Key");
                    
            }
            PlayOneEffect();
            pressCount++;
            comboParticlePlayed = false;
        }

        if (Input.GetKeyUp(key) || Input.GetKeyUp(keyBefore))
        {
            GetComponent<Image>().color = Color.white;
        }
        PlayComboEffect();
    }

    private void PlayOneEffect()
    {
        particle.transform.position = cam.ScreenToWorldPoint(transform.position);
        particle.Play();
    }
    
    private void PlayComboEffect()
    {
        
        if (pressCount % 5 == 0 && pressCount > 0 && !comboParticlePlayed)
        {
            comboParticlePlayed = true;
            //audioSource.Play();
            for (int i = 0; i < spawnCount; i++)
            {
                Vector2 position = cam.ScreenToWorldPoint(spawnPosition.position);
                GameObject spawned = Instantiate(particleObjects[Random.Range(0, particleObjects.Length)],
                    position, Quaternion.identity);
                spawned.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(xRange[0], xRange[1]),
                    Random.Range(yRange[0], yRange[1])));
                StartCoroutine(DestroyObject(spawned));
            }
        }
    }

    private IEnumerator DestroyObject(GameObject gameObjectToDestroy)
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObjectToDestroy);
    }

    private void TurnOnPress()
    {
        pressAble = true;
        GetComponent<Image>().color = Color.white;
    }

    private bool CheckSameKey()
    {
        int randomKeyIndex = Random.Range(0, keys.Count - keyLimit);
        if (randomKeyIndex != keys.IndexOf(key) || keyLimit == keys.Count - 1)
        {
            keyBefore = key;
            otherKey.Add(keyBefore);
            key = keys[randomKeyIndex];
            otherKey.Remove(key);
            GetComponent<Image>().sprite = keyImage[randomKeyIndex];
            return false;
        }
        return true;
    }
}
