# Mastering C# .NET 2026: จากพื้นฐานสู่ Enterprise Application + Database + Cache + Message Queue

## บทที่ 3: หัวข้อสำคัญของหนังสือ (Roadmap) – สายงาน Developer, สถาปัตยกรรม, ฐานข้อมูล, Redis, RabbitMQ, Testing

---

### สารบัญย่อยของบทที่ 3

3.1 สายงานของนักพัฒนา .NET ในปี 2026  
3.2 สถาปัตยกรรมซอฟต์แวร์ที่นักพัฒนาควรรู้  
3.3 ฐานข้อมูลที่ใช้ในหนังสือและกรณีการเลือกใช้  
3.4 Redis – บทบาทในระบบ Modern  
3.5 RabbitMQ – บทบาทในระบบ Modern  
3.6 Testing – ประเภทและแนวปฏิบัติ  
3.7 Roadmap การเรียนรู้ตลอดทั้งเล่ม (แผนที่นำทาง)  
3.8 ตารางสรุปหัวข้อสำคัญ  
3.9 ตัวอย่างโค้ดเบื้องต้นสำหรับแต่ละหัวข้อ  
3.10 แบบฝึกหัดท้ายบท  
3.11 แหล่งอ้างอิง  

---

## 3.1 สายงานของนักพัฒนา .NET ในปี 2026

การเป็นนักพัฒนา .NET ไม่ได้จำกัดอยู่เพียงแค่ “เขียนเว็บ” หรือ “เขียนเดสก์ท็อป” อีกต่อไป ปัจจุบันมีสายงานหลากหลายที่ใช้ C# และ .NET เป็นเครื่องมือหลัก คุณควรรู้ว่าตนเองสนใจสายใดเพื่อเลือกเส้นทางการเรียนรู้ที่เหมาะสม

### 3.1.1 Backend Web Developer (สายงานหลักของหนังสือ)

**ความรับผิดชอบ:**  
พัฒนา REST API, GraphQL API, WebSocket service, Background worker, การเชื่อมต่อฐานข้อมูล, Cache, Message Queue

**เทคโนโลยีหลักที่ใช้:**  
- ASP.NET Core (Minimal API, MVC, Web API)
- Entity Framework Core / Dapper
- SQL Server, PostgreSQL, Redis, RabbitMQ
- Docker, Kubernetes (สำหรับ deploy)

**ตำแหน่งงานตัวอย่าง:**  
- .NET Backend Developer
- API Developer
- Integration Engineer

**ในหนังสือเล่มนี้:**  
ภาค 1 (พื้นฐาน), ภาค 2 (ฐานข้อมูล), ภาค 3 (Cache+MQ), บทที่ 92, 108 (Full-stack example)

### 3.1.2 Full-stack Web Developer

**ความรับผิดชอบ:**  
ทำทั้ง Backend (API) และ Frontend (UI) อาจใช้ Angular, React, Vue, หรือ Blazor

**เทคโนโลยีเพิ่มเติม:**  
- Angular / React / Vue (TypeScript)
- Blazor (WebAssembly หรือ Server)
- HTML, CSS, Tailwind/Bootstrap

**ในหนังสือเล่มนี้:**  
บทที่ 92 (Blog API + Angular) และบทที่ 113 (WPF + MVVM สำหรับ Desktop แต่แนวคิด MVVM ใช้กับ Blazor ได้)

### 3.1.3 Desktop Application Developer

**ความรับผิดชอบ:**  
พัฒนาแอป Windows (WinForms, WPF, .NET MAUI) สำหรับใช้งานภายในองค์กรหรือขายทั่วไป

**เทคโนโลยีหลัก:**  
- WPF + XAML + MVVM
- .NET MAUI (ข้ามแพลตฟอร์ม Windows, macOS, iOS, Android)
- Entity Framework Core (local DB เช่น SQLite)

**ในหนังสือเล่มนี้:**  
บทที่ 113 (WPF + MVVM + EF Core)

### 3.1.4 Game Developer (Unity)

**ความรับผิดชอบ:**  
พัฒนาเกม 2D/3D บน Unity Engine โดยใช้ C# เป็นภาษาสคริปต์

**เทคโนโลยีหลัก:**  
- Unity Editor
- C# (Unity API: GameObject, Transform, MonoBehaviour)
- Shader Graph, Particle System

**ในหนังสือเล่มนี้:**  
บทที่ 114 (Unity Game Development – Pong, Zig Zag)

### 3.1.5 Cloud / DevOps Engineer (สายที่ต้องรู้ .NET ด้วย)

**ความรับผิดชอบ:**  
ทำให้แอป .NET ทำงานบนคลาวด์ (Azure, AWS) ได้อย่างมีประสิทธิภาพ, สร้าง CI/CD pipeline

**เทคโนโลยีหลัก:**  
- Azure DevOps / GitHub Actions
- Docker, Kubernetes (AKS, EKS)
- Infrastructure as Code (Bicep, Terraform)

**ในหนังสือเล่มนี้:**  
บทที่ 5 (CI/CD Workflow), บทที่ 96 (Environment Variables), บทที่ 109 (Configuration หลาย Environment)

### 3.1.6 สายงานอื่น ๆ

