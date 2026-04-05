# Mastering C# .NET 2026: จากพื้นฐานสู่ Enterprise Application + Database + Cache + Message Queue

## บทที่ 10: การรับข้อมูลผู้ใช้และการแปลงชนิด (Parse, TryParse)

---

### สารบัญย่อยของบทที่ 10

10.1 การรับข้อมูลจากผู้ใช้ด้วย `Console.ReadLine()`  
10.2 ปัญหาของการรับข้อมูล: ทุกอย่างเป็น `string`  
10.3 การแปลง `string` เป็นตัวเลขด้วย `Parse`  
10.4 การแปลง `string` เป็นตัวเลขแบบปลอดภัยด้วย `TryParse`  
10.5 การแปลงชนิดข้อมูลแบบอื่น ๆ (Convert class)  
10.6 ตัวอย่างโปรแกรมรับข้อมูลและคำนวณ  
10.7 ตารางสรุปวิธีการแปลงชนิด  
10.8 แบบฝึกหัดท้ายบท  
10.9 แหล่งอ้างอิง  

---

## 10.1 การรับข้อมูลจากผู้ใช้ด้วย `Console.ReadLine()`

ในบทที่แล้วเราใช้ `Console.WriteLine()` เพื่อแสดงผล แต่โปรแกรมที่มีประโยชน์ต้องสามารถรับข้อมูลจากผู้ใช้ได้ C# มีเมธอด `Console.ReadLine()` สำหรับอ่านข้อความที่ผู้ใช้พิมพ์ทางคอนโซล จนกว่าจะกด Enter

**รูปแบบพื้นฐาน:**

```csharp
string input = Console.ReadLine();
```

`Console.ReadLine()` จะคืนค่าเป็น `string` (หรือ `null` ถ้าผู้ใช้กด Ctrl+Z)

**ตัวอย่างง่าย:**

```csharp
Console.Write("กรุณาพิมพ์ชื่อของคุณ: ");
string name = Console.ReadLine();
Console.WriteLine($"สวัสดี {name}");
```

> 💡 **เคล็ดลับ:** ใช้ `Console.Write()` (ไม่ขึ้นบรรทัดใหม่) เพื่อให้เคอร์เซอร์อยู่บรรทัดเดียวกับข้อความถาม ทำให้ดูเป็นมิตรกว่า

---

## 10.2 ปัญหาของการรับข้อมูล: ทุกอย่างเป็น `string`

`Console.ReadLine()` อ่านมาทุกอย่างเป็น `string` แม้ผู้ใช้จะพิมพ์ตัวเลข เช่น `"25"` ก็ยังเป็น `string` ทำให้ไม่สามารถนำไปคำนวณทางคณิตศาสตร์ได้โดยตรง

```csharp
Console.Write("อายุ: ");
string ageInput = Console.ReadLine();
int age = ageInput + 5;   // ERROR! ไม่สามารถบวก string กับ int ได้
```

เราจำเป็นต้อง **แปลงชนิด (type conversion)** จาก `string` เป็นตัวเลข (`int`, `double`, `decimal`) ก่อนนำไปใช้

---

## 10.3 การแปลง `string` เป็นตัวเลขด้วย `Parse`

คลาส `int`, `double`, `decimal` มีเมธอด static ชื่อ `Parse()` สำหรับแปลง `string` เป็นชนิดนั้น ๆ

### 10.3.1 `int.Parse()`

```csharp
string numberStr = "100";
int number = int.Parse(numberStr);
int result = number * 2;
Console.WriteLine(result); // 200
```

### 10.3.2 `double.Parse()`

```csharp
string priceStr = "49.99";
double price = double.Parse(priceStr);
double total = price * 1.07; // รวม VAT 7%
Console.WriteLine(total);
```

### 10.3.3 ปัญหาของ `Parse`

ถ้าสตริงที่แปลงไม่ใช่ตัวเลขที่ถูกต้อง `Parse()` จะทำให้โปรแกรม **exception** (crash) ทันที:

```csharp
string badInput = "abc";
int number = int.Parse(badInput); // FormatException: Input string was not in a correct format.
```

⚠️ **ข้อควรระวัง:** การใช้ `Parse()` โดยไม่ตรวจสอบความถูกต้องอาจทำให้โปรแกรมล้มเหลวเมื่อผู้ใช้ป้อนข้อมูลผิด

---

## 10.4 การแปลง `string` เป็นตัวเลขแบบปลอดภัยด้วย `TryParse`

`TryParse()` เป็นทางเลือกที่ปลอดภัยกว่า: จะไม่เกิด exception แต่คืนค่า `bool` บอกว่าสำเร็จหรือไม่

### 10.4.1 รูปแบบการใช้ `TryParse`

```csharp
bool success = int.TryParse(stringValue, out int result);
```

- ถ้าแปลงสำเร็จ: `success` เป็น `true`, `result` มีค่าตัวเลข
- ถ้าแปลงไม่สำเร็จ: `success` เป็น `false`, `result` เป็น `0` (ค่าเริ่มต้น)

