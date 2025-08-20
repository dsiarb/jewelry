using UnityEngine;
using System.Collections.Generic;

// Bu script, oyunun en başındaki kurulum adımlarını yönetir.
// Görevini tamamladığında kendini devre dışı bırakır.
public class ShopSetupManager : MonoBehaviour
{
    // --- Gerekli Referanslar ---
    // Bu referanslar, Unity editöründen atanacak.
    public PlayerInventory playerInventory;
    public UIManager uiManager;
    public GameManager gameManager;

    // --- Görev Durumları ---
    private bool hasRentedShop = false;
    private bool hasShowcase = false;
    private bool hasSalesCounter = false;
    private bool hasManagementDesk = false;
    private bool hasInventorySafe = false;
    private bool hasPlacedFirstOrder = false;
    private bool hasSetupShowcase = false;

    // Oyun başladığında bu fonksiyon çalışır.
    void Start()
    {
        // Oyuncunun dükkanı kiraladığını varsayarak başlıyoruz.
        hasRentedShop = true;
        CheckCurrentTask();
    }

    // Her eylemden sonra mevcut görevi kontrol edip oyuncuya yeni görevi verir.
    public void CheckCurrentTask()
    {
        if (!hasRentedShop) return; // Dükkan kiralanmadıysa başlama.

        if (!hasShowcase || !hasSalesCounter || !hasManagementDesk || !hasInventorySafe)
        {
            uiManager.ShowTask("Purchase mandatory equipment for your shop and office.");
            // Burada oyuncuyu mobilya satın alma ekranına yönlendiren bir buton olabilir.
        }
        else if (!hasPlacedFirstOrder)
        {
            uiManager.ShowTask("Order your first jewelry from the supplier using the management desk.");
            // Yönetim masasını tıklanabilir hale getir.
        }
        else if (!hasSetupShowcase)
        {
            uiManager.ShowTask("Set up your showcase with the items you've ordered.");
            // Vitrini tıklanabilir hale getir.
        }
        else
        {
            uiManager.ShowTask("Everything is ready! Open your shop for customers.");
            uiManager.ShowOpenShopButton(); // "Dükkanı Aç" butonunu göster.
        }
    }

    // Bu fonksiyonlar, diğer script'ler tarafından çağrılacak.
    // Örneğin, oyuncu mobilya mağazasından bir item aldığında.
    public void ConfirmItemPurchase(string itemID)
    {
        // Bu eylemin bir zaman maliyeti olabilir.
        // GameTime.ConsumeTime(cost);

        if (itemID == "item_showcase_01") hasShowcase = true;
        if (itemID == "item_sales_counter_01") hasSalesCounter = true;
        if (itemID == "item_management_desk_01") hasManagementDesk = true;
        if (itemID == "item_inventory_safe_01") hasInventorySafe = true;

        CheckCurrentTask(); // Bir sonraki görevi kontrol et.
    }

    public void ConfirmFirstOrderPlaced()
    {
        // Bu eylemin bir zaman maliyeti olabilir.
        // GameTime.ConsumeTime(cost);
        hasPlacedFirstOrder = true;
        CheckCurrentTask();
    }

    public void ConfirmShowcaseSetup()
    {
        // Bu eylemin bir zaman maliyeti olabilir.
        // GameTime.ConsumeTime(cost);
        hasSetupShowcase = true;
        CheckCurrentTask();
    }

    // Oyuncu "Dükkanı Aç" butonuna bastığında bu çağrılır.
    public void OpenShop()
    {
        // Ana oyun yöneticisine haber ver.
        gameManager.StartMainGameLoop();

        // Bu script görevini tamamladı.
        Debug.Log("Shop setup complete. Main game loop is now active.");
        this.gameObject.SetActive(false); // Bu script'i devre dışı bırak.
    }
}