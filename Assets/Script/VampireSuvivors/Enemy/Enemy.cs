
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;


    public int hp;
    public bool isCatching;
    public Heart target;
    public HeartZone heartZone;

    private int randomIndexHeart = 0;
    private GameObject capturedHeart;
    private Vector2 spawnPoint;


    public void AssignEnemyData(EnemyData enemyData, Heart target, HeartZone heartZone)
    {
        hp = enemyData.hp;
        speed = enemyData.speed;
        this.heartZone = heartZone;
        this.target = target;
        spawnPoint = transform.position;
    }

    void Start()
    {
        spawnPoint = transform.position;
    }
    
    void Update()
    {
        if(IsEnemyDie()) return;
        if (isCatching == false)
        {
            EnemyMoveToPlayerHeart();
        }
        else
        {
            EnemyBackToStartPos();
        }
        
    }

    private void EnemyMoveToPlayerHeart()
    {
        if (target == null)
        {
            EnemyBackToStartPos(false);
            return;
        }
            
        if (target.isCatched)
        {
            target = FindNewHeart();
            return;
        }
        Vector3 direction = target.transform.position - transform.position;
        transform.Translate(direction * speed * Time.deltaTime);

    }
    
    private Heart FindNewHeart()
    {
        if(heartZone.transform.childCount == 0) return null;
        randomIndexHeart = Random.Range(0, heartZone.transform.childCount);
        Debug.Log("Find new heart");
        return heartZone.transform.GetChild(randomIndexHeart).gameObject.GetComponent<Heart>();
        

    }

    private void EnemyBackToStartPos(bool withHeart = true)
    {
        if (withHeart)
        {
            capturedHeart.transform.SetParent(transform);
            capturedHeart.transform.position = (Vector2)transform.position;
            capturedHeart.transform.Rotate(0,0,2);
        }
        Vector3 direction = spawnPoint - (Vector2)transform.position.normalized;
        transform.Translate(direction * speed * 2 * Time.deltaTime);
        Invoke(nameof(DestroyObject),3);
    }


    private bool IsEnemyDie()
    {
        if (hp > 0) return false;
        Destroy(gameObject);
        return true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(!col.gameObject.TryGetComponent<Heart>(out var touchObject)) return;
        if (touchObject.id == target.id)
        {
            target.isCatched = true;
            col.enabled = false;
            GetComponent<Collider2D>().enabled = false;
            isCatching = true;
            capturedHeart = col.gameObject;
        }
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
        Destroy(capturedHeart);
    }
}
