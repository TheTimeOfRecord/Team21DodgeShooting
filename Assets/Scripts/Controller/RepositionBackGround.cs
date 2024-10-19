using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionBackGround : MonoBehaviour
{
    [SerializeField] GameObject player;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerArea"))
        {
            Reposition();
        }
    }

    private void Reposition()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 groundPos = transform.position;

        float dirX = (playerPos.x - groundPos.x);
        float dirY = (playerPos.y - groundPos.y);

        float diffX = MathF.Abs(dirX);
        float diffY = MathF.Abs(dirY);

        dirX = dirX > 0 ? 1 : -1;
        dirY = dirY > 0 ? 1 : -1;

        if(diffX > diffY)
        {
            transform.Translate(Vector2.right * dirX * 32.4f);
        }
        else if(diffX < diffY)
        {
            transform.Translate(Vector2.up * dirY * 32.4f);
        }
        else
        {
            Vector2 diagonalDirection = new Vector2(dirX > 0 ? 1 : -1, dirY > 0 ? 1 : -1);
            transform.Translate(diagonalDirection * 32.4f);
        }
    }
}
