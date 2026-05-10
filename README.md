🚀 Inventor AutoBackup Tool (VBA)
Bu araç, Autodesk Inventor kullanıcıları için geliştirilmiş, arka planda sessizce çalışan bir otomatik yedekleme makrosudur. Tasarım sürecinde yaşanabilecek olası çökmelere veya veri kayıplarına karşı çalışmalarınızı belirlediğiniz aralıklarla güvenli bir konuma yedekler.

✨ Öne Çıkan Özellikler (Key Features)
Non-Blocking Execution: Windows SetTimer API'sini kullanır. Yedekleme sırasında Inventor donmaz, siz çalışmaya devam edersiniz.

Smart Filtering: Sadece tasarım dosyalarını (.ipt, .iam, .idw, .dwg) yedekler. Sistem dosyalarını veya Excel tablolarını es geçer.

Dirty Check: Sadece üzerinde değişiklik yapılmış (kaydedilmeyi bekleyen) dosyaları yedekleyerek disk alanından tasarruf sağlar.

Zero Configuration: Herhangi bir yol ayarı gerektirmez; yedekleri otomatik olarak kullanıcının Belgelerim/InventorBackups klasörüne tarih-saat damgasıyla kaydeder.

64-Bit Ready: PtrSafe deklarasyonu ile modern Inventor sürümleriyle tam uyumludur.

🛠️ Kurulum (Installation)
Inventor'ı açın ve Alt + F11 ile VBA editörüne girin.

Sol taraftaki panelde sağ tıklayıp Insert > Module seçeneğini seçin.

Kodun tamamını kopyalayıp bu modüle yapıştırın.

Inventor arayüzünde bu makroları (StartAutoBackup ve StopAutoBackup) birer butona atayarak kolayca kullanabilirsiniz.

📖 Kullanım (Usage)
StartAutoBackup: Yedekleme döngüsünü başlatır (Varsayılan: 5 Dakika).

StopAutoBackup: Yedeklemeyi durdurur.

Önemli: Modül kodunda değişiklik yapmadan veya Inventor'ı kapatmadan önce mutlaka StopAutoBackup çalıştırılarak Windows zamanlayıcısı serbest bırakılmalıdır.

🖥️ Teknik Detaylar (Technical Overview)
Kod içerisinde kullanılan temel yapılar:

User32 API: SetTimer ve KillTimer fonksiyonları ile Windows tabanlı zaman yönetimi.

FSO (FileSystemObject): Dinamik klasör oluşturma ve dosya yönetimi.

SaveCopyAs: Mevcut dökümanın çalışma akışını bozmadan (yolunu değiştirmeden) bir kopyasını farklı konuma kaydeder.
