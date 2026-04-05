# 📘 README - โปรเจกต์ iCommon rest book

โปรเจกต์ **TestCommon** เป็น REST API ที่พัฒนาด้วย **.NET 8** และใช้ไลบรารีภายใน `Jtech.Common` สำหรับจัดการ API Gateway, DataStore (MongoDB / SQLite), HttpClient พร้อม Policy, การส่งข้อความผ่าน Kafka/RabbitMQ, Cronjob, FileWatcher และ System Events

---

## 🚀 สิ่งที่ต้องติดตั้งก่อนรัน

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MongoDB](https://www.mongodb.com/try/download/community) (ถ้าใช้งาน MongoDB)
- [SQLite](https://www.sqlite.org/download.html) (ไม่ต้องติดตั้งเพิ่ม ใช้แพ็กเกจ `Microsoft.EntityFrameworkCore.Sqlite`)
- [Docker](https://www.docker.com/products/docker-desktop/) (optional สำหรับ Kafka / RabbitMQ)

> **หมายเหตุ:** Kafka, RabbitMQ, Cronjob, FileWatcher ถูก comment ไว้ใน `Program.cs` หากต้องการใช้งานให้แก้ไข comment และติดตั้ง Broker ที่เกี่ยวข้อง

---

## ⚙️ การตั้งค่า

### 1. โคลนโปรเจกต์และอ้างอิงไลบรารี Jtech.Common

โปรเจกต์นี้ต้องมี `Jtech.Common.csproj` อยู่ในพาธระดับเดียวกัน ( `../Jtech.Common` )  
ตรวจสอบว่าโฟลเดอร์โครงสร้างเป็นดังนี้:

```
📁 YourSolution/
├── 📁 Jtech.Common/          # ไลบรารีภายใน
└── 📁 TestCommon/            # โปรเจกต์หลัก
```

### 2. ตั้งค่า MongoDB (ถ้าใช้งาน)

แก้ไข connection string ใน `Program.cs` บรรทัด:

```csharp
options.Services.AddMongoClient<MongoClient>(() => {
    return new MongoClient(@"mongodb://localhost");  // เปลี่ยนให้ตรงกับ MongoDB ของคุณ
});
```

### 3. ตั้งค่า JWT Authentication

ใน `Program.cs` มีการตั้งค่า JWT เริ่มต้น:

```csharp
config.SecurityConfig = option =>
{
    option.JwtIssurer = "localhost.com";
    option.JwtKey = "YourSecretKeyForAuthenticationOfApplication";
    option.RequirePayload = true;
};
```

สามารถเปลี่ยน `JwtKey` เป็นรหัสลับที่แข็งแรงขึ้นได้

### 4. ตั้งค่า HttpClient (Ch3Client)

ตัวอย่างการเรียก API ภายนอก:

```csharp
options.Services.AddHttpClientWithPolicy<TestCommon.HttpClient.Ch3Client>(c =>
{
    c.BaseAddress = "https://api-ch3plus.mello.me/api/configuration";
});
```

### 5. เปิดใช้งาน Line Notify (มีให้แล้ว)

```csharp
options.Services.UseLineNotify();
```

### 6. เปิดใช้งาน Kafka / RabbitMQ (Optional)

ใน `Program.cs` มีตัวอย่างการเพิ่ม `KafkaPublisher` และ `RabbitPublisher` ถูก comment ไว้  
หากต้องการใช้งานให้ uncomment และติดตั้ง Broker (Kafka หรือ RabbitMQ) รวมถึงกำหนดค่าตามตัวอย่าง

---

## ▶️ ขั้นตอนการรัน

เปิด Terminal ที่โฟลเดอร์ `TestCommon` (ซึ่งมีไฟล์ `TestCommon.csproj`)

```bash
# คืนค่าแพ็กเกจ
dotnet restore

# สร้างโปรเจกต์
dotnet build

# รันแอปพลิเคชัน
dotnet run
```

แอปจะรันที่ `http://localhost:5119` หรือ `https://localhost:xxxx` (ขึ้นอยู่กับการกำหนดใน launchSettings)

---

## 🧪 ทดสอบ API

### ใช้ Swagger UI
เมื่อรันแอปแล้ว เปิดเบราว์เซอร์ไปที่:
```
https://localhost:{port}/swagger
```

### ใช้ TestCommon.http
ไฟล์ `TestCommon.http` อยู่ในโปรเจกต์ สามารถใช้กับ **REST Client** (Visual Studio Code extension) หรือ **JetBrains Rider**

ตัวอย่างการเรียก:
```http
GET {{TestCommon_HostAddress}}/weatherforecast/
Accept: application/json
```

---

## 📁 โครงสร้างโปรเจกต์โดยสังเขป

| โฟลเดอร์ / ไฟล์ | คำอธิบาย |
|----------------|-----------|
| `Controllers/` | ควบคุม API endpoints |
| `Models/` | คลาสโมเดล (Blog, ฯลฯ) |
| `Database/` | DbContext สำหรับ SQLite (BlogContext) |
| `Background/` | HandleMySystemEvent - จัดการ System Events |
| `Broker/` | คลาสสำหรับ Kafka / RabbitMQ (ถ้ามี) |
| `HttpClient/` | กำหนด HttpClient แบบมี Policy |
| `IC/`, `Template/`, `Book/` | โฟลเดอร์อื่น ๆ ตามโครงสร้างของ Jtech.Common |
| `Program.cs` | เริ่มต้นและลงทะเบียน services ทั้งหมด |
| `appsettings.json` | ค่ากำหนดทั่วไป (Logging, AllowedHosts) |
| `TestCommon.csproj` | ไฟล์โปรเจกต์ .NET 8 + แพ็กเกจอ้างอิง |

---

## 📌 หมายเหตุเพิ่มเติม

- **SQLite** ถูกเพิ่มผ่าน `AddDbContext<BlogContext>().AddEntityFrameworkSqlite()` และใช้ `AddSystemLogic<BlogContext>()`
- **MongoDB** ถูกเพิ่มผ่าน `AddStore<MongoClient>("test")` และ `AddLogic<CRUDLogic, MongoClient>("test")`
- **Cronjob / FileWatcher** ถูก comment ไว้ สามารถเปิดใช้งานได้โดย uncomment และสร้างคลาสที่สืบทอด `ICronjob` หรือ `IFileWatcherJob`
- **MassTransit** (RabbitMQ) มีการลงทะเบียนไว้แต่ถูก comment ไว้เช่นกัน

---

## 🛠 ปัญหาที่พบบ่อย

### 1. ไม่พบไลบรารี `Jtech.Common`
ตรวจสอบว่าโฟลเดอร์ `Jtech.Common` อยู่ระดับเดียวกับ `TestCommon` และมีการ build สำเร็จ

### 2. MongoDB connection error
ตรวจสอบว่า MongoDB รันอยู่ที่ `localhost` หรือเปลี่ยน connection string ให้ถูกต้อง

### 3. Port 5119 ถูกใช้งาน
แก้ไข `launchSettings.json` หรือใช้ `dotnet run --urls=http://localhost:5000`

---

## 📄 License

สำหรับใช้ภายในองค์กร (ตามสิทธิ์ของ Jtech.Common)

---

**Happy Coding!** 🎯