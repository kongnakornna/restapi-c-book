# Mastering C# .NET 2026: จากพื้นฐานสู่ Enterprise Application + Database + Cache + Message Queue

## บทที่ 11: ชนิดข้อมูลตัวเลข (int, double, float, decimal) และการแปลง implicit/explicit

---

### สารบัญย่อยของบทที่ 11

11.1 ภาพรวมชนิดข้อมูลตัวเลขใน C#  
11.2 ชนิดข้อมูลจำนวนเต็ม (int, long, short, byte)  
11.3 ชนิดข้อมูลทศนิยม (float, double, decimal)  
11.4 การเลือกใช้ชนิดตัวเลขให้เหมาะสม  
11.5 การแปลงชนิดอัตโนมัติ (Implicit Conversion)  
11.6 การแปลงชนิดแบบบังคับ (Explicit Conversion / Casting)  
11.7 ข้อผิดพลาดที่เกิดจากการแปลงชนิด  
11.8 ตัวอย่างโค้ดที่รันได้จริง  
11.9 ตารางสรุปชนิดตัวเลขและการแปลง  
11.10 แบบฝึกหัดท้ายบท  
11.11 แหล่งอ้างอิง  

---

## 11.1 ภาพรวมชนิดข้อมูลตัวเลขใน C#

C# มีชนิดข้อมูลตัวเลขที่หลากหลาย แต่ละชนิดมีขนาด (จำนวน bit) และช่วงของค่าที่เก็บได้ต่างกัน การเลือกชนิดที่เหมาะสมช่วยประหยัดหน่วยความจำและลดข้อผิดพลาดจากการคำนวณ

**ชนิดตัวเลขหลัก ๆ แบ่งเป็น 2 กลุ่มใหญ่:**

| กลุ่ม | ชนิด | ลักษณะ |
|------|------|---------|
| จำนวนเต็ม (Integer) | `int`, `long`, `short`, `byte` | ไม่มีจุดทศนิยม |
| ทศนิยม (Floating-point) | `float`, `double`, `decimal` | มีจุดทศนิยม |

---

## 11.2 ชนิดข้อมูลจำนวนเต็ม (int, long, short, byte)

### 11.2.1 ตารางเปรียบเทียบชนิดจำนวนเต็ม

| ชนิด | ขนาด (bit) | ช่วงค่า (ต่ำสุด - สูงสุด) | ใช้เมื่อ |
|------|------------|--------------------------|---------|
| `byte` | 8 | 0 ถึง 255 | ค่าน้อย, ประหยัด memory |
| `short` | 16 | -32,768 ถึง 32,767 | ค่าปานกลาง |
| `int` | 32 | -2.1B ถึง 2.1B (ประมาณ ±2.1 พันล้าน) | **ค่าเริ่มต้นสำหรับจำนวนเต็ม** |
| `long` | 64 | -9.2×10¹⁸ ถึง 9.2×10¹⁸ | ค่ามาก (เช่น ID ในฐานข้อมูลขนาดใหญ่) |

**ตัวอย่างการประกาศ:**

```csharp
byte age = 25;           // 0-255
short year = 2026;       // -32768 ถึง 32767
int population = 7000000; // ค่าเริ่มต้น
long worldPopulation = 8_000_000_000L; // ใส่ L ต่อท้าย
```

> 💡 **เคล็ดลับ:** ถ้าไม่แน่ใจ ให้ใช้ `int` เพราะเป็นขนาดมาตรฐานของ CPU (32-bit) และเพียงพอสำหรับค่าส่วนใหญ่

### 11.2.2 การใช้ตัวคั่นหลักพัน (Digit Separator)

C# 7.0 ขึ้นไป允许ใช้ underscore (`_`) คั่นตัวเลขเพื่อให้อ่านง่าย:

```csharp
int million = 1_000_000;
long billion = 1_000_000_000L;
int creditCard = 1234_5678_9012_3456;
```

---

## 11.3 ชนิดข้อมูลทศนิยม (float, double, decimal)