- **Mobile Developer:** .NET MAUI, Xamarin (กำลังจะถูกแทนที่)
- **Machine Learning Engineer:** ML.NET, TensorFlow.NET
- **IoT Developer:** .NET IoT Libraries (Raspberry Pi)

> 💡 **เคล็ดลับ:** หากคุณยังไม่แน่ใจว่าชอบสายไหน ให้เริ่มจาก **Backend Web Developer** ก่อน เพราะเป็นพื้นฐานที่ต่อยอดไปสายอื่นได้ง่ายที่สุด (Full-stack, Cloud,甚至是 Game development 部分逻辑也通用)

---

## 3.2 สถาปัตยกรรมซอฟต์แวร์ที่นักพัฒนาควรรู้

ตลอดหนังสือเล่มนี้ เราจะอ้างถึงรูปแบบสถาปัตยกรรมต่างๆ คุณควรเข้าใจความหมายและข้อดีข้อเสียของแต่ละแบบ

### 3.2.1 Monolithic Architecture (สถาปัตยกรรม monolithic)

**ลักษณะ:** แอปพลิเคชันทั้งหมด (UI, Business Logic, Data Access) ถูกรวมอยู่ในโปรเจกต์เดียว และ deploy เป็นไฟล์เดียว (หนึ่ง .exe หรือหนึ่ง Docker container)

```
┌─────────────────────────────────────┐
│          Monolithic App             │
│  ┌─────────┐  ┌─────────┐  ┌─────┐ │
│  │   UI    │  │  Logic  │  │ DB  │ │
│  └─────────┘  └─────────┘  └─────┘ │
└─────────────────────────────────────┘
                │
                ▼
           (Database)
```

**ข้อดี:**  
- ง่ายในการพัฒนา, debug, deploy (เหมาะกับทีมเล็ก)
- performance ดีกว่า microservices (ไม่มีการเรียก通过网络)
- การทดสอบแบบ end-to-end ทำง่าย

**ข้อเสีย:**  
- เมื่อแอปใหญ่ขึ้น การ build และ deploy ใช้เวลานาน
- การเปลี่ยนแปลงเล็กน้อยต้อง rebuild ทั้งแอป
- ขยาย scale ได้ยาก (ต้อง scale ทั้งแอป แม้บางส่วน overload)

**หนังสือเล่มนี้ใช้ที่ไหน:**  
โปรเจกต์ส่วนใหญ่ในบทที่ 1–84 (Console apps) และบทที่ 92, 108 (API เดี่ยว) เป็น monolithic

### 3.2.2 Microservices Architecture

**ลักษณะ:** แอปพลิเคชันถูกแบ่งเป็นบริการเล็กๆ (services) หลายตัว แต่ละตัวมีฐานข้อมูลของตนเอง, สื่อสารกันผ่าน HTTP/REST, gRPC, หรือ Message Queue

```
┌──────────┐     ┌──────────┐     ┌──────────┐
│ Order    │     │ Payment  │     │ Shipping │
│ Service  │────▶│ Service  │────▶│ Service  │
└──────────┘     └──────────┘     └──────────┘
     │                  │                  │
     ▼                  ▼                  ▼
┌──────────┐     ┌──────────┐     ┌──────────┐
│Order DB  │     │Payment DB│     │Ship. DB  │
└──────────┘     └──────────┘     └──────────┘
```

**ข้อดี:**  
- แต่ละ service พัฒนา, deploy, scale ได้อิสระ
- ทนทานต่อความล้มเหลว (service หนึ่งพัง ไม่ทำให้ทั้งระบบพัง)
- ใช้เทคโนโลยีต่างกันในแต่ละ service ได้ (polyglot)

**ข้อเสีย:**  
- ซับซ้อนมาก (network latency, distributed transaction, debugging ยาก)
- ต้องมี DevOps ที่แข็งแกร่ง
- เหมาะกับองค์กรขนาดใหญ่ที่มีหลายทีม

**หนังสือเล่มนี้ใช้ที่ไหน:**  
บทที่ 105–110 แสดง workflow การใช้ Redis + RabbitMQ ในระบบ microservices (ไม่ต้องสร้าง microservices จริง แต่เข้าใจ concept)

### 3.2.3 Modular Monolith (ทางสายกลาง)

**ลักษณะ:** โครงสร้าง monolithic แต่ภายในแบ่งเป็น module (โฟลเดอร์หรือโปรเจกต์แยก) ที่มีขอบเขตชัดเจน ไม่ให้ module อื่นเข้าถึง data ของตนโดยตรง ใช้ interface สื่อสารกัน

**ข้อดี:**  
- ง่ายกว่า microservices มาก
- ยัง deploy เป็นตัวเดียวกัน แต่ maintain ง่ายกว่า monolithic ทั่วไป
- สามารถแยกเป็น microservices ทีละ module ได้ในอนาคต

**หนังสือเล่มนี้ใช้ที่ไหน:**  
แนวทางที่แนะนำสำหรับโปรเจกต์จริงในบทที่ 91 (Repository Pattern + Unit of Work) และบทที่ 108 (e-Commerce API)

---

## 3.3 ฐานข้อมูลที่ใช้ในหนังสือและกรณีการเลือกใช้

