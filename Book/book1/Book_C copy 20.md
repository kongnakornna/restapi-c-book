# Mastering C# .NET 2026: จากพื้นฐานสู่ Enterprise Application + Database + Cache + Message Queue

## บทที่ 20: ตัวดำเนินการเชิงตรรกะ (&&, ||, !) และ relational

---

### สารบัญย่อยของบทที่ 20

20.1 ตัวดำเนินการ relational (เปรียบเทียบ)  
20.2 ตัวดำเนินการเชิงตรรกะ (Logical Operators)  
20.3 Short-circuit evaluation – การประเมินแบบตัดตอน  
20.4 การรวมเงื่อนไขหลายระดับ  
20.5 ข้อผิดพลาดที่พบบ่อย  
20.6 ตัวอย่างโค้ดที่รันได้จริง  
20.7 ตารางสรุปตัวดำเนินการ  
20.8 แบบฝึกหัดท้ายบท  
20.9 แหล่งอ้างอิง  

---

## 20.1 ตัวดำเนินการ relational (เปรียบเทียบ)

ตัวดำเนินการ relational ใช้เปรียบเทียบค่าสองค่า ผลลัพธ์เป็น `bool` (true/false) มีทั้งหมด 6 ชนิด:

| ตัวดำเนินการ | ความหมาย | ตัวอย่าง (a=5, b=3) | ผลลัพธ์ |
|--------------|-----------|---------------------|---------|
| `==` | เท่ากัน | `a == b` | false |
| `!=` | ไม่เท่ากัน | `a != b` | true |
| `>` | มากกว่า | `a > b` | true |
| `<` | น้อยกว่า | `a < b` | false |
| `>=` | มากกว่าหรือเท่ากัน | `a >= 5` | true |
| `<=` | น้อยกว่าหรือเท่ากัน | `b <= 3` | true |

**ตัวอย่างการใช้งาน:**

```csharp
int age = 18;
bool isAdult = age >= 18;        // true
bool isSenior = age >= 60;       // false
bool isExactly18 = age == 18;    // true

string input = "admin";
bool isAdmin = input == "admin"; // true
```

> 💡 **เคล็ดลับ:** เปรียบเทียบ string ใช้ `==` หรือ `string.Equals()` ได้ แต่ควรระวังตัวพิมพ์เล็กใหญ่ ถ้าต้องการไม่สนใจตัวพิมพ์ให้ใช้ `string.Equals(a, b, StringComparison.OrdinalIgnoreCase)`

---

## 20.2 ตัวดำเนินการเชิงตรรกะ (Logical Operators)

ใช้กับ `bool` เพื่อสร้างเงื่อนไขที่ซับซ้อนขึ้น:

| ตัวดำเนินการ | ความหมาย | ตัวอย่าง | ผลลัพธ์ |
|--------------|-----------|----------|---------|
| `&&` | และ (AND) | `true && true` | true |
| `&&` | และ (AND) | `true && false` | false |
| `\|\|` | หรือ (OR) | `true \|\| false` | true |
| `\|\|` | หรือ (OR) | `false \|\| false` | false |
| `!` | นิเสธ (NOT) | `!true` | false |
| `!` | นิเสธ (NOT) | `!false` | true |

**ตัวอย่าง:**

```csharp
int score = 85;
bool hasBonus = true;

// ผ่านเกณฑ์เมื่อคะแนน >= 50 และมีโบนัส
bool isPass = score >= 50 && hasBonus;  // true

// ได้เหรียญทองเมื่อคะแนน >= 80 หรือมีโบนัสพิเศษ
bool isGold = score >= 80 || hasBonus;  // true

// ไม่ผ่านเมื่อคะแนน < 50
bool isFail = !(score >= 50);           // false
```

---

## 20.3 Short-circuit evaluation – การประเมินแบบตัดตอน

`&&` และ `||` จะประเมินเฉพาะเท่าที่จำเป็นเพื่อให้ทราบผลลัพธ์:

- **`&&` (AND):** ถ้าฝั่งซ้ายเป็น `false` ผลลัพธ์เป็น `false` แน่นอน ไม่ต้องประเมินฝั่งขวา
- **`||` (OR):** ถ้าฝั่งซ้ายเป็น `true` ผลลัพธ์เป็น `true` แน่นอน ไม่ต้องประเมินฝั่งขวา

**ตัวอย่างที่เห็นผล:**

```csharp
int x = 10;
bool result = (x > 5) || (++x > 0);  
// ฝั่งซ้าย true จึงไม่ประเมิน ++x ทำให้ x ยังคงเป็น 10

Console.WriteLine(x);  // 10 (ไม่ถูกเพิ่ม)

bool result2 = (x > 20) && (++x > 0);
// ฝั่งซ้าย false จึงไม่ประเมิน ++x
Console.WriteLine(x);  // ยังคง 10
```

