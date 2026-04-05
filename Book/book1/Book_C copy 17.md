# Mastering C# .NET 2026: จากพื้นฐานสู่ Enterprise Application + Database + Cache + Message Queue

## บทที่ 17: String Interpolation, Concatenation, String.Format

---

### สารบัญย่อยของบทที่ 17

17.1 ความสำคัญของการจัดการข้อความ  
17.2 การต่อข้อความแบบดั้งเดิม (Concatenation)  
17.3 String.Format – การจัดรูปแบบข้อความแบบกำหนดตำแหน่ง  
17.4 String Interpolation – วิธีที่ทันสมัยและแนะนำ  
17.5 การเปรียบเทียบประสิทธิภาพและความอ่านง่าย  
17.6 การจัดรูปแบบตัวเลข วันที่ และ currency  
17.7 ตัวอย่างโค้ดที่รันได้จริง  
17.8 ตารางสรุปวิธีการรวมข้อความ  
17.9 แบบฝึกหัดท้ายบท  
17.10 แหล่งอ้างอิง  

---

## 17.1 ความสำคัญของการจัดการข้อความ

ในการเขียนโปรแกรมแทบทุกโปรแกรม เราต้องการนำค่าของตัวแปรมาแสดงผลร่วมกับข้อความ เช่น แสดงชื่อผู้ใช้ อายุ ราคาสินค้า หรือวันที่ C# มีวิธีการรวมข้อความ (string) กับตัวแปรหลายวิธี การเลือกใช้วิธีที่เหมาะสมช่วยให้โค้ดอ่านง่าย บำรุงรักษาง่าย และมีประสิทธิภาพดี

สามวิธีการหลักคือ:
1. **Concatenation (`+`)** – วิธีดั้งเดิม ใช้เครื่องหมายบวกต่อข้อความ
2. **String.Format** – ใช้ placeholders `{0}`, `{1}` แล้วระบุค่าทีหลัง
3. **String Interpolation (`$"..."`)** – วิธีที่ทันสมัย ใส่ตัวแปรใน `{}` โดยตรง

---

## 17.2 การต่อข้อความแบบดั้งเดิม (Concatenation)

ใช้เครื่องหมาย `+` ต่อ string และตัวแปรเข้าด้วยกัน

```csharp
string name = "สมชาย";
int age = 25;
string message = "ชื่อ: " + name + ", อายุ: " + age + " ปี";
Console.WriteLine(message);
```

**ข้อดี:** ง่าย เข้าใจทันที  
**ข้อเสีย:** อ่านยากเมื่อมีตัวแปรหลายตัว ต้องระวังการเว้นวรรค เปลืองหน่วยความจำ (สร้าง string ใหม่ทุกครั้ง)

---

## 17.3 String.Format – การจัดรูปแบบข้อความแบบกำหนดตำแหน่ง

ใช้เมธอด `string.Format()` โดยกำหนด placeholders `{0}`, `{1}`, `{2}` แล้วส่งค่าตามลำดับ

```csharp
string name = "สมชาย";
int age = 25;
string message = string.Format("ชื่อ: {0}, อายุ: {1} ปี", name, age);
Console.WriteLine(message);
```

**ข้อดี:** แยก placeholders ออกจากค่า ทำให้โค้ดเป็นระเบียบ  
**ข้อเสีย:** ต้องจำลำดับ index ถ้าผิดจะสับสน

**การจัดรูปแบบเพิ่มเติม:** สามารถระบุรูปแบบการแสดงผลภายใน placeholder ได้

```csharp
double price = 99.99;
string msg = string.Format("ราคา: {0:C}", price);  // ราคา: 99.99 บาท (ขึ้นกับ culture)
string msg2 = string.Format("PI = {0:F2}", Math.PI); // PI = 3.14
```

---

## 17.4 String Interpolation – วิธีที่ทันสมัยและแนะนำ

ใช้เครื่องหมาย `$` นำหน้า string แล้วใส่ตัวแปรหรือ expression ใน `{}` โดยตรง

```csharp
string name = "สมชาย";
int age = 25;
string message = $"ชื่อ: {name}, อายุ: {age} ปี";
Console.WriteLine(message);
```

**ข้อดี:** อ่านง่ายที่สุด เขียนตรงไปตรงมา รองรับ expression และการจัดรูปแบบในตัว  
**ข้อเสีย:** เวอร์ชัน C# ก่อน 6.0 ไม่รองรับ (แต่ปัจจุบัน .NET 9/10 รองรับแน่นอน)

**การใส่ expression และการจัดรูปแบบ:**

```csharp
int a = 10, b = 3;
Console.WriteLine($"{a} + {b} = {a + b}");           // 10 + 3 = 13
Console.WriteLine($"PI = {Math.PI:F2}");              // PI = 3.14
Console.WriteLine($"{(a > b ? "a มากกว่า" : "b มากกว่า")}");
```

---

## 17.5 การเปรียบเทียบประสิทธิภาพและความอ่านง่าย

