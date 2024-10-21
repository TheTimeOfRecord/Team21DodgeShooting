using UnityEngine;

public class RandomItemObject : ItemObject
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            ItemSelectionManager.instance.GetChoices();
        }
    }
}