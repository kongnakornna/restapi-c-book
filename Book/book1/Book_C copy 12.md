# Mastering C# .NET 2026: จากพื้นฐานสู่ Enterprise Application + Database + Cache + Message Queue

## บทที่ 12: ชนิดข้อมูล bool, char และ escape sequences

---

### สารบัญย่อยของบทที่ 12

12.1 ชนิดข้อมูล bool – ค่าความจริง True/False  
12.2 ชนิดข้อมูล char – อักขระตัวเดียว  
12.3 Escape Sequences – อักขระพิเศษใน string และ char  
12.4 การใช้งาน bool ในการตัดสินใจ (if)  
12.5 การแปลงระหว่าง char, string, และตัวเลข  
12.6 ตัวอย่างโค้ดที่รันได้จริง  
12.7 ตารางสรุป bool, char, escape sequences  
12.8 แบบฝึกหัดท้ายบท  
12.9 แหล่งอ้างอิง  

---

## 12.1 ชนิดข้อมูล bool – ค่าความจริง True/False

**`bool`** เป็นชนิดข้อมูลที่เก็บค่าได้เพียงสองค่า: `true` (จริง) หรือ `false` (เท็จ) ใช้สำหรับการตัดสินใจในโปรแกรม เช่น ตรวจสอบเงื่อนไข, ควบคุมการทำงานของ if, while, for

### 12.1.1 การประกาศและการกำหนดค่า

```csharp
bool isReady = true;
bool isCompleted = false;
bool isEqual = (10 > 5);   // true
```

### 12.1.2 ตัวดำเนินการเปรียบเทียบที่ให้ผลลัพธ์ bool

| ตัวดำเนินการ | ความหมาย | ตัวอย่าง (a=5, b=3) | ผลลัพธ์ |
|--------------|-----------|---------------------|---------|
| `==` | เท่ากัน | `a == b` | false |
| `!=` | ไม่เท่ากัน | `a != b` | true |
| `>` | มากกว่า | `a > b` | true |
| `<` | น้อยกว่า | `a < b` | false |
| `>=` | มากกว่าหรือเท่ากัน | `a >= 5` | true |
| `<=` | น้อยกว่าหรือเท่ากัน | `b <= 3` | true |

### 12.1.3 ตัวดำเนินการเชิงตรรกะ (Logical Operators)

| ตัวดำเนินการ | ความหมาย | ตัวอย่าง | ผลลัพธ์ |
|--------------|-----------|----------|---------|
| `&&` | และ (AND) | `true && false` | false |
| `\|\|` | หรือ (OR) | `true \|\| false` | true |
| `!` | นิเสธ (NOT) | `!true` | false |

```csharp
bool isAdult = age >= 18;
bool hasPermission = true;
bool canEnter = isAdult && hasPermission;
bool canSkip = !isAdult && hasPermission;  // เด็กแต่ได้รับอนุญาต
```

> 💡 **เคล็ดลับ:** การตั้งชื่อตัวแปร bool ควรขึ้นต้นด้วย `is`, `has`, `can`, `should` เพื่อสื่อความหมาย เช่น `isValid`, `hasValue`, `canDelete`

---

## 12.2 ชนิดข้อมูล char – อักขระตัวเดียว

**`char`** ใช้เก็บอักขระ Unicode ตัวเดียว (ขนาด 16 bit) ต้องใช้เครื่องหมายอัญประกาศเดี่ยว `' '` (single quote)

### 12.2.1 การประกาศและการกำหนดค่า

```csharp
char grade = 'A';
char digit = '5';
char symbol = '@';
char newline = '\n';     // escape sequence
char unicode = '\u0041'; // 'A' (Unicode hex)
```

⚠️ **ข้อควรระวัง:** ใช้ single quote `'A'` สำหรับ char, ใช้ double quote `"A"` สำหรับ string ที่มีตัวอักษรเดียวก็ตาม

### 12.2.2 การแปลงระหว่าง char, string, และ int

```csharp
char c = 'A';

// char → string
string s = c.ToString();   // "A"

// char → int (รหัส ASCII/Unicode)
int code = (int)c;         // 65

// int → char (ต้องตรวจสอบช่วง)
char fromCode = (char)65;  // 'A'

// char → bool? ไม่มีโดยตรง ใช้เปรียบเทียบ
bool isDigit = char.IsDigit('5');      // true
bool isLetter = char.IsLetter('A');    // true
bool isUpper = char.IsUpper('a');      // false
```

---

## 12.3 Escape Sequences – อักขระพิเศษใน string และ char

