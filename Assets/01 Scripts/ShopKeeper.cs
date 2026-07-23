using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            ShopManager.instance.OpenPopup();
        }
    }
}