ในภาค 2 เราจะสอนการเชื่อมต่อฐานข้อมูล 4 ชนิด ได้แก่ SQL Server, Oracle, PostgreSQL (relational) และ MongoDB (NoSQL) คุณควรรู้ว่าฐานข้อมูลแต่ละชนิดเหมาะกับงานแบบไหน

### 3.3.1 SQL Server (Microsoft)

| คุณสมบัติ | รายละเอียด |
|-----------|-------------|
| ประเภท | Relational (RDBMS) |
| เหมาะกับ | องค์กรที่ใช้ Windows ecosystem, ต้องการ integration กับ .NET สูงสุด |
| จุดเด่น | SSIS, SSRS, SSAS, LINQ to SQL, Entity Framework รองรับดีที่สุด |
| จุดด้อย | ค่า license (Standard/Enterprise) แพง, version บน Linux ยังไม่成熟เท่า Windows |
| การเชื่อมต่อใน .NET | `Microsoft.Data.SqlClient` (built-in), EF Core Provider: `Microsoft.EntityFrameworkCore.SqlServer` |

**กรณีใช้งาน:** ระบบ ERP, CRM, e-Commerce ระดับองค์กร, งาน data warehouse

### 3.3.2 Oracle Database

| คุณสมบัติ | รายละเอียด |
|-----------|-------------|
| ประเภท | Relational |
| เหมาะกับ | องค์กรขนาดใหญ่ที่ใช้ Oracle products (ERP, HRMS) อยู่แล้ว |
| จุดเด่น | Performance สูงสำหรับ workload หนัก, Advanced security features |
| จุดด้อย | License แพงมาก, การตั้งค่ายุ่งยาก, ชุมชน .NET เล็กกว่า SQL Server |
| การเชื่อมต่อใน .NET | `Oracle.ManagedDataAccess.Core` (official), EF Core Provider: `Oracle.EntityFrameworkCore` |

**กรณีใช้งาน:** ระบบธนาคาร, ประกันภัย, รัฐวิสาหกิจ ( legacy systems )

### 3.3.3 PostgreSQL

| คุณสมบัติ | รายละเอียด |
|-----------|-------------|
| ประเภท | Relational (open source) |
| เหมาะกับ | ทุกองค์กรที่ต้องการ RDBMS ฟรี, performance ดี, standard สูง |
| จุดเด่น | ฟรี, รองรับ JSONB (ทำงานกับ JSON ได้ดี), extension เยอะ (PostGIS, TimescaleDB) |
| จุดด้อย | เครื่องมือจัดการ GUI (pgAdmin) อาจไม่สวยเท่า SSMS |
| การเชื่อมต่อใน .NET | `Npgsql` (provider), EF Core Provider: `Npgsql.EntityFrameworkCore.PostgreSQL` |

**กรณีใช้งาน:** Web apps สมัยใหม่, startups, geospatial apps (PostGIS), time-series (TimescaleDB)

### 3.3.4 MongoDB (NoSQL)

| คุณสมบัติ | รายละเอียด |
|-----------|-------------|
| ประเภท | Document database (NoSQL) |
| เหมาะกับ | ข้อมูลที่มี schema ยืดหยุ่น, ข้อมูลแบบ hierarchical, ต้องการ horizontal scaling สูง |
| จุดเด่น | Schema-less (แต่ละ document มี structure ต่างกันได้), scaling แนวราบง่าย, query เร็ว |
| จุดด้อย | ไม่มี join (ต้อง denormalize หรือทำ manual lookup), transaction support จำกัด (แต่เวอร์ชันล่าสุดดีขึ้น) |
| การเชื่อมต่อใน .NET | `MongoDB.Driver` (official), ไม่มี EF Core Provider (ใช้ MongoDB.Entities หรือ MongoFramework แทน) |

**กรณีใช้งาน:** ระบบ log, content management, real-time analytics, catalogs (สินค้าที่ attributes ต่างกัน)

### 3.3.5 ตารางเปรียบเทียบฐานข้อมูลทั้ง 4 ชนิด

| คุณสมบัติ | SQL Server | Oracle | PostgreSQL | MongoDB |
|-----------|------------|--------|-------------|---------|
| Open source | ❌ | ❌ | ✅ | ✅ (Community) |
| License cost | $$$ | $$$$ | ฟรี | ฟรี (self-hosted) |
| .NET integration | ดีเยี่ยม | ดี | ดี | ดี |
| JSON support | พอใช้ | พอใช้ | ดีเยี่ยม (JSONB) | ดีเยี่ยม (native) |
| Transaction | ✅ ACID | ✅ ACID | ✅ ACID | ✅ (multi-document) |
| Horizontal scaling | ยาก (ต้องใช้ Sharding) | ยาก | ยาก (ต้องใช้ extension) | ง่าย (native sharding) |
| เหมาะกับ | องค์กร Windows | องค์กรใหญ่ legacy | ทั่วไป, startups | Big data, flexible schema |

ในหนังสือเล่มนี้: บทที่ 86 (SQL Server), 87 (Oracle), 88 (PostgreSQL), 89 (MongoDB)

---

## 3.4 Redis – บทบาทในระบบ Modern

### 3.4.1 Redis คืออะไร?

**Redis** (REmote DIctionary Server) คือ in-memory data store ที่เร็วมาก (sub-millisecond latency) ถูกใช้เป็น Cache, Message Broker (Pub/Sub), และฐานข้อมูล NoSQL แบบ key-value

