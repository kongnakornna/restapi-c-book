# Mastering C# .NET 2026: จากพื้นฐานสู่ Enterprise Application + Database + Cache + Message Queue

## บทที่ 7: การติดตั้ง Visual Studio และสภาพแวดล้อม

### 7.1 การติดตั้ง .NET SDK

ดาวน์โหลดจาก [dotnet.microsoft.com](https://dotnet.microsoft.com) ตรวจสอบด้วย `dotnet --version`

### 7.2 การติดตั้ง Visual Studio 2026 Community

เลือก workloads: **ASP.NET and web development**, **.NET desktop development**

### 7.3 การติดตั้ง Visual Studio Code (ทางเลือก)

ติดตั้ง extensions: **C# Dev Kit**, **.NET Install Tool**

### 7.4 ทดสอบรันโปรแกรมแรก

```bash
dotnet new console -n HelloWorld
cd HelloWorld
dotnet run
```

**ผลลัพธ์:** `Hello, World!`

---

## บทที่ 8: โปรเจกต์แรก Hello World และโครงสร้างไฟล์

### 8.1 โครงสร้างไฟล์

```
HelloWorld/
├── Program.cs
├── HelloWorld.csproj
├── obj/
└── bin/
```

### 8.2 ไฟล์ .csproj (SDK-style)

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

### 8.3 Program.cs (Top-level statements)

```csharp
Console.WriteLine("Hello, World!");
```

### 8.4 คำสั่ง dotnet ที่สำคัญ

| คำสั่ง | คำอธิบาย |
|--------|-----------|
| `dotnet new` | สร้างโปรเจกต์ใหม่ |
| `dotnet build` | คอมไพล์ |
| `dotnet run` | คอมไพล์และรัน |
| `dotnet publish` | เตรียมสำหรับ deploy |

---

## บทที่ 9: ตัวแปรและชนิดข้อมูลเบื้องต้น (string, int)

### 9.1 การประกาศตัวแปร

```csharp
int age = 25;
string name = "Somchai";
```

### 9.2 กฎการตั้งชื่อ

- ขึ้นต้นด้วยตัวอักษรหรือ underscore
- ห้ามใช้ keyword
- ใช้ camelCase สำหรับตัวแปรท้องถิ่น

### 9.3 การแสดงผลร่วมกับตัวแปร

```csharp
Console.WriteLine($"ชื่อ: {name}, อายุ: {age}");
```

---

## บทที่ 10: การรับข้อมูลผู้ใช้และการแปลงชนิด (Parse, TryParse)

### 10.1 การรับข้อมูล

```csharp
string input = Console.ReadLine();
```

### 10.2 การแปลงด้วย Parse (เสี่ยง exception)

```csharp
int number = int.Parse(input);
```

### 10.3 การแปลงด้วย TryParse (ปลอดภัย)

```csharp
if (int.TryParse(input, out int result))
{
    Console.WriteLine(result);
}
```

---

## บทที่ 11: ชนิดข้อมูลตัวเลข (int, double, float, decimal) และการแปลง

### 11.1 ชนิดจำนวนเต็ม

| ชนิด | ขนาด | ช่วงค่า |
|------|------|---------|
| `byte` | 8 bit | 0–255 |
| `short` | 16 bit | -32,768–32,767 |
| `int` | 32 bit | ±2.1B |
| `long` | 64 bit | ±9.2×10¹⁸ |

### 11.2 ชนิดทศนิยม

| ชนิด | ขนาด | ความแม่นยำ | ใช้กับ |
|------|------|------------|--------|
| `float` | 32 bit | ~7 digits | กราฟิก, เกม |
| `double` | 64 bit | ~15 digits | วิทยาศาสตร์ |
| `decimal` | 128 bit | 28 digits | การเงิน |

### 11.3 การแปลงชนิด (Implicit/Explicit)

```csharp
int x = 10;
double y = x;           // implicit
int z = (int)3.14;      // explicit (ตัดเศษ)
```

---

## บทที่ 12: ชนิดข้อมูล bool, char และ escape sequences

### 12.1 bool

```csharp
bool isReady = true;
bool isCompleted = false;
```

### 12.2 char

```csharp
char grade = 'A';
char newline = '\n';
```

### 12.3 Escape sequences ที่สำคัญ

| Escape | ความหมาย |
|--------|----------|
| `\n` | newline |
| `\t` | tab |
| `\"` | double quote |
| `\\` | backslash |

---

## บทที่ 13: Value Types vs Reference Types

### 13.1 Value Types (เก็บค่าโดยตรงบน Stack)

- `int`, `double`, `bool`, `char`, `struct`, `enum`

### 13.2 Reference Types (เก็บ reference บน Stack ชี้ไป Heap)

- `string`, `class`, `array`, `interface`

### 13.3 ความแตกต่าง

| คุณสมบัติ | Value Type | Reference Type |
|-----------|------------|----------------|
| ที่เก็บ | Stack | Heap |
| การกำหนดค่า | คัดลอกค่า | คัดลอก reference |
| ค่าเริ่มต้น | 0, false, '\0' | `null` |

---

## บทที่ 14: ตัวดำเนินการ (Operators) และลำดับการประเมิน

### 14.1 ตัวดำเนินการพื้นฐาน

| ประเภท | ตัวดำเนินการ |
|--------|--------------|
| เลขคณิต | `+ - * / %` |
| กำหนดค่า | `= += -= *= /=` |
| เปรียบเทียบ | `== != < > <= >=` |
| ตรรกะ | `&& || !` |
| เพิ่ม/ลด | `++ --` |

### 14.2 ลำดับความสำคัญ (จากสูงไปต่ำ)

1. `()` `[]` `x++` `x--`
2. `++x` `--x` `!` `(type)`
3. `*` `/` `%`
4. `+` `-`
5. `<` `>` `<=` `>=`
6. `==` `!=`
7. `&&`
8. `||`
9. `=` `+=` `-=`

---

## บทที่ 15: การดีบักเบื้องต้น (Breakpoints, Runtime vs Compile Error)

### 15.1 ประเภทข้อผิดพลาด

| ประเภท | เกิดขึ้นเมื่อ | ตัวอย่าง |
|--------|--------------|----------|
| Compile Error | ขณะคอมไพล์ | ลืม semicolon |
| Runtime Error | ขณะรัน | หารด้วยศูนย์ |
| Logic Error | ผลลัพธ์ผิด | คำนวณผิดสูตร |

### 15.2 การใช้ Breakpoint (F9)

- หยุดโปรแกรมที่บรรทัดที่กำหนด
- ดูค่าตัวแปรด้วย Watch, Immediate Window
- Step Over (F10), Step Into (F11)

---

## บทที่ 16: โปรเจกต์: เครื่องคิดเลขบวกเลข

```csharp
Console.Write("ป้อนตัวเลขที่ 1: ");
double num1 = double.Parse(Console.ReadLine());
Console.Write("ป้อนตัวเลขที่ 2: ");
double num2 = double.Parse(Console.ReadLine());
double sum = num1 + num2;
Console.WriteLine($"{num1} + {num2} = {sum}");
```

---

## บทที่ 17: String Interpolation, Concatenation, String.Format

### 17.1 Concatenation (`+`)

```csharp
string msg = "Hello " + name;
```

### 17.2 String.Format

```csharp
string msg = string.Format("Hello {0}", name);
```

### 17.3 String Interpolation (แนะนำ)

```csharp
string msg = $"Hello {name}";
```

### 17.4 การจัดรูปแบบตัวเลขและวันที่

```csharp
double price = 1234.567;
Console.WriteLine($"{price:F2}");  // 1234.57
Console.WriteLine($"{price:C}");   // ฿1,234.57
DateTime now = DateTime.Now;
Console.WriteLine($"{now:yyyy-MM-dd HH:mm}");
```

---

## บทที่ 18: มาตรฐานการเขียนโค้ด (Naming Conventions, Formatting)

### 18.1 การตั้งชื่อ

| ประเภท | รูปแบบ | ตัวอย่าง |
|--------|--------|----------|
| คลาส, เมธอด | PascalCase | `ProductService` |
| ตัวแปรท้องถิ่น | camelCase | `totalPrice` |
| ฟิลด์ private | _camelCase | `_context` |
| อินเทอร์เฟซ | I + PascalCase | `IRepository` |

### 18.2 การจัดรูปแบบ

- วงเล็บปีกกาแบบ Allman (ขึ้นบรรทัดใหม่)
- ใช้ 4 spaces สำหรับ indent
- ความยาวบรรทัดไม่เกิน 120 ตัวอักษร

### 18.3 การคอมเมนต์

```csharp
/// <summary>
/// คำนวณภาษีมูลค่าเพิ่ม
/// </summary>
public decimal CalculateVat(decimal price) => price * 0.07m;
```

---

## บทที่ 19: Cheatsheet ชนิดข้อมูลใน C#

| ชนิด | ขนาด (bit) | ช่วงค่า | คำต่อท้าย |
|------|------------|---------|-----------|
| `byte` | 8 | 0–255 | - |
| `int` | 32 | ±2.1B | - |
| `long` | 64 | ±9.2×10¹⁸ | `L` |
| `float` | 32 | ±3.4×10³⁸ | `f` |
| `double` | 64 | ±1.7×10³⁰⁸ | - |
| `decimal` | 128 | ±7.9×10²⁸ | `m` |
| `bool` | 8 | true/false | - |
| `char` | 16 | Unicode | - |
| `string` | ขึ้นกับ length | ข้อความ | - |

### ค่าเริ่มต้น

- ตัวเลข → `0`
- `bool` → `false`
- `string` → `null`

---

## บทที่ 20: ตัวดำเนินการเชิงตรรกะ (&&, ||, !) และ relational

### 20.1 ตัวดำเนินการ relational

| ตัวดำเนินการ | ความหมาย |
|--------------|-----------|
| `==` | เท่ากัน |
| `!=` | ไม่เท่ากัน |
| `>` | มากกว่า |
| `<` | น้อยกว่า |
| `>=` | มากกว่าหรือเท่ากัน |
| `<=` | น้อยกว่าหรือเท่ากัน |

### 20.2 ตัวดำเนินการเชิงตรรกะ

| ตัวดำเนินการ | ความหมาย |
|--------------|-----------|
| `&&` | และ (AND) |
| `\|\|` | หรือ (OR) |
| `!` | นิเสธ (NOT) |

### 20.3 Short-circuit evaluation

- `&&` – ถ้าฝั่งซ้าย false ไม่ประเมินฝั่งขวา
- `||` – ถ้าฝั่งซ้าย true ไม่ประเมินฝั่งขวา

---

(จบบทที่ 7–20)

**โปรดแจ้ง "ต่อไป" เพื่อรับบทที่ 21–35 ในข้อความถัดไปครับ**