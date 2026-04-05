# Mastering C# .NET 2026: จากพื้นฐานสู่ Enterprise Application + Database + Cache + Message Queue

## ภาค 1: พื้นฐานภาษา C# (บทที่ 7–84)

---

## บทที่ 7: การติดตั้ง Visual Studio และสภาพแวดล้อม

---

### สารบัญย่อยของบทที่ 7

7.1 ภาพรวมของเครื่องมือพัฒนา .NET  
7.2 การติดตั้ง .NET SDK  
7.3 การติดตั้ง Visual Studio 2026 (Community Edition)  
7.4 การติดตั้ง Visual Studio Code (ทางเลือกน้ำหนักเบา)  
7.5 การตรวจสอบการติดตั้งและทดสอบรันโปรแกรมแรก  
7.6 การตั้งค่าสภาพแวดล้อมเพิ่มเติม (Git, NuGet)  
7.7 ตารางสรุปเครื่องมือ  
7.8 ตัวอย่างโค้ด: โปรแกรมแรกด้วย `dotnet new`  
7.9 แบบฝึกหัดท้ายบท  
7.10 แหล่งอ้างอิง  

---

## 7.1 ภาพรวมของเครื่องมือพัฒนา .NET

ก่อนที่เราจะเริ่มเขียนโปรแกรม C# ได้ จำเป็นต้องติดตั้งเครื่องมือ (toolchain) ที่จำเป็น ซึ่งประกอบด้วย:

1. **.NET SDK** – ชุดพัฒนาแอปพลิเคชัน .NET (รวม Runtime, คอมไพลเลอร์, `dotnet` command)
2. **IDE (Integrated Development Environment)** หรือโค้ดเอดิเตอร์ – สำหรับเขียน, แก้ไข, ดีบักโค้ด
3. **Git** (แนะนำ) – สำหรับควบคุมเวอร์ชัน
4. **ตัวจัดการแพ็กเกจ NuGet** – มาใน .NET SDK อยู่แล้ว

ในบทนี้เราจะเลือกติดตั้ง **Visual Studio 2026 Community** (ฟรี, ครบครัน) และ **.NET SDK** (ถ้ายังไม่ติดตั้งมากับ VS) ส่วนผู้ที่ใช้ Linux หรือ macOS สามารถใช้ **Visual Studio Code** + .NET SDK ได้เช่นกัน

> 💡 **เคล็ดลับ:** Visual Studio Community ฟรีสำหรับนักพัฒนาเดี่ยว, โอเพนซอร์ส, และทีมเล็ก (ไม่เกิน 5 คน) หากคุณเป็นองค์กรใหญ่可能需要ซื้อ license

---

## 7.2 การติดตั้ง .NET SDK

**.NET SDK** เป็นหัวใจของทุกสิ่ง คุณสามารถติดตั้งแยกได้ หรือติดตั้งผ่าน Visual Studio ก็ได้

### 7.2.1 ดาวน์โหลด .NET SDK

1. เปิดเบราว์เซอร์ไปที่ [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)
2. เลือกเวอร์ชัน **.NET 9.0** หรือ **.NET 10.0** (LTS) – ตามความเหมาะสม
3. ดาวน์โหลดตัวติดตั้งที่ตรงกับระบบปฏิบัติการของคุณ:
   - Windows: `.exe` installer
   - macOS: `.pkg` installer
   - Linux: ใช้ package manager (apt, yum, หรือ snap)

### 7.2.2 ขั้นตอนการติดตั้งบน Windows

1. ดับเบิลคลิกไฟล์ `dotnet-sdk-9.0.xxx-win-x64.exe`
2. กด “Install” (ยอมรับ license terms)
3. รอจนติดตั้งเสร็จ (ไม่เกิน 2 นาที)
4. ปิดหน้าต่าง

### 7.2.3 ตรวจสอบการติดตั้ง

เปิด **Command Prompt** หรือ **Terminal** แล้วพิมพ์:

```bash
dotnet --version
```

คุณควรเห็นเลขเวอร์ชัน เช่น `9.0.100`

```bash
dotnet --info
```

แสดงข้อมูลแวดล้อมโดยละเอียด (OS, Runtime, SDK paths)

> ⚠️ **ข้อควรระวัง:** ถ้าได้รับ error “dotnet is not recognized” ให้ restart terminal หรือเพิ่ม PATH environment variable ด้วยตนเอง (ปกติตัวติดตั้งทำไว้ให้)

