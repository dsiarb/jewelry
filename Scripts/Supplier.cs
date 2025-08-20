using System.Collections.Generic;

// Bu bir MonoBehaviour DEĞİLDİR, çünkü oyunda bir objeye takılmayacak.
// Sadece veri tutmak için bir kalıptır.
[System.Serializable]
public class Supplier
{
    public string supplier_id;
    public string name;
    public string supplier_type; // "Furniture", "Gems", "Materials", "Jewelry" etc.
    public int reputation_required;
    public List<string> inventory; // Satılan ürünlerin ID listesi
}

// Bu class, JSON dosyasının en üst seviyesini temsil eder (bir "suppliers" listesi içerir).
[System.Serializable]
public class SupplierList
{
    public List<Supplier> suppliers;
}