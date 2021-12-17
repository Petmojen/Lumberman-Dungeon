using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    [SerializeField] Sprite takenLog;
    SpriteRenderer changeSprite;
    public bool taken;

    void Start()
    {
        changeSprite = GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite()
    {
        changeSprite.sprite = takenLog;
    }
}
