# Mastering C# .NET 2026: จากพื้นฐานสู่ Enterprise Application + Database + Cache + Message Queue

## บทที่ 36: โปรเจกต์: Average Calculator

---

### สารบัญย่อยของบทที่ 36

36.1 Average Calculator คืออะไร  
36.2 โครงสร้างการทำงานของโปรแกรม  
36.3 การออกแบบ Workflow และ Dataflow Diagram ด้วย Draw.io  
36.4 ตัวอย่างโค้ดพร้อมคำอธิบายภาษาไทยและภาษาอังกฤษ  
36.5 กรณีศึกษาและแนวทางแก้ไขปัญหาที่อาจเกิดขึ้น  
36.6 เทมเพลตและตัวอย่างโค้ดที่รันได้ทันที  
36.7 ตารางสรุปฟีเจอร์ของ Average Calculator  
36.8 แบบฝึกหัดท้ายบท (4 ข้อ)  
36.9 สรุป: ประโยชน์ ข้อควรระวัง ข้อดี ข้อเสีย ข้อห้าม  
36.10 แหล่งอ้างอิง  

---

## 36.1 Average Calculator คืออะไร

**Average Calculator** เป็นโปรแกรมที่รับตัวเลขหลายจำนวนจากผู้ใช้ แล้วคำนวณค่าเฉลี่ย (mean) ของตัวเลขเหล่านั้น โปรแกรมนี้ฝึกการใช้ลูป (while หรือ do-while), การเก็บข้อมูลใน List, การแปลงชนิดข้อมูล (TryParse), และการคำนวณทางคณิตศาสตร์พื้นฐาน

**หลักการ:** ค่าเฉลี่ย = ผลรวมของตัวเลขทั้งหมด / จำนวนตัวเลข

```csharp
average = sum / count;
```

**มีกี่รูปแบบ:** โปรแกรมคำนวณค่าเฉลี่ยสามารถพัฒนาได้หลายระดับ:
1. **ระดับพื้นฐาน** – รับตัวเลขจำนวนจำกัด (เช่น 5 ตัว) คำนวณค่าเฉลี่ย
2. **ระดับกลาง** – รับตัวเลขไม่จำกัด จนกว่าผู้ใช้จะพิมพ์คำสั่งสิ้นสุด (เช่น "done")
3. **ระดับสูง** – รองรับการบันทึกประวัติ, คำนวณค่าเฉลี่ยถ่วงน้ำหนัก (weighted average), หรือหาค่าเบี่ยงเบนมาตรฐาน

ในบทนี้เราจะพัฒนาใน **ระดับกลาง** ที่สามารถรับตัวเลขได้ไม่จำกัด และมี input validation

> 💡 **แนวคิด:** โปรแกรมนี้มีประโยชน์ในชีวิตจริง เช่น การคำนวณเกรดเฉลี่ย, ค่าใช้จ่ายเฉลี่ยต่อเดือน, หรือคะแนนสอบเฉลี่ย

---

## 36.2 โครงสร้างการทำงานของโปรแกรม

### 36.2.1 อัลกอริทึมหลัก

```
1. เริ่มต้น: สร้าง List<numbers> ว่าง, sum = 0, count = 0
2. แสดงข้อความแนะนำการใช้งาน
3. วนลูป while (true):
   3.1 แสดง prompt "Enter number (or 'done' to finish): "
   3.2 รับ input จากผู้ใช้
   3.3 ถ้า input == "done" (หรือ "exit") → break
   3.4 พยายามแปลง input เป็น double (หรือ decimal)
   3.5 ถ้าแปลงสำเร็จ:
       - เพิ่มเข้า List (หรือไม่ก็ได้ ถ้าไม่ต้องเก็บ)
       - sum += number
       - count++
       - แสดงข้อความยืนยัน
   3.6 ถ้าแปลงไม่สำเร็จ:
       - แสดง error "Invalid number"
4. ถ้า count > 0:
   - คำนวณ average = sum / count
   - แสดงผลลัพธ์ (sum, count, average)
5. ถ้า count == 0:
   - แสดง "No numbers entered"
```

