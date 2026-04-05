# Mastering C# .NET 2026: จากพื้นฐานสู่ Enterprise Application + Database + Cache + Message Queue

## บทที่ 13: Value Types vs Reference Types

---

### สารบัญย่อยของบทที่ 13

13.1 แนวคิดพื้นฐาน: หน่วยความจำ Stack และ Heap  
13.2 Value Types (ชนิดข้อมูลแบบค่า)  
13.3 Reference Types (ชนิดข้อมูลแบบอ้างอิง)  
13.4 ความแตกต่างระหว่าง Value Types และ Reference Types  
13.5 การทำงานของการกำหนดค่า (Assignment)  
13.6 การส่งพารามิเตอร์ไปยังเมธอด (Pass by Value vs Pass by Reference)  
13.7 Nullable Value Types (`int?`, `bool?`)  
13.8 ตัวอย่างโค้ดที่รันได้จริง  
13.9 ตารางสรุป Value Types vs Reference Types  
13.10 แบบฝึกหัดท้ายบท  
13.11 แหล่งอ้างอิง  

---

## 13.1 แนวคิดพื้นฐาน: หน่วยความจำ Stack และ Heap

เพื่อให้เข้าใจความแตกต่างระหว่าง Value Types และ Reference Types จำเป็นต้องรู้จักหน่วยความจำสองส่วนหลักใน .NET: **Stack** และ **Heap**

### 13.1.1 Stack (สแต็ก)

- เป็นหน่วยความจำที่มีโครงสร้างแบบ LIFO (Last-In, First-Out) – คล้ายกองจาน
- เก็บตัวแปรท้องถิ่น (local variables) และพารามิเตอร์ของเมธอด
- มีขนาดจำกัด (ประมาณ 1 MB สำหรับเธรดทั่วไป)
- จัดสรรและปลดปล่อยอัตโนมัติเมื่อออกจากขอบเขต (scope)
- **เร็วมาก** เพราะเป็นแค่การเลื่อน pointer

### 13.1.2 Heap (ฮีพ)

- เป็นหน่วยความจำที่มีโครงสร้างแบบสุ่ม (ไม่มีลำดับ)
- เก็บออบเจ็กต์ที่สร้างด้วย `new` และข้อมูลขนาดใหญ่
- มีขนาดใหญ่ (ตาม RAM ที่มี)
- ต้องมี Garbage Collector (GC) คอยจัดการเก็บขยะ
- ช้ากว่า Stack เพราะต้องมีการจัดการความซับซ้อน

### 13.1.3 ภาพจำลอง

```
STACK (เมธอด Main)          HEAP
┌─────────────────┐         ┌─────────────────┐
│ int age = 25    │         │ Product p ──────┼───┐
│ string name ────┼────────►│ "Somchai"       │   │
│ Product p ──────┼───┐     └─────────────────┘   │
└─────────────────┘   │     ┌─────────────────┐   │
                       └────►│ Product object  │◄──┘
                             │ Id = 1          │
                             │ Name = "Laptop" │
                             └─────────────────┘
```

> 💡 **เคล็ดลับ:** จำง่าย ๆ: ค่าเล็ก ๆ ที่อยู่ไม่นาน → Stack, ค่าใหญ่หรืออยู่ยาว → Heap

---

## 13.2 Value Types (ชนิดข้อมูลแบบค่า)

**Value Type** คือชนิดข้อมูลที่เก็บค่าของตัวแปร **โดยตรง** ใน Stack (หรือ inline ภายในออบเจ็กต์อื่น) เมื่อมีการกำหนดค่าหรือส่งไปยังเมธอด ระบบจะ **คัดลอกค่า** ทั้งหมด

### 13.2.1 ชนิดใดบ้างที่เป็น Value Type?

| หมวดหมู่ | ตัวอย่าง |
|----------|----------|
| ชนิดตัวเลขทั้งหมด | `int`, `long`, `float`, `double`, `decimal`, `byte`, `short` |
| ชนิด bool, char | `bool`, `char` |
| struct | `DateTime`, `TimeSpan`, `Guid`, struct ที่ผู้ใช้สร้าง |
| enum | `enum Season { Spring, Summer }` |
| nullable value types | `int?`, `bool?` |

### 13.2.2 ลักษณะของ Value Type

