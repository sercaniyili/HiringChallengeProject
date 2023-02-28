
#  Shopping List Proje


### Proje Tanımı

Kayıt olan kullanıcılar *User* rolü ile kategori bazlı liste oluşturabilir, silebilir, güncelleyebilir,listeye ürün ekleyebilir veya liste adı,kategori adı,oluşturulma tarihine göre liste aratabilir. Sadece *Admin* olarak giriş yapan kullanıcı ise bütün listeleri ve tamamlanmış listeleri görebilir.

***Postman*** yada ***Swagger*** ile kullanılan *Rest API* projesinde verileri saklamak için veri tabanı olarak ***MS SQL***, .Net ile bağlantı için *orm* olarak ***EntityFramework Core*** 
kullanılmıştır. *Admin* kullanıcısı *Kategoriler* ve *Roller* *seed data* *ef core* aracılığıyla eklenmiştir.

Tamamlanma durumları *ısComplete* endpoint'i üzerinden *true* olarak güncellenen listeler ***Rabbitmq*** kuyruk sistemine eklenmektedir.
***Worker service*** üzerinden sürekli dinlenen kuyruk yapımız tamamlanan listeleri ***MongoDb*** veri tabanına eklenmektedir. Ayrıca *global exception custom middleware* üzerinden ayrı bir *Logs* klasöründe *text* dosyaları halinde günlük olarak tutulmaktadır. 

Projede *distrubuted cache* için ***Redis***, *rol tabanlı yetkilendirme ve kısıtlama yetkilendirme için ***Identity ve JWT bearer***,  *validation* için ***FluentValidation***, *unit test*ler için ***Xunit***,  ***Moq*** ve ***Fluent Assertions*** kütüphaneleri,
*log* için ***SeriLog*** kütüphanleri , *Redis* ve *Rabbitmq* ***Docker image*** üzeriden, *Ms Sql* ve *Mongodb* lokal olarak kullanılmıştır.

Kurumsal mimari ve Clean Architecture için Onion Architecture mimarisi , CQRS ve MediatR kütüphaneleri kullanılmıştır.

___
## Kullanılan Yöntem ve Teknolojiler
 * ***.NET 6.0 Web API***
 * ***Microsoft SQL***
 * ***Entity Framework***
 * ***Rabbitmq***
 * ***Redis***
 * ***MongoDb***
 * ***Automapper***
 * ***XUnit***
 * ***Fluent Validation***
 * ***Identity & JWT Bearer Tokens***
 * ***SeriLogger***
 * ***CQRS***
 * ***MediatR***
 * ***Onion Architecture***
 * ***Repository Pattern***
 * ***SOLID Principles***
 * ******
 
___

## Projeyi Geliştirici Ortamında Çalıştırmak İçin

### Ön Gereksinimler
* Visual Studio 2020+
* Microsoft SQL Server 2019+
* .Net 6.0

### Çalıştırılması
Local klasöre projeyi klonlamak için :
```
 git clone https://github.com/sercaniyili/ShoppingListProject
```

<br/>


- API Endpoints

<img src="https://github.com/sercaniyili/ShoppingListProject/blob/main/Teleperformance.ShoppingList/images/Endpoints.png" />


- Veri tabanı dizayn

<img src="https://github.com/186-Teleperformans-Net-Bootcamp/ShoppingListProject-sercan-iyili/blob/main/Teleperformance.ShoppingList/images/DbDiagram.png" />


- Docker'da Redis ve RAbbitmq aktif ediliyor

<img src="https://github.com/186-Teleperformans-Net-Bootcamp/ShoppingListProject-sercan-iyili/blob/main/Teleperformance.ShoppingList/images/dockerps.png" />


- Kullanıcı giriş yapıyor

<img src="https://github.com/186-Teleperformans-Net-Bootcamp/ShoppingListProject-sercan-iyili/blob/main/Teleperformance.ShoppingList/images/Register.png" />


- Kullanıcı liste ekliyor

<img src="https://github.com/186-Teleperformans-Net-Bootcamp/ShoppingListProject-sercan-iyili/blob/main/Teleperformance.ShoppingList/images/CreateList.png" />


- Oluşturulmuş listeler

<img src="https://github.com/186-Teleperformans-Net-Bootcamp/ShoppingListProject-sercan-iyili/blob/main/Teleperformance.ShoppingList/images/DbLists.png" />


- Ürün ekleniyor

<img src="https://github.com/186-Teleperformans-Net-Bootcamp/ShoppingListProject-sercan-iyili/blob/main/Teleperformance.ShoppingList/images/AddProduct.png" />


- Id'ye göre listeler

<img src="https://github.com/186-Teleperformans-Net-Bootcamp/ShoppingListProject-sercan-iyili/blob/main/Teleperformance.ShoppingList/images/GetByIdShopingList.png" />


- Liste adı,ürün adına, oluşturulma tarihine göre arama

<img src="https://github.com/186-Teleperformans-Net-Bootcamp/ShoppingListProject-sercan-iyili/blob/main/Teleperformance.ShoppingList/images/Search.png" />


- Kullanıcı yetki kısıtlanması nedeniyle bütün listeleri göremiyor

<img src="https://github.com/186-Teleperformans-Net-Bootcamp/ShoppingListProject-sercan-iyili/blob/main/Teleperformance.ShoppingList/images/401.png" />


 - Admin role ve yetkilendirilmiş JWT token ile giriş yapıyor 
 
<img src="https://github.com/186-Teleperformans-Net-Bootcamp/ShoppingListProject-sercan-iyili/blob/main/Teleperformance.ShoppingList/images/AdminLogin.png" />


- JWT token çözümlenmesi

<img src="https://github.com/186-Teleperformans-Net-Bootcamp/ShoppingListProject-sercan-iyili/blob/main/Teleperformance.ShoppingList/images/jwt.png" />


- Admin bütün listeleri görebilyor

<img src="https://github.com/186-Teleperformans-Net-Bootcamp/ShoppingListProject-sercan-iyili/blob/main/Teleperformance.ShoppingList/images/AdminAllList.png" />


- Liste tamamlanıyor

<img src="https://github.com/186-Teleperformans-Net-Bootcamp/ShoppingListProject-sercan-iyili/blob/main/Teleperformance.ShoppingList/images/IsComplete.png" />


- Tamamlanan liste Rabbitmq'da kuyruğa ekleniyor

<img src="https://github.com/186-Teleperformans-Net-Bootcamp/ShoppingListProject-sercan-iyili/blob/main/Teleperformance.ShoppingList/images/IsComplete.png" />


- Tamamlanan listeler WorkerService üzerinden Rabbitmq'ya orayadan MongoDb'ye kayıt ediliyor

<img src="https://github.com/186-Teleperformans-Net-Bootcamp/ShoppingListProject-sercan-iyili/blob/main/Teleperformance.ShoppingList/images/Mongo.png" />


- Tamamlanan listelerin bulunduğu endpoint'i Sadece Admin görüntüleybiliyor

<img src="https://github.com/186-Teleperformans-Net-Bootcamp/ShoppingListProject-sercan-iyili/blob/main/Teleperformance.ShoppingList/images/CompletedListsAdmin.png" />

- Command Handler Unit Testleri

<img src="https://github.com/sercaniyili/ShoppingListProject/blob/main/Teleperformance.ShoppingList/images/Tests.png" />

- Proje Log'ları dış bir klasörde text dosyaları içinde tutuluyor

<img src="https://github.com/186-Teleperformans-Net-Bootcamp/ShoppingListProject-sercan-iyili/blob/main/Teleperformance.ShoppingList/images/Logs.png" />









