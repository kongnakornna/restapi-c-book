# Mastering C# .NET 2026: จากพื้นฐานสู่ Enterprise Application + Database + Cache + Message Queue

## บทที่ 19: Cheatsheet ชนิดข้อมูลใน C#

---

### สารบัญย่อยของบทที่ 19

19.1 ภาพรวมชนิดข้อมูลใน C#  
19.2 ชนิดข้อมูลจำนวนเต็ม (Integer Types)  
19.3 ชนิดข้อมูลทศนิยม (Floating-point Types)  
19.4 ชนิดข้อมูลอื่น ๆ (`bool`, `char`, `string`, `object`, `decimal`)  
19.5 ค่าเริ่มต้นของชนิดข้อมูล (Default Values)  
19.6 การเลือกชนิดข้อมูลให้เหมาะสม  
19.7 ตารางสรุปฉบับด่วน (Quick Reference)  
19.8 ตัวอย่างโค้ดประกอบ  
19.9 แบบฝึกหัดท้ายบท  
19.10 แหล่งอ้างอิง  

---

## 19.1 ภาพรวมชนิดข้อมูลใน C#

C# มีชนิดข้อมูลที่ครอบคลุมทุกการใช้งาน ตั้งแต่ตัวเลขจำนวนเต็ม ทศนิยม ไปจนถึงข้อความและออบเจ็กต์ บทนี้รวบรวมเป็น **cheatsheet** สำหรับใช้อ้างอิงด่วนระหว่างพัฒนา

ชนิดข้อมูลใน C# แบ่งเป็น 2 กลุ่มใหญ่:

| กลุ่ม | ลักษณะ | ตัวอย่าง |
|------|--------|----------|
| **Value Types** | เก็บค่าโดยตรง, อยู่บน Stack | `int`, `double`, `bool`, `char`, `struct`, `enum` |
| **Reference Types** | เก็บ reference ไปยัง Heap | `string`, `object`, `class`, `array`, `delegate` |

> 💡 **เคล็ดลับ:** พิมพ์ `typeof(int)` หรือ `default(int)` ใน Immediate Window เพื่อดูข้อมูลชนิดขณะดีบัก

---

## 19.2 ชนิดข้อมูลจำนวนเต็ม (Integer Types)

| ชนิด | ขนาด (bit) | ช่วงค่า (ต่ำสุด – สูงสุด) | คำต่อท้าย | ใช้เมื่อ |
|------|------------|--------------------------|-----------|---------|
| `byte` | 8 | 0 ถึง 255 | - | ค่าน้อย, ประหยัด memory |
| `sbyte` | 8 | -128 ถึง 127 | - | ค่าน้อยที่ติดลบได้ |
| `short` | 16 | -32,768 ถึง 32,767 | - | ค่าปานกลาง |
| `ushort` | 16 | 0 ถึง 65,535 | - | ค่าปานกลางไม่ติดลบ |
| `int` | 32 | -2,147,483,648 ถึง 2,147,483,647 | - | **ค่าเริ่มต้นสำหรับจำนวนเต็ม** |
| `uint` | 32 | 0 ถึง 4,294,967,295 | `U` | ค่าบวกจำนวนมาก |
| `long` | 64 | -9,223,372,036,854,775,808 ถึง 9,223,372,036,854,775,807 | `L` | ค่ามาก (ID, ประชากรโลก) |
| `ulong` | 64 | 0 ถึง 18,446,744,073,709,551,615 | `UL` | ค่าบวกมหาศาล |

**ตัวอย่าง:**

```csharp
byte b = 255;
int i = 1_000_000;
long l = 9_223_372_036_854_775_807L;
uint ui = 4_000_000_000U;
```

---

## 19.3 ชนิดข้อมูลทศนิยม (Floating-point Types)

| ชนิด | ขนาด (bit) | ความแม่นยำ | ช่วงค่า (ประมาณ) | คำต่อท้าย | ใช้เมื่อ |
|------|------------|------------|------------------|-----------|---------|
| `float` | 32 | ~6-9 หลัก | ±1.5×10⁻⁴⁵ ถึง ±3.4×10³⁸ | `f` หรือ `F` | กราฟิก, เกม, ประหยัด memory |
| `double` | 64 | ~15-17 หลัก | ±5.0×10⁻³²⁴ ถึง ±1.7×10³⁰⁸ | (ไม่มี) | **ค่าเริ่มต้นสำหรับทศนิยม** |
| `decimal` | 128 | 28-29 หลัก | ±1.0×10⁻²⁸ ถึง ±7.9×10²⁸ | `m` หรือ `M` | **การเงิน, เงินตรา** |

> ⚠️ **ข้อควรระวัง:** `float` และ `double` มี rounding error ห้ามใช้กับเงิน ใช้ `decimal` เสมอ

**ตัวอย่าง:**

```csharp
float f = 3.14159f;
double d = 3.14159265358979;
decimal m = 999.99m;
```

---