---

## 7.3 การติดตั้ง Visual Studio 2026 (Community Edition)

**Visual Studio** เป็น IDE ที่ทรงพลังที่สุดสำหรับ C# รองรับโปรเจกต์ขนาดใหญ่, ดีบักเกอร์ขั้นสูง, และเครื่องมือออกแบบ UI (WPF, WinForms)

### 7.3.1 ดาวน์โหลด Visual Studio Community

1. ไปที่ [https://visualstudio.microsoft.com/downloads/](https://visualstudio.microsoft.com/downloads/)
2. เลือก “Community 2026” (ฟรี)
3. ดาวน์โหลดตัวติดตั้ง (ไฟล์ `.exe` ขนาด ~2-3 MB) – ตัวติดตั้งนี้จะดาวน์โหลด component จริงระหว่างติดตั้ง

### 7.3.2 ขั้นตอนการติดตั้ง

1. เรียกใช้ตัวติดตั้ง `vs_community.exe`
2. หน้าต่าง “Visual Studio Installer” จะปรากฏ → คลิก “Install”
3. เลือก **Workloads** (ชุด component) ที่ต้องใช้:

| Workload | ความจำเป็น | คำอธิบาย |
|----------|-------------|-----------|
| **ASP.NET and web development** | ✅ จำเป็น | สำหรับสร้างเว็บแอป, REST API |
| **.NET desktop development** | ⚠️ เลือกได้ | สำหรับ WPF, WinForms (ถ้าต้องการ) |
| **Data storage and processing** | ⚠️ เลือกได้ | สำหรับ SQL Server Data Tools |
| **.NET Core cross-platform development** | ✅ จำเป็น | สำหรับ .NET Core/5+ (ครอบคลุมทุกอย่าง) |

สำหรับหนังสือเล่มนี้ ให้เลือก **ASP.NET and web development** และ **.NET desktop development** (ถ้าต้องการทำ WPF ในบทที่ 113)

4. ที่แถบด้านขวา “Installation details” ให้เลือกเพิ่ม (optional):
   - **Git for Windows** (แนะนำ)
   - **.NET Framework 4.8 targeting pack** (สำหรับ legacy)
5. คลิก “Install” แล้วรอ (อาจใช้เวลา 20-60 นาที ขึ้นอยู่กับความเร็วอินเทอร์เน็ต)

### 7.3.3 เปิด Visual Studio ครั้งแรก

- เมื่อติดตั้งเสร็จ ให้เปิด Visual Studio
- ลงชื่อเข้าใช้ด้วยบัญชี Microsoft (หรือข้ามไปก่อน)
- เลือกธีม (แนะนำ “Dark” เพื่อถนอมสายตา)
- รอการกำหนดค่าเริ่มต้น (initial configuration)

---

## 7.4 การติดตั้ง Visual Studio Code (ทางเลือกน้ำหนักเบา)

**Visual Studio Code** (VS Code) เป็นเอดิเตอร์ข้ามแพลตฟอร์มที่เบาและยืดหยุ่น เหมาะกับ Linux, macOS, หรือเครื่องที่ไม่ต้องการ IDE หนัก

### 7.4.1 ดาวน์โหลดและติดตั้ง

1. ไปที่ [https://code.visualstudio.com/](https://code.visualstudio.com/)
2. ดาวน์โหลดตัวติดตั้งตาม OS
3. ติดตั้ง (Windows: เลือก “Add to PATH” และ “Register Code as editor”)

### 7.4.2 ติดตั้ง Extension สำหรับ C#

เปิด VS Code → กด `Ctrl+Shift+X` (หรือ `Cmd+Shift+X` บน macOS) → ค้นหาและติดตั้ง:

| Extension | ผู้พัฒนา | ฟังก์ชัน |
|-----------|----------|----------|
| **C# Dev Kit** | Microsoft | รวม C# extension + IntelliSense + debugger |
| **C#** (จะติดตั้งอัตโนมัติ) | Microsoft | syntax highlighting, autocomplete |
| **.NET Install Tool** | Microsoft | ช่วยจัดการ .NET SDK version |

หลังติดตั้ง extension ให้ reload VS Code

### 7.4.3 ทดสอบสร้างโปรเจกต์ด้วย VS Code

เปิด Terminal ใน VS Code (`Ctrl+``) แล้วพิมพ์:

```bash
dotnet new console -n MyFirstApp
cd MyFirstApp
code .
```

จะเปิดโปรเจกต์ใหม่ใน VS Code พร้อมไฟล์ `Program.cs`

---

## 7.5 การตรวจสอบการติดตั้งและทดสอบรันโปรแกรมแรก

### 7.5.1 สร้างโปรเจกต์ Console ด้วย `dotnet new`

เปิด Terminal หรือ Command Prompt แล้วพิมพ์:

```bash
dotnet new console -n TestInstall
cd TestInstall
dotnet run
```

**ผลลัพธ์ที่คาดหวัง:**
```
Hello, World!
```

ถ้าเห็นข้อความนี้ แสดงว่าติดตั้ง .NET SDK สำเร็จ

### 7.5.2 ทดลองแก้ไขโค้ด

เปิดไฟล์ `Program.cs` ด้วย Notepad หรือ VS Code แล้วเปลี่ยนเป็น:

```csharp
Console.WriteLine("ยินดีต้อนรับสู่ Mastering C# .NET 2026!");
Console.Write("กรุณาพิมพ์ชื่อของคุณ: ");
string? name = Console.ReadLine();
Console.WriteLine($"สวัสดี {name}");
```

บันทึกแล้วรัน `dotnet run` อีกครั้ง

---

## 7.6 การตั้งค่าสภาพแวดล้อมเพิ่มเติม

### 7.6.1 ติดตั้ง Git

Git จำเป็นสำหรับการควบคุมเวอร์ชัน (บทที่ 5)

- ดาวน์โหลดจาก [https://git-scm.com/downloads](https://git-scm.com/downloads)
- ติดตั้งแบบ default (เลือก “Git from command line and also from 3rd-party software”)

### 7.6.2 ติดตั้ง Docker Desktop (optional)

สำหรับบทที่ 98 (Testcontainers) และบทที่ 100-104 (Redis, RabbitMQ) เราจะใช้ Docker เพื่อรัน database, Redis, RabbitMQ ใน container

- ดาวน์โหลด Docker Desktop จาก [https://www.docker.com/products/docker-desktop/](https://www.docker.com/products/docker-desktop/)
- ติดตั้ง (ต้องเปิด virtualization ใน BIOS)

### 7.6.3 การตั้งค่า PATH (ถ้า dotnet ไม่ทำงาน)

ถ้า `dotnet --version` ไม่ทำงาน ให้เพิ่ม PATH ด้วยตนเอง:

**Windows:**
1. ค้นหา “Environment Variables”
2. แก้ไข `Path` → เพิ่ม `C:\Program Files\dotnet\`
3. Restart terminal

**macOS/Linux:** โดยปกติตัวติดตั้งจะทำไว้ให้แล้ว

---

## 7.7 ตารางสรุปเครื่องมือ

| เครื่องมือ | เวอร์ชันแนะนำ | ดาวน์โหลด | จำเป็นสำหรับ |
|------------|---------------|-----------|---------------|
| .NET SDK | 9.0 หรือ 10.0 | [dotnet.microsoft.com](https://dotnet.microsoft.com) | ✅ จำเป็น |
| Visual Studio 2026 Community | 2026 | [visualstudio.microsoft.com](https://visualstudio.microsoft.com) | ⚠️ แนะนำ (Windows) |
| Visual Studio Code | ล่าสุด | [code.visualstudio.com](https://code.visualstudio.com) | ⚠️ ทางเลือก (ทุก OS) |
| Git | ล่าสุด | [git-scm.com](https://git-scm.com) | ✅ แนะนำ |
| Docker Desktop | ล่าสุด | [docker.com](https://docker.com) | ⚠️ สำหรับบททดสอบขั้นสูง |

---

## 7.8 ตัวอย่างโค้ด: โปรแกรมแรกด้วย `dotnet new`

เราจะใช้คำสั่ง `dotnet new` สร้างโปรเจกต์ประเภทต่าง ๆ:

| เทมเพลต | คำสั่ง | ผลลัพธ์ |
|----------|--------|----------|
| Console App | `dotnet new console -n MyApp` | โปรเจกต์ .csproj + Program.cs |
| Class Library | `dotnet new classlib -n MyLib` | สร้าง DLL สำหรับ reuse |
| xUnit Test | `dotnet new xunit -n MyTests` | โปรเจกต์ทดสอบ |
| Web API (Minimal) | `dotnet new webapi -minimal -n MyApi` | REST API พร้อมตัวอย่าง |

ลองสร้าง Web API และรัน:

```bash
dotnet new webapi -minimal -n MyWeatherApi
cd MyWeatherApi
dotnet run
```

เปิดเบราว์เซอร์ไปที่ `https://localhost:5001/weatherforecast` จะเห็น JSON response

> 💡 **เคล็ดลับ:** ใช้ `dotnet new --list` เพื่อดูเทมเพลตทั้งหมดที่มี

---

## 7.9 แบบฝึกหัดท้ายบท (4 ข้อ)

🧪 **แบบฝึกหัดที่ 7.1:**  
ติดตั้ง .NET SDK และ Visual Studio (หรือ VS Code) บนเครื่องของคุณ จากนั้นรัน `dotnet --info` และบันทึก output ใส่ในไฟล์ข้อความ (เพื่อยืนยันว่าติดตั้งสำเร็จ)

🧪 **แบบฝึกหัดที่ 7.2:**  
สร้างโปรเจกต์ Console App ชื่อ `MyFirstApp` ด้วย `dotnet new console` จากนั้นแก้ไข `Program.cs` ให้แสดงข้อความ “พร้อมแล้ว เริ่มเรียน C# กันเลย!” และรันด้วย `dotnet run`

🧪 **แบบฝึกหัดที่ 7.3:**  
ใช้ `dotnet new webapi -minimal -n TestApi` แล้วรันโปรเจกต์ จากนั้นลองเปลี่ยน route `/weatherforecast` ให้คืนค่าเป็นอาร์เรย์ของ string “Hello”, “World” แทน (ค้นหาวิธีแก้จากไฟล์ `Program.cs`)

🧪 **แบบฝึกหัดที่ 7.4 (ท้าทาย):**  
ติดตั้ง Docker Desktop และรัน Redis container ด้วยคำสั่ง `docker run -d -p 6379:6379 --name myredis redis` จากนั้นใช้ `docker ps` ตรวจสอบว่า container ทำงานอยู่ (เราจะใช้ Redis ในบทที่ 100)

---

## 7.10 แหล่งอ้างอิง

- 🔗 **.NET SDK Download** – [https://dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)
- 🔗 **Visual Studio Community 2026** – [https://visualstudio.microsoft.com/vs/community/](https://visualstudio.microsoft.com/vs/community/)
- 🔗 **Visual Studio Code + C#** – [https://code.visualstudio.com/docs/languages/csharp](https://code.visualstudio.com/docs/languages/csharp)
- 🔗 **dotnet new command reference** – [https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-new)
- 🔗 **Git installation guide** – [https://git-scm.com/book/en/v2/Getting-Started-Installing-Git](https://git-scm.com/book/en/v2/Getting-Started-Installing-Git)
- 🔗 **Docker Desktop for Windows/Mac** – [https://docs.docker.com/desktop/](https://docs.docker.com/desktop/)

---

## สรุปท้ายบท

บทที่ 7 นี้ได้แนะนำการติดตั้งเครื่องมือพัฒนา .NET ที่จำเป็น ได้แก่ .NET SDK, Visual Studio 2026 (หรือ VS Code), Git, และ Docker (optional) คุณได้ทดสอบการติดตั้งด้วยคำสั่ง `dotnet --version` และสร้างโปรเจกต์แรกด้วย `dotnet new console` และ `dotnet run`

เมื่อติดตั้งเสร็จสมบูรณ์ คุณก็พร้อมที่จะเริ่มเขียนโค้ด C# อย่างจริงจังแล้ว

**ในบทถัดไป (บทที่ 8)** เราจะลงรายละเอียดของโปรเจกต์ Hello World และโครงสร้างไฟล์ในโปรเจกต์ C# พร้อมอธิบายบทบาทของไฟล์ `.csproj`, `Program.cs`, `obj`, `bin`

---

*หมายเหตุ: บทที่ 7 นี้มีความยาวประมาณ 1,800 คำ*

---

(ดำเนินการส่งบทที่ 8 ต่อไปโดยอัตโนมัติ)