**ตัวอย่าง:**

```csharp
Console.Write("ป้อนอายุ: ");
string input = Console.ReadLine();

if (int.TryParse(input, out int age))
{
    Console.WriteLine($"ปีหน้า คุณจะอายุ {age + 1} ปี");
}
else
{
    Console.WriteLine("ป้อนอายุไม่ถูกต้อง กรุณาป้อนตัวเลข");
}
```

### 10.4.2 `TryParse` สำหรับ `double` และ `decimal`

```csharp
// double
if (double.TryParse(input, out double price))
{
    Console.WriteLine($"ราคารวม VAT: {price * 1.07:F2}");
}

// decimal (เหมาะกับเงินตรา)
if (decimal.TryParse(input, out decimal amount))
{
    Console.WriteLine($"จำนวนเงิน: {amount:C}");
}
```

> ⭐ **หัวข้อสำคัญ:** ในโปรแกรมจริงควรใช้ `TryParse()` เสมอ เว้นแต่คุณมั่นใจ 100% ว่าข้อมูลถูกต้อง (เช่น ข้อมูลจากระบบอื่น)

---

## 10.5 การแปลงชนิดข้อมูลแบบอื่น ๆ (Convert class)

คลาส `Convert` ให้เมธอดสำหรับแปลงระหว่างชนิดต่าง ๆ รวมถึง `string` → ตัวเลขด้วย

```csharp
string str = "123";
int num = Convert.ToInt32(str);
double dbl = Convert.ToDouble("45.67");
bool flag = Convert.ToBoolean("true");
```

แต่ `Convert.ToInt32()` ก็มีโอกาส exception เช่นเดียวกับ `Parse()` ดังนั้นควรใช้ `TryParse` หรือ `try-catch` (จะสอนในบทที่ 72)

**การแปลงจากตัวเลขเป็น `string`:** ใช้ `.ToString()`

```csharp
int score = 95;
string scoreStr = score.ToString();
Console.WriteLine("คะแนนของคุณคือ " + scoreStr);
```

---

## 10.6 ตัวอย่างโปรแกรมรับข้อมูลและคำนวณ

**ตัวอย่างที่ 10.1: เครื่องคิดเลขพื้นฐาน (บวก ลบ คูณ หาร)**

```csharp
Console.WriteLine("=== เครื่องคิดเลขง่าย ===");

// รับเลขตัวแรก
Console.Write("ป้อนเลขตัวแรก: ");
string input1 = Console.ReadLine();

// รับเลขตัวที่สอง
Console.Write("ป้อนเลขตัวที่สอง: ");
string input2 = Console.ReadLine();

// แปลงด้วย TryParse
if (double.TryParse(input1, out double num1) && double.TryParse(input2, out double num2))
{
    Console.WriteLine($"\nผลลัพธ์:");
    Console.WriteLine($"{num1} + {num2} = {num1 + num2}");
    Console.WriteLine($"{num1} - {num2} = {num1 - num2}");
    Console.WriteLine($"{num1} × {num2} = {num1 * num2}");
    
    if (num2 != 0)
        Console.WriteLine($"{num1} ÷ {num2} = {num1 / num2:F2}");
    else
        Console.WriteLine("ไม่สามารถหารด้วยศูนย์ได้");
}
else
{
    Console.WriteLine("ป้อนตัวเลขไม่ถูกต้อง กรุณาลองใหม่");
}
```

**ตัวอย่างที่ 10.2: โปรแกรมคำนวณ BMI**

```csharp
Console.WriteLine("=== โปรแกรมคำนวณ BMI ===");
Console.Write("น้ำหนัก (kg): ");
string weightInput = Console.ReadLine();

Console.Write("ส่วนสูง (cm): ");
string heightInput = Console.ReadLine();

if (double.TryParse(weightInput, out double weight) && 
    double.TryParse(heightInput, out double heightCm))
{
    double heightM = heightCm / 100;
    double bmi = weight / (heightM * heightM);
    
    Console.WriteLine($"\nBMI ของคุณ = {bmi:F2}");
    
    // แปลผล
    if (bmi < 18.5)
        Console.WriteLine("น้ำหนักน้อยกว่ามาตรฐาน");
    else if (bmi < 25)
        Console.WriteLine("น้ำหนักปกติ (สุขภาพดี)");
    else if (bmi < 30)
        Console.WriteLine("น้ำหนักเกิน");
    else
        Console.WriteLine("อ้วน (ควรพบแพทย์)");
}
else
{
    Console.WriteLine("ป้อนข้อมูลไม่ถูกต้อง");
}
```

---

## 10.7 ตารางสรุปวิธีการแปลงชนิด

