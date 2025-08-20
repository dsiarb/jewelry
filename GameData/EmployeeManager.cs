using UnityEngine;
using System.Collections.Generic;
using System.Linq;

// Bu script, oyundaki tüm çalışanları, işe alım ve görev yönetimi süreçlerini yöneten ana merkezdir.
public class EmployeeManager : MonoBehaviour
{
    public static EmployeeManager Instance { get; private set; }

    // --- Referanslar ve Listeler ---
    [Header("System References")]
    public UIManager uiManager;
    public PlayerWallet playerWallet;
    public GameTimeManager gameTimeManager;
    public PlayerEnergy playerEnergy;

    [Header("Employee Data")]
    private List<Employee> hiredEmployees = new List<Employee>();
    // Adayları, görüşülüp görüşülmediğini belirten bir yapıda tutuyoruz.
    private Dictionary<EmployeeCandidate, bool> availableCandidates = new Dictionary<EmployeeCandidate, bool>();
    // Her çalışanın bir sonraki gün için görev kuyruğunu tutar.
    private Dictionary<string, Queue<Task>> nextDayTaskQueue = new Dictionary<string, Queue<Task>>();

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

    // === İŞE ALIM FONKSİYONLARI (GELİŞMİŞ SİSTEM) ===

    public void GenerateNewCandidates()
    {
        availableCandidates.Clear();
        // Burada, JSON'dan veya bir fabrika metodundan rastgele adayların üretildiğini varsayıyoruz.
        // Örnek: availableCandidates.Add(new EmployeeCandidate(...), false);
        Debug.Log("New unscreened candidates generated for the Job Board.");
        uiManager.UpdateJobBoard(GetCandidatePreviews());
    }

    public void InterviewCandidate(EmployeeCandidate candidate)
    {
        float interviewTimeCost = 60f; // Görüşmenin 60 dakika sürdüğünü varsayalım.
        float interviewMoneyCost = 50f; // Görüşme için bir ön ödeme.

        if (playerWallet.HasEnoughMoney(interviewMoneyCost))
        {
            playerWallet.SpendMoney(interviewMoneyCost);
            gameTimeManager.AdvanceTime(interviewTimeCost);

            if (availableCandidates.ContainsKey(candidate))
            {
                availableCandidates[candidate] = true; // Adayı "görüşüldü" olarak işaretle.
                Debug.Log("Interview completed with " + candidate.Name + ". Full stats are now visible.");

                // Arayüzü, adayın tüm detaylarını gösterecek şekilde güncelle.
                uiManager.UpdateJobBoard(GetCandidatePreviews());
                uiManager.ShowDetailedCandidateView(candidate);
            }
        }
        else
        {
            uiManager.ShowNotification("You don't have enough money to conduct an interview.");
        }
    }

    public void HireEmployee(EmployeeCandidate candidate)
    {
        // Güvenlik kontrolü: Sadece görüşülmüş bir aday işe alınabilir.
        if (!availableCandidates.ContainsKey(candidate) || availableCandidates[candidate] == false)
        {
            uiManager.ShowNotification("You must interview a candidate before hiring them.");
            return;
        }
        
        // Maaş kontrolü de eklenebilir.
        Employee newEmployee = new Employee(candidate); // Adaydan yeni bir çalışan yarat.
        hiredEmployees.Add(newEmployee);
        availableCandidates.Remove(candidate);

        Debug.Log(candidate.Name + " has been hired!");
        uiManager.UpdateEmployeeList(hiredEmployees);
        uiManager.UpdateJobBoard(GetCandidatePreviews());
    }

    // === GÖREV ATAMA FONKSİYONLARI ===

    public void AssignTaskForNextDay(string employeeId, Task task)
    {
        if (!nextDayTaskQueue.ContainsKey(employeeId))
        {
            nextDayTaskQueue[employeeId] = new Queue<Task>();
        }
        nextDayTaskQueue[employeeId].Enqueue(task);
        Debug.Log("Task " + task.Name + " scheduled for " + GetEmployeeById(employeeId).Name + " for the next day.");
    }

    public void AssignTaskImmediately(Employee employee, Task task)
    {
        if (employee == null || !employee.IsIdle)
        {
            uiManager.ShowNotification("This employee is currently busy.");
            return;
        }

        float energyCostForPlayer = 5f; // Anlık yönetim oyuncu enerjisi harcar.
        if (playerEnergy.HasEnoughEnergy(energyCostForPlayer))
        {
            playerEnergy.ConsumeEnergy(energyCostForPlayer);
            employee.AssignImmediateTask(task);
            Debug.Log("Immediate task " + task.Name + " assigned to " + employee.Name);
        }
        else
        {
            uiManager.ShowNotification("You are too tired to manage your staff right now.");
        }
    }

    public void OnBusinessHoursStart()
    {
        foreach (var employee in hiredEmployees)
        {
            if (nextDayTaskQueue.ContainsKey(employee.Id))
            {
                employee.SetDailySchedule(nextDayTaskQueue[employee.Id]);
                nextDayTaskQueue[employee.Id].Clear();
            }
            employee.StartWorkDay();
        }
    }

    // === GELİŞİM VE DİĞER FONKSİYONLAR ===

    public void AddExperienceToEmployee(string employeeId, string skillId, float xpAmount)
    {
        // İlgili çalışanı bul ve XP ekle.
    }

    public void ProcessWeeklyPayroll()
    {
        // Tüm çalışanların maaşlarını öde.
    }

    // === YARDIMCI FONKSİYONLAR ===

    public Employee GetEmployeeById(string employeeId)
    {
        return hiredEmployees.FirstOrDefault(e => e.Id == employeeId);
    }
    
    public List<CandidatePreview> GetCandidatePreviews()
    {
        return availableCandidates.Select(kvp => new CandidatePreview(kvp.Key, kvp.Value)).ToList();
    }
}


// --- BU SAHTE (PLACEHOLDER) CLASS'LAR, PROJENİN DİĞER PARÇALARINI TEMSİL EDER ---

public class Employee
{
    public string Id;
    public string Name;
    public bool IsIdle = true;
    public Employee(EmployeeCandidate candidate) { /* ... */ }
    public void AssignImmediateTask(Task task) { /* ... */ }
    public void SetDailySchedule(Queue<Task> tasks) { /* ... */ }
    public void StartWorkDay() { /* ... */ }
}

public class EmployeeCandidate
{
    public string Name;
    public string Role;
    // ... Diğer tüm detaylı özellikleri
}

public struct CandidatePreview
{
    public EmployeeCandidate Candidate;
    public bool IsInterviewed;
    public CandidatePreview(EmployeeCandidate candidate, bool isInterviewed)
    {
        Candidate = candidate;
        IsInterviewed = isInterviewed;
    }
}

public class Task { public string Name; }
public class EmployeeStatsData { }
public class UIManager : MonoBehaviour {
    public void UpdateJobBoard(List<CandidatePreview> previews) { }
    public void ShowDetailedCandidateView(EmployeeCandidate candidate) { }
    public void UpdateEmployeeList(List<Employee> employees) { }
    public void ShowNotification(string message) { }
}
public class PlayerWallet : MonoBehaviour {
    public bool HasEnoughMoney(float amount) { return true; }
    public void SpendMoney(float amount) { }
}
public class GameTimeManager : MonoBehaviour {
    public void AdvanceTime(float minutes) { }
}
public class PlayerEnergy : MonoBehaviour {
    public bool HasEnoughEnergy(float amount) { return true; }
    public void ConsumeEnergy(float amount) { }
}