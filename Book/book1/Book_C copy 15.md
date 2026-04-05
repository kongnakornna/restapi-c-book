# Mastering C# .NET 2026: จากพื้นฐานสู่ Enterprise Application + Database + Cache + Message Queue

## บทที่ 15: การดีบักเบื้องต้น (Breakpoints, Runtime vs Compile Error)

---

### สารบัญย่อยของบทที่ 15

15.1 ประเภทของข้อผิดพลาดในโปรแกรม  
15.2 Compile Error (Syntax Error) – ข้อผิดพลาดตอนคอมไพล์  
15.3 Runtime Error – ข้อผิดพลาดตอนรันโปรแกรม  
15.4 Logic Error – ข้อผิดพลาดทางตรรกะ (โปรแกรมรันได้แต่ผลไม่ถูกต้อง)  
15.5 การดีบัก (Debugging) คืออะไร  
15.6 Breakpoints – การหยุดโปรแกรมเพื่อตรวจสอบ  
15.7 การใช้ Step Over, Step Into, Step Out  
15.8 การตรวจสอบค่าตัวแปรด้วย Watch, Immediate Window  
15.9 การแก้ไขข้อผิดพลาดทั่วไป  
15.10 ตัวอย่างโค้ดที่มีข้อผิดพลาดและวิธีดีบัก  
15.11 ตารางสรุปประเภทข้อผิดพลาด  
15.12 แบบฝึกหัดท้ายบท  
15.13 แหล่งอ้างอิง  

---

## 15.1 ประเภทของข้อผิดพลาดในโปรแกรม

ข้อผิดพลาด (errors) ในโปรแกรม C# แบ่งเป็น 3 ประเภทหลัก:

| ประเภท | เกิดขึ้นเมื่อ | ตัวอย่าง | ใครพบ |
|--------|--------------|----------|--------|
| **Compile Error** | ขณะคอมไพล์โค้ด | ลืม semicolon, ใช้ตัวแปรไม่ถูกต้อง | นักพัฒนา |
| **Runtime Error** | ขณะรันโปรแกรม | หารด้วยศูนย์, อ่านไฟล์不存在 | ผู้ใช้ |
| **Logic Error** | โปรแกรมรันได้ แต่ผลลัพธ์ผิด | คำนวณผิดสูตร | ผู้ใช้ (และนักพัฒนาตรวจพบทีหลัง) |

> 💡 **เคล็ดลับ:** การดีบัก (debugging) คือกระบวนการค้นหาและแก้ไขข้อผิดพลาด โดยเฉพาะ Runtime Error และ Logic Error

---

## 15.2 Compile Error (Syntax Error) – ข้อผิดพลาดตอนคอมไพล์

เกิดจากโค้ดไม่เป็นไปตามไวยากรณ์ของภาษา C# คอมไพลเลอร์จะไม่ยอมสร้างไฟล์ .exe/.dll จนกว่าจะแก้ไข

**ตัวอย่างทั่วไป:**

```csharp
// 1. ลืม semicolon
int x = 5   // Error: ; expected

// 2. ใช้ตัวแปรที่ยังไม่ประกาศ
y = 10;     // Error: The name 'y' does not exist

// 3. วงเล็บไม่สมดุล
if (x > 0   // Error: ) expected

// 4. ใช้ keyword เป็นชื่อตัวแปร
int class = 5;  // Error: 'class' is a keyword

// 5. ชนิดข้อมูลไม่ตรง
string num = 100;  // Error: cannot implicitly convert int to string
```

**วิธีแก้ไข:** อ่าน error message ในหน้าต่าง Error List (Visual Studio) หรือ terminal (dotnet build) แล้วแก้ตามที่บอก

---

## 15.3 Runtime Error – ข้อผิดพลาดตอนรันโปรแกรม

โปรแกรมคอมไพล์ผ่าน แต่เมื่อรันแล้วเกิด exception (ข้อยกเว้น) ทำให้โปรแกรมหยุดทำงานทันที

**ตัวอย่างทั่วไป:**

```csharp
// 1. หารด้วยศูนย์
int a = 10, b = 0;
int c = a / b;  // DivideByZeroException

// 2. อ้างอิง null
string s = null;
int len = s.Length;  // NullReferenceException

// 3. แปลงข้อมูลผิดรูปแบบ
string input = "abc";
int num = int.Parse(input);  // FormatException

// 4. Index นอกช่วงอาร์เรย์
int[] arr = {1, 2, 3};
int x = arr[5];  // IndexOutOfRangeException
```