| วิธีการ | รูปแบบ | เกิด exception? | เหมาะกับ |
|--------|--------|----------------|----------|
| `int.Parse()` | `int.Parse(string)` | ✅ (FormatException) | ข้อมูลเชื่อถือได้ |
| `double.Parse()` | `double.Parse(string)` | ✅ | ข้อมูลเชื่อถือได้ |
| `int.TryParse()` | `int.TryParse(string, out int)` | ❌ | ข้อมูลจากผู้ใช้ |
| `double.TryParse()` | `double.TryParse(string, out double)` | ❌ | ข้อมูลจากผู้ใช้ |
| `Convert.ToInt32()` | `Convert.ToInt32(object)` | ✅ | แปลงจาก object หลายชนิด |
| `.ToString()` | `ตัวเลข.ToString()` | ❌ | ตัวเลข → string |

### ตารางเปรียบเทียบ Parse กับ TryParse

| คุณสมบัติ | Parse | TryParse |
|-----------|-------|----------|
| คืนค่า | ตัวเลข (int, double, ...) | bool (success/fail) |
| รับค่าผ่าน | คืนค่าโดยตรง | out parameter |
| เมื่อแปลงไม่ได้ | Exception (โปรแกรมหยุด) | คืน false, ไม่ exception |
| การใช้ | `int x = int.Parse("123");` | `if(int.TryParse("123", out int x))` |
| ประสิทธิภาพ | เร็วกว่าเล็กน้อย | ช้ากว่าเล็กน้อย (out parameter) |

---

## 10.8 แบบฝึกหัดท้ายบท (4 ข้อ)

🧪 **แบบฝึกหัดที่ 10.1:**  
เขียนโปรแกรมรับอุณหภูมิเป็นองศาเซลเซียส (string → double) แล้วแปลงเป็นฟาเรนไฮต์ (สูตร: °F = °C × 9/5 + 32) แสดงผลทศนิยม 1 ตำแหน่ง ใช้ `TryParse`

🧪 **แบบฝึกหัดที่ 10.2:**  
โปรแกรมรับค่าสินค้า 3 รายการ (ราคาเป็นทศนิยม) จากผู้ใช้ แล้วหาผลรวม และค่าเฉลี่ย ถ้าผู้ใช้ป้อนไม่ถูกต้องให้แจ้งเตือนและให้ป้อนใหม่ (Hint: ใช้ loop while – จะสอนในบท 31 แต่ลองทำด้วย if ซ้อนกันก่อน)

🧪 **แบบฝึกหัดที่ 10.3:**  
จงอธิบายความแตกต่างระหว่าง `int.Parse("abc")` และ `int.TryParse("abc", out int result)` พร้อมบอกผลลัพธ์ที่เกิดขึ้น

🧪 **แบบฝึกหัดที่ 10.4 (ท้าทาย):**  
เขียนโปรแกรมที่รับรหัสสินค้า (string), ราคา (double), และจำนวน (int) จากผู้ใช้ ถ้าข้อมูลทั้งหมดถูกต้อง ให้คำนวณราคารวม (ราคา × จำนวน) และแสดง receipt (ใบเสร็จ) แบบสวยงาม ถ้าข้อมูลผิดให้แจ้ง error เฉพาะฟิลด์ที่ผิด

---

## 10.9 แหล่งอ้างอิง

- 🔗 **Console.ReadLine()** – [https://docs.microsoft.com/en-us/dotnet/api/system.console.readline](https://docs.microsoft.com/en-us/dotnet/api/system.console.readline)
- 🔗 **Int32.Parse vs TryParse** – [https://docs.microsoft.com/en-us/dotnet/api/system.int32.parse](https://docs.microsoft.com/en-us/dotnet/api/system.int32.parse)
- 🔗 **Type conversion in C#** – [https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/casting-and-type-conversions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/types/casting-and-type-conversions)
- 🔗 **Convert Class** – [https://docs.microsoft.com/en-us/dotnet/api/system.convert](https://docs.microsoft.com/en-us/dotnet/api/system.convert)

---

## สรุปท้ายบท

บทที่ 10 สอนการรับข้อมูลผู้ใช้ด้วย `Console.ReadLine()` และการแปลง `string` เป็นตัวเลขด้วย `Parse()` (เสี่ยง exception) และ `TryParse()` (ปลอดภัย) หลักการสำคัญคือ **ควรใช้ TryParse เสมอเมื่อรับข้อมูลจากผู้ใช้** เพื่อป้องกันโปรแกรมหยุดทำงาน

นอกจากนี้ยังรู้จัก `Convert` class และการแปลงตัวเลขกลับเป็น `string` ด้วย `.ToString()` คุณสามารถนำเทคนิคเหล่านี้ไปสร้างโปรแกรมที่โต้ตอบกับผู้ใช้ได้จริง เช่น เครื่องคิดเลข BMI หรือโปรแกรมคำนวณราคาสินค้า

**ในบทถัดไป (บทที่ 11)** เราจะพูดถึง **ชนิดข้อมูลตัวเลข** อย่างละเอียดมากขึ้น ทั้ง `int`, `double`, `float`, `decimal` และการแปลงระหว่างชนิด (implicit/explicit casting)

---

*หมายเหตุ: บทที่ 10 นี้มีความยาวประมาณ 1,600 คำ*

---

(ดำเนินการส่งบทที่ 11 ต่อไปโดยอัตโนมัติ)