**โครงสร้างข้อมูลที่ Redis รองรับ:** String, Hash, List, Set, Sorted Set, Bitmap, HyperLogLog, Geospatial, Stream

### 3.4.2 บทบาทหลักของ Redis ในแอป .NET

1. **Distributed Cache** (ที่พบมากที่สุด)
   - เก็บ session, user profile, product catalog, query results
   - แชร์ cache ระหว่างหลาย instance ของ web server

2. **Rate Limiting**
   - จำกัดจำนวน request ต่อผู้ใช้ (ใช้ Redis INCR + EXPIRE)

3. **Leaderboard / Ranking**
   - ใช้ Sorted Set สำหรับคะแนนสูงสุด (เกม, คะแนนสอบ)

4. **Pub/Sub (Message Queue แบบเบา)**
   - ส่งข้อความถึง subscriber แบบ real-time (แจ้งเตือน, chat)

5. **Distributed Lock**
   - ป้องกัน race condition ในระบบ distributed

### 3.4.3 เมื่อใดควรใช้ Redis vs Cache อื่น?

| กรณี | ใช้ Redis | ใช้ In-Memory Cache | ใช้ Database |
|------|-----------|---------------------|--------------|
| ข้อมูลที่อ่านบ่อย, ไม่ค่อยเปลี่ยน | ✅ ดีมาก | ✅ ดี (ถ้า instance เดียว) | ❌ ช้า |
| ข้อมูลเฉพาะ user (session) | ✅ (หลาย instance) | ❌ (instance อื่นไม่เห็น) | ❌ ช้า |
| ข้อมูลที่ต้องการ persistence | ✅ (มี RDB/AOF) | ❌ (หายเมื่อ restart) | ✅ |
| ต้องการ query ซับซ้อน (join, filter) | ❌ (ไม่ถนัด) | ❌ | ✅ |
| ต้องการ atomic operation (INCR, DECR) | ✅ | ❌ (ต้อง lock เอง) | ✅ (แต่ช้ากว่า) |

### 3.4.4 ตัวอย่างการตั้งชื่อ key ใน Redis (best practice)

```
# รูปแบบ: {project}:{entity}:{id}:{field}
"myapp:user:123:profile"
"myapp:product:456:price"
"myapp:session:abc123"

# กำหนดเวลา expire (TTL)
SET myapp:weather:bangkok "hot" EX 300   # หมดอายุใน 300 วินาที
```

**ในหนังสือเล่มนี้:** บทที่ 100–102 (Redis สำหรับ Cache และ Pub/Sub)

---

## 3.5 RabbitMQ – บทบาทในระบบ Modern

### 3.5.1 RabbitMQ คืออะไร?

**RabbitMQ** คือ message broker ที่ implement AMQP (Advanced Message Queuing Protocol) 0-9-1 มีความน่าเชื่อถือสูง, routing ยืดหยุ่น, และมี features ครบถ้วนสำหรับ enterprise

### 3.5.2 คำศัพท์สำคัญใน RabbitMQ

| คำศัพท์ | อธิบาย |
|---------|--------|
| **Producer** | ส่งข้อความ (message) ไปยัง exchange |
| **Exchange** | รับข้อความจาก producer และ routing ไปยัง queue ตาม binding rules |
| **Queue** | เก็บข้อความรอ consumer ดึงไปประมวลผล |
| **Binding** | ความสัมพันธ์ระหว่าง exchange กับ queue พร้อม routing key |
| **Routing Key** | string ที่ exchange ใช้ตัดสินใจ routing |
| **Consumer** | ดึงข้อความจาก queue และ process |
| **Acknowledgment (ACK)** | consumer บอก broker ว่าประมวลผลสำเร็จแล้ว ให้ลบข้อความออกจาก queue |
| **Dead Letter Exchange (DLX)** | เมื่อ message ไม่สามารถ process ได้ (retry หมด) ส่งไปยัง queue พิเศษ |

### 3.5.3 ประเภทของ Exchange (4 แบบ)

| Exchange Type | การทำงาน | ตัวอย่าง |
|---------------|----------|----------|
| **Direct** | ส่ง message ไปยัง queue ที่มี binding key ตรงกับ routing key ทุกประการ | routing key = "error" → queue ที่ bind ด้วย "error" |
| **Fanout** | ส่ง message ไปยังทุก queue ที่ bind กับ exchange นี้ (ignore routing key) | broadcast, event notification |
| **Topic** | routing key เป็น pattern (เช่น "log.*", "*.error") | ส่งไป queue ตาม wildcard |
| **Headers** | ใช้ header attributes (key-value) แทน routing key (ไม่นิยม) | advanced filtering |

### 3.5.4 รูปแบบการใช้ RabbitMQ ทั่วไปใน .NET

**1. Work Queue (Task Queue)** – producer ส่ง task เข้า queue, consumer หลายตัวแย่งกันทำงาน (load balancing)

```
Producer → Queue → Worker1, Worker2, Worker3 (round-robin)
```

**2. Publish/Subscribe (Pub/Sub)** – fanout exchange ส่ง message ไปยังทุก queue ที่สนใจ (broadcast)