### 11.3.1 ตารางเปรียบเทียบ

| ชนิด | ขนาด (bit) | ความแม่นยำ (หลักทศนิยม) | ช่วงค่า | ใช้เมื่อ |
|------|------------|-------------------------|---------|---------|
| `float` | 32 | ~6-9 หลัก | ±1.5×10⁻⁴⁵ ถึง ±3.4×10³⁸ | กราฟิก, เกม (ประหยัด memory) |
| `double` | 64 | ~15-17 หลัก | ±5.0×10⁻³²⁴ ถึง ±1.7×10³⁰⁸ | **ค่าเริ่มต้นสำหรับทศนิยม** |
| `decimal` | 128 | 28-29 หลัก | ±1.0×10⁻²⁸ ถึง ±7.9×10²⁸ | **การเงิน, เงินตรา** |

> ⚠️ **ข้อควรระวัง:** `float` และ `double` มีข้อผิดพลาดจากการปัดเศษ (rounding error) เนื่องจากเก็บในฐานสอง ห้ามใช้กับเงิน!

### 11.3.2 การประกาศและการระบุ suffix

```csharp
float f = 3.14f;        // ต้องมี f หรือ F ต่อท้าย
double d = 3.14;        // ค่าเริ่มต้น (ไม่ต้อง suffix)
decimal m = 3.14m;      // ต้องมี m หรือ M ต่อท้าย

// ตัวอย่าง scientific notation
double avogadro = 6.022e23;  // 6.022 × 10²³
```

### 11.3.3 ข้อผิดพลาดของ float/double (ไม่ใช้กับเงิน)

```csharp
double a = 0.1;
double b = 0.2;
double c = a + b;
Console.WriteLine(c);           // 0.30000000000000004 (ไม่ใช่ 0.3 ตรง ๆ!)
Console.WriteLine(c == 0.3);    // False !!!
```

**ทางออก: ใช้ `decimal` สำหรับเงิน**

```csharp
decimal x = 0.1m;
decimal y = 0.2m;
decimal z = x + y;
Console.WriteLine(z);           // 0.3
Console.WriteLine(z == 0.3m);   // True
```

> ⭐ **หัวข้อสำคัญ:** ห้ามใช้ `float` หรือ `double` กับเงินเด็ดขาด! ใช้ `decimal` เสมอ

---

## 11.4 การเลือกใช้ชนิดตัวเลขให้เหมาะสม

| กรณีการใช้งาน | ชนิดที่แนะนำ | เหตุผล |
|---------------|--------------|--------|
| นับจำนวนสิ่งของ (คน, สินค้า) | `int` | พอเพียง, เร็ว |
| ID ในฐานข้อมูล (เกิน 2 พันล้าน) | `long` | รองรับค่ามาก |
| อายุ, ปีเกิด | `int` | พอเพียง |
| คะแนนสอบ | `int` หรือ `double` | ขึ้นกับว่ามีทศนิยมไหม |
| ราคาสินค้า, เงินเดือน | `decimal` | แม่นยำทางการเงิน |
| พิกัด GPS, ค่าทางวิทยาศาสตร์ | `double` | ความแม่นยำสูง, ช่วงกว้าง |
| กราฟิก 3D, game physics | `float` | ประหยัด memory, GPU รองรับ |

---

## 11.5 การแปลงชนิดอัตโนมัติ (Implicit Conversion)

การแปลงอัตโนมัติเกิดขึ้นเมื่อไม่มีโอกาสสูญเสียข้อมูล (จากช่วงแคบไปช่วงกว้าง) C# จะทำ conversion ให้เอง

**ลำดับการแปลงอัตโนมัติ:**  
`byte` → `short` → `int` → `long` → `float` → `double`  
(และ `int` → `decimal` ได้ด้วย)

