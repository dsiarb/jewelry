# Mini Oyun GDD: Cilalama (Polishing)

## 1. Ana Konsept (High Concept)
* **Oyuncu Fantezisi:** Bir zanaatkârın, bir mücevhere son, mükemmel parlaklığını verirken gösterdiği odaklanma ve "doğru anı yakalama" hissi.
* **Temel Mekanik:** Sırayla parlayan hedeflere, parlaklığın en üst seviyeye ulaştığı anda tıklamaya dayalı bir zamanlama ve refleks oyunu.

## 2. Oynanış Döngüsü (Gameplay Loop)
* **Oyuncunun Amacı:** Mücevherin yüzeyindeki tüm belirlenmiş bölgelerin parlaklığını, doğru zamanda tıklayarak "kilitlemek".
* **Adım Adım Oynanış:**
    1.  Mini oyun başlar. Ekranda, üretilen mücevherin mat bir silüeti ve üzerinde parlayacak olan birkaç "hedef bölge" belirir.
    2.  Hedef bölgelerden biri, soluk bir ışıltıyla başlayarak yavaşça parlamaya başlar.
    3.  Bu parlaklık, kısa bir anlığına göz alıcı bir "yıldız" 🌟 efektine ulaşır ve hemen ardından sönmeye başlar.
    4.  Oyuncunun görevi, bölge tam o en parlak "yıldız" anına ulaştığı anda o bölgeye tıklamaktır.
    5.  Başarılı bir tıklama, o bölgenin parlaklığını kalıcı olarak "kilitler".
    6.  Sıra, bir sonraki hedef bölgeye geçer. Tüm bölgeler kilitlenene kadar bu devam eder.
* **Kontroller:** Tek Tuş (Sol Fare Tıkı).

## 3. Arayüz ve Geri Bildirim (UI & Feedback)
* **Arayüz Elemanları (UI):** Mücevherin silüeti, hedef bölgeler, parlayan ışık ve "yıldız" efekti. Ekranın bir köşesinde "Parlatılan Bölgeler: 3/5" gibi bir sayaç.
* **Görsel Geri Bildirim:** "Mükemmel" zamanlamada göz alıcı bir parlama ve yıldız efekti. "İyi" zamanlamada daha sönük bir parlaklık. "Kötü" zamanlamada ise bölgenin mat kalması veya hafifçe parlaması.
* **İşitsel Geri Bildirim:** "Mükemmel" zamanlamada tatmin edici bir "çınlama" veya "zil" sesi. "İyi" zamanlamada daha basit bir "klik" sesi. "Kötü" zamanlamada ise boğuk bir ses.

## 4. Puanlama ve Gelişim (Scoring & Progression)
* **Puanlama Sistemi:**
    * Her bir hedef bölgeye tıklama zamanlamasına göre bir puan verilir (Perfect, Good, Poor).
    * Mini oyunun sonundaki tüm bölgelerin puanlarının ortalaması alınarak 0.0-1.0 arasında bir `performanceScore` hesaplanır.
* **Zorluk Seviyesinin Artması:**
    * Daha karmaşık tasarımlar, daha fazla sayıda ve daha küçük hedef bölgelere sahip olur.
    * Oyuncunun `crafting_skill`'i arttıkça, parlama anları daha hızlı gerçekleşir, bu da daha iyi refleksler gerektirir.
* **Gerekli Oyuncu Yetenekleri:** Yüksek `production_skill` (Üretim Becerisi), "mükemmel" zamanlama aralığını bir miktar genişletebilir.

## 5. Sistemsel Çıktı (System Output)
* **Ödül:** `CraftingManager`'a, ürünün "Parlaklık Kalitesi"ne eklenecek olan bir `performanceScore` değeri döndürülür. Bu, ürünün nihai satış fiyatını doğrudan etkiler.
* **Ceza:** Düşük bir toplam performans, ürünün "donuk"