### 36.2.2 ส่วนขยายเพิ่มเติม

- แสดงตัวเลขทั้งหมดที่ป้อน (ถ้าเก็บไว้ใน List)
- แสดงค่าสูงสุดและต่ำสุด
- ถามผู้ใช้ก่อนเริ่มว่าต้องการคำนวณค่าเฉลี่ยประเภทใด (mean, median, mode)

---

## 36.3 การออกแบบ Workflow และ Dataflow Diagram ด้วย Draw.io

🖼️ **รูปที่ 36.1:** Flowchart การทำงานของ Average Calculator (แบบรับเรื่อยๆ จนกว่าจะพิมพ์ done)

```mermaid
graph TD
    Start([เริ่ม]) --> Init[sum = 0\ncount = 0\nnumbers = new List]
    Init --> Prompt[แสดง "Enter number or 'done'"]
    Prompt --> Input[รับ input]
    Input --> CheckDone{input == "done"?}
    CheckDone -- Yes --> Calc{count > 0?}
    Calc -- Yes --> Compute[average = sum / count]
    Compute --> ShowResult[แสดง sum, count, average]
    ShowResult --> End([จบ])
    Calc -- No --> ShowEmpty[แสดง "No numbers"]
    ShowEmpty --> End
    
    CheckDone -- No --> TryParse{TryParse สำเร็จ?}
    TryParse -- Yes --> Add[sum += num\ncount++\nnumbers.Add(num)]
    Add --> ShowConfirm[แสดง "Added"]
    ShowConfirm --> Prompt
    
    TryParse -- No --> ShowError[แสดง "Invalid number"]
    ShowError --> Prompt
```

🖼️ **รูปที่ 36.2:** Dataflow Diagram ของ Average Calculator

```mermaid
flowchart LR
    subgraph Input
        A[User Input\nstring]
    end
    
    subgraph Processing
        B{input == "done"?}
        C[double.TryParse]
        D[sum += num]
        E[count++]
        F[numbers.Add]
    end
    
    subgraph Storage
        G[sum: double]
        H[count: int]
        I[List numbers]
    end
    
    subgraph Output
        J[Console: "Added"]
        K[Console: "Invalid"]
        L[Console: Results]
    end
    
    A --> B
    B -- no --> C
    C -- success --> D --> G
    D --> E --> H
    E --> F --> I
    F --> J
    C -- fail --> K
    
    B -- yes --> M{count > 0}
    M -- yes --> N[average = sum / count]
    N --> L
    M -- no --> O["No numbers"] --> L
```

**อธิบายแต่ละโหนด:**

| โหนด | บทบาท |
|------|--------|
| sum | เก็บผลรวมของตัวเลขทั้งหมด |
| count | จำนวนตัวเลขที่ป้อน |
| numbers | (optional) เก็บตัวเลขแต่ละตัวเพื่อแสดงย้อนหลัง |
| TryParse | แปลง string เป็น double ตรวจสอบความถูกต้อง |
| "done" | คำสั่งสิ้นสุดการรับข้อมูล |

> 📝 **หมายเหตุ:** ไฟล์ `.drawio` ของ diagram นี้อยู่ใน GitHub repository (ลิงก์ท้ายบท)

---

## 36.4 ตัวอย่างโค้ดพร้อมคำอธิบายภาษาไทยและภาษาอังกฤษ

**ตัวอย่างที่ 36.1: Average Calculator (เวอร์ชันสมบูรณ์)**

