using UnityEngine;
using UnityEngine.UI; // UI elemanlarını kullanmak için gerekli (Button, Text, etc.)
using System.Collections.Generic;

public class SupplierUI : MonoBehaviour
{
    // --- UI Referansları ---
    // Bu alanlar, Unity editöründen atanacak olan UI objeleridir.
    public GameObject supplierWindowPanel; // Tedarikçi penceresinin tamamı
    public Text supplierNameText;
    public Transform itemListContentPanel; // Ürün listesinin oluşturulacağı panel
    public GameObject itemButtonPrefab; // Listedeki her bir ürün için kullanılacak buton prefab'ı

    // --- Seçili Ürün Detayları İçin UI Referansları ---
    public Text selectedItemNameText;
    public Text selectedItemDescriptionText;
    public Text selectedItemPriceText;
    public Button purchaseButton;

    // --- Diğer Sistemlere Referanslar ---
    private ShopSetupManager shopSetupManager;
    private PlayerWallet playerWallet; // Oyuncunun cüzdanını yöneten bir script olduğunu varsayalım
    private PlayerInventory playerInventory;

    private string selectedItemId; // Satın alınmak için seçilen ürünün ID'si

    void Start()
    {
        // Gerekli yöneticileri bul ve referansları ata
        shopSetupManager = FindObjectOfType<ShopSetupManager>();
        playerWallet = FindObjectOfType<PlayerWallet>();
        playerInventory = FindObjectOfType<PlayerInventory>();
        
        // Başlangıçta pencereyi gizle
        supplierWindowPanel.SetActive(false);
    }

    // Bu ana fonksiyon, dışarıdan (örn: Yönetim Masası'ndan) çağrılacak.
    public void ShowSupplierWindow(Supplier supplier)
    {
        supplierWindowPanel.SetActive(true);
        supplierNameText.text = supplier.name;

        // Önceki listeden kalanları temizle
        foreach (Transform child in itemListContentPanel)
        {
            Destroy(child.gameObject);
        }

        // Tedarikçinin envanterindeki her ürün için bir buton oluştur
        foreach (string itemId in supplier.inventory)
        {
            GameObject itemButtonObj = Instantiate(itemButtonPrefab, itemListContentPanel);
            itemButtonObj.GetComponentInChildren<Text>().text = "Product ID: " + itemId; // Gerçekte ürün adını gösterecek
            
            // Butona tıklandığında ne olacağını belirle
            string currentId = itemId;
            itemButtonObj.GetComponent<Button>().onClick.AddListener(() => OnItemClicked(currentId));
        }
    }

    // Listeden bir ürüne tıklandığında çalışır.
    private void OnItemClicked(string itemId)
    {
        selectedItemId = itemId;
        
        // Ürün detaylarını JsonDataManager'dan alıp ekrana yazdırdığımızı varsayalım
        // ItemData itemData = JsonDataManager.Instance.GetItemData(itemId);
        // selectedItemNameText.text = itemData.name;
        // selectedItemDescriptionText.text = itemData.description;
        // selectedItemPriceText.text = itemData.price + " G";

        // Satın al butonunu aktif et
        purchaseButton.interactable = true;
    }

    // "Purchase" butonuna basıldığında çalışır.
    public void OnPurchaseButtonClicked()
    {
        // int itemPrice = JsonDataManager.Instance.GetItemData(selectedItemId).price;
        int itemPrice = 100; // Örnek fiyat

        // Oyuncunun yeterli parası var mı?
        if (playerWallet.HasEnoughMoney(itemPrice))
        {
            // Parayı düş
            playerWallet.SpendMoney(itemPrice);
            // Ürünü envantere ekle
            playerInventory.AddItem(selectedItemId);

            Debug.Log(selectedItemId + " purchased successfully!");

            // Eğer kurulum aşamasındaysak, ilgili yöneticiye haber ver.
            if (shopSetupManager != null && shopSetupManager.isActiveAndEnabled)
            {
                shopSetupManager.ConfirmItemPurchase(selectedItemId);

                // Kurulum için gerekli tüm mobilyalar alındıysa, bu pencereyi kapat.
                // if (shopSetupManager.AllFurniturePurchased()) {
                //     supplierWindowPanel.SetActive(false);
                // }
            }
        }
        else
        {
            Debug.Log("Not enough money!");
            // Ekranda "Yetersiz Bakiye" uyarısı gösterilebilir.
        }
    }
}