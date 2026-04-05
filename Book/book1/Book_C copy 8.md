# Mastering C# .NET 2026: จากพื้นฐานสู่ Enterprise Application + Database + Cache + Message Queue

## บทที่ 8: โปรเจกต์แรก Hello World และโครงสร้างไฟล์

---

### สารบัญย่อยของบทที่ 8

8.1 การสร้างโปรเจกต์ Hello World อย่างละเอียด  
8.2 โครงสร้างไฟล์ของโปรเจกต์ C#  
8.3 ไฟล์ .csproj – หัวใจของโปรเจกต์  
8.4 ไฟล์ Program.cs – จุดเริ่มต้นของแอปพลิเคชัน  
8.5 โฟลเดอร์ obj และ bin – ผลลัพธ์จากการคอมไพล์  
8.6 การทำงานของ `dotnet run` และ `dotnet build`  
8.7 การแก้ไขและรันโปรเจกต์ด้วย Visual Studio  
8.8 ตารางสรุปโครงสร้างไฟล์  
8.9 ตัวอย่างโค้ดและการทดลอง  
8.10 แบบฝึกหัดท้ายบท  
8.11 แหล่งอ้างอิง  

---

## 8.1 การสร้างโปรเจกต์ Hello World อย่างละเอียด

ในบทที่แล้วเราสร้างโปรเจกต์ Hello World อย่างคร่าว ๆ บทนี้เราจะมาทำความเข้าใจในรายละเอียดของทุกไฟล์และโฟลเดอร์ที่ถูกสร้างขึ้น

### 8.1.1 สร้างโปรเจกต์ใหม่ด้วย `dotnet new`

เปิด Terminal (Command Prompt, PowerShell, หรือ bash) แล้วพิมพ์:

```bash
dotnet new console -n HelloWorldProject
cd HelloWorldProject
```

คำสั่งนี้จะสร้างโฟลเดอร์ชื่อ `HelloWorldProject` พร้อมไฟล์เริ่มต้นดังนี้:

```
HelloWorldProject/
├── Program.cs
├── HelloWorldProject.csproj
├── obj/
│   ├── Debug/
│   │   └── net9.0/
│   │       ├── HelloWorldProject.AssemblyInfo.cs
│   │       ├── HelloWorldProject.csproj.FileListAbsolute.txt
│   │       └── ...
│   └── HelloWorldProject.csproj.nuget.g.props
└── bin/
    └── Debug/
        └── net9.0/
            ├── HelloWorldProject.dll
            ├── HelloWorldProject.exe (บน Windows)
            └── ...
```

### 8.1.2 เปิดโปรเจกต์ใน Visual Studio หรือ VS Code