**ประโยชน์:** ป้องกันการ execute โค้ดที่ไม่จำเป็น, เช่น ตรวจสอบ null ก่อน:

```csharp
if (person != null && person.Age >= 18)
{
    // ถ้า person เป็น null จะไม่เข้าไปตรวจ person.Age (ป้องกัน NullReferenceException)
}
```

---

## 20.4 การรวมเงื่อนไขหลายระดับ

สามารถรวมตัวดำเนินการ relational และ logical เข้าด้วยกันเป็นเงื่อนไขที่ซับซ้อนได้:

```csharp
int age = 25;
int income = 30000;
bool hasJob = true;

// เงื่อนไข: อายุระหว่าง 18-60, มีรายได้ > 20000, และมีงานทำ
bool isEligible = (age >= 18 && age <= 60) && (income > 20000) && hasJob;

// เงื่อนไข: ได้ส่วนลดเมื่อเป็นสมาชิก (isMember) หรือซื้อครบ 1000
bool getDiscount = isMember || totalAmount >= 1000;

// เงื่อนไขซับซ้อน: นักศึกษา (อายุ < 25) หรือ ผู้สูงอายุ (อายุ > 60) ได้ส่วนลดพิเศษ
bool specialDiscount = (age < 25) || (age > 60);
```

---

## 20.5 ข้อผิดพลาดที่พบบ่อย

### 20.5.1 ใช้ `=` แทน `==` ในเงื่อนไข

```csharp
int x = 5;
if (x = 10)  // Error: ไม่สามารถใช้ assignment ในเงื่อนไข (ยกเว้น bool)
{
    // ...
}

// ถูกต้อง
if (x == 10) { ... }
```

### 20.5.2 ลำดับความสำคัญผิด

```csharp
// ต้องการ: age > 18 และ (country == "TH" หรือ country == "US")
// ผิด: เพราะ && มี优先级สูงกว่า ||
bool condition = age > 18 && country == "TH" || country == "US";
// เท่ากับ (age > 18 && country == "TH") || country == "US"

// ถูกต้อง: ใส่วงเล็บ
bool condition = age > 18 && (country == "TH" || country == "US");
```

### 20.5.3 การเปรียบเทียบ double/tolerance

```csharp
double a = 0.1 + 0.2;
double b = 0.3;
if (a == b)  // อาจเป็น false เนื่องจาก rounding error
{
    // ควรใช้ tolerance
}

if (Math.Abs(a - b) < 0.000001)
{
    // ถูกต้อง
}
```

---

## 20.6 ตัวอย่างโค้ดที่รันได้จริง

**ตัวอย่างที่ 20.1: ระบบตรวจสอบสิทธิ์เข้าใช้งาน**

```csharp
Console.WriteLine("=== ระบบตรวจสอบสิทธิ์ ===");
Console.Write("อายุ: ");
int age = int.Parse(Console.ReadLine());
Console.Write("มีบัตรประชาชน? (true/false): ");
bool hasId = bool.Parse(Console.ReadLine());
Console.Write("เป็นสมาชิกหรือไม่? (true/false): ");
bool isMember = bool.Parse(Console.ReadLine());

bool canEnter = (age >= 18) && hasId;
bool canGetDiscount = isMember && canEnter;
bool isVip = isMember && (age >= 60);

Console.WriteLine($"\nสามารถเข้าได้: {canEnter}");
Console.WriteLine($"ได้รับส่วนลด: {canGetDiscount}");
Console.WriteLine($"สถานะ VIP: {isVip}");
```

**ตัวอย่างที่ 20.2: การตรวจสอบช่วง (Range check)**

```csharp
int score = 75;

// ตรวจสอบว่า score อยู่ในช่วง 0-100 หรือไม่
bool isValid = score >= 0 && score <= 100;

// ตรวจสอบเกรด
if (score >= 80 && score <= 100)
    Console.WriteLine("เกรด A");
else if (score >= 70 && score < 80)
    Console.WriteLine("เกรด B");
else if (score >= 60 && score < 70)
    Console.WriteLine("เกรด C");
else if (score >= 0 && score < 60)
    Console.WriteLine("เกรด F");
else
    Console.WriteLine("คะแนนไม่ถูกต้อง");
```

**ตัวอย่างที่ 20.3: การตรวจสอบปีอธิกสุรทิน (Leap Year)**