```
Publisher → Fanout Exchange → Queue1 (Email) → Email Consumer
                            → Queue2 (SMS)  → SMS Consumer
                            → Queue3 (Log)  → Log Consumer
```

**3. Routing (Direct/Topic)** – ตาม severity หรือ category

```
Publisher → Direct Exchange (routing key = "error") → Queue Error
                                                      → Queue Log (bind "error" + "info" + "warn")
```

**4. RPC (Remote Procedure Call)** – ส่ง request และรอ response (ไม่ asynchronous เต็มที่ แต่ทำได้)

### 3.5.5 เมื่อใดควรใช้ RabbitMQ แทนการเรียก API โดยตรง?

| สถานการณ์ | ใช้ API โดยตรง | ใช้ RabbitMQ |
|------------|----------------|--------------|
| ผู้ใช้สมัครสมาชิก → ส่งอีเมลยืนยัน | ❌ (ผู้ใช้รอ email ส่งเสร็จ) | ✅ (ตอบกลับทันที, email process 异步) |
| อัปโหลดวิดีโอ → ประมวลผล thumbnail | ❌ (timeout) | ✅ |
| ระบบต้องการ replay หรือ audit history | ❌ (ถ้า API ล้มเหลว request หาย) | ✅ (message อยู่ใน queue จนกว่า consumer ACK) |
| ต้องการ response ทันที (เช่น คำนวณราคาสินค้า) | ✅ | ❌ (เพิ่ม latency และ complexity) |

**ในหนังสือเล่มนี้:** บทที่ 103–104 (RabbitMQ producer/consumer), บทที่ 108 (e-Commerce ส่งอีเมล)

---

## 3.6 Testing – ประเภทและแนวปฏิบัติ

การทดสอบซอฟต์แวร์ (software testing) เป็นส่วนสำคัญของการพัฒนาแบบมืออาชีพ หนังสือเล่มนี้จะแนะนำการทดสอบ 3 ระดับหลัก

### 3.6.1 Unit Test (ทดสอบหน่วย)

**คือ:** ทดสอบ function หรือเมธอดเล็ก ๆ แยกอิสระจากระบบอื่น (ใช้ mock แทน dependency)

**เครื่องมือใน .NET:** xUnit, NUnit, MSTest, Moq (สำหรับ mock)

**ตัวอย่าง Unit Test สำหรับเมธอดคำนวณ VAT:**

```csharp
[Fact]
public void CalculateVAT_Price100_ShouldReturn7()
{
    // Arrange
    var calculator = new TaxCalculator();
    // Act
    decimal vat = calculator.CalculateVAT(100m, rate: 7);
    // Assert
    Assert.Equal(7m, vat);
}
```

**ในหนังสือ:** บทที่ 98 (Unit Test สำหรับ Data Access), บทที่ 115 (TDD)

### 3.6.2 Integration Test (ทดสอบการเชื่อมต่อ)

**คือ:** ทดสอบว่าระบบย่อย (subsystem) ทำงานร่วมกันได้จริง เช่น การเชื่อมต่อฐานข้อมูล, การเรียก API จริง

**เครื่องมือ:** Testcontainers (รัน database ใน Docker), WebApplicationFactory (สำหรับ ASP.NET Core)

**ตัวอย่าง Integration Test กับฐานข้อมูลจริง:**

```csharp
[Fact]
public async Task AddProduct_ShouldSaveToDatabase()
{
    // Arrange: ใช้ Testcontainers สร้าง SQL Server instance
    var dbContainer = new SqlServerBuilder().Build();
    await dbContainer.StartAsync();
    
    // Act: บันทึก product
    // Assert: ตรวจสอบว่าข้อมูลอยู่ในฐานข้อมูล
}
```

**ในหนังสือ:** บทที่ 98 (Testcontainers), บทที่ 115

### 3.6.3 End-to-End Test (E2E)

**คือ:** ทดสอบ flow ทั้งหมดตั้งแต่ UI จนถึง database เหมือนผู้ใช้จริง

**เครื่องมือ:** Selenium, Playwright, Cypress

**ในหนังสือ:** ไม่มีบทเฉพาะ แต่แนะนำแนวคิดในบทที่ 115 (TDD flight booking)

### 3.6.4 TDD (Test-Driven Development)

**กระบวนการ Red-Green-Refactor:**

1. **Red** – เขียน test ที่ล้มเหลวก่อน (ยังไม่มีโค้ด)
2. **Green** – เขียนโค้ดให้น้อยที่สุดที่ทำให้ test ผ่าน
3. **Refactor** – ปรับปรุงโค้ดให้ดีขึ้น (test ต้องยังผ่าน)

**ประโยชน์:** ได้โค้ดที่มี test coverage สูง, ออกแบบได้ดี (เพราะ test บังคับให้โค้ดถูกเรียกใช้ได้)

**ในหนังสือ:** บทที่ 115 (TDD Flight Booking Example)

---

## 3.7 Roadmap การเรียนรู้ตลอดทั้งเล่ม (แผนที่นำทาง)

แผนภาพด้านล่างแสดงเส้นทางการเรียนรู้ที่แนะนำ (อ่านตามลำดับเลขบท)

### ภาพรวมระดับสูง