```csharp
byte small = 100;
int medium = small;      // ✅ byte → int (อัตโนมัติ)
long large = medium;     // ✅ int → long
float f = large;         // ✅ long → float (อาจเสียความแม่นยำ แต่ทำได้)
double d = f;            // ✅ float → double

decimal dec = medium;    // ✅ int → decimal
// decimal dec2 = f;     // ❌ float ไม่สามารถ implicit ไป decimal ได้ (ต้อง explicit)
```

---

## 11.6 การแปลงชนิดแบบบังคับ (Explicit Conversion / Casting)

เมื่อแปลงจากชนิดที่มีช่วงกว้างไปสู่ช่วงแคบ (อาจสูญเสียข้อมูล) ต้องใช้ **casting** ด้วย `(ชนิด)` นำหน้า

### 11.6.1 Casting ระหว่างจำนวนเต็ม

```csharp
long big = 1_000_000_000;
int small = (int)big;     // ต้อง cast (แม้ค่าไม่เกิน int ก็ตาม)

long tooBig = 5_000_000_000;
int overflow = (int)tooBig;  // เกิด overflow (ได้ค่า -1,294,967,296 โดยไม่ error)
```

⚠️ **over flow:** ถ้าเกินช่วง จะเกิด **overflow** (ค่าเปลี่ยนไปโดยไม่แจ้งเตือน) ใช้ `checked` เพื่อตรวจจับ:

```csharp
checked
{
    int x = (int)5_000_000_000L;  // OverflowException
}
```

### 11.6.2 Casting ระหว่างตัวเลขกับทศนิยม

```csharp
double pi = 3.14159;
int intPi = (int)pi;           // ตัดเศษทิ้ง → 3
float floatPi = (float)pi;     // double → float (เสียความแม่นยำบ้าง)

decimal price = 99.99m;
int intPrice = (int)price;     // 99 (ตัดทศนิยม)
```

### 11.6.3 การปัดเศษเมื่อ cast (ไม่ใช่ rounding)

Casting จากทศนิยมเป็นจำนวนเต็มจะ **ตัดเศษทิ้ง** เสมอ (truncate) ไม่ใช่ปัดเศษ

```csharp
double d = 3.9;
int i = (int)d;     // 3 (ไม่ใช่ 4)

// ถ้าต้องการปัดเศษ ใช้ Math.Round()
int rounded = (int)Math.Round(d);  // 4
```

---

## 11.7 ข้อผิดพลาดที่เกิดจากการแปลงชนิด

### 11.7.1 Overflow (ล้น)

```csharp
byte b = 255;
b = (byte)(b + 1);   // 0 (เกิด overflow wrap around)
```

ตรวจสอบด้วย `checked` หรือใช้ `Convert.ToByte()`:

```csharp
byte b2 = Convert.ToByte(256);  // OverflowException
```

### 11.7.2 Loss of Precision (เสียความแม่นยำ)

```csharp
int bigInt = 123456789;
float f = bigInt;           // implicit conversion
int back = (int)f;          // อาจได้ 123456792 (เสียความแม่นยำ)
```

### 11.7.3 การหาร int ได้ int

```csharp
int a = 5, b = 2;
int result = a / b;     // 2 (ไม่ใช่ 2.5)
double correct = (double)a / b;  // 2.5 (ต้อง cast ตัวใดตัวหนึ่ง)
```

---

## 11.8 ตัวอย่างโค้ดที่รันได้จริง

**ตัวอย่างที่ 11.1: การใช้ชนิดตัวเลขต่างๆ**

```csharp
Console.WriteLine("=== ชนิดข้อมูลตัวเลข ===");

// จำนวนเต็ม
byte smallNumber = 255;
int normalNumber = 2_147_483_647;
long largeNumber = 9_223_372_036_854_775_807L;

Console.WriteLine($"byte: {smallNumber}");
Console.WriteLine($"int: {normalNumber}");
Console.WriteLine($"long: {largeNumber}");

// ทศนิยม
float piFloat = 3.1415926535f;
double piDouble = 3.14159265358979;
decimal priceDecimal = 999.99m;

Console.WriteLine($"float (7 digits): {piFloat}");     // 3.1415927 (ปัด)
Console.WriteLine($"double (15 digits): {piDouble}");  // 3.14159265358979
Console.WriteLine($"decimal: {priceDecimal:C}");

// การใช้ digit separator
int population = 69_000_000;
long earthPopulation = 8_000_000_000L;
Console.WriteLine($"ประชากรไทย: {population:N0}");
Console.WriteLine($"ประชากรโลก: {earthPopulation:N0}");
```

