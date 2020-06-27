using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] int maxHits;
    [SerializeField] Sprite[] hitSprites;

    //cached references
    Level level;
    GameStatus gameStatus;

    //state var's
    [SerializeField] int timesHit; //TODO only serialized for debug purposes

    
    
    private void Start() {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBreakableBlocks();
        }
        gameStatus = FindObjectOfType<GameStatus>();
    }


    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (tag == "Breakable")
        {
            timesHit++;
            if (timesHit >= maxHits)
            {
                DestroyBlock();
            }
            else
            {
                ShowNextHitSprite();
            }
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (spriteIndex < maxHits - 1) {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        Destroy(gameObject);
        level.BlockDestroyed();
        gameStatus.AddToScore();
        TriggerSparklesVFX();
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }

}
