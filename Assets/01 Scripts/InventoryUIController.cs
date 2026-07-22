using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] GameObject inventoryPrefab;
    [SerializeField] Canvas UICanvas;
    
    GameObject inventoryPanel;

    bool isOpen = false;
    private void Start()
    {
        inventoryPanel = Instantiate(inventoryPrefab, UICanvas.transform);
        inventoryPanel.SetActive(false);
    }
    private void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            
            if (isOpen)
            {
                CloseInventoryPanel();
            }
            else
            {
                OpenInventoryPanel();
            }
            
        }
    }

    public void OpenInventoryPanel()
    {
        if (inventoryPanel == null)
        {
            inventoryPanel = Instantiate(inventoryPrefab, UICanvas.transform);
        }
        inventoryPanel.SetActive(true);
        inventoryPanel.transform.SetAsLastSibling();
        isOpen = true;
    }

    public void CloseInventoryPanel()
    {
        inventoryPanel.SetActive(false);
        isOpen = false;
    }

}
