# ASCII Donut in C#

This project is a C# console application that renders an animated rotating 3D donut (torus) using ASCII characters. It utilizes mathematical formulas to project 3D points onto a 2D plane and updates the console to animate the donut.

![2024-11-1809-14-39-ezgif com-resize](https://github.com/user-attachments/assets/2a959222-e980-40ba-b534-4f6b2636cdd5)

## Table of Contents

- [Introduction](#introduction)
- [How It Works](#how-it-works)
- [Code Explanation](#code-explanation)
  - [Variables Initialization](#variables-initialization)
  - [Main Loop](#main-loop)
  - [Inner Loop Calculations](#inner-loop-calculations)
  - [Rendering the Frame](#rendering-the-frame)
  - [Updating Rotation Angles](#updating-rotation-angles)
- [Running the Program](#running-the-program)
- [Mathematical Background](#mathematical-background)
- [Visual Explanation](#visual-explanation)
- [License](#license)

## Introduction

This program demonstrates how to create a simple 3D animation in the console by rendering a rotating donut using ASCII characters. It involves 3D math, projections, and an understanding of how to manipulate the console output.

## How It Works

The program calculates the 3D coordinates of points on a torus (donut shape), applies rotation transformations, projects them onto a 2D plane, calculates luminance for shading, and updates the console buffer to display the animation.

## Code Explanation

Here's the full code:

```csharp
float A = 0, B = 0;
double i, j;
float[] z = new float[1760];
char[] b = new char[1760];

Console.WriteLine("\x1b[2J");

for (; ; )
{
    Array.Fill(b, ' ');
    Array.Fill(z, 0);

    for (j = 0; j < 6.28; j += 0.07)
    {
        for (i = 0; i < 6.28; i += 0.02)
        {
            float c = (float)Math.Sin(i);
            float d = (float)Math.Cos(j);
            float e = (float)Math.Sin(A);
            float f = (float)Math.Sin(j);
            float g = (float)Math.Cos(A);
            float h = d + 2;
            float D = 1f / (c * h * e + f * g + 5);
            float l = (float)Math.Cos(i);
            float m = (float)Math.Cos(B);
            float n = (float)Math.Sin(B);
            float t = c * h * g - f * e;

            int x = (int)(40 + 30 * D * (l * h * m - t * n));
            int y = (int)(12 + 15 * D * (l * h * n + t * m));
            int o = x + 80 * y;

            int N = (int)(8 * ((f * e - c * d * g) * m - c * d * e - f * g - l * d * n));

            if (22 > y && y > 0 && x > 0 && 80 > x && D > z[o])
            {
                z[o] = D;
                b[o] = ".,-~:;=!*#$@"[Math.Max(0, Math.Min(11, N))];
            }
        }
    }

    Console.SetCursorPosition(0, 0);

    for (int k = 0; k < 1760; k++)
    {
        Console.Write(k % 80 != 0 ? b[k] : '\n');
    }

    A += 0.04f;
    B += 0.02f;

    System.Threading.Thread.Sleep(30);
}
```

### Variables Initialization

- `float A = 0, B = 0;`  
  These variables represent the rotation angles of the donut around the X-axis (`A`) and Z-axis (`B`).

- `double i, j;`  
  Loop variables used for iterating over the torus surface.

- `float[] z = new float[1760];`  
  Depth buffer (`z-buffer`) to handle occlusion. Initialized to zero.

- `char[] b = new char[1760];`  
  Screen buffer to store ASCII characters for each frame.

- `Console.WriteLine("\x1b[2J");`  
  Clears the console screen.

### Main Loop

The infinite loop `for (; ; )` runs the animation continuously.

```csharp
for (; ; )
{
    // Clear buffers
    Array.Fill(b, ' ');
    Array.Fill(z, 0);

    // Rest of the code...
}
```

- `Array.Fill(b, ' ');`  
  Fills the screen buffer with spaces.

- `Array.Fill(z, 0);`  
  Resets the depth buffer.

### Inner Loop Calculations

The nested loops iterate over the angles `j` and `i` to calculate points on the torus surface.

```csharp
for (j = 0; j < 6.28; j += 0.07)
{
    for (i = 0; i < 6.28; i += 0.02)
    {
        // Calculations...
    }
}
```

- **Calculating Sines and Cosines:**

  ```csharp
  float c = (float)Math.Sin(i);
  float d = (float)Math.Cos(j);
  float e = (float)Math.Sin(A);
  float f = (float)Math.Sin(j);
  float g = (float)Math.Cos(A);
  float l = (float)Math.Cos(i);
  float m = (float)Math.Cos(B);
  float n = (float)Math.Sin(B);
  ```

- **Intermediate Variables:**

  - `h = d + 2;`  
    A helper variable used in the calculation of `x`, `y`, and `D`.

  - `D = 1f / (c * h * e + f * g + 5);`  
    Calculates the depth factor (`ooz`), which is used for scaling and depth buffering.

  - `t = c * h * g - f * e;`  
    Another helper variable for coordinate transformations.

- **Calculating Screen Coordinates:**

  ```csharp
  int x = (int)(40 + 30 * D * (l * h * m - t * n));
  int y = (int)(12 + 15 * D * (l * h * n + t * m));
  int o = x + 80 * y;
  ```

  - `x` and `y` are the projected 2D screen coordinates.
  - `o` is the index in the buffer arrays corresponding to the `(x, y)` position.

- **Calculating Luminance (`N`):**

  ```csharp
  int N = (int)(8 * ((f * e - c * d * g) * m - c * d * e - f * g - l * d * n));
  ```

  - `N` determines the luminance of the point, which is used to select an ASCII character for shading.

### Rendering the Frame

- **Updating Buffers:**

  ```csharp
  if (22 > y && y > 0 && x > 0 && 80 > x && D > z[o])
  {
      z[o] = D;
      b[o] = ".,-~:;=!*#$@"[Math.Max(0, Math.Min(11, N))];
  }
  ```

  - Checks if the point is within the screen boundaries.
  - Updates the depth buffer `z[o]` and the screen buffer `b[o]` if the current point is closer to the viewer.

- **Displaying the Frame:**

  ```csharp
  Console.SetCursorPosition(0, 0);

  for (int k = 0; k < 1760; k++)
  {
      Console.Write(k % 80 != 0 ? b[k] : '\n');
  }
  ```

  - Resets the cursor to the top-left corner.
  - Writes the contents of the screen buffer to the console.

### Updating Rotation Angles

```csharp
A += 0.04f;
B += 0.02f;
```

- Increment the rotation angles to animate the donut's rotation over time.

### Frame Rate Control

```csharp
System.Threading.Thread.Sleep(30);
```

- Pauses the loop for 30 milliseconds to control the frame rate of the animation.

## Running the Program

1. **Set Up the Environment:**
   - Ensure you have .NET installed on your system.
   - Use an IDE like Visual Studio or Visual Studio Code.

2. **Create a Console App:**
   - Create a new C# Console Application project.

3. **Replace the Code:**
   - Copy and paste the provided code into your `Program.cs` file.

4. **Run the Application:**
   - Build and run the project.
   - You should see the animated ASCII donut in the console window.

## Mathematical Background

The program simulates a 3D torus by calculating the coordinates of points on its surface and projecting them onto a 2D plane.

### Torus Parametric Equations

A torus can be represented parametrically with two angles, `i` (theta) and `j` (phi):

- **3D Coordinates:**

  ```plaintext
  x = (R2 + R1 * cos(i)) * cos(j)
  y = (R2 + R1 * cos(i)) * sin(j)
  z = R1 * sin(i)
  ```

  - `R1` is the radius of the tube.
  - `R2` is the distance from the center of the tube to the center of the torus.

### Rotation Transformations

- The torus is rotated around the X-axis and Z-axis using rotation matrices.
- The rotations are controlled by angles `A` and `B`.

### Projection onto 2D Plane

- The 3D coordinates are projected onto a 2D plane using perspective projection.
- Depth (`D`) is calculated to simulate the effect of perspective and handle occlusion.

### Luminance Calculation

- The luminance `N` is calculated based on the surface normal and a light source direction.
- Different ASCII characters represent different levels of brightness.

## Visual

![OIP](https://github.com/user-attachments/assets/71064265-d4f1-48bc-9add-f8b5df2ddd22)
