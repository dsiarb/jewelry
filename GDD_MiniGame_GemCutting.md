# Mini Oyun GDD: Taş Kesme (Gem Cutting)

## 1. Ana Konsept (High Concept)
* **Oyuncu Fantezisi:** Bir ham taş dehası gibi, önce taşın potansiyelini görmek, en iyi kesim planını yapmak ve ardından bu planı bir cerrah hassasiyetiyle uygulamak.
* **Temel Mekanik:** İki aşamalı bir mini oyun: Önce stratejik "Planlama", ardından beceriye dayalı "Uygulama".

## 2. Oynanış Döngüsü (Gameplay Loop)
* **Oyuncunun Amacı:** Ham bir taştan, kusurları temizleyerek ve doğru fasetleri (yüzeyleri) oluşturarak maksimum değerde parlak bir mücevher elde etmek.

### **Aşama 1: Planlama (Pattern Yerleştirme)**
* **Arayüz:** Ham taşın iç yapısını gösteren X-ray benzeri bir görünüm. Taşın içindeki "kusurlar" görünür. Oyuncuya birkaç kesim deseni (Yuvarlak, Zümrüt vb.) sunulur.
* **Oynanış:**
    1. Oyuncu, sunulan desenlerden birini seçer.
    2. Deseni, taşın üzerinde döndürerek ve konumlandırarak en ideal yere yerleştirir.
    3. Amaç, maksimum kusuru desenin dışında bırakırken, en yüksek ağırlığı (karatı) desenin içinde tutmaktır.
* **Sonuç:** Bu aşamadaki karar, taşın potansiyel **"Karat"** ve **"Berraklık"** puanlarını belirler.

### **Aşama 2: Uygulama (Kesme)**
* **Arayüz:** Planın onaylandığı taşa yakınlaşılır. Ekranda, oyuncunun yerleştirdiği desenin ana hatları ve kritik açı noktaları belirir.
* **Oynanış:** Bu aşama, iki farklı beceriyi test eden birbiri ardına gelen adımlardan oluşur:
    1.  **"Açı Kilitleme" (Tıklama):** Taşın ana ve en kritik fasetleri için, daha önce konuştuğumuz "Açı Kilitleme" mekaniği devreye girer. Ekranda bir açı ölçer belirir ve oyuncu, hareket eden bir gösterge doğru açıya geldiğinde tıklayarak ana kesimleri yapar.
    2.  **"Desen Takip Etme" (Takip):** Ana açılar kilitlendikten sonra, bu ana fasetleri birbirine bağlayan daha küçük ve daha detaylı çizgiler belirir. Oyuncu, fareyi kullanarak bu çizgileri hassas bir şekilde takip eder.
* **Kontroller:** Fare Sol Tık (Tıklama ve Basılı Tutup Sürükleme).

## 3. Arayüz ve Geri Bildirim (UI & Feedback)
* **Arayüz Elemanları (UI):** X-ray görünümü, kesim desenleri, açı ölçer, desen çizgileri, ilerleme çubuğu.
* **Görsel Geri Bildirim:** Başarılı açı kilitlemede "kilit" animasyonu. Hassas çizgi takibinde parlak bir iz. Hatalı kesimlerde küçük bir "çatlak" veya "kıymık" efekti.
* **İşitsel Geri Bildirim:** Hassas kesim sırasında sürekli bir "vızıldama" sesi. Başarılı kilitlenmede net bir "klik" sesi. Hatalı kesimde rahatsız edici bir "gıcırtı" sesi.

## 4. Puanlama ve Gelişim (Scoring & Progression)
* **Puanlama Sistemi:**
    * **Planlama Puanı:** 1. Aşamada korunan karat ve temizlenen kusurlara göre hesaplanır.
    * **Uygulama Puanı:** 2. Aşamada yapılan açı kilitleme ve desen takip etme hassasiyetine göre hesaplanır.
    * Bu iki puanın birleşimi, taşın nihai değerini (Berraklık, Kesim, Karat) belirler.
* **Zorluk Seviyesinin Artması:** Daha değerli ham taşlar, daha karmaşık iç kusur yapılarına, daha fazla sayıda ve daha hassas kilitlenecek açılara sahip olur.
* **Gerekli Oyuncu Yetenekleri:** Yüksek `gem_cutting_precision` yeteneği, "Açı Kilitleme" sırasında zamanı yavaşlatabilir veya "Desen Takip Etme" sırasında imlecin hassasiyetini artırabilir.

## 5. Sistemsel Çıktı (System Output)
* **Ödül:** Oyuncunun envanterine, kalitesi ve değeri tamamen kendi performansı tarafından belirlenen, eşsiz bir "kesilmiş taş" eklenir.
* **Ceza:** Kötü bir performans (hem planlama hem uygulama), değerli bir ham taşın çok düşük kaliteli veya hatta "kırık" (değersiz) bir parçaya dönüşmesine neden olabilir.