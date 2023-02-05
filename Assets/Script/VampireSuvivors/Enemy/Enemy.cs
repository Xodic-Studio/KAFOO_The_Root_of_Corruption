
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    public AudioSource audioSource;
     private int level;
     [SerializeField] private SoundData data;

    public int hp;
    public bool isCatching;
    public Heart target;
    public HeartZone heartZone;
    public Sprite sprite;
    public Color color;

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
        sprite = enemyData.sprite;
        color = enemyData.color;
        level = enemyData.level;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SoundManager.Instance.PlaySound(data.GetSoundClip("EnemySpawn"));
        spawnPoint = transform.position;
        GetComponent<SpriteRenderer>().sprite = sprite;
        GetComponent<SpriteRenderer>().color = color;
    }
    
    void Update()
    {
        if(IsEnemyDie()) return ;
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
        switch (level)
        {
            case 1: 
                SoundManager.Instance.PlaySound(data.GetSoundClip("Enemy_1_Die"));
                break;
            case 2:
                SoundManager.Instance.PlaySound(data.GetSoundClip("Enemy_2_Die"));
                break;
            case > 3:
                SoundManager.Instance.PlaySound(data.GetSoundClip("Enemy_3_4_Die"));
                break;
        }
        Destroy(gameObject);
        return true;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Bullet"))
        {
            switch (level)
            {
                case 1: 
                    SoundManager.Instance.PlaySound(data.GetSoundClip("Enemy_1_2_Hurt"));
                    break;
                case 2:
                    SoundManager.Instance.PlaySound(data.GetSoundClip("Enemy_1_2_Hurt"));
                    break;
                case 3:
                    SoundManager.Instance.PlaySound(data.GetSoundClip("Enemy_3_4_Hurt"));
                    break;
                case 4:
                    SoundManager.Instance.PlaySound(data.GetSoundClip("Enemy_3_4_Hurt"));
                    break;
            }

            hp -= col.GetComponent<Bullet>().bulletDmg;
            Destroy(col.gameObject);
        }
        
        if(!col.gameObject.TryGetComponent<Heart>(out var touchObject)) return;
        
        if (touchObject.id == target.id)
        {
            SoundManager.Instance.PlaySound(data.GetSoundClip("EnemyDragPlayer"));

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
