# Arayüz GDD: Personel Yönetimi (Staff Management)

## 1. Ana Konsept (High Concept)
* **Oyuncu Fantezisi:** Bir işletme sahibi gibi, ekibinin güçlü ve zayıf yönlerini analiz etme, onları doğru görevlere atama ve potansiyellerini ortaya çıkarmak için onlara yatırım (eğitim) yapma hissi.
* **Temel Mekanik:** İki panelli bir arayüz üzerinden çalışanların bilgilerini görüntüleme ve onlar üzerinde çeşitli yönetimsel eylemlerde bulunma.

## 2. Arayüz ve Akış (UI & Flow)
* **Erişim Noktası:**
    1.  Yönetim odasındaki bilgisayarda bulunan "Personelim" (My Staff) ikonu.
    2.  Dükkan veya atölyedeki bir çalışanın üzerine doğrudan tıklama.
* **Genel Yerleşim:** Ekran iki ana panele ayrılır:
    * **Sol Panel: Personel Listesi (Staff Roster):** İşe alınmış tüm çalışanların bir listesi.
    * **Sağ Panel: Çalışan Profili (Employee Profile):** Soldan seçilen çalışanın detaylı bilgileri ve eylem butonları.

### **Sol Panel: Personel Listesi Detayları**
* Her çalışan için şu bilgileri içeren bir satır veya kart bulunur:
    * **Portre ve Adı:** Örn: "Elif Kaya".
    * **Rolü:** Örn: "Satış Uzmanı".
    * **Seviyesi:** Örn: "Seviye 8".
    * **Durumu:** O anda ne yaptığını gösteren bir ikon (💰 Kasada, 🔨 Atölyede, 📚 Eğitimde, 休憩 Boşta).

### **Sağ Panel: Çalışan Profili Detayları**
* **A) Genel Bakış (Overview):**
    * Adı, Rolü, Seviyesi, Haftalık Maaşı.
    * **Moral (Morale):** Çalışanın mutluluğunu gösteren bir bar (Mutlu, Normal, Mutsuz).
    * **Özel Nitelik (Trait):** İşe alımda ortaya çıkan pozitif/negatif özellik (örn: "Hızlı Öğrenir").

* **B) Beceriler (Skills):**
    * 5 ana yeteneğin (`communication`, `commercial_intelligence`, `design_sense`, `crafting_skill`, `gem_cutting_precision`) tamamı listelenir.
    * Her yeteneğin yanında Seviyesi, bir sonraki seviye için XP Barı ve 1-100'lük net sayısal değeri görünür.

* **C) Eylemler (Actions):**
    * Oyuncunun çalışan üzerinde yapabileceği eylemleri içeren butonlar:
        * **"Görev Ata" (Assign Task):** Çalışanı uygun bir iş istasyonuna (kasa, makine) atar.
        * **"Eğitime Gönder" (Send to Training):** `unlock_employee_training` araştırması açıldıktan sonra aktif olur. Tıklandığında, çalışanın rolüne göre "Usta Eğitimi" seçenekleri sunulur:
            * **Eğer çalışan Üretim rolündeyse (Artisan, Gem Cutter):**
                * "Usta Artin'den Üretim Dersi Aldır" (Maliyet: Zaman/Kaynak, Sonuç: `crafting_skill` XP'si artar).
                * "Usta Artin'den Taş Kesim Dersi Aldır" (Sonuç: `gem_cutting_precision` XP'si artar).
            * **Eğer çalışan Satış/İşletme rolündeyse (Sales Specialist):**
                * "Izzy'den Marka ve Satış Dersi Aldır" (Maliyet: Para/Zaman, Sonuç: `communication` ve `commercial_intelligence` XP'si artar).
        * **"Maaşı Gözden Geçir" (Review Salary):** Çalışanın moralini artırmak için maaş zammı yapma ekranını açar.
        * **"Terfi Et" (Promote):** Belirli koşulları sağlayan çalışanı bir üst role terfi ettirir.
        * **"İşten Çıkar" (Fire):** Çalışanla olan iş sözleşmesini sonlandırır.

## 3. Sistemsel Bağlantılar
* **Veri Kaynağı:** Tüm bilgiler, `EmployeeManager` tarafından yönetilen ve `employee_stats.json` ile `story_characters.json` dosyalarından beslenen çalışan objelerinden çekilir.
* **Sonuçlar:** Yapılan eylemler (`Eğitime Gönder` vb.),