**Escape sequence** คือการเขียนอักขระที่พิมพ์ยากหรือมีความหมายพิเศษด้วยเครื่องหมาย backslash (`\`) ตามด้วยรหัส

### 12.3.1 ตาราง escape sequences ที่สำคัญ

| Escape | ชื่อ | ความหมาย | ตัวอย่างผลลัพธ์ |
|--------|------|-----------|----------------|
| `\'` | single quote | อัญประกาศเดี่ยว | `'\''` → ' |
| `\"` | double quote | อัญประกาศคู่ | `"\""` → " |
| `\\` | backslash | เครื่องหมาย \ | `"C:\\Temp"` → C:\Temp |
| `\n` | newline | ขึ้นบรรทัดใหม่ | `"A\nB"` → A(ขึ้นบรรทัดใหม่)B |
| `\r` | carriage return | กลับหัวบรรทัด | ส่วนน้อย ใช้กับ \n เป็น \r\n (Windows) |
| `\t` | tab | เว้นวรรคใหญ่ | `"A\tB"` → A    B |
| `\uXXXX` | Unicode | อักขระ Unicode 16-bit | `"\u0E17"` → 'ท' (ไทย) |
| `\xXX` | Unicode (hex) | ตัวแปรสั้นของ \u | `"\xE17"` → ตัว ท เช่นกัน |

**ตัวอย่าง:**

```csharp
Console.WriteLine("Hello\nWorld");
Console.WriteLine("C:\\MyFolder\\File.txt");
Console.WriteLine("Name\tAge\tCity");
Console.WriteLine("\u0E17\u0E33");  // "ทำ"
```

### 12.3.2 Verbatim string literal (`@"..."`)

ใช้ `@` นำหน้า string เพื่อให้ไม่ต้อง escape อักขระพิเศษ (ยกเว้น `"` ที่ต้องใช้ `""` แทน)

```csharp
string path = @"C:\MyFolder\File.txt";   // ไม่ต้อง \\\\
string multiLine = @"บรรทัดแรก
บรรทัดที่สอง
บรรทัดที่สาม";
string quote = @"เขาพูดว่า ""สวัสดี""";  // ใช้ "" แทน "

Console.WriteLine(multiLine);
Console.WriteLine(quote);
```

---

## 12.4 การใช้งาน bool ในการตัดสินใจ (if)

แม้เราจะเรียน if อย่างละเอียดในบทที่ 21 แต่ขอให้ตัวอย่างสั้น ๆ เพื่อให้เห็นการประยุกต์ใช้ bool:

```csharp
int score = 85;
bool isPass = score >= 50;

if (isPass)
{
    Console.WriteLine("สอบผ่าน");
}
else
{
    Console.WriteLine("สอบไม่ผ่าน");
}
```

การรวม bool หลายเงื่อนไข:

```csharp
bool isWeekend = true;
bool isHoliday = false;
bool canSleepLate = isWeekend || isHoliday;   // OR
bool mustWork = !canSleepLate;                // NOT
```

---

## 12.5 การแปลงระหว่าง char, string, และตัวเลข (เพิ่มเติม)

### 12.5.1 char ↔ int (รหัส Unicode)

```csharp
char thaiChar = 'ก';
int code = thaiChar;                // implicit conversion → 3585
char fromCode = (char)3585;         // 'ก'
```

### 12.5.2 char.IsDigit, char.IsLetter, char.ToUpper

```csharp
char c = 'a';
Console.WriteLine(char.IsDigit(c));     // False
Console.WriteLine(char.IsLetter(c));    // True
Console.WriteLine(char.ToUpper(c));     // 'A'
```

### 12.5.3 การวนลูปด้วย char (จะใช้ในบทอาเรย์)

```csharp
for (char ch = 'A'; ch <= 'Z'; ch++)
{
    Console.Write(ch + " ");
}
// A B C ... Z
```

---

## 12.6 ตัวอย่างโค้ดที่รันได้จริง

**ตัวอย่างที่ 12.1: ตรวจสอบรหัสผ่านอย่างง่าย (ใช้ bool และ char)**

```csharp
Console.Write("ป้อนรหัสผ่าน: ");
string password = Console.ReadLine();

bool hasUpper = false;
bool hasDigit = false;

foreach (char ch in password)
{
    if (char.IsUpper(ch)) hasUpper = true;
    if (char.IsDigit(ch)) hasDigit = true;
}

if (password.Length >= 6 && hasUpper && hasDigit)
{
    Console.WriteLine("รหัสผ่านแข็งแรง");
}
else
{
    Console.WriteLine("รหัสผ่านอ่อน: ต้องยาว ≥6 มีตัวพิมพ์ใหญ่และตัวเลข");
}
```

**ตัวอย่างที่ 12.2: แสดงตาราง Unicode ภาษาไทย**

```csharp
Console.WriteLine("พยัญชนะไทย บางส่วน:");
for (char thai = '\u0E01'; thai <= '\u0E0E'; thai++) // ก ถึง ฎ
{
    Console.Write($"{thai} ");
}
Console.WriteLine();
```

**ตัวอย่างที่ 12.3: การใช้ escape sequences และ verbatim string**

```csharp
string json1 = "{\"name\":\"Somchai\",\"age\":30}";
string json2 = @" {""name"":""Somchai"",""age"":30} ";  // verbatim

Console.WriteLine(json1);
Console.WriteLine(json2);

string path = @"C:\Users\Documents\File.txt";
Console.WriteLine($"Path: {path}");
```

---

## 12.7 ตารางสรุป bool, char, escape sequences

### ตารางที่ 12.1: ชนิด bool และ char

| ชนิด | ค่า可能的 | ขนาด | การใช้งานหลัก |
|------|------------|------|----------------|
| `bool` | `true`, `false` | 1 byte (logical) | เงื่อนไข, การตัดสินใจ |
| `char` | อักขระ Unicode 16-bit | 2 byte | ตัวอักษรเดียว, escape sequences |

### ตารางที่ 12.2: escape sequences ที่พบบ่อย

| Escape | ความหมาย |
|--------|-----------|
| `\n` | newline (LF) |
| `\r` | carriage return (CR) |
| `\t` | tab |
| `\\` | backslash |
| `\"` | double quote |
| `\'` | single quote |
| `\uXXXX` | Unicode (hex) |

### ตารางที่ 12.3: เมธอดช่วยของ char

| เมธอด static | ตัวอย่าง | ผลลัพธ์ |
|---------------|----------|---------|
| `char.IsDigit(char)` | `char.IsDigit('5')` | true |
| `char.IsLetter(char)` | `char.IsLetter('A')` | true |
| `char.IsLetterOrDigit(char)` | `char.IsLetterOrDigit('_')` | false |
| `char.IsUpper(char)` | `char.IsUpper('a')` | false |
| `char.ToUpper(char)` | `char.ToUpper('a')` | 'A' |
| `char.ToLower(char)` | `char.ToLower('A')` | 'a' |

---

## 12.8 แบบฝึกหัดท้ายบท (4 ข้อ)

🧪 **แบบฝึกหัดที่ 12.1:**  
จงเขียนโปรแกรมรับตัวอักษรหนึ่งตัวจากผู้ใช้ แล้วตรวจสอบว่าเป็นสระภาษาอังกฤษ (a, e, i, o, u) หรือไม่ (ทั้งพิมพ์เล็กและพิมพ์ใหญ่) โดยใช้ bool และ char

🧪 **แบบฝึกหัดที่ 12.2:**  
ให้ประกาศตัวแปร `char` ชื่อ `ch` มีค่า `'x'` จากนั้นใช้ escape sequence เพื่อแสดงข้อความ `'x' is a letter` (โดยใช้ single quote จริง ๆ ใน output)

🧪 **แบบฝึกหัดที่ 12.3:**  
เขียนโปรแกรมรับรหัสผ่าน แล้วตรวจสอบเงื่อนไขทั้งหมด:  
- ความยาวอย่างน้อย 8 ตัวอักษร  
- มีตัวพิมพ์ใหญ่อย่างน้อย 1 ตัว  
- มีตัวพิมพ์เล็กอย่างน้อย 1 ตัว  
- มีตัวเลขอย่างน้อย 1 ตัว  
แสดงผลว่า “ผ่าน” หรือ “ไม่ผ่าน” พร้อมระบุเงื่อนไขที่ขาด

🧪 **แบบฝึกหัดที่ 12.4 (ท้าทาย):**  
ใช้ verbatim string (`@"..."`) สร้าง string ที่มีข้อความหลายบรรทัดและมี double quote ภายใน เช่น:
```
เขาพูดว่า "Hello World"
แล้วก็จากไป
```
จากนั้นแสดงผลทางคอนโซล

---

## 12.9 แหล่งอ้างอิง

- 🔗 **bool type (C# reference)** – [https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool)
- 🔗 **char type (C# reference)** – [https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/char](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/char)
- 🔗 **Escape sequences in C#** – [https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/strings/#escape-sequences](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/strings/#escape-sequences)
- 🔗 **Char Struct (methods)** – [https://docs.microsoft.com/en-us/dotnet/api/system.char](https://docs.microsoft.com/en-us/dotnet/api/system.char)

---

## สรุปท้ายบท

บทที่ 12 ครอบคลุมชนิดข้อมูลที่ใช้สำหรับค่าความจริง (`bool`) และอักขระตัวเดียว (`char`) รวมถึง escape sequences สำหรับแทนอักขระพิเศษใน string และ char คุณได้เรียนรู้:

- `bool` มีค่า `true`/`false` ใช้กับตัวดำเนินการเปรียบเทียบและตรรกะ
- `char` เก็บอักขระ Unicode 1 ตัว ใช้ single quote, มีเมธอดช่วย `char.IsDigit()`, `char.ToUpper()` เป็นต้น
- Escape sequences (`\n`, `\t`, `\"`, `\\`, `\uXXXX`) ช่วยเขียนอักขระพิเศษ
- Verbatim string (`@"..."`) ทำให้ไม่ต้อง escape (ยกเว้น `""` สำหรับ double quote)

ความเข้าใจ `bool` และ `char` เป็นพื้นฐานสำคัญสำหรับการเขียนเงื่อนไขและการจัดการข้อความ ในบทถัดไป (บทที่ 13) เราจะพูดถึง **Value Types vs Reference Types** ซึ่งเป็นหัวใจของโมเดลหน่วยความจำใน C#

---

*หมายเหตุ: บทที่ 12 นี้มีความยาวประมาณ 1,800 คำ*

---

(ดำเนินการส่งบทที่ 13 ต่อไปโดยอัตโนมัติ)