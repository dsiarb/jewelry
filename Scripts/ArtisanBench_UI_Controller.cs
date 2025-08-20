using UnityEngine;
using System.Collections.Generic;

// Bu script, Zanaatkâr Masası'ndaki tüm kullanıcı arayüzü etkileşimlerini yönetir.
public class ArtisanBench_UI_Controller : MonoBehaviour
{
    // --- Gerekli Sistem Referansları ---
    [Header("System References")]
    public CraftingManager craftingManager; // Üretim mantığını yöneten ana script
    // public JsonDataManager dataManager;
    // public PlayerInventory playerInventory;

    // --- Arayüz Objeleri (Unity'den Sürüklenecek) ---
    [Header("UI Elements")]
    public GameObject orderDossier_Button; // Sipariş Dosyası
    public GameObject materialCabinet_UI; // Malzeme Dolabı Arayüzü
    public GameObject consumablesGauge_UI; // Sarf Malzemeleri Göstergesi
    public GameObject mastersLoupe_Button; // Usta Büyüteci

    // Atölye masasının mevcut durumunu takip etmek için
    private enum BenchState { Idle, SelectingBlueprint, SelectingComponents, PostCraftInspection }
    private BenchState currentState;

    void Start()
    {
        // Başlangıçta masa boştur, oyuncu bir blueprint seçmelidir.
        currentState = BenchState.Idle;
        UpdateUI_ForCurrentState();
    }

    // Arayüzü, mevcut duruma göre güncelleyen ana fonksiyon
    private void UpdateUI_ForCurrentState()
    {
        // Hangi durumda hangi butonların/panellerin aktif olacağını belirler.
        orderDossier_Button.SetActive(currentState == BenchState.Idle);
        materialCabinet_UI.SetActive(currentState == BenchState.SelectingComponents);
        mastersLoupe_Button.SetActive(currentState == BenchState.PostCraftInspection);
    }
    
    // === OYUNCU EYLEM FONKSİYONLARI ===

    // Oyuncu, Sipariş Dosyası'na tıkladığında çağrılır.
    public void OnOrderDossierClicked()
    {
        currentState = BenchState.SelectingBlueprint;
        // Blueprint'leri listeleyen UI'ı aç
        // uiManager.ShowBlueprintSelection(dataManager.GetAllBlueprints());
    }

    // Oyuncu, Blueprint Seçim Ekranı'ndan bir blueprint seçtiğinde çağrılır.
    public void OnBlueprintSelected(Blueprint selectedBlueprint)
    {
        // Seçilen blueprint ile CraftingManager'da yeni bir seans başlat.
        craftingManager.StartCraftingSession(selectedBlueprint);
        
        currentState = BenchState.SelectingComponents;
        UpdateUI_ForCurrentState();
    }

    // Oyuncu, üretim bittikten sonra Büyüteç'e tıkladığında çağrılır.
    public void OnMastersLoupeClicked()
    {
        // Son üretilen ürünün kalite detaylarını gösteren ekranı aç.
        // uiManager.ShowQualityReport(craftingManager.GetLastCraftedItem());
    }
}