# Mastering C# .NET 2026: จากพื้นฐานสู่ Enterprise Application + Database + Cache + Message Queue

## บทที่ 9: ตัวแปรและชนิดข้อมูลเบื้องต้น (string, int)

---

### สารบัญย่อยของบทที่ 9

9.1 ตัวแปร (Variable) คืออะไร  
9.2 ชนิดข้อมูลเบื้องต้น (Primitive Types)  
9.3 ชนิดข้อมูล int – จำนวนเต็ม  
9.4 ชนิดข้อมูล string – ข้อความ  
9.5 การประกาศตัวแปรและการกำหนดค่า  
9.6 กฎการตั้งชื่อตัวแปร  
9.7 การแสดงผลตัวแปรร่วมกับข้อความ  
9.8 ตัวอย่างโค้ดที่รันได้จริง  
9.9 ตารางสรุปชนิดข้อมูล  
9.10 แบบฝึกหัดท้ายบท  
9.11 แหล่งอ้างอิง  

---

## 9.1 ตัวแปร (Variable) คืออะไร

**ตัวแปร** คือชื่อที่ใช้เรียกพื้นที่ในหน่วยความจำสำหรับเก็บข้อมูลที่มีค่าเปลี่ยนแปลงได้ เปรียบเสมือนกล่องใส่ข้อมูลที่มีป้ายชื่อ เวลาเราจะใช้ข้อมูลก็เรียกตามชื่อตัวแปร

**หลักการสำคัญ:**
- ตัวแปรทุกตัวใน C# ต้องมี **ชนิดข้อมูล (data type)** ที่แน่นอน
- ต้อง **ประกาศตัวแปร** ก่อนใช้งานเสมอ
- สามารถเปลี่ยนค่าได้ระหว่างรันโปรแกรม

```csharp
int age = 25;      // ประกาศตัวแปร age ชนิด int มีค่า 25
age = 26;          // เปลี่ยนค่าเป็น 26
```

> 💡 **เคล็ดลับ:** การใช้ตัวแปรช่วยให้โค้ดอ่านง่ายขึ้นและสามารถเปลี่ยนแปลงค่าได้โดยไม่ต้องแก้หลายตำแหน่ง

---

## 9.2 ชนิดข้อมูลเบื้องต้น (Primitive Types)

C# มีชนิดข้อมูลพื้นฐาน (built-in) หลายชนิด แต่ในบทนี้เราจะเน้น 2 ชนิดที่ใช้บ่อยที่สุด:

| ชนิด | คำอธิบาย | ตัวอย่างค่า | ขนาด (byte) |
|------|-----------|-------------|--------------|
| `int` | จำนวนเต็ม (ไม่มีจุดทศนิยม) | -2,147,483,648 ถึง 2,147,483,647 | 4 |
| `string` | ข้อความ (ลำดับของตัวอักษร) | "Hello", "123", "สวัสดี" | ขึ้นอยู่กับความยาว |
| `double` | จำนวนทศนิยมความแม่นยำสองเท่า | 3.14, -0.5, 1.0e-10 | 8 |
| `bool` | ค่าความจริง | `true`, `false` | 1 |
| `char` | อักขระตัวเดียว | 'A', '5', '#' | 2 |

> 📝 **หมายเหตุ:** ในบทนี้เราจะโฟกัสที่ `int` และ `string` ก่อน ส่วน `double`, `bool`, `char` จะอธิบายในบทที่ 11-12

---

## 9.3 ชนิดข้อมูล int – จำนวนเต็ม

**`int`** (integer) ใช้เก็บตัวเลขที่ไม่มีจุดทศนิยม เช่น อายุ, จำนวนสินค้า, รหัสไปรษณีย์

### 9.3.1 การประกาศและการกำหนดค่า

```csharp
int count;           // ประกาศอย่างเดียว (ยังไม่มีค่า)
count = 10;          // กำหนดค่า

int score = 100;     // ประกาศและกำหนดค่าพร้อมกัน
int year = 2026, month = 4;  // ประกาศหลายตัวในบรรทัดเดียว
```

### 9.3.2 การดำเนินการทางคณิตศาสตร์กับ int

```csharp
int a = 10;
int b = 3;

int sum = a + b;        // 13
int diff = a - b;       // 7
int product = a * b;    // 30
int quotient = a / b;   // 3 (เศษถูกตัดทิ้ง เพราะเป็น int)
int remainder = a % b;  // 1 (เศษจากการหาร)
```