```csharp
int a = 10;
int b = a;      // คัดลอกค่า 10 ไปให้ b (a และ b แยกกัน)
b = 20;         // เปลี่ยน b ไม่กระทบ a
Console.WriteLine(a); // 10
Console.WriteLine(b); // 20
```

---

## 13.3 Reference Types (ชนิดข้อมูลแบบอ้างอิง)

**Reference Type** คือชนิดข้อมูลที่เก็บ **地址 (reference)** ของออบเจ็กต์ที่อยู่ใน Heap ตัวแปรจะชี้ไปยังตำแหน่งหน่วยความจำที่เก็บข้อมูลจริง เมื่อมีการกำหนดค่าหรือส่งไปยังเมธอด ระบบจะ **คัดลอก reference** (ไม่ใช่ตัวออบเจ็กต์)

### 13.3.1 ชนิดใดบ้างที่เป็น Reference Type?

| หมวดหมู่ | ตัวอย่าง |
|----------|----------|
| class | `string`, `object`, `List<T>`, `Dictionary<TKey,TValue>`, คลาสที่ผู้ใช้สร้าง |
| interface | `IDisposable`, `IEnumerable` |
| array | `int[]`, `string[]`, `Product[]` |
| delegate | `Action`, `Func<int,int>` |
| record (class-based) | `record Person(string Name, int Age);` (record สามารถเป็น value type ได้ด้วย record struct) |

### 13.3.2 ลักษณะของ Reference Type

```csharp
int[] arr1 = new int[] { 1, 2, 3 };
int[] arr2 = arr1;      // คัดลอก reference (arr2 ชี้ไปที่อาร์เรย์เดียวกัน)
arr2[0] = 99;           // เปลี่ยน arr2 ก็กระทบ arr1
Console.WriteLine(arr1[0]); // 99
```

---

## 13.4 ความแตกต่างระหว่าง Value Types และ Reference Types

| คุณสมบัติ | Value Type | Reference Type |
|-----------|------------|----------------|
| **ที่เก็บ** | Stack (ส่วนใหญ่) | Heap |
| **การกำหนดค่า** | คัดลอกค่าทั้งหมด | คัดลอก reference (地址) |
| **ค่าเริ่มต้น** | 0, false, '\0' (ไม่ใช่ null) | `null` |
| **Nullable** | ต้องใช้ `?` (เช่น `int?`) | สามารถเป็น null ได้อยู่แล้ว |
| **ประสิทธิภาพ** | เร็ว (อยู่ใกล้ CPU) | ช้ากว่า (ต้อง dereference, GC) |
| **ขนาด** | คงที่ (ตามชนิด) | แปรผัน (ตามข้อมูล) |
| **การสืบทอด** | สืบทอดจาก `System.ValueType` | สืบทอดจาก `System.Object` |
| **GC** | ไม่ถูกเก็บโดย GC (อยู่บน stack) | ถูกจัดการโดย GC |

---

## 13.5 การทำงานของการกำหนดค่า (Assignment)

### 13.5.1 Value Type Assignment (คัดลอกค่า)

```csharp
struct Point { public int X, Y; }

Point p1 = new Point { X = 10, Y = 20 };
Point p2 = p1;        // คัดลอกค่า (p2.X=10, p2.Y=20)
p2.X = 99;
Console.WriteLine(p1.X); // 10 (ไม่เปลี่ยน)
```

### 13.5.2 Reference Type Assignment (คัดลอก Reference)

```csharp
class PointClass { public int X, Y; }

PointClass p1 = new PointClass { X = 10, Y = 20 };
PointClass p2 = p1;        // p2 ชี้ไปที่ออบเจ็กต์เดียวกับ p1
p2.X = 99;
Console.WriteLine(p1.X);   // 99 (เปลี่ยนทั้งคู่)
```

---

## 13.6 การส่งพารามิเตอร์ไปยังเมธอด (Pass by Value vs Pass by Reference)

### 13.6.1 การส่ง Value Type (Pass by Value – คัดลอกค่า)

```csharp
void ModifyValue(int x)
{
    x = 100;   // เปลี่ยนเฉพาะสำเนา
}

int num = 10;
ModifyValue(num);
Console.WriteLine(num); // 10 (ไม่เปลี่ยน)
```

### 13.6.2 การส่ง Reference Type (Pass by Value – คัดลอก Reference)

