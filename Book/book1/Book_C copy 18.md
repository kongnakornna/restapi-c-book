# Mastering C# .NET 2026: จากพื้นฐานสู่ Enterprise Application + Database + Cache + Message Queue

## บทที่ 18: มาตรฐานการเขียนโค้ด (Naming Conventions, Formatting)

---

### สารบัญย่อยของบทที่ 18

18.1 เหตุใดมาตรฐานการเขียนโค้ดจึงสำคัญ  
18.2 หลักการตั้งชื่อ (Naming Conventions)  
18.3 การจัดรูปแบบโค้ด (Code Formatting)  
18.4 การคอมเมนต์ (Comments) และเอกสารในโค้ด  
18.5 การจัดระเบียบ using directives และ namespace  
18.6 การใช้ตัวย่อและคำพิเศษ  
18.7 เครื่องมือช่วยรักษามาตรฐาน (EditorConfig, formatter)  
18.8 ตัวอย่างโค้ดที่ดี vs ไม่ดี  
18.9 ตารางสรุปมาตรฐานการเขียนโค้ด  
18.10 แบบฝึกหัดท้ายบท  
18.11 แหล่งอ้างอิง  

---

## 18.1 เหตุใดมาตรฐานการเขียนโค้ดจึงสำคัญ

มาตรฐานการเขียนโค้ด (coding conventions) คือชุดแนวปฏิบัติที่ทีมพัฒนาตกลงร่วมกัน เพื่อให้โค้ดในโปรเจกต์มีลักษณะสอดคล้องเป็นหนึ่งเดียว แม้จะมีคนหลายคนเขียนหรือเขียนคนละเวลากัน

**ประโยชน์ของมาตรฐาน:**
- อ่านง่าย เข้าใจเร็ว – โค้ดที่จัดรูปแบบสม่ำเสมอช่วยลดเวลาในการทำความเข้าใจ
- ลดข้อผิดพลาด – การตั้งชื่อที่ดีช่วยป้องกันการใช้ตัวแปรผิด
- บำรุงรักษาง่าย – นักพัฒนาคนใหม่สามารถเข้ามาทำต่อได้ทันที
- รองรับเครื่องมืออัตโนมัติ – เช่น refactoring, code analysis

> ⭐ **หัวข้อสำคัญ:** “Code is read much more often than it is written.” (โค้ดถูกอ่านบ่อยกว่าถูกเขียน) – ลงทุนเวลาในการทำให้โค้ดอ่านง่าย

---

## 18.2 หลักการตั้งชื่อ (Naming Conventions)

### 18.2.1 รูปแบบการเขียน (Casing)

| รูปแบบ | ลักษณะ | ตัวอย่าง | ใช้กับ |
|--------|--------|----------|--------|
| **PascalCase** | ขึ้นต้นด้วยตัวพิมพ์ใหญ่ทุกคำ | `ProductService`, `GetUserById` | คลาส, เมธอด, พร็อพเพอร์ตี้, event |
| **camelCase** | ขึ้นต้นด้วยตัวพิมพ์เล็ก คำถัดไปพิมพ์ใหญ่ | `totalPrice`, `userName` | ตัวแปรท้องถิ่น, พารามิเตอร์ |
| **_camelCase** | underscore + camelCase | `_dbContext`, `_logger` | ฟิลด์ private (นิยม) |
| **UPPER_CASE** | ตัวพิมพ์ใหญ่ทั้งหมด ขีดคั่น | `MAX_RETRY_COUNT` | ค่าคงที่ (const) – บางทีมใช้ PascalCase |

### 18.2.2 กฎการตั้งชื่อตามประเภท

