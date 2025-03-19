# Movie Store Web API
## Genel Bakış
#### Bu proje, bir Movie Store'u yönetmek için .NET ve Entity Framework ile oluşturulmuş bir Web API'sidir. Kimlik doğrulama, yetkilendirme, günlük kaydı, doğrulama ve bağımlılık enjeksiyonu içerir.

#### Uygulama kullanıcıların filmleri, yönetmenleri, aktörleri ve müşterileri yönetmesine olanak tanır. Müşteriler film satın alabilir ve tüm işlemler kaydedilir. Sistem veri tutarlılığını sağlar ve varlık nesneleri yerine DTO'ları kullanma gibi en iyi uygulamaları takip eder.

## Özellikler
* Filmler, yönetmenler, aktörler, siparişler ve müşteriler için CRUD İşlemleri.

* Müşterilerin film satın alması için Satın Alma Sistemi.

* FluentValidation kullanarak Veri Doğrulaması.

* Doğrudan varlık ifşasını önlemek için DTO ve Görünüm Modelleri.

* Farklı nesne türleri arasında eşleme yapmak için AutoMapper Entegrasyonu.

* İstekleri, yanıtları ve hataları izlemek için Günlük Kaydı Orta Katmanı.

* Kimlik Doğrulama ve Yetkilendirme erişim kontrolü.

* Bağımlılıkları yönetmek için Bağımlılık Enjeksiyonu.

* Sistem güvenilirliğini sağlamak için Birim Testi.

* Kolay API testi için JWT kimlik doğrulama düğmesiyle Swagger Entegrasyonu.

## Kullanılan Teknolojiler
* C# - Birincil programlama dili

* .NET 5 - Web API'sini oluşturmak için çerçeve

* Entity Framework Core - Veritabanı işlemleri için ORM

* FluentValidation - İstek doğrulaması için

* AutoMapper - Nesneleri dönüştürmek için

* ASP.NET Core Identity - Kimlik doğrulama ve yetkilendirme için

* Middleware Logging - İstekleri ve hataları konsola ve veritabanına kaydetmek için

* Bağımlılık Enjeksiyonu (DI) - Hizmet ömürlerini yönetmek için

* xUnit ve Moq ile Birim Testi - Otomatik testler yazmak için

* Swagger UI - Etkileşimli API dokümantasyonu ve testi için