> ⚠️ **ข้อควรระวัง:** การหาร `int` ด้วย `int` ได้ผลลัพธ์เป็น `int` (ปัดเศษทิ้ง) ถ้าต้องการผลเป็นทศนิยมต้องใช้ `double`

---

## 9.4 ชนิดข้อมูล string – ข้อความ

**`string`** ใช้เก็บข้อความ (ตัวอักษรหลายตัว) ต้องอยู่ในเครื่องหมายอัญประกาศคู่ `"..."`

### 9.4.1 การประกาศและการกำหนดค่า

```csharp
string name;                  // ประกาศ
name = "Somchai";             // กำหนดค่า

string message = "Hello C#"; // ประกาศ+กำหนด
string empty = "";            // ข้อความว่าง
string nullString = null;     // ไม่มีค่า (ต้องระวัง)
```

### 9.4.2 การรวมข้อความ (Concatenation)

```csharp
string firstName = "Somchai";
string lastName = "Jaidee";
string fullName = firstName + " " + lastName;  // "Somchai Jaidee"
```

### 9.4.3 อักขระพิเศษ (Escape Sequences)

| Escape | ความหมาย | ตัวอย่าง |
|--------|-----------|----------|
| `\n` | ขึ้นบรรทัดใหม่ | `"Line1\nLine2"` |
| `\t` | tab | `"Name\tAge"` |
| `\"` | อัญประกาศเดี่ยว | `"He said \"Hi\""` |
| `\\` | backslash | `"C:\\MyFolder"` |
| `@` | verbatim string (ไม่ต้อง escape) | `@"C:\MyFolder"` |

```csharp
Console.WriteLine("Hello\nWorld");
Console.WriteLine(@"C:\Program Files\MyApp");
```

---

## 9.5 การประกาศตัวแปรและการกำหนดค่า

### 9.5.1 รูปแบบทั่วไป

```
ชนิดข้อมูล ชื่อตัวแปร [= ค่าเริ่มต้น];
```

**ตัวอย่าง:**

```csharp
int age;                 // ประกาศอย่างเดียว
age = 30;                // กำหนดค่าทีหลัง

string city = "Bangkok"; // ประกาศพร้อมกำหนดค่า
int x = 5, y = 10;       // หลายตัวแปรในบรรทัดเดียว
```

### 9.5.2 การใช้ `var` (implicit typing)

C# มี keyword `var` ให้คอมไพลเลอร์อนุมานชนิดจากค่าที่กำหนด:

```csharp
var count = 100;        // คอมไพลเลอร์รู้ว่าเป็น int
var name = "John";      // คอมไพลเลอร์รู้ว่าเป็น string
// var something;       // Error: ต้องมีค่าเริ่มต้น
```

> 💡 **เคล็ดลับ:** ใช้ `var` เมื่อชนิดชัดเจนจากฝั่งขวา (`new Product()`, `GetList()`) แต่ไม่ควรใช้ถ้าอ่านแล้วไม่รู้ว่าเป็นชนิดอะไร

### 9.5.3 ค่าเริ่มต้นของตัวแปร

ตัวแปรระดับท้องถิ่น (ในเมธอด) **ไม่มีค่าเริ่มต้น** – ต้องกำหนดค่าก่อนใช้งาน:

```csharp
int number;          // ยังไม่มีค่า
// Console.WriteLine(number); // Error: unassigned local variable
number = 10;         // ต้องกำหนดก่อนใช้
```

---

## 9.6 กฎการตั้งชื่อตัวแปร

1. ขึ้นต้นด้วยตัวอักษร (a-z, A-Z), underscore (`_`), หรือ @ (แต่ไม่แนะนำ)
2. ห้ามขึ้นต้นด้วยตัวเลข
3. ห้ามใช้ keyword ของภาษา (เช่น `int`, `class`, `if`)
4. ตัวพิมพ์ใหญ่-เล็กต่างกัน (`myVar` ≠ `myvar`)
5. ใช้ **camelCase** สำหรับตัวแปรท้องถิ่น (ขึ้นต้นด้วยตัวพิมพ์เล็ก)

**ตัวอย่างที่ถูกต้อง:**
```csharp
int age;
string firstName;
int _internalCounter;
int count2;
```