| วิธี | ความอ่านง่าย | ประสิทธิภาพ | ความยืดหยุ่น | แนะนำเมื่อ |
|------|-------------|-------------|--------------|------------|
| Concatenation (`+`) | แย่ (ถ้ามีหลายตัวแปร) | แย่ (สร้าง string หลายครั้ง) | ต่ำ | ต่อ string คงที่ 2-3 ชิ้น |
| String.Format | ปานกลาง | ดี (สร้างครั้งเดียว) | ปานกลาง | ต้องการรูปแบบซับซ้อน หรือ localization |
| String Interpolation | ดีที่สุด | ดีเทียบเท่า Format | ดีมาก | **กรณีทั่วไป แนะนำที่สุด** |

> 💡 **เคล็ดลับ:** ใน .NET 9/10 ควรใช้ **String Interpolation** เป็นค่าเริ่มต้น ยกเว้นกรณีที่ต้องการใช้ placeholders ซ้ำหลายครั้ง หรือต้องเก็บ template ไว้ใช้ทีหลัง (ซึ่ง interpolation ก็ทำได้โดยใช้ `FormattableString`)

---

## 17.6 การจัดรูปแบบตัวเลข วันที่ และ currency

### 17.6.1 รูปแบบตัวเลขมาตรฐาน

| รูปแบบ | ความหมาย | ตัวอย่าง (value=1234.567) |
|--------|----------|---------------------------|
| `F2` | ทศนิยม 2 ตำแหน่ง | 1234.57 |
| `N0` | จำนวนเต็ม มี comma คั่น | 1,235 |
| `C` | สกุลเงิน | 1,234.57 บาท (ขึ้นกับ culture) |
| `P` | เปอร์เซ็นต์ | 123456.70% |
| `E` | scientific notation | 1.234567E+003 |

```csharp
double num = 1234.5678;
Console.WriteLine($"{num:F2}");   // 1234.57
Console.WriteLine($"{num:N0}");   // 1,235
Console.WriteLine($"{num:C}");    // 1,234.57 ฿
```

### 17.6.2 รูปแบบวันที่และเวลา

| รูปแบบ | ความหมาย | ตัวอย่าง (DateTime.Now) |
|--------|----------|------------------------|
| `d` | วันที่สั้น | 4/2/2026 |
| `D` | วันที่ยาว | Thursday, April 2, 2026 |
| `t` | เวลาสั้น | 2:30 PM |
| `T` | เวลายาว | 2:30:45 PM |
| `yyyy-MM-dd` | แบบกำหนดเอง | 2026-04-02 |

```csharp
DateTime now = DateTime.Now;
Console.WriteLine($"{now:dd/MM/yyyy}");   // 02/04/2026
Console.WriteLine($"{now:HH:mm:ss}");     // 14:30:45
Console.WriteLine($"{now:yyyy-MM-dd HH:mm}");
```

---

## 17.7 ตัวอย่างโค้ดที่รันได้จริง

**ตัวอย่างที่ 17.1: เปรียบเทียบสามวิธีการ**

```csharp
string name = "สมศรี";
int age = 30;
double salary = 25000.5;

// Concatenation
string concat = "ชื่อ: " + name + ", อายุ: " + age + ", เงินเดือน: " + salary;
Console.WriteLine("Concatenation: " + concat);

// String.Format
string format = string.Format("ชื่อ: {0}, อายุ: {1}, เงินเดือน: {2:F2}", name, age, salary);
Console.WriteLine("String.Format: " + format);

// Interpolation (แนะนำ)
string interp = $"ชื่อ: {name}, อายุ: {age}, เงินเดือน: {salary:F2}";
Console.WriteLine("Interpolation: " + interp);
```

**ตัวอย่างที่ 17.2: การจัดรูปแบบขั้นสูง**

```csharp
// การจัดรูปแบบตัวเลข
Console.WriteLine("=== การจัดรูปแบบตัวเลข ===");
double value = 12345.6789;
Console.WriteLine($"ทศนิยม 2 ตำแหน่ง: {value:F2}");
Console.WriteLine($"Comma คั่น: {value:N0}");
Console.WriteLine($"สกุลเงิน: {value:C}");
Console.WriteLine($"เปอร์เซ็นต์: {0.25:P}");

// การจัดรูปแบบวันที่
Console.WriteLine("\n=== การจัดรูปแบบวันที่ ===");
DateTime today = DateTime.Today;
DateTime now = DateTime.Now;
Console.WriteLine($"วันนี้: {today:dddd, dd MMMM yyyy}");
Console.WriteLine($"เวลาปัจจุบัน: {now:HH:mm:ss}");
Console.WriteLine($"ISO 8601: {now:yyyy-MM-ddTHH:mm:ss}");

// การจัดรูปแบบแบบมีเงื่อนไข (ternary)
int score = 85;
Console.WriteLine($"ผลสอบ: {score} คะแนน => {(score >= 50 ? "ผ่าน" : "ไม่ผ่าน")}");
```

**ตัวอย่างที่ 17.3: การใช้ FormattableString (สำหรับเก็บ template)**