**วิธีแก้ไข:** ใช้ debugger เพื่อหาบรรทัดที่เกิด exception และตรวจสอบค่าตัวแปร

---

## 15.4 Logic Error – ข้อผิดพลาดทางตรรกะ

โปรแกรมรันได้ ไม่มี exception แต่ผลลัพธ์ไม่ถูกต้องตามที่ต้องการ พบยากที่สุดเพราะคอมพิวเตอร์ทำตามที่เราสั่ง (แต่เราสั่งผิด)

**ตัวอย่าง:**

```csharp
// ต้องการหาค่าเฉลี่ย 3 ตัวเลข แต่ลืมหาร
int sum = 10 + 20 + 30;
int average = sum;  // ควรเป็น sum / 3

// เงื่อนไขผิดพลาด
if (score = 100)  // ใช้ = แทน == ทำให้เป็น true เสมอ (แต่ compiler เตือนบ้าง)
{
    Console.WriteLine("Perfect");
}
```

**วิธีแก้ไข:** ใช้ debugger หยุดโปรแกรมทีละขั้น ตรวจสอบค่าตัวแปรว่าตรงกับที่คาดหวังหรือไม่

---

## 15.5 การดีบัก (Debugging) คืออะไร

**ดีบัก** คือกระบวนการรันโปรแกรมแบบ step-by-step เพื่อสังเกตพฤติกรรมและค่าตัวแปร ช่วยให้พบว่า logic error เกิดขึ้นที่จุดใด

เครื่องมือดีบักใน Visual Studio และ VS Code มีความสามารถ:
- **Breakpoint** – หยุดโปรแกรมที่บรรทัดที่กำหนด
- **Step Over (F10)** – รันทีละบรรทัด (ไม่เข้าไปในเมธอด)
- **Step Into (F11)** – รันและเข้าไปในเมธอดที่เรียก
- **Step Out (Shift+F11)** – ออกจากเมธอดปัจจุบัน
- **Watch** – ดูค่าตัวแปร
- **Immediate Window** – ประเมินนิพจน์ขณะดีบัก

---

## 15.6 Breakpoints – การหยุดโปรแกรมเพื่อตรวจสอบ

**Breakpoint** คือจุดหยุดชั่วคราว เมื่อโปรแกรมรันถึงบรรทัดนี้ จะหยุดและให้เราตรวจสอบค่าตัวแปร

### 15.6.1 การตั้ง Breakpoint

- **Visual Studio:** คลิกที่ margin ซ้ายของบรรทัด (หรือกด F9)
- **VS Code:** คลิกที่ margin ซ้าย (หรือกด F9)

### 15.6.2 ตัวอย่างการใช้งาน

```csharp
int sum = 0;
for (int i = 1; i <= 5; i++)
{
    sum += i;   // ตั้ง breakpoint ที่บรรทัดนี้
}
Console.WriteLine(sum);
```

เมื่อรันดีบัก (F5) โปรแกรมจะหยุดทุกครั้งที่เข้า for loop ดูค่า i และ sum ได้

---

## 15.7 การใช้ Step Over, Step Into, Step Out

สมมติมีโค้ด:

```csharp
static int Add(int a, int b) => a + b;

static void Main()
{
    int x = 10;
    int y = 20;
    int result = Add(x, y);  // บรรทัดนี้
    Console.WriteLine(result);
}
```

| การทำงาน | คำสั่ง | พฤติกรรม |
|-----------|-------|-----------|
| Step Over (F10) | ข้ามการเข้าไปในเมธอด | จะรัน Add ทั้งเมธอดแล้วหยุดที่บรรทัดถัดไป |
| Step Into (F11) | เข้าไปในเมธอด | จะหยุดที่บรรทัดแรกของเมธอด Add |
| Step Out (Shift+F11) | ออกจากเมธอดปัจจุบัน | รันที่เหลือของเมธอดแล้วกลับมา |

> 💡 **Step Into** มีประโยชน์เมื่อต้องการตรวจสอบว่าเมธอดทำงานถูกต้องหรือไม่

---

## 15.8 การตรวจสอบค่าตัวแปรด้วย Watch, Immediate Window

### 15.8.1 Watch Window

- ขณะดีบัก ให้คลิกขวาที่ตัวแปร → Add Watch
- หรือเปิด Watch window (Debug → Windows → Watch)
- สามารถใส่ expression เช่น `i * 2`, `sum > 100`

### 15.8.2 Immediate Window

- เปิด Immediate Window (Debug → Windows → Immediate)
- ขณะดีบัก พิมพ์นิพจน์เพื่อประเมินค่า เช่น `? sum` หรือ `? Add(5,3)`
- สามารถเปลี่ยนค่าตัวแปรได้: `sum = 100`

