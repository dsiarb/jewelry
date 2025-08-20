using UnityEngine;
using System.Collections.Generic;
using System.Linq;

// Bu script, oyundaki tüm çalışanları ve işe alım sürecini yöneten ana merkezdir.
public class EmployeeManager : MonoBehaviour
{
    public static EmployeeManager Instance { get; private set; }

    // --- Referanslar ve Listeler ---
    public UIManager uiManager;
    public PlayerWallet playerWallet;
    // public GameTimer gameTimer; // Oyun zamanını yöneten sistem

    private List<Employee> hiredEmployees = new List<Employee>();
    
    // Artık adayları, görüşülüp görüşülmediğini belirten bir yapıda tutuyoruz.
    private Dictionary<EmployeeCandidate, bool> availableCandidates = new Dictionary<EmployeeCandidate, bool>();

    private EmployeeStatsData employeeStatsData; 

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); }
        else { Instance = this; }
    }

    public void Initialize(EmployeeStatsData statsData)
    {
        employeeStatsData = statsData;
        GenerateNewCandidates(); 
    }

    // --- İşe Alım (Hiring) Fonksiyonları ---

    public void GenerateNewCandidates()
    {
        availableCandidates.Clear();
        // Rastgele 3 yeni aday oluşturulduğunu ve "görüşülmedi" (false) olarak işaretlendiğini varsayalım.
        // EmployeeCandidate candidate1 = new EmployeeCandidate(...);
        // availableCandidates.Add(candidate1, false); 
        
        Debug.Log("New unscreened candidates generated for the Job Board.");
        // UI'a sadece limitli bilgileri gönder.
        uiManager.UpdateJobBoard(GetCandidatePreviews());
    }

    // YENİ FONKSİYON: İş Görüşmesi Yapma
    public void InterviewCandidate(EmployeeCandidate candidate)
    {
        float interviewTimeCost = 1.0f; // Görüşmenin 1 saat sürdüğünü varsayalım.
        // gameTimer.ConsumeTime(interviewTimeCost); // Zaman maliyetini uygula

        // Adayı "görüşüldü" olarak işaretle.
        if (availableCandidates.ContainsKey(candidate))
        {
            availableCandidates[candidate] = true;
            Debug.Log("Interview completed with " + candidate.Name + ". Full stats are now visible.");

            // Arayüzü, bu adayın artık tüm detaylarını gösterecek şekilde güncelle.
            uiManager.UpdateJobBoard(GetCandidatePreviews()); // Liste güncellenir.
            uiManager.ShowDetailedCandidateView(candidate); // Detaylı CV görünümü açılır.
        }
    }

    // Oyuncu, UI'dan bir adayı işe aldığında bu fonksiyon çağrılır.
    public void HireEmployee(EmployeeCandidate candidate)
    {
        // Sadece görüşülmüş bir aday işe alınabilir. Bu bir güvenlik kontrolü.
        if (!availableCandidates.ContainsKey(candidate) || availableCandidates[candidate] == false)
        {
            Debug.LogError("Cannot hire a candidate who has not been interviewed.");
            return;
        }

        Employee newEmployee = new Employee(candidate);
        hiredEmployees.Add(newEmployee);
        availableCandidates.Remove(candidate);

        Debug.Log(candidate.Name + " has been hired!");
        uiManager.UpdateEmployeeList();
        uiManager.UpdateJobBoard(GetCandidatePreviews()); // Aday artık listede görünmez.
    }

    // UI için adayların önizleme verisini hazırlayan yardımcı fonksiyon.
    public List<CandidatePreview> GetCandidatePreviews()
    {
        List<CandidatePreview> previews = new List<CandidatePreview>();
        foreach (var candidateEntry in availableCandidates)
        {
            previews.Add(new CandidatePreview(candidateEntry.Key, candidateEntry.Value)); // Adayı ve görüşüldü bilgisini gönder.
        }
        return previews;
    }
    
    // --- Diğer Yönetim Fonksiyonları (Gelişim, Maaş vb. öncekiyle aynı kalır) ---
    public void AddExperienceToEmployee(string employeeId, string skillId, float xpAmount) { /* ... */ }
    public void ProcessWeeklyPayroll() { /* ... */ }
    public List<Employee> GetHiredEmployees() { return hiredEmployees; }
}

// --- Bu sahte class'lar ve struct'lar, projenin diğer parçalarını temsil eder. ---
public class Employee
{
    public string Name;
    public string Role;
    public Employee(EmployeeCandidate candidate) { /* ... */ }
    // ...
}

// Adayın tüm detaylarını içeren ana veri yapısı.
public class EmployeeCandidate 
{
    public string Name;
    public string Role;
    public int Level;
    public int SalaryExpectation;
    public Dictionary<string, int> StarRatings; // Ana yeteneklerin yıldızları (Önizlemede gösterilir)
    public Dictionary<string, int> FullStats; // Tüm 5 yeteneğin 1-100'lük değeri (Görüşmeden sonra görünür)
    public string Trait; // Gizli özellik (Görüşmeden sonra görünür)
}

// Adayın sadece önizleme bilgilerini UI'a göndermek için kullanılan basit veri yapısı.
public struct CandidatePreview
{
    public EmployeeCandidate Candidate;
    public bool IsInterviewed;
    public CandidatePreview(EmployeeCandidate candidate, bool isInterviewed)
    {
        Candidate = candidate;
        IsInterviewed = isInterviewed;