```csharp
// FormattableString ช่วยให้เก็บ interpolation ไว้ใช้ทีหลัง
FormattableString template = $"สวัสดี {name} คุณอายุ {age} ปี";
string result = template.ToString();  // แปลงเป็น string ปกติ

// หรือจะใช้ invariant culture ก็ได้
string invariantResult = FormattableString.Invariant(template);
```

---

## 17.8 ตารางสรุปวิธีการรวมข้อความ

| วิธี | Syntax | ตัวอย่าง | เหมาะกับ |
|------|--------|----------|----------|
| Concatenation | `"A" + var + "B"` | `"Hello " + name` | ต่อ string สั้น ๆ |
| String.Format | `string.Format("...{0}...", var)` | `string.Format("Hi {0}", name)` | localization, template reuse |
| String Interpolation | `$"...{var}..."` | `$"Hi {name}"` | **กรณีทั่วไป** |

### ตารางรูปแบบการจัดรูปแบบตัวเลข

| Specifier | ความหมาย | ตัวอย่าง (1234.567) | ผลลัพธ์ |
|-----------|----------|---------------------|---------|
| `F0` | ทศนิยม 0 ตำแหน่ง | `{value:F0}` | 1235 |
| `F2` | ทศนิยม 2 ตำแหน่ง | `{value:F2}` | 1234.57 |
| `N2` | มี comma, 2 ทศนิยม | `{value:N2}` | 1,234.57 |
| `C` | สกุลเงิน | `{value:C}` | ฿1,234.57 |
| `P` | เปอร์เซ็นต์ | `{0.25:P}` | 25.00% |

### ตารางรูปแบบวันที่

| Specifier | ความหมาย | ตัวอย่าง |
|-----------|----------|----------|
| `d` | วันที่สั้น | 4/2/2026 |
| `D` | วันที่ยาว | Thursday, April 2, 2026 |
| `t` | เวลาสั้น | 2:30 PM |
| `T` | เวลายาว | 2:30:45 PM |
| `yyyy-MM-dd` | แบบกำหนดเอง | 2026-04-02 |

---

## 17.9 แบบฝึกหัดท้ายบท (3 ข้อ)

🧪 **แบบฝึกหัดที่ 17.1:**  
จงเขียนโปรแกรมรับชื่อ นามสกุล และปีเกิด (ค.ศ.) จากผู้ใช้ แล้วแสดงผลโดยใช้ **string interpolation** ในรูปแบบ "ชื่อ-นามสกุล: Somchai Jaidee, อายุ: 31 ปี" (คำนวณอายุจากปีปัจจุบัน 2026)

🧪 **แบบฝึกหัดที่ 17.2:**  
ให้แปลงโปรแกรมเดิมที่ใช้ string interpolation ให้เป็น `String.Format` แทน พร้อมกับจัดรูปแบบตัวเลขเงินเดือนให้มี comma คั่นทุก 3 หลัก (ใช้ `{0:N2}`)

🧪 **แบบฝึกหัดที่ 17.3 (ท้าทาย):**  
สร้างโปรแกรมที่รับราคาสินค้า (decimal) และส่วนลด (เปอร์เซ็นต์) แล้วแสดง:
- ราคาเต็ม: {ราคา:C}
- ส่วนลด: {ส่วนลด}%
- ราคาหลังหักส่วนลด: {ราคาหลังหัก:C}
ใช้ string interpolation และจัดรูปแบบสกุลเงินให้สวยงาม

---

## 17.10 แหล่งอ้างอิง

- 🔗 **String interpolation (C# reference)** – [https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated)
- 🔗 **String.Format Method** – [https://docs.microsoft.com/en-us/dotnet/api/system.string.format](https://docs.microsoft.com/en-us/dotnet/api/system.string.format)
- 🔗 **Standard numeric format strings** – [https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings)
- 🔗 **Standard date and time format strings** – [https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings](https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings)

---

## สรุปท้ายบท

บทที่ 17 ได้เรียนรู้วิธีการรวมข้อความกับตัวแปรใน C# สามแบบ: Concatenation (`+`), String.Format, และ String Interpolation (`$"..."`) โดย **String Interpolation เป็นวิธีที่แนะนำ** เพราะอ่านง่าย เขียนสะดวก และประสิทธิภาพดี นอกจากนี้ยังได้เรียนรู้การจัดรูปแบบตัวเลข วันที่ และสกุลเงินด้วย format specifiers เช่น `F2`, `N0`, `C`, `yyyy-MM-dd`

ความสามารถในการจัดการข้อความอย่างมีประสิทธิภาพเป็นทักษะพื้นฐานที่จำเป็นสำหรับการพัฒนาแอปพลิเคชันทุกประเภท

**ในบทถัดไป (บทที่ 18)** เราจะพูดถึง **มาตรฐานการเขียนโค้ด (Naming Conventions, Formatting)** ซึ่งเป็นสิ่งสำคัญสำหรับการทำงานเป็นทีมและการบำรุงรักษาซอฟต์แวร์ระยะยาว

 