**ตัวอย่างที่ 11.2: การแปลงชนิด (Implicit/Explicit) และปัญหาการเงิน**

```csharp
// การแปลงอัตโนมัติ (implicit)
int count = 100;
long total = count;           // int → long
double average = count;       // int → double

Console.WriteLine($"int to long: {total}");
Console.WriteLine($"int to double: {average}");

// การแปลงแบบบังคับ (explicit) - ตัดเศษ
double temperature = 36.8;
int tempInt = (int)temperature;
Console.WriteLine($"ตัดเศษ: {tempInt}°C");  // 36

// การปัดเศษด้วย Math.Round
int tempRounded = (int)Math.Round(temperature);
Console.WriteLine($"ปัดเศษ: {tempRounded}°C");  // 37

// ปัญหาการเงินกับ double
double priceD = 0.1;
double totalD = priceD * 10;
Console.WriteLine($"double: 0.1 × 10 = {totalD}");  // 1.0 (บางครั้งก็ 0.9999999999)

decimal priceM = 0.1m;
decimal totalM = priceM * 10;
Console.WriteLine($"decimal: 0.1 × 10 = {totalM}");  // 1.0 แน่นอน
```

**ตัวอย่างที่ 11.3: โปรแกรมแปลงสกุลเงิน (ใช้ decimal)**

```csharp
Console.WriteLine("=== โปรแกรมแปลงสกุลเงิน ===");
Console.Write("จำนวนเงิน (บาท): ");
string input = Console.ReadLine();

if (decimal.TryParse(input, out decimal baht))
{
    decimal usdRate = 0.028m;  // สมมติ 1 บาท = 0.028 USD
    decimal eurRate = 0.026m;
    
    decimal usd = baht * usdRate;
    decimal eur = baht * eurRate;
    
    Console.WriteLine($"\n{baht:N2} บาท = {usd:N2} USD");
    Console.WriteLine($"{baht:N2} บาท = {eur:N2} EUR");
}
else
{
    Console.WriteLine("กรุณาป้อนตัวเลขที่ถูกต้อง");
}
```

---

## 11.9 ตารางสรุปชนิดตัวเลขและการแปลง

### ตารางที่ 11.1: สรุปชนิดตัวเลข

| ชนิด | ขนาด | ช่วงค่า | Suffix | ใช้กับ |
|------|------|---------|--------|--------|
| `byte` | 8 bit | 0–255 | - | ค่าน้อย |
| `short` | 16 bit | -32,768–32,767 | - | ค่าปานกลาง |
| `int` | 32 bit | ±2.1B | - | ค่าเริ่มต้น |
| `long` | 64 bit | ±9.2×10¹⁸ | `L` | ค่ามาก |
| `float` | 32 bit | ±3.4×10³⁸ | `f` | กราฟิก, เกม |
| `double` | 64 bit | ±1.7×10³⁰⁸ | (default) | วิทยาศาสตร์ |
| `decimal` | 128 bit | ±7.9×10²⁸ | `m` | การเงิน |

### ตารางที่ 11.2: การแปลงชนิด

| จาก → ไป | Implicit? | Explicit? | ข้อควรระวัง |
|-----------|-----------|-----------|--------------|
| `int` → `long` | ✅ | - | - |
| `long` → `int` | ❌ | `(int)` | Overflow |
| `int` → `double` | ✅ | - | อาจเสียความแม่นยำ (ถ้าค่ามาก) |
| `double` → `int` | ❌ | `(int)` | ตัดเศษทิ้ง |
| `double` → `decimal` | ❌ | `(decimal)` | ต้อง explicit |
| `decimal` → `double` | ✅ | - | อาจเสียความแม่นยำ |

