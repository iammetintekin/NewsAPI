# NewsAPI
Haber yönetim paneli **RESTful API**

Kurulum
Connection string değiştirildikten sonra projeyi çalıştırmanız yeterlidir. Otomatik migration çalışmaktadır.

Admin kullanıcı bilgileri.
k.adi : admin
şifre : admin123

Rol bazlı senaryo

**Admin**

- Kullanıcı tanımlayabilir
- Kategori oluşturabilir.
- User rolü gibi davranabilir, ve user rolündeki bütün işlemlere hakimdir.

**User**

- Haber oluşturma
- Kendi haberlerini “silme, güncelleme”
- Detaylı filtreleme (anahtar kelime, tarih, aktiflik durumu, kullanıcı bazlı, kategori bazlı) yapabilir.
- Haber detayına erişim SLUG veya Id ile sağlanmaktadır.

Genel loglar  “TRNews\bin\Debug\net7.0\logs” günlük olarak txt dosyasında kaydedilir.

Hata logları ise internal_logs/internal_logs.txt dosyasında loglanır.

Ek bilgiler https://documenter.getpostman.com/view/9868592/2s9YeD8YPd adresinde yer alır.