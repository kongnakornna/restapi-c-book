# Mastering C# .NET 2026: จากพื้นฐานสู่ Enterprise Application + Database + Cache + Message Queue

## บทที่ 36: โปรเจกต์: Average Calculator

```csharp
List<double> numbers = new List<double>();
while (true)
{
    Console.Write("Enter number (or 'done'): ");
    string input = Console.ReadLine();
    if (input == "done") break;
    if (double.TryParse(input, out double num))
        numbers.Add(num);
    else
        Console.WriteLine("Invalid number");
}
if (numbers.Count > 0)
{
    double sum = numbers.Sum();
    double avg = sum / numbers.Count;
    Console.WriteLine($"Sum: {sum}, Average: {avg:F2}, Count: {numbers.Count}");
}
```

---

## บทที่ 37: Cheatsheet ลูปใน C#

| ลูป | ไวยากรณ์ | จำนวนรอบขั้นต่ำ | เหมาะกับ |
|-----|----------|----------------|----------|
| `for` | `for(init; cond; iter)` | 0 | รู้จำนวนรอบ |
| `while` | `while(cond)` | 0 | ไม่รู้จำนวนรอบ |
| `do-while` | `do{}while(cond)` | 1 | ต้องทำอย่างน้อย 1 ครั้ง |
| `foreach` | `foreach(var x in col)` | 0 | 遍历 collection |

**break** – ออกจากลูปทันที  
**continue** – ข้ามรอบปัจจุบัน

---

## บทที่ 38: อาร์เรย์มิติเดียว (declaration, access, Length)

```csharp
// ประกาศ
int[] numbers = new int[5];
int[] scores = { 85, 90, 78 };
string[] names = new string[] { "Alice", "Bob" };

// เข้าถึง
numbers[0] = 10;
int first = scores[0];

// Length
for (int i = 0; i < scores.Length; i++)
    Console.WriteLine(scores[i]);
```

---

## บทที่ 39: foreach loop กับอาร์เรย์

```csharp
int[] numbers = { 10, 20, 30 };
foreach (int num in numbers)
{
    Console.WriteLine(num);
}
```

**ข้อดี:** ไม่ต้องใช้ index, ป้องกัน IndexOutOfRange  
**ข้อเสีย:** ไม่สามารถแก้ไขค่าได้โดยตรง

---

## บทที่ 40: อาร์เรย์สองมิติและสามมิติ

```csharp
// 2D
int[,] matrix = { { 1, 2 }, { 3, 4 }, { 5, 6 } };
for (int i = 0; i < matrix.GetLength(0); i++)
    for (int j = 0; j < matrix.GetLength(1); j++)
        Console.Write($"{matrix[i, j]} ");

// 3D
int[,,] cube = new int[2, 3, 4];
cube[1, 2, 3] = 99;
```

---

## บทที่ 41: Jagged Arrays

```csharp
int[][] jagged = new int[3][];
jagged[0] = new int[] { 1, 2 };
jagged[1] = new int[] { 3, 4, 5 };
jagged[2] = new int[] { 6 };
for (int i = 0; i < jagged.Length; i++)
    for (int j = 0; j < jagged[i].Length; j++)
        Console.Write(jagged[i][j]);
```

---

## บทที่ 42: โปรเจกต์: Weather Simulator (อุณหภูมิ)

```csharp
int[] temps = new int[7];
Random rnd = new Random();
for (int i = 0; i < 7; i++)
    temps[i] = rnd.Next(15, 36);
double avg = temps.Average();
int max = temps.Max(), min = temps.Min();
Console.WriteLine($"Avg: {avg:F1}, Max: {max}, Min: {min}");
// Bar chart
foreach (int t in temps)
    Console.WriteLine(new string('█', t));
```

---

## บทที่ 43: การเรียงลำดับอาร์เรย์ (Array.Sort, Reverse)

```csharp
int[] numbers = { 5, 2, 8, 1, 9 };
Array.Sort(numbers);      // 1,2,5,8,9
Array.Reverse(numbers);   // 9,8,5,2,1
```

---

## บทที่ 44: Cheatsheet อาร์เรย์

| การดำเนินการ | 1D | 2D | Jagged |
|--------------|----|----|--------|
| ประกาศ | `int[] a` | `int[,] a` | `int[][] a` |
| สร้าง | `new int[5]` | `new int[3,4]` | `new int[3][]` |
| เข้าถึง | `a[i]` | `a[i,j]` | `a[i][j]` |
| จำนวนสมาชิก | `a.Length` | `a.Length` | `a.Length` (แถว) |
| ขนาดมิติ | `a.Length` | `a.GetLength(0)`, `a.GetLength(1)` | `a.Length`, `a[i].Length` |

---

## บทที่ 45: รู้จักเมธอด – เหตุผล, โครงสร้าง

```csharp
[access] [static] return_type MethodName(params)
{
    // body
    return value;
}
```

**เหตุผล:** ลดโค้ดซ้ำ (DRY), อ่านง่าย, บำรุงรักษาง่าย

---

## บทที่ 46: เมธอดแบบ void (ไม่รับพารามิเตอร์, รับพารามิเตอร์)

```csharp
void SayHello() => Console.WriteLine("Hello");
void Greet(string name) => Console.WriteLine($"Hello {name}");
void AddAndPrint(int a, int b) => Console.WriteLine(a + b);
```

---

## บทที่ 47: เมธอดที่คืนค่า (return, expression-bodied)

```csharp
int Add(int a, int b) => a + b;
int Square(int x) => x * x;
```

---

## บทที่ 48: Parameter Modifiers (ref, out, in)

```csharp
void Swap(ref int a, ref int b) { int t = a; a = b; b = t; }
bool TryParse(string s, out int result) { return int.TryParse(s, out result); }
double Distance(in Point p1, in Point p2) => Math.Sqrt(...);
```

| Modifier | ต้อง init ก่อน | ต้อง assign ในเมธอด | เหมาะกับ |
|----------|---------------|---------------------|----------|
| (none) | ✅ | ❌ | ค่าเล็ก |
| `ref` | ✅ | ❌ | เปลี่ยนต้นทาง |
| `out` | ❌ | ✅ | คืนหลายค่า |
| `in` | ✅ | ❌ | struct ขนาดใหญ่ |

---

## บทที่ 49: Recursion และ local functions

```csharp
int Factorial(int n) => n <= 1 ? 1 : n * Factorial(n - 1);

void Outer()
{
    int LocalFact(int x) => x <= 1 ? 1 : x * LocalFact(x - 1);
    Console.WriteLine(LocalFact(5));
}
```

---

## บทที่ 50: Method Overloading

```csharp
int Add(int a, int b) => a + b;
double Add(double a, double b) => a + b;
int Add(int a, int b, int c) => a + b + c;
```

**กฎ:** ต่างกันที่จำนวน, ชนิด, หรือลำดับพารามิเตอร์ (ห้ามต่างกันแค่ return type หรือ ref/out)

---

(จบบทที่ 36–50)

**โปรดแจ้ง "ต่อไป" เพื่อรับบทที่ 51–65 ในข้อความถัดไปครับ**