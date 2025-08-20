using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

// Bu class, tek bir müşteri ile olan satış etkileşiminin tamamını yönetir.
public class SalesInteractionManager : MonoBehaviour
{
    // --- Gerekli Sistem Referansları ---
    // Bu scriptlerin sahnede mevcut olduğunu ve atanacağını varsayıyoruz.
    [Header("System References")]
    public GameManager gameManager;
    public UIManager uiManager;
    public PlayerInventory playerInventory;
    public PlayerWallet playerWallet;

    // --- Dahili Durum Değişkenleri (Internal State) ---
    private Customer currentCustomer;
    private List<Item> currentTrayItems = new List<Item>();
    private float currentCustomerPatience;
    private float timeCostPerTrayChange = 0.25f;
    private float timeCostPerPresentation = 1.0f;

    // Oyuncu "Present Items" butonuna bastığında bu fonksiyon çağrılır.
    // Bu, Adım 5'i başlatır.
    public void OnPresentItemsClicked()
    {
        uiManager.ShowInventoryAndTrayScreen();
    }

    // --- Adım 5 & 5.5: Envanter, Sunum ve Tepsi Analizi ---
    public void AddItemToTray(Item item)
    {
        if (currentTrayItems.Count < 3)
        {
            currentTrayItems.Add(item);
            if (currentTrayItems.Count == 3)
            {
                uiManager.ShowTrayAnalysis(currentTrayItems);
                uiManager.ShowFinalPresentButton(true);
            }
        }
    }

    public void ClearOrChangeTray()
    {
        // GameTime.ConsumeTime(timeCostPerTrayChange); // Zaman maliyeti
        currentCustomerPatience -= 5; // Sabır maliyeti
        uiManager.UpdatePatienceBar(currentCustomerPatience);
        
        currentTrayItems.Clear();
        uiManager.HideTrayAnalysis();
        uiManager.ShowFinalPresentButton(false);
    }
    
    // Oyuncu son "Present" butonuna basınca bu çağrılır.
    public void OnFinalPresentClicked()
    {
        EvaluateTray();
    }

    // --- Adım 6: Değerlendirme ---
    private void EvaluateTray()
    {
        // GameTime.ConsumeTime(time_cost_per_presentation); // Sunumun ana zaman maliyeti
        
        float totalSatisfactionScore = 0;
        List<Item> winningItems = new List<Item>();

        foreach (var item in currentTrayItems)
        {
            // Burada her bir ürünün müşteri beklentilerine göre skorlandığını varsayıyoruz.
            // Bu skorlama fonksiyonu, JsonDataManager'dan gelen verilerle çalışır.
            // float itemStyleScore = StyleScoringSystem.CalculateScore(item, currentCustomer);
            float itemStyleScore = Random.Range(0, 100); // Örnek olarak rastgele bir skor üretiyoruz.

            // Skorlara göre renk kodlarını ve tatmin puanlarını alıp UI'da gösteriyoruz.
            var evaluationResult = GetEvaluationResultForScore(itemStyleScore);
            uiManager.ShowItemEvaluation(item, evaluationResult.color); // Yeşil, Sarı vb.
            
            totalSatisfactionScore += evaluationResult.satisfactionPoints;

            if (evaluationResult.isWinner)
            {
                winningItems.Add(item);
            }
        }
        
        // Karar anı
        if (totalSatisfactionScore >= currentCustomer.satisfaction_threshold)
        {
            HandleSuccessfulSale(winningItems);
        }
        else
        {
            HandleFailedTray();
        }
    }

    // --- Adım 7: Sonuç ve Stratejik Seçim ---
    private void HandleSuccessfulSale(List<Item> winningItems)
    {
        if (winningItems.Count == 0)
        {
             // Bu durum, eşik geçilmesine rağmen kazanan ürün olmamasını engeller. Güvenlik kontrolü.
             HandleFailedTray();
        }
        else if (winningItems.Count == 1)
        {
            // Tek kazanan varsa, otomatik olarak onu sat.
            FinalizeSale(winningItems[0]);
        }
        else
        {
            // Birden fazla kazanan varsa, oyuncuya stratejik seçim hakkı sun.
            uiManager.ShowStrategicChoiceScreen(winningItems); 
        }
    }

    public void FinalizeSale(Item itemSold)
    {
        // playerWallet.AddMoney(itemSold.price);
        // playerWallet.AddReputation(itemSold.reputation_gain);
        uiManager.ShowSaleCompleteScreen(itemSold);
        EndInteraction();
    }

    private void HandleFailedTray()
    {
        currentCustomerPatience -= 35; // Başarısız sunumun büyük sabır maliyeti
        uiManager.UpdatePatienceBar(currentCustomerPatience);
        
        if (currentCustomerPatience > 0)
        {
            uiManager.ShowFailedTrayDialogue();
            currentTrayItems.Clear();
            uiManager.ShowInventoryAndTrayScreen(); 
        }
        else
        {
            // Sabır bitti, müşteri gidiyor.
            // playerWallet.LoseReputation();
            uiManager.ShowCustomerLeavesAngryScreen();
            EndInteraction();
        }
    }
    
    // --- Yardımcı Fonksiyonlar ve Veri Yapıları ---

    // Bu fonksiyon, skorlara göre renk, puan ve kazanma durumunu döndürür.
    private (Color color, int satisfactionPoints, bool isWinner) GetEvaluationResultForScore(float score)
    {
        if (score >= 85) return (Color.green, 40, true);  // Perfect Match
        if (score >= 50) return (Color.yellow, 20, true); // Good/Acceptable Match
        if (score >= 25) return (new Color(1.0f, 0.5f, 0.0f), 5, false); // Orange - Weak Match
        return (Color.red, 0, false); // Bad Match
    }
    
    // Bu sahte class'lar, projenin diğer parçalarını temsil eder.
    public class Customer { public float satisfaction_threshold; public float base_patience; }
    public class Item { }
}