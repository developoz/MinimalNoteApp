# MinimalNote

MinimalNote, .NET 9 Minimal API kullanýlarak geliþtirilmiþ küçük bir not alma servisi örneðidir. Bu repository, minimal API desenleri, Entity Framework Core ile kod-öncelikli (code-first) veritabaný kullanýmý ve temel CRUD iþlemlerini göstermek için bir portfolyo / CV örneði olarak hazýrlanmýþtýr.

## Teknoloji yýðýný

- .NET 9 (Minimal API)
- C#
- ASP.NET Core
- Entity Framework Core (Code-First)
- SQL Server (baðlantý `appsettings.json` veya `ConnectionStrings__DefaultConnection` çevre deðiþkeni ile)
- OpenAPI (geliþtirme ortamýnda API dokümantasyonu)

## Proje yapýsý (önemli dosyalar)

- `Program.cs` — uygulama giriþ noktasý ve rota tanýmlarý  
- `Models/Note.cs` — `Note` varlýk sýnýfý (Id, Title, Content, CreatedAt)  
- `Datas/NoteDbContext.cs` — EF Core `DbContext` ve `DbSet<Note>`  
- `appsettings.json` — `ConnectionStrings:DefaultConnection`  
- `Migrations/` — EF Core göç (migrations) dosyalarý  
- `MinimalNote.http` — yerel test için örnek HTTP istekleri

## Uygulanan özellikler

- `Note` için tam CRUD uç noktalarý (Minimal API):
  - `POST /notes` — not oluþturur (server tarafýnda `CreatedAt` atanýr)
  - `GET /notes` — tüm notlarý listeler
  - `GET /notes/{id}` — id ile not getirir
  - `PUT /notes/{id}` — notu günceller
  - `DELETE /notes/{id}` — notu siler
- CORS: `http://localhost:5173` için izin verildi
- Geliþtirme ortamýnda OpenAPI (Swagger) hazýr

## Veri modeli

`Note` (dosya: `Models/Note.cs`)
- `int Id`
- `string Title`
- `string Content`
- `DateTime CreatedAt`

## Lokal çalýþma (kýsa)

Gereksinimler:
- .NET 9 SDK
- SQL Server (veya baðlantý dizesini deðiþtirin)

Adýmlar:
1. Depoyu klonlayýn
2. `appsettings.json` içindeki baðlantý dizesini güncelleyin veya `ConnectionStrings__DefaultConnection` çevre deðiþkeni atayýn
3. Proje klasöründe çalýþtýrýn: