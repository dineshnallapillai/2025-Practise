# Time and Space Complexity Examples

---

## Example 1: Constant Time – O(1)

Regardless of input size, execution takes the same amount of time.

```csharp
void PrintFirstElement(int[] arr)
{
    Console.WriteLine(arr[0]); // Always executes once
}
```

✅ Performance remains the same for any array size.

---

## Example 2: Linear Time – O(n)

Time grows linearly with input size.

```csharp
void PrintAllElements(int[] arr)
{
    foreach (int num in arr)
    {
        Console.WriteLine(num); // Runs n times
    }
}
```

✅ For n = 1000, it runs 1000 times.

---

## Example 3: Logarithmic Time – O(log n)

Each step reduces the problem size by a factor.

```csharp
int BinarySearch(int[] arr, int target)
{
    int left = 0, right = arr.Length - 1;
    while (left <= right)
    {
        int mid = left + (right - left) / 2;
        if (arr[mid] == target) return mid;
        if (arr[mid] < target) left = mid + 1;
        else right = mid - 1;
    }
    return -1;
}
```

✅ Halves the search space each step. Works efficiently for large datasets.

---

## Example 4: Quadratic Time – O(n²)

Nested loops cause O(n²) complexity.

```csharp
void PrintPairs(int[] arr)
{
    for (int i = 0; i < arr.Length; i++)
    {
        for (int j = 0; j < arr.Length; j++)
        {
            Console.WriteLine($"({arr[i]}, {arr[j]})");
        }
    }
}
```

✅ If n = 10, it runs 100 times (10²). Not scalable for large inputs.

---

## Example 5: Exponential Time – O(2ⁿ)

A recursive function that grows exponentially.

```csharp
int Fibonacci(int n)
{
    if (n <= 1) return n;
    return Fibonacci(n - 1) + Fibonacci(n - 2);
}
```

✅ If n = 40, it requires ≈ 2⁴⁰ computations—very slow!

---

## Analyzing Complexities in Code

When analyzing code:

- **Loops** → Linear **O(n)**
- **Nested Loops** → Quadratic **O(n²)**
- **Dividing Problem in Half** → Logarithmic **O(log n)**
- **Recursion without Memoization** → Exponential **O(2ⁿ)**
- **Efficient Sorting (Merge Sort, Quick Sort avg case)** → **O(n log n)**

---

## Space Complexity

Space complexity measures the memory used by an algorithm.

| Space Complexity | Example                    |
|------------------|----------------------------|
| O(1)             | Using a few variables      |
| O(n)             | Storing results in an array|
| O(n²)            | Creating a 2D matrix       |

```csharp
void StoreResults(int n)
{
    int[] arr = new int[n]; // O(n) space
}
```

---

## Practical Performance Tips

✅ Prefer **O(log n)** or **O(n)** solutions for large data  
✅ Avoid **O(n²)** and worse in high-performance applications  
✅ Use memoization in recursive algorithms to improve efficiency