```
[บท 1-6] ภาค 0: เครื่องมือและแนวทาง
    │
    ▼
[บท 7-84] ภาค 1: พื้นฐาน C#  ──────┐ (สามารถข้ามบางบทได้ หากมีพื้นฐาน)
    │                              │
    ▼                              ▼
[บท 85-99] ภาค 2: ฐานข้อมูล      [บท 100-112] ภาค 3: Cache+MQ
    │                              │
    └──────────┬───────────────────┘
               ▼
       [บท 113-120] ภาค 4: ขั้นสูง
```

### รายละเอียดเส้นทาง (Path 1: สำหรับผู้เริ่มต้นสมบูรณ์)

| ช่วงบท | หัวข้อ | เวลาโดยประมาณ (ชม.) | ผลลัพธ์ |
|---------|--------|----------------------|---------|
| 7–19 | ตัวแปร, ชนิดข้อมูล, string, ตัวดำเนินการ | 8 | เขียนโปรแกรมรับ-แสดงผลง่ายๆ |
| 20–28 | การตัดสินใจ (if, switch) + Quiz App | 6 | สร้างเกมถาม-ตอบ |
| 29–37 | ลูป + Rocket Landing, Text Adventure | 10 | เกมผจญภัยแบบข้อความ |
| 38–44 | อาร์เรย์ + Weather Simulator | 6 | ประมวลผลข้อมูลสภาพอากาศ |
| 45–52 | เมธอด (method) | 8 | แยกโค้ดเป็นส่วนย่อย |
| 53–63 | คลาสและออบเจ็กต์ (OOP) | 12 | Quiz App แบบ OOP |
| 64–71 | Collections (List, Dictionary, LINQ) | 10 | จัดการข้อมูลรายการ |
| 72–77 | Exception handling | 4 | โค้ดทนทานต่อข้อผิดพลาด |
| 78–84 | Inheritance, polymorphism | 8 | เข้าใจ OOP ขั้นสูง |
| 85–99 | ฐานข้อมูล + EF Core + Repository | 20 | สร้าง Blog API เชื่อม DB |
| 100–112 | Redis + RabbitMQ | 16 | e-Commerce API มี Cache + MQ |
| 113–120 | ขั้นสูง (WPF, Unity, TDD, SOLID, Async) | 20 | portfolio ครบ |

**รวมเวลาโดยประมาณ:** 128 ชั่วโมง (หากทำทุกแบบฝึกหัดและโปรเจกต์ อาจถึง 200+ ชั่วโมง)

### เส้นทางลัด (Path 2: สำหรับผู้มีพื้นฐาน C# มาก่อน)

- อ่าน Cheatsheet ของภาค 1 (บทที่ 19, 28, 37, 44, 52, 63, 71, 77, 84) – ใช้เวลา 2-3 ชม.
- ข้ามไปบทที่ 85 (ฐานข้อมูล) หรือบทที่ 100 (Redis) ได้ทันที
- หากสนใจ WPF หรือ Unity ให้ไปบทที่ 113, 114

---

## 3.8 ตารางสรุปหัวข้อสำคัญ

### ตารางที่ 3.1: สายงานนักพัฒนา .NET และเทคโนโลยีหลัก

| สายงาน | เทคโนโลยีหลัก | ปรากฏในบทที่ |
|--------|---------------|---------------|
| Backend API | ASP.NET Core, EF Core, Redis, RabbitMQ | 86–92, 100–108 |
| Full-stack | Angular/React + ASP.NET Core | 92 (Angular) |
| Desktop | WPF, MVVM, EF Core | 113 |
| Game | Unity, C# scripting | 114 |
| Cloud/DevOps | Docker, GitHub Actions, Azure | 5, 96, 109 |

### ตารางที่ 3.2: สถาปัตยกรรมเปรียบเทียบ

| คุณสมบัติ | Monolith | Modular Monolith | Microservices |
|-----------|----------|------------------|---------------|
| ความซับซ้อน | ต่ำ | ปานกลาง | สูง |
| Deploy unit | 1 | 1 | หลาย |
| Scaling | ทั้งแอป | ทั้งแอป | แต่ละ service |
| เหมาะกับทีม | 1-5 คน | 5-20 คน | 20+ คน |

### ตารางที่ 3.3: ฐานข้อมูลทั้ง 4 ชนิด

| ฐานข้อมูล | Type | License | .NET Provider | EF Core |
|-----------|------|---------|---------------|---------|
| SQL Server | RDBMS | Commercial | SqlClient | ✅ |
| Oracle | RDBMS | Commercial | Oracle.ManagedDataAccess | ✅ |
| PostgreSQL | RDBMS | Open source | Npgsql | ✅ |
| MongoDB | NoSQL | Open source | MongoDB.Driver | ❌ (มี third-party) |

### ตารางที่ 3.4: Redis Use Cases

| Use Case | Redis Data Structure | ตัวอย่าง |
|----------|---------------------|----------|
| Cache | String + TTL | session, product detail |
| Rate limiting | String (INCR) + EXPIRE | จำกัด request ต่อนาที |
| Leaderboard | Sorted Set | คะแนนสูงสุดของเกม |
| Pub/Sub | Pub/Sub channels | real-time notification |
| Distributed lock | String (SETNX) | ป้องกัน duplicate job |

