using UnityEngine;

public class WandManager : MonoBehaviour
{
    public static WandManager instance;

    public WandInstance wand;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        wand = new WandInstance();
    }
    public void SetWand(WandInstance data)
    {
        this.wand = data;
    }
    public WandInstance GetWand()
    {
        return wand;
    }
    public WandInstance WandCreate(WandData wandData)
    {
        WandInstance wand = new WandInstance();

        float range = 1.4f;

        wand.slot = Random.Range(2, wandData.SlotCount + 1);
        wand.castDelay = Random.Range(wandData.CastDelay / range, wandData.CastDelay * range);
        wand.rechargeTime = Random.Range(wandData.RechargeTime / range, wandData.RechargeTime * range);
        wand.maxMana = Random.Range(wandData.MaxMana - 50f, wandData.MaxMana + 50f);
        wand.manaRegen = Random.Range(wandData.ManaRegen / 2f, wandData.ManaRegen * 2f);
        wand.price = Random.Range(wandData.Price - 50, wandData.Price + 50);

        return wand;
    }
}