### 15.8.3 DataTip

- แค่เอาเมาส์ไปชี้ที่ตัวแปรระหว่างดีบัก จะเห็นค่าปัจจุบัน

---

## 15.9 การแก้ไขข้อผิดพลาดทั่วไป

### 15.9.1 NullReferenceException

**สาเหตุ:** เรียกใช้สมาชิกของตัวแปรที่เป็น null

**ตัวอย่าง:**
```csharp
string name = null;
int len = name.Length;  // NullReferenceException
```

**วิธีแก้ไข:** ตรวจสอบก่อนใช้งาน
```csharp
if (name != null)
{
    int len = name.Length;
}
// หรือใช้ null-conditional operator
int? len = name?.Length;
```

### 15.9.2 IndexOutOfRangeException

**สาเหตุ:** เข้าถึง index ที่เกินขนาดอาร์เรย์หรือ List

**วิธีแก้ไข:** ตรวจสอบ Length/Count ก่อน
```csharp
if (index >= 0 && index < array.Length)
{
    var value = array[index];
}
```

### 15.9.3 FormatException

**สาเหตุ:** แปลง string เป็นตัวเลขแต่รูปแบบไม่ถูกต้อง

**วิธีแก้ไข:** ใช้ `TryParse` แทน `Parse`
```csharp
if (int.TryParse(input, out int number))
{
    // ใช้ number ได้
}
```

---

## 15.10 ตัวอย่างโค้ดที่มีข้อผิดพลาดและวิธีดีบัก

**ตัวอย่างที่ 15.1: โปรแกรมรวมตัวเลขที่มี logic error**

```csharp
// โปรแกรมควรหาผลรวมของเลข 1 ถึง N แต่ผลลัพธ์ผิด
Console.Write("ป้อน N: ");
int n = int.Parse(Console.ReadLine());
int sum = 0;
for (int i = 1; i <= n; i--);  // ผิด: i-- แทน i++, และมี semicolon เกิน
{
    sum += i;
}
Console.WriteLine($"ผลรวม 1 ถึง {n} = {sum}");
```

**การดีบัก:**
1. ตั้ง breakpoint ที่ `int sum = 0;`
2. กด F5 (Start Debugging)
3. ป้อน N = 5
4. สังเกตว่า i ลดลง (1, 0, -1, ...) และ loop ไม่สิ้นสุด → พบว่าใช้ `i--` ผิด
5. แก้เป็น `i++` และลบ semicolon หลัง for

**ตัวอย่างที่ 15.2: Runtime Error – การหารด้วยศูนย์**

```csharp
Console.Write("ป้อนตัวเลข: ");
string input = Console.ReadLine();
int divisor = int.Parse(input);
int result = 100 / divisor;  // ถ้าผู้ใช้ป้อน 0 จะเกิด DivideByZeroException
Console.WriteLine(result);
```

**การดีบัก:**
- ตั้ง breakpoint ที่ `int result = ...`
- ดูค่า divisor ก่อน执行
- แก้ไขโดยเพิ่มการตรวจสอบ:
```csharp
if (divisor != 0)
    result = 100 / divisor;
else
    Console.WriteLine("ไม่สามารถหารด้วยศูนย์ได้");
```

**ตัวอย่างที่ 15.3: การใช้ Immediate Window แก้ไขค่าขณะดีบัก**

```csharp
int CalculateDiscount(int price, int percent)
{
    return price * percent / 100;
}

// Main
int total = 1500;
int discountPercent = 15;
int discount = CalculateDiscount(total, discountPercent);
int finalPrice = total - discount;
Console.WriteLine(finalPrice);
```

หาก discountPercent ควรเป็น 20 แต่เผลอใส่ 15:
- ตั้ง breakpoint ที่ `int discount = ...`
- เปิด Immediate Window พิมพ์ `discountPercent = 20`
- กด continue (F5) โปรแกรมจะใช้ค่า 20 ที่เปลี่ยน

---

## 15.11 ตารางสรุปประเภทข้อผิดพลาด

| ประเภท | เกิดเมื่อ | ตัวอย่าง | การตรวจจับ |
|--------|----------|----------|-------------|
| Compile Error | คอมไพล์ | `int x = "hello"` | compiler แจ้งทันที |
| Runtime Error | รันโปรแกรม | `int.Parse("abc")` | exception |
| Logic Error | รันโปรแกรม (ผลลัพธ์ผิด) | `avg = sum` แทน `sum/3` | ต้องตรวจสอบด้วย debugger |