---

## 11.10 แบบฝึกหัดท้ายบท (4 ข้อ)

🧪 **แบบฝึกหัดที่ 11.1:**  
จงประกาศตัวแปรต่อไปนี้ด้วยชนิดที่เหมาะสม:  
- ราคาสินค้า 299.99 บาท  
- ระยะทางระหว่างโลกถึงดวงจันทร์ 384,400 กิโลเมตร  
- จำนวนประชากรในห้องเรียน 30 คน  
- ค่า π 3.14159265358979  

🧪 **แบบฝึกหัดที่ 11.2:**  
เขียนโปรแกรมรับความกว้างและความยาวเป็น `double` คำนวณพื้นที่สี่เหลี่ยมผืนผ้า แล้วแสดงผลโดยใช้ `double` จากนั้นลองคำนวณด้วย `decimal` สังเกตความแตกต่าง (ป้อนทศนิยมหลายตำแหน่ง เช่น 3.3333333)

🧪 **แบบฝึกหัดที่ 11.3:**  
จากโค้ดต่อไปนี้ ผลลัพธ์ที่แสดงคืออะไร? อธิบายว่าทำไม:
```csharp
int x = 7;
int y = 2;
double z = x / y;
Console.WriteLine(z);
```
จากนั้นแก้ไขให้ได้ผลลัพธ์ 3.5 โดยใช้ casting

🧪 **แบบฝึกหัดที่ 11.4 (ท้าทาย):**  
เขียนโปรแกรมรับจำนวนเงิน (บาท) และอัตราดอกเบี้ยต่อปี (%) เป็น `decimal` แล้วคำนวณดอกเบี้ยทบต้น (compound interest) หลังจาก 5 ปี สูตร: `A = P × (1 + r/100)^t` โดย `P` = เงินต้น, `r` = อัตราดอกเบี้ย, `t` = ปี (ใช้ `Math.Pow` แต่รับ `double` อาจต้องแปลงชั่วคราว)

---

## 11.11 แหล่งอ้างอิง

- 🔗 **Integral numeric types (C#)** – [https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/integral-numeric-types)
- 🔗 **Floating-point numeric types** – [https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types)
- 🔗 **decimal vs double for money** – [https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types#decimal](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/floating-point-numeric-types#decimal)
- 🔗 **Casting and type conversions** – [https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/casting-and-type-conversions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/casting-and-type-conversions)

---

## สรุปท้ายบท

บทที่ 11 ได้เรียนรู้ชนิดข้อมูลตัวเลขใน C# อย่างละเอียด ตั้งแต่จำนวนเต็ม (`byte`, `short`, `int`, `long`) ไปจนถึงทศนิยม (`float`, `double`, `decimal`) พร้อมเกณฑ์การเลือกใช้ให้เหมาะสมกับงาน:

- **การเงิน** → `decimal` เสมอ (ป้องกัน rounding error)
- **วิทยาศาสตร์/วิศวกรรม** → `double` (ความแม่นยำสูง)
- **กราฟิก/เกม** → `float` (ประหยัด)
- **ทั่วไป** → `int` (จำนวนเต็ม), `double` (ทศนิยม)

นอกจากนี้ยังได้เรียนรู้การแปลงชนิดแบบอัตโนมัติ (implicit) และแบบบังคับ (explicit casting) รวมถึงข้อผิดพลาดที่อาจเกิดขึ้น เช่น overflow และ loss of precision

**ในบทถัดไป (บทที่ 12)** เราจะพูดถึง **ชนิดข้อมูล bool, char และ escape sequences** เพื่อให้ครอบคลุมชนิดข้อมูลพื้นฐานครบทุกชนิด

---

*หมายเหตุ: บทที่ 11 นี้มีความยาวประมาณ 2,100 คำ*

---

(ดำเนินการส่งบทที่ 12 ต่อไปโดยอัตโนมัติ)