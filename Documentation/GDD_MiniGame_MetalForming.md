# Mini Oyun GDD: Metal Şekillendirme (Metal Forming)

## 1. Ana Konsept (High Concept)
* **Oyuncu Fantezisi:** Bir metal parçasını, ritmik ve güçlü çekiç darbeleriyle, bir heykeltıraş gibi istenen forma getirme hissi.
* **Temel Mekanik:** Üretilen ürünün silüetine uyum sağlayan, ritim ve zamanlama tabanlı bir mini oyun.

## 2. Oynanış Döngüsü (Gameplay Loop)
* **Oyuncunun Amacı:** Merkezden yayılan bir nabız dalgası, ürünün kenarlarındaki hedef noktalarına ulaştığı anda tuşa basarak metalin şeklini kusursuz hale getirmek.
* **Adım Adım Oynanış:**
    1.  Mini oyun başlar. Ekranda, üretilecek ürünün (yüzük, kolye ucu vb.) mat bir silüeti belirir.
    2.  Silüetin kenarlarında sabit "hedef noktaları" görünür.
    3.  Şeklin geometrik merkezinden dışarıya doğru, düzenli aralıklarla "nabız" dalgaları yayılmaya başlar.
    4.  Oyuncu, her bir nabız dalgası tam hedef noktalarından birinin üzerine geldiği anda tuşa basar.
    5.  Tüm hedef noktaları, başarılı bir vuruşla "aktif hale getirilene" kadar döngü devam eder.
* **Kontroller:** Tek Tuş (Boşluk Tuşu veya Sol Fare Tıkı).

## 3. Arayüz ve Geri Bildirim (UI & Feedback)
* **Arayüz Elemanları (UI):** Ürünün adaptif silüeti, silüet üzerindeki hedef noktaları, merkezden yayılan nabız dalgası efekti.
* **Görsel Geri Bildirim:** Başarılı bir vuruşta, hedef noktasında bir çekiç animasyonu ve parlama efekti belirir; hedef noktası yeşile döner. Başarısız vuruşta, noktada kırmızı bir "çatlak" efekti belirir.
* **İşitsel Geri Bildirim:** Başarılı vuruşta tok ve tatmin edici bir "metal dövme" sesi. Ritimle uyumlu bir arka plan müziği. Başarısız vuruşta tiz bir "çınlama" veya yanlış nota sesi.

## 4. Puanlama ve Gelişim (Scoring & Progression)
* **Puanlama Sistemi:**
    * Her hedef noktası için zamanlama puanlanır (Perfect, Good, Miss).
    * Mini oyun sonundaki tüm noktaların ortalaması alınarak 0.0-1.0 arasında bir `performanceScore` hesaplanır.
* **Zorluk Seviyesinin Artması:**
    * Daha karmaşık ürünler, daha fazla ve daha girintili hedef noktalarına sahip olur.
    * Oyuncunun `crafting_skill`'i arttıkça, nabız dalgasının hızı artar, ancak "Perfect" vuruş için zamanlama aralığı da daralarak daha yüksek beceri gerektirir.
* **Gerekli Oyuncu Yetenekleri:** Yüksek `crafting_skill`, "Perfect" vuruşlardan kazanılan kalite bonusunu artırabilir.

## 5. Sistemsel Çıktı (System Output)
* **Ödül:** `CraftingManager`'a, ürünün "Form Kalitesi"ne eklenecek olan bir `performanceScore` değeri döndürülür.
* **Ceza:** Çok sayıda başarısız vuruş, ürünün kalitesini düşürür ve en kötü senaryoda, malzemenin kullanılamaz hale gelmesine ve israf olmasına neden olabilir.