```csharp
int year = 2024;
bool isLeapYear = (year % 4 == 0) && (year % 100 != 0 || year % 400 == 0);
Console.WriteLine($"{year} เป็นปีอธิกสุรทิน: {isLeapYear}");
// 2024 → true, 1900 → false, 2000 → true
```

---

## 20.7 ตารางสรุปตัวดำเนินการ

### ตารางที่ 20.1: Relational Operators

| ตัวดำเนินการ | ชื่อ | ตัวอย่าง | ผลลัพธ์ (a=5,b=3) |
|--------------|------|----------|-------------------|
| `==` | เท่ากัน | `a == b` | false |
| `!=` | ไม่เท่ากัน | `a != b` | true |
| `>` | มากกว่า | `a > b` | true |
| `<` | น้อยกว่า | `a < b` | false |
| `>=` | มากกว่าหรือเท่ากัน | `a >= b` | true |
| `<=` | น้อยกว่าหรือเท่ากัน | `a <= b` | false |

### ตารางที่ 20.2: Logical Operators

| ตัวดำเนินการ | ชื่อ | A | B | ผลลัพธ์ |
|--------------|------|---|---|---------|
| `&&` | AND | true | true | true |
| `&&` | AND | true | false | false |
| `&&` | AND | false | (ใด ๆ) | false |
| `\|\|` | OR | true | (ใด ๆ) | true |
| `\|\|` | OR | false | true | true |
| `\|\|` | OR | false | false | false |
| `!` | NOT | true | - | false |
| `!` | NOT | false | - | true |

### ตารางที่ 20.3: ลำดับความสำคัญ (จากสูงไปต่ำ)

| ลำดับ | ตัวดำเนินการ |
|-------|--------------|
| 1 | `!` |
| 2 | `<`, `>`, `<=`, `>=` |
| 3 | `==`, `!=` |
| 4 | `&&` |
| 5 | `\|\|` |

---

## 20.8 แบบฝึกหัดท้ายบท (3 ข้อ)

🧪 **แบบฝึกหัดที่ 20.1:**  
จงหาค่าของนิพจน์ต่อไปนี้ (โดยไม่ต้องเขียนโค้ด):
ก) `(10 > 5) && (3 < 1)`  
ข) `(5 == 5) || (4 != 4)`  
ค) `!(true && false)`  
ง) `(8 >= 8) && (5 < 10) || (2 == 3)`

🧪 **แบบฝึกหัดที่ 20.2:**  
เขียนโปรแกรมรับปีเกิด (ค.ศ.) แล้วตรวจสอบว่าผู้ใช้เป็น Gen Z (เกิดระหว่างปี 1997–2012) หรือไม่ โดยใช้ตัวดำเนินการ relational และ logical

🧪 **แบบฝึกหัดที่ 20.3 (ท้าทาย):**  
สร้างโปรแกรมตรวจสอบรหัสผ่านที่มีเงื่อนไข:
- ความยาว >= 8 ตัวอักษร
- มีตัวพิมพ์ใหญ่อย่างน้อย 1 ตัว
- มีตัวเลขอย่างน้อย 1 ตัว
(ใช้ `char.IsUpper`, `char.IsDigit`) และแสดงผลว่า “ผ่าน” หรือ “ไม่ผ่าน” พร้อมระบุเงื่อนไขที่ขาด

---

## 20.9 แหล่งอ้างอิง

- 🔗 **Relational and equality operators** – [https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/equality-operators](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/equality-operators)
- 🔗 **Boolean logical operators** – [https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/boolean-logical-operators](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/boolean-logical-operators)
- 🔗 **Operator precedence** – [https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/operator-precedence](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/operator-precedence)

---

## สรุปท้ายบท

บทที่ 20 ครอบคลุมตัวดำเนินการ relational (เปรียบเทียบ) และ logical (ตรรกะ) ที่ใช้ในการสร้างเงื่อนไข:

- **Relational:** `==`, `!=`, `<`, `>`, `<=`, `>=` – ผลลัพธ์เป็น bool
- **Logical:** `&&` (AND), `||` (OR), `!` (NOT) – ใช้รวมเงื่อนไข
- **Short-circuit evaluation:** `&&` และ `||` จะประเมินเท่าที่จำเป็น
- **ลำดับความสำคัญ:** `!` > relational > `&&` > `||` – ควรใช้วงเล็บเพื่อความชัดเจน

ความเข้าใจตัวดำเนินการเหล่านี้เป็นพื้นฐานสำคัญสำหรับการเขียน `if`, `while`, `for` ที่มีเงื่อนไขซับซ้อน

**ในบทถัดไป (บทที่ 21)** เราจะนำความรู้นี้ไปใช้กับ **คำสั่ง if, else, else if, nested if** เพื่อควบคุมทิศทางของโปรแกรม

---
 