## 19.4 ชนิดข้อมูลอื่น ๆ (bool, char, string, object)

| ชนิด | คำอธิบาย | ค่า可能的 | ขนาด | ตัวอย่าง |
|------|----------|-----------|------|----------|
| `bool` | ค่าความจริง | `true`, `false` | 1 byte | `bool isReady = true;` |
| `char` | อักขระ Unicode ตัวเดียว | อักขระใด ๆ ใน `''` | 2 byte | `char grade = 'A';` |
| `string` | ข้อความ (ลำดับของ char) | ข้อความใน `""` | ขึ้นกับ length | `string name = "John";` |
| `object` | รากฐานของทุกชนิด | 任何ค่า | ขึ้นกับชนิด | `object obj = 123;` |
| `dynamic` | ตรวจสอบชนิดตอนรัน | 任何ค่า | ขึ้นกับชนิด | `dynamic d = "hello";` |

**ตัวอย่าง:**

```csharp
bool isActive = false;
char ch = '\u0E17';   // 'ท'
string message = $"Hello {name}";
object boxed = 42;    // boxing
```

---

## 19.5 ค่าเริ่มต้นของชนิดข้อมูล (Default Values)

เมื่อประกาศตัวแปรระดับคลาส (field) โดยไม่กำหนดค่า จะได้ค่าเริ่มต้นตามชนิด:

| ชนิด | ค่าเริ่มต้น |
|------|-------------|
| ตัวเลขทั้งหมด (`int`, `long`, `float`, `double`, `decimal`, `byte`, ฯลฯ) | `0` |
| `bool` | `false` |
| `char` | `'\0'` (null character) |
| `string` | `null` |
| `object` | `null` |
| ชนิดที่ nullable (`int?`, `bool?`) | `null` |

**ตัวอย่าง:**

```csharp
public class MyClass
{
    int number;        // 0
    bool flag;         // false
    string text;       // null
}

// ใช้ default keyword
int defaultValue = default(int);  // 0
string? nullValue = default;       // null
```

---

## 19.6 การเลือกชนิดข้อมูลให้เหมาะสม

| สถานการณ์ | ชนิดที่แนะนำ | เหตุผล |
|------------|--------------|--------|
| นับจำนวนสิ่งของ (อายุ, จำนวนสินค้า) | `int` | พอเพียง, เร็ว |
| ID ฐานข้อมูล (เกิน 2 พันล้าน) | `long` | รองรับค่ามาก |
| คะแนนสอบ (มีทศนิยม) | `double` | ความแม่นยำปานกลาง |
| ราคาสินค้า, เงินเดือน | `decimal` | แม่นยำทางการเงิน |
| พิกัด GPS, ค่าทางวิทยาศาสตร์ | `double` | ความแม่นยำสูง, ช่วงกว้าง |
| กราฟิก 3D, game physics | `float` | ประหยัด memory |
| ตัวอักษรเดียว | `char` | ประหยัดกว่า string |
| ข้อความ | `string` | มาตรฐาน |
| ค่าจริง/เท็จ | `bool` | ชัดเจน |

---

## 19.7 ตารางสรุปฉบับด่วน (Quick Reference)

### ตารางที่ 19.1: ชนิดข้อมูลทั้งหมดแบบย่อ

| ชนิด | ขนาด (bit) | ช่วงค่า (โดยประมาณ) | คำต่อท้าย |
|------|------------|---------------------|-----------|
| `byte` | 8 | 0–255 | - |
| `sbyte` | 8 | -128–127 | - |
| `short` | 16 | -32k–32k | - |
| `ushort` | 16 | 0–65k | - |
| `int` | 32 | ±2.1B | - |
| `uint` | 32 | 0–4.2B | `U` |
| `long` | 64 | ±9.2×10¹⁸ | `L` |
| `ulong` | 64 | 0–1.8×10¹⁹ | `UL` |
| `float` | 32 | ±3.4×10³⁸ | `f` |
| `double` | 64 | ±1.7×10³⁰⁸ | - |
| `decimal` | 128 | ±7.9×10²⁸ | `m` |
| `bool` | 8 | true/false | - |
| `char` | 16 | Unicode U+0000–U+FFFF | - |
| `string` | ขึ้นกับ length | ข้อความ | `""` |
| `object` | ขึ้นกับชนิด | 任何ค่า | - |

### ตารางที่ 19.2: วิธีการตรวจสอบชนิด

| วิธี | ตัวอย่าง | ผลลัพธ์ |
|------|----------|---------|
| `typeof(int).Name` | `typeof(int).Name` | `"Int32"` |
| `default(int)` | `default(int)` | `0` |
| `int.MinValue` | `int.MinValue` | -2147483648 |
| `int.MaxValue` | `int.MaxValue` | 2147483647 |
| `sizeof(int)` | `sizeof(int)` | 4 (byte) |

---

## 19.8 ตัวอย่างโค้ดประกอบ

**ตัวอย่างที่ 19.1: สำรวจชนิดข้อมูลด้วย typeof และ default**