| ประเภท | รูปแบบ | ตัวอย่างที่ถูกต้อง | ตัวอย่างที่ผิด |
|---------|--------|-------------------|----------------|
| คลาส, struct | PascalCase | `Customer`, `OrderItem` | `customer`, `order_item` |
| อินเทอร์เฟซ | I + PascalCase | `IRepository`, `ILogger` | `Repository`, `ILog` (ชื่อไม่สื่อ) |
| เมธอด | PascalCase (Verb) | `CalculateTotal()`, `SendEmail()` | `calc()`, `send_email()` |
| พร็อพเพอร์ตี้ | PascalCase (Noun) | `FirstName`, `Price` | `firstName`, `_price` |
| ตัวแปรท้องถิ่น | camelCase | `itemCount`, `isValid` | `ItemCount`, `iCnt` |
| พารามิเตอร์ | camelCase | `productId`, `userName` | `id`, `name` (สั้นเกินไปถ้าไม่ชัด) |
| ฟิลด์ private | _camelCase | `_context`, `_cache` | `m_context`, `context_` |
| ค่าคงที่ (const) | PascalCase (หรือ UPPER_CASE) | `MaxPageSize`, `DEFAULT_NAME` | `maxPageSize`, `DEFAULTNAME` |
| enum | PascalCase (singular) | `Color`, `OrderStatus` | `Colors`, `order_status` |

### 18.2.3 หลักการตั้งชื่อที่ดี

1. **สื่อความหมาย** – ใช้ชื่อที่บอกหน้าที่หรือความหมายของตัวแปร
   ```csharp
   int d;               // ไม่ดี: d คืออะไร?
   int daysSinceLastLogin;  // ดี
   ```

2. **หลีกเลี่ยงตัวย่อ** – เว้นแต่เป็นมาตรฐานสากล (ID, URL, HTTP)
   ```csharp
   int custCnt;    // ไม่ดี
   int customerCount; // ดี
   ```

3. **ยาวพอเหมาะ** – ไม่สั้นเกินไปและไม่ยาวเกินไป (20-30 ตัวอักษร)

4. **ใช้คำนามสำหรับตัวแปร/พร็อพเพอร์ตี้** – `total`, `productList`
5. **ใช้คำกริยาสำหรับเมธอด** – `GetData()`, `SaveChanges()`

---

## 18.3 การจัดรูปแบบโค้ด (Code Formatting)

### 18.3.1 วงเล็บปีกกา (Braces)

ใช้รูปแบบ **Allman** (วงเล็บปีกกาขึ้นบรรทัดใหม่) ตามที่แนะนำในบทที่ 4:

```csharp
// ดี - Allman style
public void ProcessOrder(Order order)
{
    if (order == null)
    {
        throw new ArgumentNullException(nameof(order));
    }
}
```

สำหรับบล็อกสั้น ๆ (หนึ่งคำสั่ง) อาจละวงเล็บได้ แต่ควรระวัง:

```csharp
if (condition)
    DoSomething();   // อนุโลม แต่บาง guideline ไม่แนะนำ
```

### 18.3.2 การย่อหน้า (Indentation)

- ใช้ **4 spaces** ห้ามใช้แท็บ (tab)
- แต่ละระดับ indent เพิ่ม 4 spaces

### 18.3.3 การเว้นวรรค (Spacing)

```csharp
// เว้นวรรคหลัง if, for, foreach, while, catch
if (x > 0) { }

// เว้นวรรครอบ binary operators
int sum = a + b;
bool result = (x > 5) && (y < 10);

// ไม่เว้นวรรคหลัง ( และ ก่อน )
methodCall(a, b, c);
```

### 18.3.4 ความยาวบรรทัด

ไม่ควรเกิน **120 ตัวอักษร** ถ้าเกินให้ขึ้นบรรทัดใหม่:

```csharp
// ไม่ดี (ยาวเกิน)
var result = someVeryLongMethodName(parameter1, parameter2, parameter3, parameter4, parameter5);

// ดี (แบ่งบรรทัด)
var result = someVeryLongMethodName(
    parameter1,
    parameter2,
    parameter3,
    parameter4,
    parameter5);
```

---

## 18.4 การคอมเมนต์ (Comments) และเอกสารในโค้ด

### 18.4.1 คอมเมนต์บรรทัดเดียว (`//`)