**ตัวอย่างที่ผิด:**
```csharp
int 2ndNumber;      // ขึ้นต้นด้วยเลข
string class;       // ใช้ keyword
string first-name;  // มีเครื่องหมายขีด (ใช้ underscore แทน)
```

> ⭐ **หัวข้อสำคัญ:** การตั้งชื่อที่ดีควรสื่อความหมาย เช่น `price` แทน `p`, `customerName` แทน `cn`

---

## 9.7 การแสดงผลตัวแปรร่วมกับข้อความ

### 9.7.1 การใช้ `+` (concatenation)

```csharp
string name = "สมชาย";
int age = 25;
Console.WriteLine("ชื่อ: " + name + ", อายุ: " + age + " ปี");
```

### 9.7.2 การใช้ String Interpolation (แนะนำ)

ใช้เครื่องหมาย `$` นำหน้า แล้วใส่ตัวแปรใน `{}`:

```csharp
Console.WriteLine($"ชื่อ: {name}, อายุ: {age} ปี");
```

สะอาดกว่าและอ่านง่ายกว่า

### 9.7.3 การใช้ `String.Format`

```csharp
Console.WriteLine(string.Format("ชื่อ: {0}, อายุ: {1} ปี", name, age));
```

> 💡 **เคล็ดลับ:** ในหนังสือเล่มนี้เราจะใช้ **string interpolation** เป็นหลัก เพราะทันสมัยและอ่านง่าย

---

## 9.8 ตัวอย่างโค้ดที่รันได้จริง

**ตัวอย่างที่ 9.1: โปรแกรมเก็บข้อมูลส่วนตัว**

```csharp
// บทที่ 9 - ตัวอย่างตัวแปร int และ string
Console.WriteLine("=== โปรแกรมบันทึกข้อมูลส่วนตัว ===");

// ประกาศตัวแปร
string firstName;
string lastName;
int birthYear;
int currentYear = 2026;

// รับข้อมูลจากผู้ใช้
Console.Write("ชื่อ: ");
firstName = Console.ReadLine();

Console.Write("นามสกุล: ");
lastName = Console.ReadLine();

Console.Write("ปีเกิด (ค.ศ.): ");
birthYear = int.Parse(Console.ReadLine());  // แปลง string → int

// คำนวณอายุ
int age = currentYear - birthYear;

// แสดงผลแบบสวยงาม
Console.WriteLine("\n=== ข้อมูลของคุณ ===");
Console.WriteLine($"ชื่อ-นามสกุล: {firstName} {lastName}");
Console.WriteLine($"ปีเกิด: {birthYear}");
Console.WriteLine($"อายุ (ประมาณ): {age} ปี");

// ตัวอย่างการรวมข้อความแบบอื่น
string fullName = firstName + " " + lastName;
Console.WriteLine($"ชื่อเต็ม: {fullName}");

Console.Write("\nกด Enter เพื่อปิด...");
Console.ReadLine();
```

**ผลลัพธ์ตัวอย่าง:**
```
=== โปรแกรมบันทึกข้อมูลส่วนตัว ===
ชื่อ: สมชาย
นามสกุล: ใจดี
ปีเกิด (ค.ศ.): 1995

=== ข้อมูลของคุณ ===
ชื่อ-นามสกุล: สมชาย ใจดี
ปีเกิด: 1995
อายุ (ประมาณ): 31 ปี
ชื่อเต็ม: สมชาย ใจดี
```

**ตัวอย่างที่ 9.2: การคำนวณพื้นฐานด้วย int**

```csharp
int apples = 15;
int oranges = 8;

int totalFruits = apples + oranges;
int difference = apples - oranges;
int pricePerApple = 7;
int totalCost = apples * pricePerApple;

Console.WriteLine($"แอปเปิ้ล: {apples} ผล, ส้ม: {oranges} ผล");
Console.WriteLine($"รวมผลไม้: {totalFruits} ผล");
Console.WriteLine($"ผลต่าง: {difference} ผล");
Console.WriteLine($"แอปเปิ้ลราคาผลละ {pricePerApple} บาท รวม {totalCost} บาท");
```

---

## 9.9 ตารางสรุปชนิดข้อมูล