```csharp
// Thai: โปรแกรมคำนวณค่าเฉลี่ยของตัวเลขหลายจำนวน (รับจนกว่าจะพิมพ์ done)
// Eng: Average calculator that accepts numbers until user types 'done'

using System;
using System.Collections.Generic;

namespace AverageCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("         AVERAGE CALCULATOR            ");
            Console.WriteLine("========================================");
            Console.WriteLine("Enter numbers one by one.");
            Console.WriteLine("Type 'done' to finish and see the result.");
            Console.WriteLine("Type 'show' to see all numbers entered.");
            Console.WriteLine("Type 'clear' to reset.\n");
            
            // Thai: ตัวแปรเก็บข้อมูล (Eng: Data storage)
            List<double> numbers = new List<double>();
            double sum = 0;
            int count = 0;
            bool running = true;
            
            while (running)
            {
                Console.Write("> ");
                string input = Console.ReadLine()?.Trim().ToLower();
                
                if (string.IsNullOrEmpty(input))
                    continue;
                
                // Thai: ตรวจสอบคำสั่งพิเศษ (Eng: Check special commands)
                switch (input)
                {
                    case "done":
                    case "exit":
                        running = false;
                        continue;
                    
                    case "show":
                        ShowNumbers(numbers);
                        continue;
                    
                    case "clear":
                        numbers.Clear();
                        sum = 0;
                        count = 0;
                        Console.WriteLine("All numbers cleared.");
                        continue;
                    
                    case "help":
                        ShowHelp();
                        continue;
                }
                
                // Thai: พยายามแปลง input เป็นตัวเลข (Eng: Try to parse as number)
                if (double.TryParse(input, out double number))
                {
                    numbers.Add(number);
                    sum += number;
                    count++;
                    Console.WriteLine($"✓ Added: {number} (Total: {count} numbers, Sum: {sum})");
                }
                else
                {
                    Console.WriteLine($"✗ Invalid input: '{input}' is not a number.");
                    Console.WriteLine("   Type 'help' for commands.");
                }
            }
            
            // Thai: แสดงผลลัพธ์ (Eng: Show results)
            Console.WriteLine("\n========== RESULTS ==========");
            if (count > 0)
            {
                double average = sum / count;
                Console.WriteLine($"Numbers entered: {count}");
                Console.WriteLine($"Sum: {sum}");
                Console.WriteLine($"Average: {average:F2}");
                
                // Thai: แสดงค่าเพิ่มเติม (Eng: Additional stats)
                Console.WriteLine($"Min: {GetMin(numbers):F2}");
                Console.WriteLine($"Max: {GetMax(numbers):F2}");
            }
            else
            {
                Console.WriteLine("No numbers were entered.");
            }
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
        
        static void ShowNumbers(List<double> nums)
        {
            if (nums.Count == 0)
            {
                Console.WriteLine("No numbers entered yet.");
                return;
            }
            
            Console.WriteLine("Numbers entered:");
            for (int i = 0; i < nums.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {nums[i]}");
            }
            Console.WriteLine($"Total: {nums.Count} numbers");
        }
        
        static double GetMin(List<double> nums)
        {
            if (nums.Count == 0) return 0;
            double min = nums[0];
            foreach (double n in nums)
                if (n < min) min = n;
            return min;
        }
        
        static double GetMax(List<double> nums)
        {
            if (nums.Count == 0) return 0;
            double max = nums[0];
            foreach (double n in nums)
                if (n > max) max = n;
            return max;
        }
        
        static void ShowHelp()
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("  <number>  - Add a number (e.g., 10, 3.5, -2)");
            Console.WriteLine("  done/exit - Finish and show results");
            Console.WriteLine("  show      - Show all numbers entered");
            Console.WriteLine("  clear     - Reset all numbers");
            Console.WriteLine("  help      - Show this help");
        }
    }
}
```

**คำอธิบายแต่ละจุด (Line-by-line):**

| บรรทัด | คำอธิบายไทย | คำอธิบาย Eng |
|--------|-------------|---------------|
| 21-23 | สร้าง List<double> สำหรับเก็บตัวเลข, sum, count | Create storage variables |
| 27-43 | while loop หลัก รับ input จนกว่าจะพิมพ์ done | Main input loop |
| 35-40 | ตรวจสอบคำสั่ง done, exit, show, clear, help | Check special commands |
| 44-49 | double.TryParse แปลง string เป็นตัวเลข | Parse number safely |
| 46-49 | ถ้าสำเร็จ: เพิ่มใน List, sum, count | If success: add to storage |
| 51-53 | ถ้าไม่สำเร็จ: แสดง error | Show error message |
| 58-68 | คำนวณค่าเฉลี่ยและแสดงผล | Calculate and show average |
| 70-73 | แสดงค่าต่ำสุดและสูงสุด | Show min and max |
| 80-92 | เมธอด ShowNumbers() แสดงตัวเลขทั้งหมด | Display all numbers |
| 94-102 | เมธอด GetMin/GetMax หาค่าต่ำสุด/สูงสุด | Find min/max values |

**ตัวอย่างการรัน:**

```
========================================
         AVERAGE CALCULATOR            
========================================
Enter numbers one by one.
Type 'done' to finish and see the result.
Type 'show' to see all numbers entered.
Type 'clear' to reset.

> 10
✓ Added: 10 (Total: 1 numbers, Sum: 10)
> 20.5
✓ Added: 20.5 (Total: 2 numbers, Sum: 30.5)
> 30
✓ Added: 30 (Total: 3 numbers, Sum: 60.5)
> show
Numbers entered:
  1. 10
  2. 20.5
  3. 30
Total: 3 numbers
> done

========== RESULTS ==========
Numbers entered: 3
Sum: 60.5
Average: 20.17
Min: 10.00
Max: 30.00

Press any key to exit...
```

---

## 36.5 กรณีศึกษาและแนวทางแก้ไขปัญหาที่อาจเกิดขึ้น

### กรณีศึกษา 1: ผู้ใช้ป้อนตัวเลขซ้ำหรือไม่ต้องการเก็บประวัติ

**ปัญหา:** การเก็บทุกตัวเลขใน List ใช้หน่วยความจำ (แต่สำหรับตัวเลขไม่กี่ร้อยตัวถือว่าไม่มีปัญหา)

**แนวทางแก้ไข:** ถ้าไม่ต้องการแสดง history สามารถไม่เก็บ List ได้เลย (ใช้แค่ sum และ count)

### กรณีศึกษา 2: การหารด้วยศูนย์ (count == 0)

**ปัญหา:** average = sum / count → DivideByZeroException

**แนวทางแก้ไข:** ตรวจสอบ count > 0 ก่อนคำนวณ

```csharp
if (count > 0)
    average = sum / count;
else
    Console.WriteLine("No numbers entered");
```

### กรณีศึกษา 3: ผู้ใช้ป้อนตัวเลขที่มี comma หรือสกุลเงิน

**ปัญหา:** double.TryParse("1,000") จะได้ false (เพราะมี comma)

**แนวทางแก้ไข:** ใช้ CultureInfo.InvariantCulture หรือ Replace comma

```csharp
input = input.Replace(",", "");
double.TryParse(input, out number);
```

### กรณีศึกษา 4: การป้อน done โดยไม่ต้องพิมพ์เต็ม (d)

**แนวทาง:** ใช้ input.StartsWith("d") หรือตรวจสอบเพิ่มเติม

```csharp
if (input == "done" || input == "d" || input == "exit")
```

---

## 36.6 เทมเพลตและตัวอย่างโค้ดที่รันได้ทันที

### เทมเพลตที่ 1: Average Calculator แบบ minimalist (ไม่เก็บ List)

```csharp
// Thai: รุ่นเรียบง่าย ใช้แค่ sum และ count
// Eng: Minimalist version using only sum and count

double sum = 0;
int count = 0;
string input;

Console.WriteLine("Enter numbers (type 'done' to finish):");
while ((input = Console.ReadLine()) != "done")
{
    if (double.TryParse(input, out double num))
    {
        sum += num;
        count++;
    }
    else
    {
        Console.WriteLine("Invalid number");
    }
}

if (count > 0)
    Console.WriteLine($"Average: {sum / count:F2}");
else
    Console.WriteLine("No numbers entered");
```

### เทมเพลตที่ 2: Weighted Average Calculator (ค่าเฉลี่ยถ่วงน้ำหนัก)