```csharp
Console.WriteLine("=== ชนิดข้อมูลและค่าเริ่มต้น ===\n");

Console.WriteLine($"int: {typeof(int).Name}, default={default(int)}, size={sizeof(int)} bytes");
Console.WriteLine($"long: {typeof(long).Name}, default={default(long)}, size={sizeof(long)} bytes");
Console.WriteLine($"float: {typeof(float).Name}, default={default(float)}, size={sizeof(float)} bytes");
Console.WriteLine($"double: {typeof(double).Name}, default={default(double)}, size={sizeof(double)} bytes");
Console.WriteLine($"decimal: {typeof(decimal).Name}, default={default(decimal)}, size={sizeof(decimal)} bytes");
Console.WriteLine($"bool: {typeof(bool).Name}, default={default(bool)}");
Console.WriteLine($"char: {typeof(char).Name}, default='{default(char)}' (null character)");
Console.WriteLine($"string: {typeof(string).Name}, default={(default(string) == null ? "null" : "not null")}");
```

**ตัวอย่างที่ 19.2: การใช้งานจริง – เลือกชนิดให้เหมาะกับงาน**

```csharp
// การเงิน - ต้องใช้ decimal
decimal price = 99.99m;
decimal taxRate = 0.07m;
decimal total = price * (1 + taxRate);
Console.WriteLine($"ราคารวมภาษี: {total:C}");

// วิทยาศาสตร์ - ใช้ double
double radius = 5.5;
double area = Math.PI * radius * radius;
Console.WriteLine($"พื้นที่วงกลมรัศมี {radius} = {area:F4}");

// กราฟิก - ใช้ float (ประหยัด memory)
float x = 10.5f, y = 20.3f;
float distance = MathF.Sqrt(x * x + y * y);
Console.WriteLine($"ระยะทาง = {distance:F2}");
```

**ตัวอย่างที่ 19.3: Boxing และ Unboxing (value ↔ object)**

```csharp
int number = 42;
object boxed = number;          // boxing: int → object
int unboxed = (int)boxed;       // unboxing: object → int
Console.WriteLine($"Boxed: {boxed}, Unboxed: {unboxed}");

// ระวัง: unboxing ผิดชนิดจะเกิด InvalidCastException
// object wrong = 42;
// long wrongUnbox = (long)wrong;  // Exception!
```

---

## 19.9 แบบฝึกหัดท้ายบท (3 ข้อ)

🧪 **แบบฝึกหัดที่ 19.1:**  
จงบอกชนิดข้อมูลที่เหมาะสมที่สุดสำหรับ:  
ก) จำนวนดาวในจักรวาล (ประมาณ 10²³ ดวง)  
ข) อัตราดอกเบี้ยเงินกู้ (เช่น 3.5%)  
ค) รหัสไปรษณีย์ไทย (5 หลัก)  
ง) สถานะการเปิด-ปิดของสวิตช์  

🧪 **แบบฝึกหัดที่ 19.2:**  
เขียนโปรแกรมแสดงค่า `MinValue` และ `MaxValue` ของ `int`, `long`, `float`, `double`, `decimal` (ใช้ property เช่น `int.MinValue`)

🧪 **แบบฝึกหัดที่ 19.3 (ท้าทาย):**  
อธิบายความแตกต่างระหว่าง `float`, `double`, และ `decimal` พร้อมยกตัวอย่างสถานการณ์ที่ใช้แต่ละชนิด พร้อมแสดงโค้ดที่เห็นปัญหาการปัดเศษของ `double` แต่ `decimal` ไม่มีปัญหา

---

## 19.10 แหล่งอ้างอิง

- 🔗 **Built-in types (C# reference)** – [https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/built-in-types](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/built-in-types)
- 🔗 **Integral numeric types** – [https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types)
- 🔗 **Floating-point numeric types** – [https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types)
- 🔗 **default keyword** – [https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/default](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/default)

---

## สรุปท้ายบท

บทที่ 19 เป็น cheatsheet สำหรับอ้างอิงด่วนเกี่ยวกับชนิดข้อมูลใน C# ครอบคลุม:
- ชนิดจำนวนเต็ม (byte, short, int, long) และ unsigned
- ชนิดทศนิยม (float, double, decimal) พร้อมข้อควรระวังเรื่อง rounding error
- ชนิดอื่น ๆ (bool, char, string, object)
- ค่าเริ่มต้นของแต่ละชนิด
- หลักการเลือกชนิดให้เหมาะสมกับงาน

คุณสามารถใช้ตารางในบทนี้เป็นข้อมูลอ้างอิงขณะเขียนโค้ด

**ในบทถัดไป (บทที่ 20)** เราจะพูดถึง **ตัวดำเนินการเชิงตรรกะ (&&, ||, !) และ relational** เพื่อเตรียมพร้อมสำหรับการเขียนเงื่อนไขและการตัดสินใจในโปรแกรม

 