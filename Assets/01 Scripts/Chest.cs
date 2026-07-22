using UnityEngine;

public class Chest : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            RewardManager.instance.GetReward();
        }
    }
}
