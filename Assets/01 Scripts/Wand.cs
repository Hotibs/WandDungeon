using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Wand : MonoBehaviour
{
    [SerializeField] WandData data;
    [SerializeField] SpellCaster spellCaster;
    Camera camera;

    string wandName;
    int slotCount;
    float castDelay;
    float rechargeTime;
    float maxMana;
    float manaRegen;

    int currentSlot;

    float timer;
    float castTimer;
    float rechargeTimer;

    private void Start()
    {
        camera = Camera.main;

        wandName = data.WandName;
        slotCount = data.SlotCount;
        castDelay = data.CastDelay;
        rechargeTime = data.RechargeTime;
        maxMana = data.MaxMana;
        manaRegen = data.ManaRegen;

        castTimer = 0;
        rechargeTimer = rechargeTime;

        currentSlot = 0;
    }

    private void Update()
    {
        LookMouse();
        castTimer -= Time.deltaTime;
        if (currentSlot >= slotCount) rechargeTimer -= Time.deltaTime;
        if (Mouse.current.leftButton.isPressed)
        {
                TryCast();
        }
    }

    void TryCast()
    {
        if (castTimer > 0)
        {
            return;
        }

        if (currentSlot >= slotCount)
        {
            if (rechargeTimer > 0)
            {
                return;
            }
            currentSlot = 0;
        }
        Spell(currentSlot);
        currentSlot++;
        castTimer = castDelay;

        if (currentSlot >= slotCount)
        {
            rechargeTimer = rechargeTime;
        }
    }

    void Spell(int slotSpell)
    {
        spellCaster.Cast(slotSpell);
    }

    Vector2 MousePos()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = camera.ScreenToWorldPoint(mousePos);
        Vector2 dir = worldPos - transform.position;

        mousePos = new Vector3(dir.x, dir.y);

        return mousePos;
    }

    void LookMouse()
    {
        float angle = Mathf.Atan2(MousePos().y,MousePos().x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle+180);
    }

    
    
}