ใช้อธิบาย **ทำไม** (why) ไม่ใช่อธิบาย **อะไร** (what) – เพราะสิ่งที่โค้ดทำควรอ่านได้จากโค้ดเอง

```csharp
// ไม่ดี: บอกสิ่งที่เห็นอยู่แล้ว
i++; // เพิ่ม i ขึ้น 1

// ดี: บอกเหตุผล
i++; // ข้ามเรกคอร์ดนี้เพราะซ้ำกับ ID ก่อนหน้า
```

### 18.4.2 XML Documentation (`///`)

ใช้สำหรับ document public API (คลาส, เมธอด, พร็อพเพอร์ตี้):

```csharp
/// <summary>
/// คำนวณภาษีมูลค่าเพิ่ม (VAT) จากราคาสินค้า
/// </summary>
/// <param name="price">ราคาก่อนภาษี</param>
/// <param name="rate">อัตราภาษี (เปอร์เซ็นต์)</param>
/// <returns>จำนวนภาษีที่ต้องจ่าย</returns>
public decimal CalculateVat(decimal price, decimal rate)
{
    return price * rate / 100;
}
```

เมื่อเขียน XML doc แล้ว IntelliSense จะแสดงคำอธิบายเวลานำเมธอดไปใช้

### 18.4.3 คอมเมนต์ TODO และ HACK

ใช้ `// TODO:` สำหรับงานที่ต้องทำเพิ่ม, `// HACK:` สำหรับแก้ปัญหาชั่วคราว:

```csharp
// TODO: เพิ่ม validation สำหรับ input ที่เป็นลบ
// HACK: รอให้ทีม infrastructure สร้าง API ก่อน
```

Visual Studio จะแสดง TODO ใน Task List

---

## 18.5 การจัดระเบียบ using directives และ namespace

### 18.5.1 การเรียงลำดับ using

- เรียงตามตัวอักษร
- กลุ่ม `System.*` ไว้ด้านบนสุด
- เว้นบรรทัดว่างระหว่างกลุ่ม

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using MyApp.Data;
using MyApp.Models;
```

### 18.5.2 Global using (C# 10+)

ในไฟล์ `GlobalUsings.cs` (หรือใน `.csproj`) ประกาศ using ที่ใช้บ่อยทั้งโปรเจกต์:

```csharp
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading.Tasks;
```

### 18.5.3 File-scoped namespace (C# 10+)

ลดการเยื้องซ้ำซ้อน:

```csharp
namespace MyApp.Services;  // ไม่ต้องมีวงเล็บปีกกา

public class ProductService
{
    // ...
}
```

---

## 18.6 การใช้ตัวย่อและคำพิเศษ

| คำ | รูปแบบที่ถูกต้อง | ตัวอย่าง |
|----|-----------------|----------|
| ID | PascalCase: `Id` (ไม่ใช่ `ID`) | `productId`, `UserId` |
| URL | `Url` (ไม่ใช่ `URL`) | `imageUrl` |
| HTTP | `Http` (ไม่ใช่ `HTTP`) | `HttpClient`, `HttpStatusCode` |
| JSON | `Json` | `JsonSerializer` |
| XML | `Xml` | `XmlDocument` |

---

## 18.7 เครื่องมือช่วยรักษามาตรฐาน (EditorConfig, formatter)

### 18.7.1 EditorConfig

สร้างไฟล์ `.editorconfig` ที่รากโปรเจกต์ เพื่อบังคับรูปแบบใน Visual Studio / VS Code:

```ini
root = true

[*]
indent_style = space
indent_size = 4
charset = utf-8
trim_trailing_whitespace = true
insert_final_newline = true

[*.cs]
csharp_indent_braces = false
csharp_new_line_before_open_brace = all
```

### 18.7.2 dotnet format

คำสั่งสำหรับจัดรูปแบบโค้ดอัตโนมัติ:

```bash
dotnet format
dotnet format --verify-no-changes  # ตรวจสอบว่าจัดรูปแบบถูกต้อง
```

---

## 18.8 ตัวอย่างโค้ดที่ดี vs ไม่ดี

**ตัวอย่างที่ไม่ดี:**

```csharp
class data{
public int x,y;
public int calc(){
return x+y;
}
}
```

**ตัวอย่างที่ดี (ตามมาตรฐาน):**

```csharp
/// <summary>
/// เก็บข้อมูลพิกัดจุดบนระนาบ 2 มิติ
/// </summary>
public class Point
{
    public int X { get; set; }
    public int Y { get; set; }
    