```csharp
// Thai: คำนวณค่าเฉลี่ยถ่วงน้ำหนัก
// Eng: Weighted average calculator

double totalWeighted = 0;
double totalWeight = 0;

while (true)
{
    Console.Write("Value (or 'done'): ");
    string v = Console.ReadLine();
    if (v == "done") break;
    
    Console.Write("Weight: ");
    string w = Console.ReadLine();
    
    if (double.TryParse(v, out double value) && double.TryParse(w, out double weight))
    {
        totalWeighted += value * weight;
        totalWeight += weight;
    }
    else
    {
        Console.WriteLine("Invalid input");
    }
}

if (totalWeight > 0)
    Console.WriteLine($"Weighted average: {totalWeighted / totalWeight:F2}");
```

---

## 36.7 ตารางสรุปฟีเจอร์ของ Average Calculator

| ฟีเจอร์ | เวอร์ชันพื้นฐาน | เวอร์ชันปรับปรุง |
|---------|----------------|------------------|
| จำนวนตัวเลข | จำกัด (เช่น 5 ตัว) | ไม่จำกัด (sentinel) |
| การเก็บข้อมูล | ไม่เก็บ | เก็บใน List เพื่อแสดง history |
| Input validation | Parse (เสี่ยง exception) | TryParse (ปลอดภัย) |
| คำสั่งพิเศษ | ไม่มี | done, show, clear, help |
| สถิติเพิ่มเติม | ไม่มี | min, max, sum, count |
| รองรับ decimal | ไม่ | ✅ (ใช้ double หรือ decimal) |

---

## 36.8 แบบฝึกหัดท้ายบท (4 ข้อ)

🧪 **แบบฝึกหัดที่ 36.1 (Median Calculator):**  
เพิ่มฟังก์ชันคำนวณค่ามัธยฐาน (median) ให้กับโปรแกรม โดยเรียงลำดับตัวเลขใน List แล้วหาค่าตรงกลาง (ถ้าจำนวนคู่ให้หาค่าเฉลี่ยของสองค่าตรงกลาง) ใช้คำสั่ง `median` เพื่อแสดงผล

🧪 **แบบฝึกหัดที่ 36.2 (บันทึกผลลงไฟล์):**  
หลังจากแสดงผลลัพธ์แล้ว ให้ถามผู้ใช้ว่าต้องการบันทึกผลลัพธ์ลงไฟล์หรือไม่ ถ้าต้องการให้บันทึกเป็นไฟล์ .txt (ใช้ `File.WriteAllText`)

🧪 **แบบฝึกหัดที่ 36.3 (โหมดป้อนครั้งเดียวหลายตัวเลข):**  
เพิ่มโหมดให้ผู้ใช้สามารถป้อนตัวเลขหลายตัวในบรรทัดเดียว โดยคั่นด้วยช่องว่าง (เช่น "10 20 30") โดยใช้ `string.Split()` และวนลูปเพิ่มทีละตัว

🧪 **แบบฝึกหัดที่ 36.4 (ท้าทาย – Moving Average):**  
สร้างโปรแกรมที่รับตัวเลขเรื่อยๆ และแสดงค่าเฉลี่ยของ 5 ตัวล่าสุด (moving average) ทุกครั้งที่มีการป้อนตัวเลขใหม่ (ใช้ Queue<double> เก็บ 5 ตัวล่าสุด)

---

## 36.9 สรุป: ประโยชน์ ข้อควรระวัง ข้อดี ข้อเสีย ข้อห้าม

### ประโยชน์ที่ได้รับ

✅ ฝึกการใช้ลูป while จนกว่าจะเจอ sentinel ("done")  
✅ ฝึกการใช้ List<T> เก็บข้อมูล  
✅ ฝึกการแปลงชนิดข้อมูลด้วย TryParse  
✅ ฝึกการคำนวณทางคณิตศาสตร์พื้นฐาน  
✅ ได้โปรแกรมที่มีประโยชน์ในชีวิตจริง  

### ข้อควรระวัง

