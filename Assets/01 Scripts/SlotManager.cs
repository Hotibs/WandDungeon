using UnityEngine;

public class SlotManager : MonoBehaviour
{
    public static SlotManager Instance;

    public SpellData[] spellDatas;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        
    }
    public void GetSlot(SpellData[] sd)
    {
        spellDatas = sd;
    }

}