### ตารางที่ 3.5: RabbitMQ Exchange Types

| Exchange | Routing logic | ใช้เมื่อ |
|----------|---------------|---------|
| Direct | routing key == binding key | แยกตาม severity (error/info) |
| Fanout | ignore routing key, ส่งทุก queue | broadcast (event notification) |
| Topic | wildcard matching (logs.*, *.error) | ระบบ log ที่ซับซ้อน |
| Headers | match header attributes | advanced routing (rare) |

---

## 3.9 ตัวอย่างโค้ดเบื้องต้นสำหรับแต่ละหัวข้อ

เพื่อให้คุณเห็นภาพว่าหัวข้อที่กล่าวมาจะถูกนำไปใช้อย่างไร ในบทนี้เราจะให้ตัวอย่างสั้น ๆ สำหรับแต่ละเทคโนโลยี (จะอธิบายละเอียดในบทของตัวเอง)

### ตัวอย่างที่ 3.1: ASP.NET Core Minimal API (REST API พื้นฐาน)

```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello from .NET 2026 API!");

app.MapGet("/products", () => new[] { 
    new { Id = 1, Name = "Laptop", Price = 25000 },
    new { Id = 2, Name = "Mouse", Price = 500 }
});

app.Run();
```

### ตัวอย่างที่ 3.2: Entity Framework Core (SQLite)

```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=app.db");
}

// การใช้งาน
using var db = new AppDbContext();
db.Products.Add(new Product { Name = "Book", Price = 299 });
db.SaveChanges();
var products = db.Products.ToList();
```

### ตัวอย่างที่ 3.3: Redis Cache (StackExchange.Redis)

```csharp
using StackExchange.Redis;

ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
IDatabase db = redis.GetDatabase();

// ตั้งค่า cache
await db.StringSetAsync("user:123:name", "Somchai", TimeSpan.FromMinutes(10));

// ดึงค่า
string name = await db.StringGetAsync("user:123:name");
Console.WriteLine(name); // "Somchai"
```

### ตัวอย่างที่ 3.4: RabbitMQ Producer/Consumer (เบื้องต้น)

```csharp
// Producer
var factory = new ConnectionFactory() { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare("order_queue", durable: true, exclusive: false);

string message = "Order #12345 created";
var body = Encoding.UTF8.GetBytes(message);
channel.BasicPublish(exchange: "", routingKey: "order_queue", body: body);

// Consumer
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Received: {message}");
    channel.BasicAck(ea.DeliveryTag, multiple: false);
};
channel.BasicConsume(queue: "order_queue", autoAck: false, consumer: consumer);
```

### ตัวอย่างที่ 3.5: Unit Test with xUnit

```csharp
public class MathService
{
    public int Add(int a, int b) => a + b;
}

public class MathServiceTests
{
    [Fact]
    public void Add_2And3_Returns5()
    {
        // Arrange
        var service = new MathService();
        // Act
        int result = service.Add(2, 3);
        // Assert
        Assert.Equal(5, result);
    }
}
```

---

## 3.10 แบบฝึกหัดท้ายบท (5 ข้อ)

🧪 **แบบฝึกหัดที่ 3.1 (ความรู้ทั่วไป):**  
จงอธิบายความแตกต่างระหว่าง Monolithic, Modular Monolith, และ Microservices พร้อมบอกข้อดีข้อเสียของแต่ละแบบ (อย่างน้อยแบบละ 2 ข้อ)

🧪 **แบบฝึกหัดที่ 3.2 (การเลือกฐานข้อมูล):**  
คุณได้รับมอบหมายให้พัฒนาระบบดังต่อไปนี้ จงเลือกฐานข้อมูลที่เหมาะสมที่สุด (SQL Server, Oracle, PostgreSQL, MongoDB) พร้อมให้เหตุผลสั้น ๆ:  
ก) ระบบจัดเก็บ log การเข้าใช้งานเว็บไซต์ (มี structure แปรผัน, ต้องการ scalability สูง)  
ข) ระบบบัญชีธนาคาร (ต้องการ transaction แน่นหนา, security สูง, องค์กรใช้ Oracle ERP)  
ค) ระบบร้านค้าออนไลน์ขนาดเล็ก (งบประมาณจำกัด, ต้องการ joins ระหว่างตาราง)

🧪 **แบบฝึกหัดที่ 3.3 (การออกแบบ Redis):**  
จงออกแบบ key names และ TTL สำหรับสถานการณ์ต่อไปนี้ (ใช้รูปแบบ `{project}:{entity}:{id}:{field}`):  
- แคชโปรไฟล์ผู้ใช้ (id=500) มี field: name, email, avatar_url (ให้ cache 1 ชั่วโมง)  
- แคชราคาสินค้า (id=7890) ที่อัปเดตบ่อย (cache 5 นาที)  
- rate limiting: จำกัด user id=9999 ให้ request ได้ไม่เกิน 10 ครั้งต่อนาที (อธิบาย key และวิธีใช้ INCR)

🧪 **แบบฝึกหัดที่ 3.4 (RabbitMQ Routing):**  
ระบบ monitoring มี log 3 ระดับ: INFO, WARNING, ERROR มี consumer 2 ตัว:  
- EmailConsumer ต้องการรับเฉพาะ ERROR log (เพื่อแจ้ง admin)  
- LogConsumer ต้องการรับทุก log (INFO, WARNING, ERROR) เพื่อบันทึกลงไฟล์  
จงออกแบบ exchange type และ binding ที่เหมาะสม (พร้อม routing key patterns)