- **Visual Studio:** ดับเบิลคลิกไฟล์ `HelloWorldProject.csproj` หรือเปิดผ่าน File → Open → Project/Solution
- **VS Code:** พิมพ์ `code .` ในโฟลเดอร์โปรเจกต์ (ต้องติดตั้ง C# extension)

> 💡 **เคล็ดลับ:** ชื่อโปรเจกต์ควรเป็น PascalCase (ขึ้นต้นด้วยตัวพิมพ์ใหญ่) และไม่มีช่องว่าง เช่น `MyWebApi`, `OrderService`

---

## 8.2 โครงสร้างไฟล์ของโปรเจกต์ C#

### 8.2.1 ภาพรวม

โปรเจกต์ C# มาตรฐาน (Console, Web, Class Library) ประกอบด้วย:

| ไฟล์/โฟลเดอร์ | หน้าที่ | สร้างอัตโนมัติ? | ควรแก้ไขด้วยมือ? |
|----------------|--------|----------------|-------------------|
| `*.csproj` | กำหนดค่าโปรเจกต์ (dependencies, output type) | ✅ | ✅ (บ่อย) |
| `Program.cs` | โค้ดหลัก (entry point) | ✅ | ✅ (ตลอดเวลา) |
| `obj/` | วัตถุระหว่างคอมไพล์ (temporary) | ✅ | ❌ (ห้ามแก้) |
| `bin/` | ผลลัพธ์ที่คอมไพล์แล้ว (.dll, .exe) | ✅ | ❌ (ห้ามแก้) |
| `Properties/` | metadata (AssemblyInfo.cs, launchSettings.json) | ✅ (ในบางเทมเพลต) | ⚠️ (น้อยครั้ง) |
| `appsettings.json` | การตั้งค่า (สำหรับ Web App) | ✅ (Web template) | ✅ (บ่อย) |

### 8.2.2 ความสัมพันธ์ระหว่างไฟล์

```
.csproj (โปรเจกต์)
   │
   ├── อ้างอิงถึง Program.cs และไฟล์ .cs อื่น ๆ
   ├── กำหนด TargetFramework (net9.0)
   ├── กำหนด PackageReference (NuGet)
   │
   └── เมื่อคอมไพล์ → สร้าง obj/ (ชั่วคราว) → สร้าง bin/ (ผลลัพธ์)
```

---

## 8.3 ไฟล์ .csproj – หัวใจของโปรเจกต์

**`.csproj`** (C# Project) เป็นไฟล์ XML (หรือ SDK-style รูปแบบใหม่ที่สั้นกว่า) ที่บอก .NET SDK เกี่ยวกับโปรเจกต์ของคุณ

### 8.3.1 เนื้อหาของ HelloWorldProject.csproj (SDK-style)

เปิดไฟล์ด้วย Notepad หรือ VS Code:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

</Project>
```

**อธิบายแต่ละแท็ก:**

| แท็ก | ความหมาย |
|------|-----------|
| `<Project Sdk="...">` | ระบุ SDK (Microsoft.NET.Sdk สำหรับ Console, Web, ClassLib) |
| `<OutputType>Exe</OutputType>` | ประเภทเอาต์พุต: `Exe` = executable, `Library` = DLL |
| `<TargetFramework>net9.0</TargetFramework>` | เวอร์ชัน .NET เป้าหมาย (net9.0, net8.0, net6.0) |
| `<Nullable>enable</Nullable>` | เปิดใช้งาน nullable reference types (ป้องกัน null reference exception) |
| `<ImplicitUsings>enable</ImplicitUsings>` | รวม using อัตโนมัติ (global using) |

### 8.3.2 การเพิ่มแพ็กเกจ NuGet

แก้ไขไฟล์ `.csproj` หรือใช้คำสั่ง `dotnet add package`:

```bash
dotnet add package Newtonsoft.Json
```

ไฟล์ `.csproj` จะถูกเพิ่ม:

```xml
<ItemGroup>
  <PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
</ItemGroup>
```

### 8.3.3 การเพิ่มไฟล์โค้ดใหม่

ถ้าคุณสร้างไฟล์ `Product.cs` ในโฟลเดอร์เดียวกับ `.csproj` จะถูกคอมไพล์อัตโนมัติ (ไม่ต้องประกาศใน .csproj)

---

## 8.4 ไฟล์ Program.cs – จุดเริ่มต้นของแอปพลิเคชัน

### 8.4.1 โค้ดเริ่มต้น (Top-level statements)

```csharp
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
```

นี่คือรูปแบบ **Top-level statements** (C# 9+) ที่ไม่ต้องมีคลาสและเมธอด Main อย่างชัดเจน คอมไพลเลอร์จะสร้างให้อัตโนมัติ

### 8.4.2 รูปแบบดั้งเดิม (ที่มี Main)

ถ้าคุณไม่ชอบ Top-level statements ให้ลบโค้ดเดิมแล้วพิมพ์:

```csharp
using System;

namespace HelloWorldProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
```

ทั้งสองแบบทำงานเหมือนกัน เลือกแบบที่ถนัด

### 8.4.3 การรับอาร์กิวเมนต์จาก命令行

ด้วย Top-level statements:

```csharp
if (args.Length > 0)
{
    Console.WriteLine($"Hello {args[0]}!");
}
else
{
    Console.WriteLine("Hello, World!");
}
```

รันด้วย: `dotnet run -- Somchai`

### 8.4.4 การส่งค่ากลับ (exit code)

```csharp
int result = DoSomething();
return result;  // 0 = success, ไม่ใช่ 0 = error
```

---

## 8.5 โฟลเดอร์ obj และ bin – ผลลัพธ์จากการคอมไพล์

### 8.5.1 โฟลเดอร์ obj (object files)

- เก็บไฟล์ชั่วคราวระหว่างการคอมไพล์ เช่น `.AssemblyInfo.cs`, `.cache`, `.props`
- ถูกสร้างขึ้นใหม่ทุกครั้งที่ build
- **ห้ามแก้ไขด้วยมือ** และควรเพิ่มใน `.gitignore`

### 8.5.2 โฟลเดอร์ bin (binaries)

- เก็บผลลัพธ์ที่คอมไพล์เสร็จแล้ว: `.dll` (assembly), `.exe` (บน Windows), `.pdb` (debug symbols)
- แบ่งตาม configuration (`Debug`, `Release`) และ target framework (`net9.0`)
- **ห้ามแก้ไขด้วยมือ**

### 8.5.3 ความแตกต่างระหว่าง Debug และ Release

| Configuration | Optimization | Debug symbols | ใช้เมื่อ |
|---------------|--------------|---------------|---------|
| Debug | ❌ (ช้า) | ✅ (เต็ม) | พัฒนา, debug |
| Release | ✅ (เร็ว) | ⚠️ (บางส่วน) | พร้อม deploy |

เลือก configuration ด้วย:

```bash
dotnet build -c Release
dotnet run -c Release
```

---

## 8.6 การทำงานของ `dotnet run` และ `dotnet build`

### 8.6.1 ขั้นตอนการทำงานของ `dotnet run`

1. **Restore** – ดาวน์โหลดแพ็กเกจ NuGet ที่อ้างอิง (ถ้ายังไม่มี)
2. **Build** – คอมไพล์โค้ดเป็น IL (ไปที่ `bin/`)
3. **Execute** – รันแอปพลิเคชัน (เรียก `dotnet exec`)

### 8.6.2 คำสั่งที่เกี่ยวข้อง

| คำสั่ง | คำอธิบาย |
|--------|-----------|
| `dotnet build` | คอมไพล์อย่างเดียว ไม่รัน |
| `dotnet run` | build + รัน |
| `dotnet clean` | ลบโฟลเดอร์ bin และ obj |
| `dotnet restore` | ดาวน์โหลด dependencies (มักทำอัตโนมัติ) |
| `dotnet publish` | เตรียมแอปสำหรับ deployment (รวม runtime ถ้าต้องการ) |

### 8.6.3 ตัวอย่างการ publish แบบ standalone (ไม่ต้องติดตั้ง .NET บนเครื่องเป้าหมาย)

```bash
dotnet publish -c Release --self-contained -r win-x64 -o ./publish
```

สร้าง `.exe` ที่ทำงานได้บน Windows โดยไม่ต้องติดตั้ง .NET

---

## 8.7 การแก้ไขและรันโปรเจกต์ด้วย Visual Studio

### 8.7.1 เปิดโปรเจกต์

- ดับเบิลคลิกไฟล์ `.csproj` หรือเปิด Visual Studio → “Open a project or solution”

### 8.7.2 โครงสร้างใน Solution Explorer

```
Solution 'HelloWorldProject' (1 project)
└── HelloWorldProject
    ├── Dependencies
    │   ├── Frameworks
    │   │   └── Microsoft.NETCore.App
    │   └── Packages (NuGet)
    ├── Program.cs
    └── (ไฟล์อื่น ๆ)
```

### 8.7.3 การรันและดีบัก

- กด **F5** – รันในโหมดดีบัก (Debug)
- กด **Ctrl+F5** – รันโดยไม่ดีบัก (Start without debugging)
- ตั้ง **breakpoint**: คลิกที่ margin ซ้ายของบรรทัดโค้ด

---

## 8.8 ตารางสรุปโครงสร้างไฟล์

| ไฟล์/โฟลเดอร์ | สร้างเมื่อ | ควร commit ขึ้น Git? | บทบาท |
|----------------|-----------|----------------------|--------|
| `*.csproj` | `dotnet new` | ✅ ใช่ | กำหนดค่าโปรเจกต์ |
| `Program.cs` | `dotnet new` | ✅ ใช่ | โค้ดหลัก |
| ไฟล์ `.cs` อื่น ๆ | ด้วยมือ | ✅ ใช่ | คลาส, เมธอดเพิ่มเติม |
| `obj/` | `dotnet build` | ❌ ไม่ (เพิ่มใน .gitignore) | วัตถุชั่วคราว |
| `bin/` | `dotnet build` | ❌ ไม่ | ผลลัพธ์คอมไพล์ |
| `appsettings.json` | `dotnet new webapi` | ✅ ใช่ (แต่ไม่ควรมี secret) | การตั้งค่า |
| `Properties/launchSettings.json` | `dotnet new webapi` | ⚠️ เฉพาะ environment variables | การตั้งค่าการรัน |

---

## 8.9 ตัวอย่างโค้ดและการทดลอง

**ตัวอย่างที่ 8.1: Hello World แบบโต้ตอบ**

```csharp
// โปรแกรมทักทายแบบมีชื่อ
Console.Write("กรุณาพิมพ์ชื่อของคุณ: ");
string? name = Console.ReadLine();

if (string.IsNullOrWhiteSpace(name))
{
    Console.WriteLine("ไม่ได้พิมพ์ชื่อนะครับ");
}
else
{
    Console.WriteLine($"สวัสดี {name} ยินดีต้อนรับสู่โลก C#");
}

Console.Write("กด Enter เพื่อปิด...");
Console.ReadLine();
```

**ตัวอย่างที่ 8.2: การใช้ args และ return code**

สร้างไฟล์ `Program.cs` ใหม่:

```csharp
if (args.Length == 0)
{
    Console.WriteLine("ERROR: กรุณาระบุชื่อ");
    return 1;  // exit code แสดง error
}

Console.WriteLine($"Hello {args[0]}!");
return 0;  // success
```

รัน: `dotnet run -- Somchai`  
ตรวจสอบ exit code ใน Windows: `echo %ERRORLEVEL%` ใน Linux/macOS: `echo $?`

---

## 8.10 แบบฝึกหัดท้ายบท (4 ข้อ)

🧪 **แบบฝึกหัดที่ 8.1:**  
สร้างโปรเจกต์ Console ชื่อ `MyCalculator` จากนั้นเพิ่มไฟล์ใหม่ `MathHelper.cs` (คลาสที่มีเมธอด Add, Subtract) และเรียกใช้ใน `Program.cs` (Hint: คลาสต้องเป็น public และอยู่ใน namespace เดียวกัน)

🧪 **แบบฝึกหัดที่ 8.2:**  
เปิดไฟล์ `.csproj` ของโปรเจกต์ที่สร้าง แล้วเปลี่ยน `<TargetFramework>` จาก `net9.0` เป็น `net8.0` แล้วรัน `dotnet restore` และ `dotnet build` สังเกตความแตกต่าง (ต้องติดตั้ง .NET 8 SDK หรือไม่?)

🧪 **แบบฝึกหัดที่ 8.3:**  
ใช้ `dotnet new gitignore` เพื่อสร้างไฟล์ `.gitignore` ที่เหมาะสมสำหรับ .NET (หรือ copy จาก GitHub) แล้วอธิบายว่าเหตุใดจึงต้องไม่รวมโฟลเดอร์ `bin/` และ `obj/`

🧪 **แบบฝึกหัดที่ 8.4 (ท้าทาย):**  
สร้างโปรเจกต์ Class Library (`dotnet new classlib -n MyLib`) และโปรเจกต์ Console ที่อ้างอิงถึง MyLib (ใช้ `<ProjectReference>` ใน .csproy หรือ `dotnet add reference`). ให้ Console เรียกเมธอดจาก Library และแสดงผล

---

## 8.11 แหล่งอ้างอิง

- 🔗 **.NET Project SDK overview** – [https://docs.microsoft.com/en-us/dotnet/core/project-sdk/overview](https://docs.microsoft.com/en-us/dotnet/core/project-sdk/overview)
- 🔗 **csproj format for .NET Core** – [https://docs.microsoft.com/en-us/dotnet/core/tools/csproj](https://docs.microsoft.com/en-us/dotnet/core/tools/csproj)
- 🔗 **Top-level statements (C# 9)** – [https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/top-level-statements](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/program-structure/top-level-statements)
- 🔗 **.NET Core application deployment** – [https://docs.microsoft.com/en-us/dotnet/core/deploying/](https://docs.microsoft.com/en-us/dotnet/core/deploying/)
- 🔗 **Common .NET project files to gitignore** – [https://github.com/github/gitignore/blob/main/VisualStudio.gitignore](https://github.com/github/gitignore/blob/main/VisualStudio.gitignore)

---

## สรุปท้ายบท

บทที่ 8 ได้อธิบายโครงสร้างไฟล์ของโปรเจกต์ C# อย่างละเอียด ตั้งแต่ `.csproj` (กำหนดค่าโปรเจกต์), `Program.cs` (จุดเริ่มต้น), ไปจนถึงโฟลเดอร์ `obj/` และ `bin/` ที่เก็บผลลัพธ์จากการคอมไพล์ คุณยังได้เรียนรู้การทำงานของ `dotnet run`, `dotnet build`, และการ publish แอปพลิเคชัน

ความเข้าใจในโครงสร้างนี้จะช่วยให้คุณจัดการโปรเจกต์ได้อย่างเป็นระเบียบ และสามารถแก้ปัญหาเกี่ยวกับการ build หรือการอ้างอิง dependencies ได้

**ในบทถัดไป (บทที่ 9)** เราจะเริ่มเรียนรู้ **ตัวแปรและชนิดข้อมูลเบื้องต้น** ในภาษา C# เช่น string, int, double, bool พร้อมตัวอย่างการประกาศและการใช้งาน

---

*หมายเหตุ: บทที่ 8 นี้มีความยาวประมาณ 1,900 คำ*

---

(ดำเนินการส่งบทที่ 9 ต่อไปโดยอัตโนมัติ)