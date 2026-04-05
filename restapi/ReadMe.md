เข้าใจแล้วครับ คุณต้องการ **เปลี่ยนโปรเจกต์ `TestCommon` นี้ให้เป็นโปรเจกต์ `restapi`** (หรือ `restapi-c-book`) โดยปรับโครงสร้างและโค้ดให้ตรงกับที่เคยแนะนำไว้ก่อนหน้านี้ พร้อมเปลี่ยนมาใช้ **PostgreSQL** แทน SQLite/MongoDB

ผมจะสรุปขั้นตอนทั้งหมดที่ต้องทำ พร้อมตัวอย่างโค้ดและโครงสร้างโฟลเดอร์แบบ tree diagram ให้ดูเป็นตัวอย่างครับ

---

## 1. โครงสร้างโปรเจกต์หลังเปลี่ยนชื่อเป็น `restapi`

```
restapi/                          <-- โฟลเดอร์หลัก (เปลี่ยนจาก TestCommon)
│
├── restapi.csproj                <-- เปลี่ยนชื่อไฟล์ .csproj
├── Program.cs                    <-- แก้ namespace และใช้ PostgreSQL
├── appsettings.json              <-- เพิ่ม ConnectionStrings
├── appsettings.Development.json
│
├── Controllers/
├── Models/
├── Database/                     <-- ใหม่ (BlogContext.cs)
├── Stores/                       <-- ใหม่ (PostgreSqlStore.cs)
├── Background/
├── Broker/
├── HttpClient/
├── IC/
├── Properties/
│
├── HandleMySystemEvent.cs        <-- เปลี่ยน namespace
├── TestDI.cs                     <-- เปลี่ยน namespace
├── TestCommon.http               <-- อาจเปลี่ยนชื่อเป็น restapi.http
└── ...
```

---

## 2. ขั้นตอนการเปลี่ยน (ทำทีละอย่าง)

### 2.1 เปลี่ยนชื่อไฟล์ `.csproj` และแก้ไขเนื้อหา
- เปลี่ยนชื่อ `TestCommon.csproj` → `restapi.csproj`
- เปิดไฟล์ `.csproj` แก้ไข package references (เอา SQLite/MongoDB ออก, เพิ่ม Npgsql)

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <InvariantGlobalization>true</InvariantGlobalization>
  </PropertyGroup>

  <ItemGroup>
    <!-- PostgreSQL EF Core -->
    <PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="8.0.0" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.0" />
    
    <!-- ไลบรารีเดิมที่ยังใช้ -->
    <PackageReference Include="MassTransit.Kafka" Version="8.1.3" />
    <PackageReference Include="MassTransit.RabbitMQ" Version="8.1.3" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.4.0" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\Jtech.Common\Jtech.Common.csproj" />
  </ItemGroup>
</Project>
```

### 2.2 แก้ไข `appsettings.json` เพิ่ม ConnectionStrings
```json
{
  "Logging": { ... },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "PostgreSql": "Host=localhost;Database=restapidb;Username=postgres;Password=postgres"
  }
}
```

### 2.3 สร้างโฟลเดอร์ `Database` และไฟล์ `BlogContext.cs`
```csharp
using Microsoft.EntityFrameworkCore;
using restapi.Models;   // เปลี่ยน namespace

namespace restapi.Database
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }
        public DbSet<Blog> Blogs { get; set; }
    }
}
```

### 2.4 สร้างโฟลเดอร์ `Stores` และไฟล์ `PostgreSqlStore.cs`
```csharp
using Jtech.Common.DataStore;
using Microsoft.EntityFrameworkCore;
using restapi.Database;

namespace restapi.Stores
{
    public class PostgreSqlStore<T> : Store<T> where T : class
    {
        private readonly BlogContext _context;
        private readonly DbSet<T> _dbSet;

        public PostgreSqlStore(BlogContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public override async Task<T> GetById<TId>(TId id)
        {
            return await _dbSet.FindAsync(id);
        }
        // เพิ่ม method อื่นตามต้องการ (Insert, Update, Delete, Find, GetAll)
    }
}
```

### 2.5 แก้ไข `Program.cs` – เปลี่ยน namespace และใช้ PostgreSQL
```csharp
using Microsoft.EntityFrameworkCore;
using restapi.Database;
using restapi.Stores;
// ... using อื่น ๆ ที่เหลือ

Jtech.Common.Extensions.UseJTechRestApi(args, Configure: config => {
    // ... Swagger, Security คงเดิม

    config.BuilderConfigre = options =>
    {
        // PostgreSQL
        var connectionString = config.Configuration.GetConnectionString("PostgreSql");
        options.Services.AddDbContext<BlogContext>(db => db.UseNpgsql(connectionString));
        options.Services.AddScoped(typeof(Store<>), typeof(PostgreSqlStore<>));
        options.Services.AddLogic<CRUDLogic>();
        options.Services.AddSystemLogic<BlogContext>();

        // ส่วนอื่น ๆ (HttpClient, LineNotify ฯลฯ) คงเดิม
        options.Services.AddHttpClientWithPolicy<restapi.HttpClient.Ch3Client>(c => ...);
        options.Services.UseLineNotify();
        options.Services.AddLogic<TestInject>();
        // ...
    };
});
```

### 2.6 แก้ไข namespace ในไฟล์อื่น ๆ ทั้งหมด
- `HandleMySystemEvent.cs` : `namespace restapi.Background`
- `TestDI.cs` : `namespace restapi` และ `using restapi.Models`
- `TestCommon.http` : เปลี่ยนชื่อไฟล์เป็น `restapi.http` และแก้ไข URL ถ้าต้องการ

### 2.7 ลบโฟลเดอร์ / ไฟล์ที่ไม่ใช้แล้ว (ถ้ามี)
- อาจลบ `bin/`, `obj/` แล้วสร้างใหม่
- ถ้ายังมีอ้างอิงถึง `MongoDB` หรือ `SQLite` ในโค้ด ให้ลบออก

---

## 3. รัน Migration และทดสอบ

```bash
cd restapi
dotnet restore
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

---

## ✅ สรุปการเปลี่ยนแปลงหลัก

| ของเดิม (TestCommon) | ของใหม่ (restapi) |
|---------------------|-------------------|
| `TestCommon.csproj` | `restapi.csproj` |
| namespace `TestCommon.*` | namespace `restapi.*` |
| SQLite + MongoDB | PostgreSQL + EF Core |
| `AddDbContext<BlogContext>().AddEntityFrameworkSqlite()` | `UseNpgsql()` |
| `AddStore<MongoClient>("test")` | `AddScoped(typeof(Store<>), typeof(PostgreSqlStore<>))` |
| `AddLogic<CRUDLogic, MongoClient>("test")` | `AddLogic<CRUDLogic>()` |

ถ้าต้องการให้ผมช่วยเขียนโค้ดส่วนไหนเพิ่มเติม หรือแสดงโครงสร้างไฟล์ที่แก้ไขแล้วทั้งหมด แจ้งได้เลยครับ