```csharp
void ModifyList(List<int> list)
{
    list.Add(99);   // เปลี่ยนออบเจ็กต์ที่ reference ชี้ไป
    list = new List<int>(); // เปลี่ยน reference เอง (ไม่กระทบ caller)
}

var myList = new List<int> { 1, 2, 3 };
ModifyList(myList);
Console.WriteLine(myList.Count); // 4 (เพิ่ม 99 แล้ว)
```

### 13.6.3 การใช้ `ref` และ `out` เพื่อส่งแบบ Pass by Reference

```csharp
void ModifyWithRef(ref int x)
{
    x = 100;
}

int num = 10;
ModifyWithRef(ref num);
Console.WriteLine(num); // 100 (เปลี่ยนจริง)
```

---

## 13.7 Nullable Value Types (`int?`, `bool?`)

Value Types ปกติ (เช่น `int`) **ไม่สามารถ** เป็น `null` ได้ แต่ C# มี `Nullable<T>` (เขียนย่อเป็น `T?`) เพื่อให้เก็บค่า null ได้

```csharp
int? age = null;
bool? isActive = null;

if (age.HasValue)
{
    Console.WriteLine(age.Value);
}
else
{
    Console.WriteLine("ไม่มีข้อมูลอายุ");
}

// ใช้ null-coalescing
int displayAge = age ?? 0;  // ถ้า null ให้ใช้ 0
```