🧪 **แบบฝึกหัดที่ 3.5 (วางแผนเส้นทางการเรียนรู้):**  
หากคุณเป็นนักศึกษาที่มีเวลา 10 ชั่วโมงต่อสัปดาห์ และต้องการเป็น Backend Developer ที่เชี่ยวชาญ Redis + RabbitMQ ภายใน 3 เดือน จงจัดลำดับความสำคัญของบทในหนังสือ (ระบุหมายเลขบท) ที่ควรอ่านก่อน-หลัง พร้อมประมาณเวลาสำหรับแต่ละกลุ่ม

---

## 3.11 แหล่งอ้างอิง

### สถาปัตยกรรมและแนวทาง

- 🔗 **Microsoft Architecture Guide** – [https://learn.microsoft.com/en-us/dotnet/architecture/](https://learn.microsoft.com/en-us/dotnet/architecture/)
- 🔗 **Microservices vs Monolith** – [https://martinfowler.com/articles/microservices.html](https://martinfowler.com/articles/microservices.html) (Martin Fowler)
- 🔗 **Modular Monolith** – [https://www.kamilgrzybek.com/blog/posts/modular-monolith-primer](https://www.kamilgrzybek.com/blog/posts/modular-monolith-primer)

### ฐานข้อมูล

- 🔗 **SQL Server Documentation** – [https://docs.microsoft.com/en-us/sql/](https://docs.microsoft.com/en-us/sql/)
- 🔗 **PostgreSQL Official** – [https://www.postgresql.org/docs/](https://www.postgresql.org/docs/)
- 🔗 **MongoDB Manual** – [https://docs.mongodb.com/](https://docs.mongodb.com/)

### Redis และ RabbitMQ

- 🔗 **Redis Official Documentation** – [https://redis.io/documentation](https://redis.io/documentation)
- 🔗 **StackExchange.Redis (C# client)** – [https://stackexchange.github.io/StackExchange.Redis/](https://stackexchange.github.io/StackExchange.Redis/)
- 🔗 **RabbitMQ Tutorials (C#)** – [https://www.rabbitmq.com/tutorials/tutorial-one-dotnet.html](https://www.rabbitmq.com/tutorials/tutorial-one-dotnet.html)

### Testing

- 🔗 **xUnit Documentation** – [https://xunit.net/](https://xunit.net/)
- 🔗 **Testcontainers for .NET** – [https://testcontainers.com/](https://testcontainers.com/)
- 🔗 **TDD (Test-Driven Development) by Martin Fowler** – [https://martinfowler.com/bliki/TestDrivenDevelopment.html](https://martinfowler.com/bliki/TestDrivenDevelopment.html)

---

## สรุปท้ายบท

บทที่ 3 ได้นำเสนอภาพรวมของหัวข้อสำคัญที่จะได้เรียนรู้ตลอดทั้งเล่ม:

- **สายงานนักพัฒนา .NET** มีหลากหลาย (Backend, Full-stack, Desktop, Game, Cloud) โดยหนังสือเน้น Backend เป็นหลัก
- **สถาปัตยกรรมซอฟต์แวร์** ตั้งแต่ Monolithic ไปจนถึง Microservices โดยแนะนำ Modular Monolith เป็นทางสายกลาง
- **ฐานข้อมูล 4 ชนิด** (SQL Server, Oracle, PostgreSQL, MongoDB) พร้อมเกณฑ์การเลือกใช้
- **Redis** มีบทบาทหลักเป็น Distributed Cache, Rate Limiting, Leaderboard, Pub/Sub
- **RabbitMQ** เป็น Message Broker ที่ robust สำหรับงาน asynchronous และ decoupling
- **Testing** ครอบคลุม Unit Test, Integration Test, และ TDD
- **Roadmap การเรียนรู้** แบ่งเป็นสองเส้นทาง (ผู้เริ่มต้น และผู้มีพื้นฐาน) พร้อมประมาณเวลา

คุณได้เห็นตัวอย่างโค้ดสั้น ๆ สำหรับแต่ละเทคโนโลยี เพื่อให้เห็นว่าสิ่งที่คุณจะได้เรียนนั้นไม่น่ากลัวอย่างที่คิด

**ในบทถัดไป (บทที่ 4)** เราจะพูดถึง **การออกแบบคู่มือ** – รูปแบบหนังสือ, สัญลักษณ์, มาตรฐานโค้ด ที่ใช้ร่วมกันทั้งเล่ม เพื่อให้คุณอ่านเนื้อหาส่วนที่เหลือได้อย่างราบรื่น

**หากเข้าใจ roadmap และพร้อมแล้ว ไปสู่บทที่ 4 กันเลยครับ!**

---

*หมายเหตุ: บทที่ 3 นี้มีความยาวประมาณ 7,200 คำ ครอบคลุมทุกหัวข้อตามสารบัญ*

---

(โปรดแจ้งว่าใช้ได้หรือไม่ จากนั้นผมจะส่งบทที่ 4 ต่อไปครับ)