| ชนิด | คำอธิบาย | ตัวอย่าง | การประกาศ |
|------|-----------|----------|------------|
| `int` | จำนวนเต็ม (ไม่มีจุด) | `42`, `-7`, `0` | `int score = 100;` |
| `string` | ข้อความ | `"Hello"`, `"123"` | `string name = "John";` |
| `double` | จำนวนทศนิยม | `3.14`, `-0.5` | `double pi = 3.14159;` |
| `bool` | จริง/เท็จ | `true`, `false` | `bool isReady = true;` |
| `char` | อักขระเดียว | `'A'`, `'5'` | `char grade = 'A';` |

### ตารางการดำเนินการกับ int

| ตัวดำเนินการ | ความหมาย | ตัวอย่าง (a=10,b=3) | ผลลัพธ์ |
|--------------|-----------|---------------------|---------|
| `+` | บวก | `a + b` | 13 |
| `-` | ลบ | `a - b` | 7 |
| `*` | คูณ | `a * b` | 30 |
| `/` | หาร (จำนวนเต็ม) | `a / b` | 3 |
| `%` | เศษ (modulo) | `a % b` | 1 |

### ตาราง escape sequences สำหรับ string

| Escape | ผลลัพธ์ |
|--------|---------|
| `\n` | ขึ้นบรรทัดใหม่ |
| `\t` | tab |
| `\"` | อัญประกาศ |
| `\\` | backslash |
| `@` | verbatim (ไม่ escape) |

---

## 9.10 แบบฝึกหัดท้ายบท (4 ข้อ)

🧪 **แบบฝึกหัดที่ 9.1:**  
จงประกาศตัวแปร `int` ชื่อ `height` มีค่า 175 และ `string` ชื่อ `color` มีค่า "แดง" จากนั้นแสดงผล "ส่วนสูง: 175 ซม., สีที่ชอบ: แดง" โดยใช้ string interpolation

🧪 **แบบฝึกหัดที่ 9.2:**  
เขียนโปรแกรมรับความกว้างและความยาวของสี่เหลี่ยม (เป็นจำนวนเต็ม) จากผู้ใช้ แล้วคำนวณพื้นที่ (กว้าง × ยาว) และแสดงผล

🧪 **แบบฝึกหัดที่ 9.3:**  
จากโค้ดต่อไปนี้ มีข้อผิดพลาดอะไรบ้าง? จงแก้ไขให้ถูกต้อง:
```csharp
string 2name = "John";
int age = "25";
Console.WriteLine(name + age);
```

🧪 **แบบฝึกหัดที่ 9.4 (ท้าทาย):**  
เขียนโปรแกรมรับจำนวนวินาที (เป็น int) แล้วคำนวณว่าคิดเป็นกี่ชั่วโมง กี่นาที และกี่วินาที (เช่น 3665 วินาที = 1 ชั่วโมง 1 นาที 5 วินาที) โดยใช้ `/` และ `%` ช่วย

---

## 9.11 แหล่งอ้างอิง

- 🔗 **C# Variables (Microsoft Docs)** – [https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/variables](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/variables)
- 🔗 **Built-in types (C# reference)** – [https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/built-in-types](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/built-in-types)
- 🔗 **String interpolation** – [https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/tokens/interpolated)
- 🔗 **Escape sequences in C#** – [https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/strings/#escape-sequences](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/strings/#escape-sequences)

---

## สรุปท้ายบท

บทที่ 9 นี้ได้เรียนรู้เกี่ยวกับตัวแปรและชนิดข้อมูลเบื้องต้นที่สำคัญที่สุดสองชนิดคือ `int` (จำนวนเต็ม) และ `string` (ข้อความ) คุณสามารถประกาศตัวแปร กำหนดค่า เปลี่ยนค่า นำไปคำนวณหรือรวมกับข้อความ และแสดงผลผ่าน `Console.WriteLine` ร่วมกับ string interpolation

ความเข้าใจในเรื่องตัวแปรและชนิดข้อมูลเป็นรากฐานสำคัญสำหรับการเขียนโปรแกรมทุกประเภท ในบทถัดไป (บทที่ 10) เราจะพูดถึง **การรับข้อมูลผู้ใช้และการแปลงชนิดข้อมูล (Parse, TryParse)** เพื่อให้โปรแกรมโต้ตอบกับผู้ใช้ได้สมบูรณ์ยิ่งขึ้น

---

*หมายเหตุ: บทที่ 9 นี้มีความยาวประมาณ 1,700 คำ*

---

(ดำเนินการส่งบทที่ 10 ต่อไปโดยอัตโนมัติ)