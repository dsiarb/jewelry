using UnityEngine;
using System.Collections.Generic;
using System.Linq; // Bu satır, veriler üzerinde sorgulama yapmak için eklenmiştir.

public class SupplierManager : MonoBehaviour
{
    // Singleton Pattern - Bu script'e her yerden kolayca erişmek için.
    public static SupplierManager Instance { get; private set; }

    // Tüm tedarikçilerin yüklendiği ve tutulduğu ana liste.
    private List<Supplier> allSuppliers = new List<Supplier>();

    void Awake()
    {
        // Singleton kurulumu
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Bu fonksiyon, JsonDataManager tarafından oyunun en başında çağrılır.
    public void LoadSuppliers(List<Supplier> suppliersFromJson)
    {
        allSuppliers = suppliersFromJson;
        Debug.Log(allSuppliers.Count + " suppliers loaded into SupplierManager.");
    }

    // Belirli bir tipteki tüm tedarikçileri getiren fonksiyon.
    // Örneğin, oyuncu mobilya almak istediğinde bu çağrılır.
    public List<Supplier> GetSuppliersByType(string type)
    {
        // LINQ sorgusu ile 'allSuppliers' listesi içinde, supplier_type'ı eşleşenleri bul ve yeni bir liste olarak döndür.
        return allSuppliers.Where(s => s.supplier_type == type).ToList();
    }

    // Belirli bir ID'ye sahip tek bir tedarikçiyi getiren fonksiyon.
    public Supplier GetSupplierById(string id)
    {
        return allSuppliers.FirstOrDefault(s => s.supplier_id == id);
    }
}