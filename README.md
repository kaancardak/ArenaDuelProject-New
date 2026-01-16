# **Medieval Knight: The Witch's Curse**

## **🎮 PLAY NOW**

**OYNA (Play Now):**
[https://kaancardak.itch.io/medieval-knight-the-witchs-curse-final](https://kaancardak.itch.io/medieval-knight-the-witchs-curse-final)

---

## **📝 PROJE HAKKINDA**

Bu proje, **Oyun Programlama** dersi dönem projesi kapsamında **Unity** oyun motoru kullanılarak geliştirilmiştir.

**2D Platformer** türündeki bu oyunda, şövalye karakterimiz ile düşmanları alt edip hayatta kalmaya çalışıyoruz. Proje aşağıdaki özelliklerle zenginleştirilmiştir:

* **Eğitilebilir Yapay Zeka (AI) Entegrasyonu**
* **WebGL Üzerinde Harici Dosya Yükleme**

---

## **🕹️ OYNANIŞ & KONTROLLER**

### **🎯 Temel Kontroller**

* **W / A / S / D** → Hareket (Yürüme)
* **Space (Boşluk)** → Zıplama
* **Sol Tık (Mouse)** → Kılıç Saldırısı
* **Sağ Tık (Basılı Tut)** → Kalkan (Savunma)
* **Sol Shift** → Takla Atma (Dash)

---

## **🧠 YAPAY ZEKA SİSTEMİ (AI SYSTEM)**

Oyun, **Tek Katmanlı Algılayıcı (Single Layer Perceptron)** mimarisine sahip **eğitilebilir bir yapay zeka** içermektedir.

### **⚖️ Karar Mekanizması**

Düşman aşağıdaki girdileri kullanarak karar verir:

* **Oyuncuya Olan Mesafe**
* **Düşmanın Can Değeri**

Bu girdiler doğrultusunda düşman:

* **Saldırır**
* **Hareket Eder / Dash Atar**

### **📁 Ağırlık Dosyası (ai_weights.json)**

* Yapay zeka önceden eğitilmiştir
* Ağırlık değerleri **.json** dosyasında saklanır

### **🔀 Rastgele vs. Eğitilmiş Mod**

**📌 Dosya Yüklenmezse**

* Ağırlıklar rastgele atanır (**Random Weights**)
* Düşman anlamsız ve rastgele hareketler sergiler

**📌 Dosya Yüklenirse**

* Eğitilmiş veriler kullanılır
* Düşman mantıklı (**rasyonel**) kararlar verir:

  * Uzaktaysa yaklaşır
  * Menzile girince saldırır

---

## **⚙️ MEKANİKLER & ÖZELLİKLER**

### **🌐 WebGL Dosya Yükleme Sistemi**

Tarayıcıda çalışabilmesi için özel bir **Javascript Plugin (.jslib)** yazılmıştır. Kullanıcı, bilgisayarından **ai_weights.json** dosyasını oyuna yükleyebilir.

### **⚔️ Dövüş Sistemi**

* Kılıç ile saldırı
* Kalkan ile savunma

### **👾 Düşman Davranışları**

* Perceptron çıktısına göre hareket eder
* Oyuncuyu takip eder, saldırır veya dash atar
* **Düşman 5 vuruşta ölür**

### **❤️ Can & İyileşme Sistemi**

* Kamp ateşi yanında durularak can yenilenir
* **Her 3 saniyede +1 yarım kalp**

### **🏁 Kazan / Kaybet Durumu**

* Düşman ölürse → **Oyun kazanılır**
* Oyuncu ölürse → **Oyun biter**

---

## **📥 YAPAY ZEKA NASIL YÜKLENİR?**

1. github sayfasından **ai_weights.json** dosyasını indirin
2. Oyunu tarayıcıda başlatın
3. Ana menüden **Upload AI** butonuna tıklayın
4. İndirdiğiniz **.json** dosyasını seçin
5. **Success** mesajını gördükten sonra oyuna başlayın

---

## **🛠️ KULLANILAN TEKNOLOJİLER**

* **Oyun Motoru:** Unity 2022 LTS
* **Programlama Dili:** C#
* **Yapay Zeka:** Single Layer Perceptron (Supervised Learning)
* **Platform:** WebGL (Tarayıcı)
* **Web Entegrasyonu:** Custom **.jslib** Plugin

---

## **🎨 ASSET KAYNAKLARI**

Projede kullanılan tüm görseller ve çizimler **eğitim amaçlıdır**:

* Pixel Art Knight & Enemy Sprites
* Medieval Environment Tileset
* Free UI Asset Pack