    /// <summary>
    /// คำนวณผลรวมของ X และ Y
    /// </summary>
    public int CalculateSum()
    {
        return X + Y;
    }
}
```

---

## 18.9 ตารางสรุปมาตรฐานการเขียนโค้ด

| องค์ประกอบ | มาตรฐาน |
|------------|----------|
| คลาส, เมธอด, property | PascalCase |
| ตัวแปรท้องถิ่น, พารามิเตอร์ | camelCase |
| ฟิลด์ private | `_camelCase` |
| อินเทอร์เฟซ | `I` + PascalCase |
| ค่าคงที่ | PascalCase หรือ UPPER_CASE |
| วงเล็บปีกกา | Allman (ขึ้นบรรทัดใหม่) |
| Indentation | 4 spaces |
| ความยาวบรรทัด | ≤ 120 ตัวอักษร |
| การคอมเมนต์ | XML doc สำหรับ public API |
| การเว้นวรรค | หลัง `if`, `for`, รอบ binary operator |

---

## 18.10 แบบฝึกหัดท้ายบท (3 ข้อ)

🧪 **แบบฝึกหัดที่ 18.1:**  
จงแก้ไขโค้ดต่อไปนี้ให้เป็นไปตามมาตรฐาน (ตั้งชื่อใหม่, จัดรูปแบบ, เพิ่ม XML doc):
```csharp
class calc{public int a,b;public int add(){return a+b;}}
```

🧪 **แบบฝึกหัดที่ 18.2:**  
สร้างไฟล์ `.editorconfig` อย่างง่ายสำหรับโปรเจกต์ C# ของคุณ (กำหนด indent = 4 spaces, ใช้ Allman braces)

🧪 **แบบฝึกหัดที่ 18.3 (ท้าทาย):**  
นำโปรเจกต์เครื่องคิดเลขจากบทที่ 16 มา refactor ให้เป็นไปตามมาตรฐานทั้งหมด (เปลี่ยนชื่อตัวแปร, จัดรูปแบบ, เพิ่ม XML doc สำหรับเมธอดถ้ามี)

---

## 18.11 แหล่งอ้างอิง

- 🔗 **C# Coding Conventions (Microsoft)** – [https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- 🔗 **Identifier names** – [https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/identifier-names](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/identifier-names)
- 🔗 **XML documentation comments** – [https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/xmldoc/)
- 🔗 **EditorConfig for .NET** – [https://docs.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/editorconfig](https://docs.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/editorconfig)

---

## สรุปท้ายบท

บทที่ 18 ได้เรียนรู้มาตรฐานการเขียนโค้ดที่เป็นที่ยอมรับในวงการ .NET ซึ่งรวมถึง:
- การตั้งชื่อแบบ PascalCase, camelCase, _camelCase
- การจัดรูปแบบวงเล็บปีกกา การย่อหน้า การเว้นวรรค
- การคอมเมนต์และ XML documentation
- การจัดเรียง using directives และการใช้ global using
- การใช้ `.editorconfig` และ `dotnet format` เพื่อรักษามาตรฐานอัตโนมัติ

การปฏิบัติตามมาตรฐานเหล่านี้จะทำให้โค้ดของคุณเป็นมืออาชีพและทำงานร่วมกับผู้อื่นได้อย่างราบรื่น

**ในบทถัดไป (บทที่ 19)** เราจะนำเสนอ **Cheatsheet ชนิดข้อมูลใน C#** สรุปทุกชนิดข้อมูล ขนาด ค่าเริ่มต้น และช่วงค่า เพื่อให้คุณใช้อ้างอิงด่วน

 