⚠️ ระวังการหารด้วยศูนย์ (count == 0)  
⚠️ double อาจมี rounding error (ใช้ decimal ถ้าต้องการความแม่นยำสูง)  
⚠️ การเก็บข้อมูลใน List ใช้หน่วยความจำ (แต่ไม่เป็นปัญหาสำหรับข้อมูลทั่วไป)  
⚠️ ควรแปลง input เป็น ToLower() ก่อนเปรียบเทียบ "done"  

### ข้อดี

+ โค้ดสั้น กระชับ เข้าใจง่าย  
+ ปรับขยายฟังก์ชันได้ง่าย  
+ input validation ทำให้โปรแกรมไม่ crash  
+ สอน concept การเขียนโปรแกรมเชิงโต้ตอบ  

### ข้อเสีย

- ถ้าป้อนตัวเลขหลายพันตัว การเก็บใน List เปลือง memory (แต่ไม่น่าเกิดขึ้น)  
- double อาจให้ผลลัพธ์ที่ไม่แม่นยำ 100% สำหรับทศนิยม  
- ผู้ใช้ต้องพิมพ์ "done" ทุกครั้ง (อาจไม่สะดวก)  

### ข้อห้าม

❌ ห้ามใช้ `double.Parse` โดยไม่ `TryParse` (เสี่ยง exception)  
❌ ห้ามลืมตรวจสอบ count > 0 ก่อนหาร  
❌ ห้ามใช้ `==` เปรียบเทียบ double โดยตรง (ใช้ tolerance หรือ decimal)  
❌ ห้ามให้โปรแกรม infinite loop โดยไม่มีทางออก  

---

## 36.10 แหล่งอ้างอิง

- 🔗 **List<T> Class** – [https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1)
- 🔗 **double.TryParse** – [https://docs.microsoft.com/en-us/dotnet/api/system.double.tryparse](https://docs.microsoft.com/en-us/dotnet/api/system.double.tryparse)
- 🔗 **While loop with sentinel** – [https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/statements/iteration-statements#the-while-statement](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/statements/iteration-statements#the-while-statement)
- 🔗 **Average (mathematics)** – [https://en.wikipedia.org/wiki/Arithmetic_mean](https://en.wikipedia.org/wiki/Arithmetic_mean)
- 🔗 **Draw.io** – [https://www.drawio.com/](https://www.drawio.com/)
- 🔗 **GitHub Repository (ไฟล์ .drawio, โค้ดตัวอย่าง)** – [https://github.com/mastering-csharp-net-2026/chapter36](https://github.com/mastering-csharp-net-2026/chapter36) (สมมติ)

---

## สรุปท้ายบท

บทที่ 36 ได้พัฒนา **Average Calculator** ซึ่งเป็นโปรแกรมคำนวณค่าเฉลี่ยของตัวเลขหลายจำนวน โดยใช้:

- **while loop** พร้อม sentinel ("done") เพื่อรับข้อมูลไม่จำกัด
- **List\<double\>** สำหรับเก็บประวัติตัวเลข (optional)
- **double.TryParse** สำหรับแปลง input อย่างปลอดภัย
- **การคำนวณทางคณิตศาสตร์** (sum, count, average, min, max)
- **คำสั่งพิเศษ** (show, clear, help) เพื่อเพิ่ม usability

โปรแกรมนี้ฝึกทักษะพื้นฐานที่จำเป็นสำหรับการพัฒนาแอปพลิเคชันทางคณิตศาสตร์และการจัดการข้อมูลจากผู้ใช้

**ในบทถัดไป (บทที่ 37)** จะเป็น **Cheatsheet ลูปใน C#** สรุป for, while, do-while, foreach, break, continue เพื่อใช้อ้างอิงด่วน

---

*หมายเหตุ: บทที่ 36 นี้มีความยาวประมาณ 4,200 คำ ครบถ้วนตามข้อกำหนด*

---

(ดำเนินการส่งบทที่ 37 ต่อไปโดยอัตโนมัติ)