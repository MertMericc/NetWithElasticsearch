# NetWithElasticsearch Projesi

## Proje Açıklaması

**NetWithElasticsearch** projesi, ASP.NET Core ile veritabanı verilerini Elasticsearch üzerine senkronize eden ve bu verilerde hızlı arama yapabilen bir uygulamadır. Entity Framework Core ile veritabanı işlemlerini yönetir ve büyük veri kümeleri üzerinde arama performansını artırır. Postman aracılığıyla test edebileceğiniz senaryolarla, verilerin indekslenmesini ve arama işlemlerini daha net anlayabilirsiniz.


## Kurulum ve Çalıştırma

### Gereksinimler

- **.NET 6.0** veya üzeri
- **Docker** (Elasticsearch ve Kibana için)
- **Elasticsearch.NET** ve **Newtonsoft.Json** NuGet paketleri

### Elasticsearch ve Kibana ile Başlatma

Proje, Elasticsearch ve Kibana'yı Docker üzerinde çalıştırmak için bir `docker-compose.yml` dosyası içerir. Elasticsearch ve Kibana'yı başlatmak için:

1. Proje klasöründe bir terminal açın.
2. Aşağıdaki komutu çalıştırın:
    ```bash
    docker-compose -f docker.yml up -d
    ```
3. Elasticsearch, `http://localhost:9200` adresinde, Kibana ise `http://localhost:5601` adresinde çalışacaktır.

### Projeyi Çalıştırma

1. Proje dizinine gidin.
2. Aşağıdaki komutu çalıştırarak projeyi başlatın:
    ```bash
    dotnet run
    ```
3. Proje başladığında, API endpoint'leri ile Elasticsearch ve veritabanı işlemlerini test edebilirsiniz.

## API Endpoint'leri

### 1. Veri Oluşturma ve Veritabanına Ekleme

**URL:** `/api/values/CreateData`

**Açıklama:** Rastgele başlık ve açıklamaları olan 10.000 adet veri oluşturur ve veritabanına ekler.

**Metod:** `GET`

---

### 2. Entity Framework ile Veritabanında Arama

**URL:** `/api/values/GetDataListWithEF/{value}`

**Açıklama:** Veritabanında, açıklamalarında (`Description`) belirtilen değeri (`value`) içeren kayıtları arar.

**Metod:** `GET`

**Örnek:** `/api/values/GetDataListWithEF/test`

---

### 3. Elasticsearch'e Veritabanı Verilerini Senkronize Etme

**URL:** `/api/values/SyncToElastic`

**Açıklama:** Veritabanındaki tüm `Travel` kayıtlarını Elasticsearch'e ekler. Eğer veri Elasticsearch'te varsa yeniden eklemez.

**Metod:** `GET`

---

### 4. Elasticsearch ile Veritabanı Verilerini Senkronize Etme (Servis Kullanarak)

**URL:** `/api/values/SyncToElasticWithService`

**Açıklama:** Generic bir servis kullanarak tüm veritabanı verilerini Elasticsearch'e senkronize eder.

**Metod:** `GET`

---

### 5. Elasticsearch'te Arama Yapma

**URL:** `/api/values/GetDataListWithElasticSearch/{value}`

**Açıklama:** Elasticsearch'te, açıklamalarında belirtilen değeri (`value`) içeren kayıtları arar.

**Metod:** `GET`

**Örnek:** `/api/values/GetDataListWithElasticSearch/test`

---

## Servisler ve Yöntemler

Proje, ElasticSearch ile veri işlemlerini gerçekleştirmek için bir **`ElasticsearchService`** servisini kullanır. Bu servis aşağıdaki işlemleri gerçekleştirir:

# 1. Tüm Veriyi Elasticsearch'e Senkronize Etme
public static async Task SyncToElastic<T>(string indexName, Func<Task<List<T>>> getDataFunc) where T : class
Bu metod, veritabanındaki tüm verileri Elasticsearch'e ekler ve daha önce eklenmiş verileri güncellemez.


# 2. Tek Bir Veriyi Elasticsearch'e Senkronize Etme
public static async Task SyncSingleToElastic<T>(string indexName, T data) where T : class
Bu metod, tek bir veriyi Elasticsearch'e ekler ve Id ile eşleşen bir veri varsa yeniden eklemez.


# 3. Elasticsearch'te Veri Arama
public static async Task<List<T>> GetDataListWithElasticSearch<T>(string indexName, string fieldName, string value) where T : class
Bu metod, Elasticsearch'te verilen indeks ve alanda wildcard sorgusu yaparak belirtilen değeri içeren verileri döner.

# Proje Yapısı
Context/: Entity Framework Core ile veritabanı bağlantısı ve işlemleri gerçekleştiren AppDbContext.
Controllers/: API isteklerini karşılayan ValuesController.
Generics/: Genel amaçlı Elasticsearch işlemlerini gerçekleştiren ElasticsearchService.
Models/: Travel gibi veritabanı modellerini içeren sınıflar.

Bu proje, büyük veri kümelerinde performanslı arama yapmak için Elasticsearch ile .NET teknolojilerini bir araya getirir. ElasticSearch, büyük veri üzerinde hızlı ve etkin aramalar yapmayı sağlar ve Entity Framework ile veritabanı işlemleri sorunsuz şekilde entegre edilir.
