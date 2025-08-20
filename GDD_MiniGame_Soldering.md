# Mini Oyun GDD: Lehimleme (Soldering)

## 1. Ana Konsept (High Concept)
* **Oyuncu Fantezisi:** Bir pürmüzün (torch) gücünü kontrol ederek, mücevheri eritme riskini göze alıp, iki metal parçasını kusursuzca birleştiren bir ustanın odaklanma ve kontrol hissi.
* **Temel Mekanik:** Bir sıcaklık göstergesini, ritmik ve kontrollü girdilerle (input) dar bir "ideal bölge" içinde tutmaya dayalı bir denge oyunu.

## 2. Oynanış Döngüsü (Gameplay Loop)
* **Oyuncunun Amacı:** Sıcaklık göstergesini, belirlenen süre boyunca "İdeal Akış Sıcaklığı" (yeşil bölge) içinde tutarak lehimin temiz bir şekilde akmasını sağlamak.
* **Adım Adım Oynanış:**
    1.  Mini oyun başlar. Ekranda, birleştirilecek mücevher parçası ve bir sıcaklık göstergesi belirir.
    2.  Oyuncu, bir tuşa (örn: Sol Fare Tuşu) ritmik olarak basıp bırakarak ısıyı kontrol eder.
    3.  Her basış, ısıyı bir miktar artırır. Basmayı bırakmak, ısının yavaşça düşmesine neden olur.
    4.  Oyuncu, doğru ritmi bularak göstergeyi yeşil bölgede tutmaya çalışır.
    5.  Ekranda bir "İlerleme Çubuğu" (Progress Bar) dolar. Bu çubuk, oyuncu yeşil bölgedeyken daha hızlı dolar.
    6.  İlerleme çubuğu %100 olduğunda mini oyun başarıyla biter.
* **Kontroller:** Tek Tuş (Basma ve Bırakma).

## 3. Arayüz ve Geri Bildirim (UI & Feedback)
* **Arayüz Elemanları (UI):** Dikey bir Sıcaklık Göstergesi. Bu gösterge üzerinde renkli bölgeler bulunur: Mavi (Soğuk), Yeşil (İdeal), Turuncu (Aşırı Isı), Kırmızı (Erime Tehlikesi). Bir İlerleme Çubuğu.
* **Görsel Geri Bildirim:** Gösterge yeşil bölgedeyken, mücevherin birleşme noktasında temiz ve parlak bir lehim animasyonu görünür. Turuncu bölgedeyken, metalin üzerinde hafif bir kararma (oksitlenme) efekti belirir. Kırmızı bölgeye ulaşırsa, mücevherde gözle görülür bir deformasyon veya "erime" efekti oluşur.
* **İşitsel Geri Bildirim:** Pürmüzün (torch) ritmik "pıslama" sesi. Yeşil bölgedeyken hafif bir "çıtırdama" sesi. Kırmızı bölgeye yaklaşıldığında artan bir "tehlike" sesi.

## 4. Puanlama ve Gelişim (Scoring & Progression)
* **Puanlama Sistemi:**
    * Puan, tamamen İlerleme Çubuğu dolana kadar geçen sürenin ne kadarının yeşil bölgede geçirildiğine göre hesaplanır.
    * Turuncu bölgede geçirilen her saniye, final puandan düşürür.
    * Kırmızı bölgeye ulaşmak, mini oyunun anında başarısız olmasına ve `performanceScore`'un 0 olmasına neden olur.
* **Zorluk Seviyesinin Artması:**
    * Daha zorlu metaller (örn: Platin) veya daha ince parçalar, daha dar bir "yeşil bölgeye" ve daha hassas bir ısı kontrolüne (gösterge daha hızlı yükselip alçalır) sahip olur.
* **Gerekli Oyuncu Yetenekleri:** Yüksek `crafting_skill`, "yeşil bölgeyi" bir miktar genişletebilir veya ısı kontrolünü daha stabil hale getirebilir.

## 5. Sistemsel Çıktı (System Output)
* **Ödül:** `CraftingManager`'a, ürünün "Birleşim Kalitesi"ne eklenecek olan bir `performanceScore` değeri döndürülür.
* **Ceza:** Kırmızı bölgeye ulaşmak, ürünün erimesine ve kullanılan malzemenin bir kısmının israf olmasına neden olabilir. Düşük performans ise, üründe temizlenmesi gereken (belki ekstra bir Zımparalama süresi gerektiren) bir leke bırakır.