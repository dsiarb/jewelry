# Mini Oyun GDD: Taş Mıhlama (Stone Setting)

## 1. Ana Konsept (High Concept)
* **Oyuncu Fantezisi:** Değerli bir taşı, bir sanat eseri olan mücevherin üzerine, bir cerrah hassasiyetiyle yerleştiren usta bir zanaatkâr olma hissi.
* **Temel Mekanik:** Görsel bir sıralamayı hafızada tutup, doğru zamanlama ve hassasiyetle tekrar etme.

## 2. Oynanış Döngüsü (Gameplay Loop)
* **Oyuncunun Amacı:** Taşın etrafındaki metal tırnakları (prongs), gösterilen doğru sırada ve doğru basınçla kapatarak taşı yuvaya kusursuzca sabitlemek.
* **Adım Adım Oynanış:**
    1.  **Gözlem Aşaması:** Oyun başlar. Tırnakların üzerinde, izlemesi gereken yolu gösteren bir "Rehber Işık" animasyonu 2-3 saniye boyunca oynatılır.
    2.  **Uygulama Aşaması:** Işık animasyonu kaybolur. Oyuncu, hafızasındaki sırayı takip ederek tırnaklara tek tek tıklar.
    3.  **(Opsiyonel Beceri):** Her tıklamada, tırnak üzerinde küçülen bir halka belirir. Oyuncu, halka ideal boyuta geldiğinde fareyi bırakarak "mükemmel basınç" uygulamaya çalışır.
    4.  **Yardımcı Mekanik:** Oyuncu sırayı unutursa, oyun zamanına mal olan "Hatırlat" butonuna basarak ışık animasyonunu 1 saniyeliğine tekrar görebilir.
* **Kontroller:** Fare Sol Tık (Tıklama ve Basılı Tutup Bırakma).

## 3. Arayüz ve Geri Bildirim (UI & Feedback)
* **Arayüz Elemanları (UI):** Taş yuvasının yakınlaştırılmış görüntüsü, tırnaklar, "Rehber Işık" efekti, "Hatırlat" butonu.
* **Görsel Geri Bildirim:** "Rehber Işık" akış animasyonu. Başarılı tırnak kapatmada tırnağın yerine oturma animasyonu. Mükemmel basınçta bir parlama efekti. Hatalı sırada tıklanan tırnağın kırmızı renkte yanıp sönmesi.
* **İşitsel Geri Bildirim:** Başarılı tıklamada net bir "klik" sesi. Mükemmel basınçta tatmin edici bir "çın" sesi. Hatalı tıklamada boğuk bir hata sesi.

## 4. Puanlama ve Gelişim (Scoring & Progression)
* **Puanlama Sistemi:**
    * Her doğru sıradaki tıklama, taban puanı artırır.
    * Her "mükemmel basınç", puana ekstra bonus ekler.
    * Hatalı sıradaki her tıklama, puandan düşürür.
    * Final puan, 0.0-1.0 arasına normalize edilerek `performanceScore` olarak belirlenir.
* **Zorluk Seviyesinin Artması:** Daha değerli taşlar ve karmaşık tasarımlar, daha fazla sayıda tırnak (örn: 4 yerine 6), daha hızlı bir "Rehber Işık" gösterimi ve "mükemmel basınç" için daha dar bir zamanlama aralığı gerektirir.
* **Gerekli Oyuncu Yetenekleri:** Yüksek `design_sense` yeteneği, "Gözlem Aşaması" süresini uzatabilir veya "Hatırlat" butonunun zaman maliyetini düşürebilir.

## 5. Sistemsel Çıktı (System Output)
* **Ödül:** `CraftingManager`'a, ürünün nihai kalitesini etkileyecek olan bir `performanceScore` değeri döndürülür.
* **Ceza:** Çok düşük bir performans, taşın çizilmesine (kalite düşüşü) veya en kötü senaryoda kırılmasına (malzeme kaybı) neden olabilir.