### ตาราง快捷键ดีบักใน Visual Studio

| คำสั่ง | แป้น | คำอธิบาย |
|--------|------|-----------|
| Start Debugging | F5 | เริ่มดีบัก |
| Stop Debugging | Shift+F5 | หยุดดีบัก |
| Toggle Breakpoint | F9 | ตั้ง/ลบ breakpoint |
| Step Over | F10 | รันทีละบรรทัด (ไม่เข้าเมธอด) |
| Step Into | F11 | รันและเข้าเมธอด |
| Step Out | Shift+F11 | ออกจากเมธอด |
| Continue | F5 (ขณะหยุด) | รันต่อจนถึง breakpoint ถัดไป |

---

## 15.12 แบบฝึกหัดท้ายบท (4 ข้อ)

🧪 **แบบฝึกหัดที่ 15.1:**  
จงบอกประเภทข้อผิดพลาด (Compile/Runtime/Logic) ของแต่ละกรณี:
ก) `Console.WriteLine("Hello")` ลืม semicolon  
ข) หาค่าเฉลี่ยแต่ลืมหาร  
ค) อ่านไฟล์ที่ไม่มีอยู่จริง  
ง) ใช้ตัวแปร `count` โดยไม่ประกาศ  

🧪 **แบบฝึกหัดที่ 15.2:**  
เขียนโปรแกรมที่มี logic error (เช่น คำนวณพื้นที่สี่เหลี่ยมผิดสูตร) จากนั้นใช้ debugger (F10, F11) เพื่อหาจุดผิดและแก้ไข

🧪 **แบบฝึกหัดที่ 15.3:**  
จากโค้ดต่อไปนี้ มีข้อผิดพลาดอะไรบ้าง (อาจมากกว่า 1) จงแก้ไข:
```csharp
int[] numbers = { 10, 20, 30 };
int sum = 0;
for (int i = 0; i <= numbers.Length; i++)
{
    sum += numbers[i];
}
double average = sum / numbers.Length;
Console.WriteLine(average);
```

🧪 **แบบฝึกหัดที่ 15.4 (ท้าทาย):**  
สร้างโปรแกรมที่รับอาร์เรย์ของตัวเลขจากผู้ใช้ (คั่นด้วยช่องว่าง) แล้วหาค่าเฉลี่ย จงใช้ debugger ตรวจสอบกรณีผู้ใช้ป้อนข้อมูลไม่ถูกต้อง (เช่น มีตัวอักษร) และเพิ่มการป้องกันด้วย `TryParse`

---

## 15.13 แหล่งอ้างอิง

- 🔗 **Debugging in Visual Studio** – [https://docs.microsoft.com/en-us/visualstudio/debugger/](https://docs.microsoft.com/en-us/visualstudio/debugger/)
- 🔗 **First look at the debugger** – [https://docs.microsoft.com/en-us/visualstudio/debugger/debugger-feature-tour](https://docs.microsoft.com/en-us/visualstudio/debugger/debugger-feature-tour)
- 🔗 **Breakpoints in Visual Studio** – [https://docs.microsoft.com/en-us/visualstudio/debugger/using-breakpoints](https://docs.microsoft.com/en-us/visualstudio/debugger/using-breakpoints)
- 🔗 **Debugging in VS Code** – [https://code.visualstudio.com/docs/editor/debugging](https://code.visualstudio.com/docs/editor/debugging)

---

## สรุปท้ายบท

บทที่ 15 ได้แนะนำการดีบักเบื้องต้น ซึ่งเป็นทักษะสำคัญสำหรับนักพัฒนา:

- **Compile Error** – ตรวจพบตอนคอมไพล์ แก้ตาม error message
- **Runtime Error** – เกิด exception ขณะรัน ใช้ breakpoint และดู call stack
- **Logic Error** – ผลลัพธ์ผิดแต่โปรแกรมไม่ break ต้องใช้ step-by-step และ watch

คุณได้เรียนรู้การใช้ breakpoint, Step Over/Into/Out, Watch, Immediate Window เพื่อตรวจสอบและแก้ไขโค้ด

**ในบทถัดไป (บทที่ 16)** เราจะลงมือทำโปรเจกต์แรก: **เครื่องคิดเลขบวกเลข** เพื่อนำความรู้เกี่ยวกับตัวแปร การรับข้อมูล และการดีบักมาประยุกต์ใช้

---

*หมายเหตุ: บทที่ 15 นี้มีความยาวประมาณ 1,900 คำ*

---

(ดำเนินการส่งบทที่ 16 ต่อไปโดยอัตโนมัติ)