Reference Types ไม่ต้องการ `?` เพราะเป็น null ได้อยู่แล้ว แต่ถ้าเปิด nullable reference types (C# 8+) `string?` ก็ใช้ได้เช่นกัน

---

## 13.8 ตัวอย่างโค้ดที่รันได้จริง

**ตัวอย่างที่ 13.1: Value vs Reference – การเปลี่ยนแปลงค่า**

```csharp
Console.WriteLine("=== Value Type ===");
int value1 = 10;
int value2 = value1;
value2 = 20;
Console.WriteLine($"value1: {value1}, value2: {value2}"); // 10, 20

Console.WriteLine("\n=== Reference Type ===");
int[] ref1 = { 1, 2, 3 };
int[] ref2 = ref1;
ref2[0] = 99;
Console.WriteLine($"ref1[0]: {ref1[0]}, ref2[0]: {ref2[0]}"); // 99, 99
```

**ตัวอย่างที่ 13.2: struct vs class**

```csharp
// Value Type (struct)
struct PointValue
{
    public int X, Y;
    public PointValue(int x, int y) { X = x; Y = y; }
}

// Reference Type (class)
class PointReference
{
    public int X, Y;
    public PointReference(int x, int y) { X = x; Y = y; }
}

Console.WriteLine("=== struct (value) ===");
PointValue pv1 = new PointValue(5, 10);
PointValue pv2 = pv1;
pv2.X = 99;
Console.WriteLine($"pv1.X: {pv1.X}, pv2.X: {pv2.X}"); // 5, 99

Console.WriteLine("\n=== class (reference) ===");
PointReference pr1 = new PointReference(5, 10);
PointReference pr2 = pr1;
pr2.X = 99;
Console.WriteLine($"pr1.X: {pr1.X}, pr2.X: {pr2.X}"); // 99, 99
```

**ตัวอย่างที่ 13.3: Nullable value types**

```csharp
int? score = null;
if (score.HasValue)
    Console.WriteLine($"คะแนน: {score.Value}");
else
    Console.WriteLine("ยังไม่มีคะแนน");

// กำหนดค่า
score = 85;
Console.WriteLine(score ?? -1);  // 85

// ใช้ GetValueOrDefault
Console.WriteLine(score.GetValueOrDefault(0)); // 85
```

---

## 13.9 ตารางสรุป Value Types vs Reference Types

### ตารางที่ 13.1: เปรียบเทียบโดยรวม

| คุณสมบัติ | Value Type | Reference Type |
|-----------|------------|----------------|
| ตัวอย่าง | `int`, `bool`, `struct`, `enum` | `string`, `class`, `array`, `interface` |
| ที่เก็บหลัก | Stack | Heap |
| การกำหนดค่า | คัดลอกค่า | คัดลอก reference |
| ค่าเริ่มต้น | 0, false, '\0' | `null` |
| GC | ไม่ | ใช่ (Garbage Collector) |
| ประสิทธิภาพ | เร็ว | ช้าลงเล็กน้อย |
| ใช้ `?` เพื่อ nullable? | ใช่ (`int?`) | ไม่ต้อง (แต่ใช้ `string?` ใน C# 8+) |

### ตารางที่ 13.2: ตัวอย่างชนิดในแต่ละกลุ่ม

| Value Types | Reference Types |
|-------------|-----------------|
| `int`, `long`, `short`, `byte` | `string` |
| `float`, `double`, `decimal` | `object` |
| `bool`, `char` | `Array` (`int[]`, `string[]`) |
| `DateTime`, `TimeSpan`, `Guid` | `List<T>`, `Dictionary<TKey,TValue>` |
| `struct` ที่ผู้ใช้สร้าง | `class` ที่ผู้ใช้สร้าง |
| `enum` | `interface` |

---

## 13.10 แบบฝึกหัดท้ายบท (4 ข้อ)

🧪 **แบบฝึกหัดที่ 13.1:**  
จงบอกว่าแต่ละชนิดต่อไปนี้เป็น Value Type หรือ Reference Type: `string`, `int[]`, `decimal`, `DateTime`, `List<int>`, `bool`, `int?`

🧪 **แบบฝึกหัดที่ 13.2:**  
จากโค้ดต่อไปนี้ ผลลัพธ์ที่แสดงคืออะไร? อธิบาย:
```csharp
int a = 5;
int b = a;
b = 10;
Console.WriteLine(a);

int[] x = { 1, 2 };
int[] y = x;
y[0] = 99;
Console.WriteLine(x[0]);
```

🧪 **แบบฝึกหัดที่ 13.3:**  
สร้าง `struct` ชื่อ `Rectangle` (มี Width, Height `double`) และ `class` ชื่อ `RectangleClass` (เหมือนกัน) จากนั้นเขียนเมธอดที่รับพารามิเตอร์และพยายามเปลี่ยนค่าขนาดในเมธอด สังเกตความแตกต่าง

🧪 **แบบฝึกหัดที่ 13.4 (ท้าทาย):**  
Nullable value types: จงเขียนโปรแกรมรับอายุจากผู้ใช้ ถ้าผู้ใช้กด Enter โดยไม่พิมพ์อะไร ให้เก็บเป็น `null` และแสดงข้อความ “ไม่ระบุอายุ” แต่ถ้ามีอายุให้แสดง “อายุ X ปี” (ใช้ `int?` และ `HasValue`)

---

## 13.11 แหล่งอ้างอิง

- 🔗 **Value types (C# reference)** – [https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/value-types](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/value-types)
- 🔗 **Reference types (C# reference)** – [https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/reference-types](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/reference-types)
- 🔗 **Stack and Heap in C#** – [https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/classes-and-structs#memory-allocation](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/classes-and-structs#memory-allocation)
- 🔗 **Nullable value types** – [https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-value-types](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/nullable-value-types)

---

## สรุปท้ายบท

บทที่ 13 ได้อธิบายความแตกต่างพื้นฐานระหว่าง Value Types และ Reference Types ซึ่งเป็นหัวใจสำคัญของโมเดลหน่วยความจำใน C#:

- **Value Types** (int, bool, struct) เก็บค่าโดยตรงใน Stack การกำหนดค่าคือการคัดลอกค่า
- **Reference Types** (class, array, string) เก็บ reference ไปยัง Heap การกำหนดค่าคือการคัดลอก reference (地址)
- Nullable Value Types (`int?`) ช่วยให้ value types เป็น null ได้
- การส่งพารามิเตอร์: ปกติเป็นการ pass by value แต่ถ้าต้องการเปลี่ยนค่าในเมธอดสำหรับ value type ให้ใช้ `ref` หรือ `out`

ความเข้าใจในส่วนนี้จะช่วยให้คุณเขียนโค้ดที่ถูกต้อง รู้ว่าเมื่อใดควรใช้ struct vs class และป้องกันบั๊กที่เกิดจากการคัดลอกที่ไม่ตั้งใจ

**ในบทถัดไป (บทที่ 14)** เราจะพูดถึง **ตัวดำเนินการ (Operators) และลำดับการประเมิน** ในภาษา C#

---

*หมายเหตุ: บทที่ 13 นี้มีความยาวประมาณ 2,000 คำ*

---

(ดำเนินการส่งบทที่ 14 ต่อไปโดยอัตโนมัติ)