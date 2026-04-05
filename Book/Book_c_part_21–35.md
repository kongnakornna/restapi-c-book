# Mastering C# .NET 2026: จากพื้นฐานสู่ Enterprise Application + Database + Cache + Message Queue

## บทที่ 21: คำสั่ง if, else, else if, nested if

### 21.1 รูปแบบของ if

```csharp
if (เงื่อนไข) { }
if (เงื่อนไข) { } else { }
if (เงื่อนไข1) { } else if (เงื่อนไข2) { } else { }
```

### 21.2 ตัวอย่าง

```csharp
int score = 85;
if (score >= 80)
    Console.WriteLine("A");
else if (score >= 70)
    Console.WriteLine("B");
else if (score >= 60)
    Console.WriteLine("C");
else
    Console.WriteLine("F");
```

### 21.3 Nested if

```csharp
if (age >= 18)
{
    if (hasLicense)
        Console.WriteLine("สามารถขับรถได้");
}
```

---

## บทที่ 22: ขอบเขตของตัวแปร (Scope)

### 22.1 ประเภท Scope

1. **Local Scope** – ภายในเมธอด
2. **Block Scope** – ภายใน `{ }` ของ if, for, while
3. **Class Scope** – ฟิลด์ของคลาส
4. **Static Scope** – static field
5. **Namespace Scope** – ระดับไฟล์

### 22.2 กฎการเข้าถึง

- บล็อกชั้นในเข้าถึงตัวแปรชั้นนอกได้
- บล็อกชั้นนอกเข้าถึงตัวแปรชั้นในไม่ได้
- ไม่สามารถประกาศตัวแปรซ้ำใน scope เดียวกัน

```csharp
int x = 10;
{
    int x = 20; // OK (shadowing)
    Console.WriteLine(x); // 20
}
Console.WriteLine(x); // 10
```

---

## บทที่ 23: คำสั่ง switch และ switch expression

### 23.1 switch statement

```csharp
switch (day)
{
    case 1: Console.WriteLine("จันทร์"); break;
    case 2: Console.WriteLine("อังคาร"); break;
    default: Console.WriteLine("อื่นๆ"); break;
}
```

### 23.2 switch expression (C# 8+)

```csharp
string dayName = day switch
{
    1 => "จันทร์",
    2 => "อังคาร",
    _ => "อื่นๆ"
};
```

### 23.3 case guard (when)

```csharp
case int score when score >= 80:
    return "A";
```

---

## บทที่ 24: โปรเจกต์: Quiz App แบบข้อความ

```csharp
string[] questions = { "เมืองหลวงไทย?", "C# พัฒนาโดย?" };
string[,] options = { { "A.โซล", "B.เชียงใหม่", "C.กรุงเทพฯ", "D.พัทยา" },
                      { "A.Google", "B.Microsoft", "C.Apple", "D.Amazon" } };
char[] answers = { 'C', 'B' };
int score = 0;
for (int i = 0; i < questions.Length; i++)
{
    Console.WriteLine(questions[i]);
    for (int j = 0; j < 4; j++) Console.WriteLine(options[i, j]);
    char answer = char.ToUpper(Console.ReadKey().KeyChar);
    if (answer == answers[i]) score++;
}
Console.WriteLine($"คะแนน: {score}/{questions.Length}");
```

---

## บทที่ 25: การสร้างตัวเลขสุ่ม (Random class)

### 25.1 การใช้งาน Random

```csharp
Random rnd = new Random();
int dice = rnd.Next(1, 7);     // 1-6
int any = rnd.Next();           // 0 ถึง 2,147,483,646
double ratio = rnd.NextDouble(); // 0.0-1.0
```

### 25.2 Random.Shared (Singleton)

```csharp
int value = Random.Shared.Next(1, 101);
```

### 25.3 การสุ่มเลือกจากอาร์เรย์

```csharp
string[] colors = { "แดง", "เขียว", "น้ำเงิน" };
string randomColor = colors[Random.Shared.Next(colors.Length)];
```

---

## บทที่ 26: ตัวดำเนินการ modulo (%) และการใช้งาน

### 26.1 การใช้งานพื้นฐาน

```csharp
int remainder = 10 % 3; // 1
bool isEven = (n % 2 == 0);
int lastDigit = number % 10;
int circularIndex = i % array.Length;
```

### 26.2 ตัวอย่าง

```csharp
// แยกเลขหลัก
int units = num % 10;
int tens = (num / 10) % 10;
// แสดงตาราง
for (int i = 1; i <= 20; i++)
{
    Console.Write($"{i,3}");
    if (i % 5 == 0) Console.WriteLine();
}
```

---

## บทที่ 27: โปรเจกต์: เครื่องคิดเลขแบบมีเงื่อนไข

```csharp
while (true)
{
    Console.WriteLine("1.+ 2.- 3.* 4./ 5.% 6.Exit");
    string choice = Console.ReadLine();
    if (choice == "6") break;
    Console.Write("Enter first number: ");
    double a = double.Parse(Console.ReadLine());
    Console.Write("Enter second number: ");
    double b = double.Parse(Console.ReadLine());
    switch (choice)
    {
        case "1": Console.WriteLine(a + b); break;
        case "2": Console.WriteLine(a - b); break;
        case "3": Console.WriteLine(a * b); break;
        case "4": if (b != 0) Console.WriteLine(a / b); else Console.WriteLine("Cannot divide by zero"); break;
        case "5": if (b != 0) Console.WriteLine(a % b); else Console.WriteLine("Cannot modulo by zero"); break;
        default: Console.WriteLine("Invalid choice"); break;
    }
}
```

