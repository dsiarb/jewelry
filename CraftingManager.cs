using UnityEngine;
using System.Collections.Generic;
using System.Linq;

// Bu script, YENİ "Modüler Komponent" sistemine göre üretim sürecini yönetir.
public class CraftingManager : MonoBehaviour
{
    // --- Gerekli Sistem Referansları ---
    [Header("System References")]
    public UIManager uiManager;
    public PlayerInventory playerInventory;
    // public JsonDataManager dataManager; // Komponent verilerini çekmek için

    // --- Üretim Seansı Değişkenleri ---
    private Blueprint currentBlueprint;
    private Dictionary<string, BaseComponent> selectedComponents = new Dictionary<string, BaseComponent>();
    private int currentSlotIndexToFill;
    
    private float totalPerformanceScore;

    // 1. ÜRETİMİ BAŞLATMA (Oyuncu, menüden bir blueprint seçince çağrılır)
    public void StartCraftingSession(Blueprint blueprint)
    {
        currentBlueprint = blueprint;
        selectedComponents.Clear();
        currentSlotIndexToFill = 0;
        totalPerformanceScore = 0f;

        // Blueprint'in ilk zorunlu slotu için komponent seçimini başlat.
        ShowNextComponentSelection();
    }

    // 2. SIRADAKİ KOMPONENT SEÇİMİNİ GÖSTERME
    private void ShowNextComponentSelection()
    {
        // Blueprint'teki tüm zorunlu slotlar doldu mu?
        if (currentSlotIndexToFill >= currentBlueprint.required_component_slots.Count)
        {
            // Tüm parçalar seçildi, üretim onayı ve mini oyunlara geç.
            OnAllComponentsSelected();
            return;
        }

        // Doldurulacak bir sonraki slotun bilgilerini al.
        var nextSlot = currentBlueprint.required_component_slots[currentSlotIndexToFill];
        
        // O slota uygun, oyuncunun kilidini açtığı komponentleri bul.
        // List<BaseComponent> availableComponents = dataManager.GetComponentsByCategory(nextSlot.component_category);
        // uiManager.ShowComponentSelectionScreen(nextSlot.slot_name, availableComponents, this);
    }

    // 3. OYUNCU BİR KOMPONENT SEÇTİĞİNDE (UI tarafından çağrılır)
    public void OnComponentSelected(string slotId, BaseComponent component)
    {
        selectedComponents[slotId] = component;
        
        // Bir sonraki slotu doldurmak için index'i artır.
        currentSlotIndexToFill++;
        // Döngüyü devam ettir.
        ShowNextComponentSelection();
    }

    // 4. TÜM KOMPONENTLER SEÇİLDİĞİNDE
    public void OnAllComponentsSelected()
    {
        // Seçilen tüm komponentlerden bir "üretim planı" (oynanacak mini oyunlar) oluşturulur.
        // List<string> miniGameSteps = GenerateStepsFromComponents(selectedComponents.Values.ToList());
        
        // Mini oyun sekansını başlatmak için CraftingManager'ın diğer bölümünü çağırabiliriz.
        // StartMiniGameSequence(miniGameSteps);
        
        Debug.Log("All components selected! Ready to start mini-games.");
        uiManager.ShowCraftingConfirmationScreen(selectedComponents.Values.ToList());
    }

    // Mini oyun döngüsü ve kalite hesaplama mantığı önceki tasarımlarımızdaki gibi devam eder...
    // (StartNextCraftingStep, OnMiniGameComplete, FinishCraftingSession vb.)

    // --- Bu sahte class'lar, projenin diğer parçalarını temsil eder. ---
    public class Blueprint { public List<Slot> required_component_slots; }
    public class Slot { public string slot_name; public string component_category; }
    public class BaseComponent { } // Gerçekte component_base, _setting, _addon için ayrı class'lar olur.
}