---

## บทที่ 28: Cheatsheet การตัดสินใจใน C#

| รูปแบบ | ตัวอย่าง | ใช้เมื่อ |
|--------|----------|---------|
| `if` | `if (x > 0) { }` | เงื่อนไขเดียว |
| `if-else` | `if (x > 0) { } else { }` | สองทางเลือก |
| `else-if` | `if (x>0){} else if(x<0){} else{}` | หลายทางเลือก (ช่วง) |
| `switch` | `switch(day){ case 1: break; }` | ค่าคงที่ |
| `switch expression` | `day switch {1=>"Mon", _=>"Other"}` | คืนค่า |
| `ternary` | `var msg = (score>=50)?"Pass":"Fail";` | สองทางเลือกสั้นๆ |

---

## บทที่ 29: ภาพรวมลูป (for, while, do-while, foreach)

### 29.1 for loop

```csharp
for (int i = 0; i < 10; i++) { }
```

### 29.2 while loop

```csharp
while (condition) { }
```

### 29.3 do-while loop

```csharp
do { } while (condition);
```

### 29.4 foreach loop

```csharp
foreach (var item in collection) { }
```

---

## บทที่ 30: for loop – โครงสร้าง, การนับขึ้น/ลง, Thread.Sleep

### 30.1 การนับขึ้น/ลง

```csharp
for (int i = 0; i < 10; i++)      // 0-9
for (int i = 1; i <= 10; i++)     // 1-10
for (int i = 10; i > 0; i--)      // 10-1
for (int i = 0; i <= 10; i += 2)  // 0,2,4,6,8,10
```

### 30.2 Thread.Sleep

```csharp
for (int i = 10; i >= 0; i--)
{
    Console.WriteLine(i);
    Thread.Sleep(1000); // หยุด 1 วินาที
}
```

### 30.3 Nested for

```csharp
for (int i = 1; i <= 12; i++)
{
    for (int j = 1; j <= 12; j++)
        Console.Write($"{i * j,4}");
    Console.WriteLine();
}
```

---

## บทที่ 31: while loop – การนับรอบและ infinite loop, เกมทายตัวเลข

### 31.1 while loop

```csharp
int i = 0;
while (i < 10)
{
    Console.WriteLine(i);
    i++;
}
```

### 31.2 infinite loop

```csharp
while (true)
{
    string input = Console.ReadLine();
    if (input == "exit") break;
}
```

### 31.3 เกมทายตัวเลข

```csharp
int secret = Random.Shared.Next(1, 101);
int guess = 0;
while (guess != secret)
{
    Console.Write("ทายตัวเลข (1-100): ");
    guess = int.Parse(Console.ReadLine());
    if (guess < secret) Console.WriteLine("ต่ำไป");
    else if (guess > secret) Console.WriteLine("สูงไป");
    else Console.WriteLine("ถูกต้อง!");
}
```

---

## บทที่ 32: do-while loop

### 32.1 ลักษณะการทำงาน (ทำอย่างน้อย 1 ครั้ง)

```csharp
int choice;
do
{
    Console.WriteLine("1.Start 2.Exit");
    choice = int.Parse(Console.ReadLine());
} while (choice != 2);
```

### 32.2 Input validation

```csharp
int age;
do
{
    Console.Write("Enter age (1-120): ");
} while (!int.TryParse(Console.ReadLine(), out age) || age < 1 || age > 120);
```

---

## บทที่ 33: break และ continue

### 33.1 break (ออกจากลูป)

```csharp
for (int i = 0; i < 10; i++)
{
    if (i == 5) break;
    Console.WriteLine(i); // 0,1,2,3,4
}
```

### 33.2 continue (ข้ามรอบปัจจุบัน)

```csharp
for (int i = 0; i < 10; i++)
{
    if (i % 2 == 0) continue;
    Console.WriteLine(i); // 1,3,5,7,9
}
```

---

## บทที่ 34: โปรเจกต์: Rocket Landing Simulation

```csharp
double height = 1000, velocity = 0, fuel = 500;
const double GRAVITY = 1.62, THRUST = 10, SAFE_SPEED = 5;
while (height > 0)
{
    Console.WriteLine($"Height: {height:F1}m, Velocity: {velocity:F2}m/s, Fuel: {fuel:F1}");
    Console.Write("Burn fuel (0-50): ");
    double burn = Math.Min(double.Parse(Console.ReadLine()), fuel);
    double decel = burn * THRUST;
    velocity = velocity + GRAVITY - decel;
    if (velocity > 0) height -= velocity;
    fuel -= burn;
    Thread.Sleep(200);
}
Console.WriteLine(velocity <= SAFE_SPEED ? "Landing SUCCESS!" : "CRASH!");
```

---

## บทที่ 35: โปรเจกต์: Text Adventure Game

```csharp
class Room
{
    public string Name, Description;
    public Dictionary<string, string> Exits = new();
    public List<string> Items = new();
}
// สร้าง world, player inventory, รับคำสั่ง go/take/use/inventory
```

---

(จบบทที่ 21–35)

**โปรดแจ้ง "ต่อไป" เพื่อรับบทที่ 36–50 ในข้อความถัดไปครับ**