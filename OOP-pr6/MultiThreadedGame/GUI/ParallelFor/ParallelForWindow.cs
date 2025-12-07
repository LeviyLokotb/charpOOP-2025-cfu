
using System.Diagnostics;
using Gtk;
using Mandelbrot;

public class ParallelForWindow : baseWindow
{
    byte[]? pixels;
    Picture image;
    int ImageWidth = 0;
    int ImageHeigth = 0;
    Stopwatch sw = new Stopwatch();
    Label TimeLabel = Label.New("Time: --");
    public ParallelForWindow() : base()
    {
        // Widgets
        Button defaultButton = WindowTools.AddButton("⏳ Обычный for", async (sender, e) =>
        {
            await GenerateFractalDefault();
        });
        
        Button parallelButton = WindowTools.AddButton("⚡ Parallel.For", async (sender, e) =>
        {
            await GenerateFractalParallel();
        });

        Button clearButton = WindowTools.AddButton("🧼 Clear", async (sender, e) =>
        {
            InitComponents();
        });

        image = new Picture();

        Frame ImageFrame = new();
        ImageFrame.Child = image;

        Box buttonBox = WindowTools.AddPrettyBox(Orientation.Horizontal, homogeneus: true);
        buttonBox.Append(defaultButton);
        buttonBox.Append(parallelButton);
        buttonBox.Append(clearButton);

        // mainBox
        mainBox.Append(Label.New("Генерация множества Мандельброта"));
        mainBox.Append(buttonBox);
        mainBox.Append(TimeLabel);
        mainBox.Append(ImageFrame);

        // controlPanel
        controlPanel.Append(Label.New("♻ Сравнение for и ParallelFor"));
        controlPanel.Append(closeButton);

        //
        InitComponents();
    }

    private void InitComponents()
    {
        ImageWidth = 900;
        ImageHeigth = 900;

        image.SetSizeRequest(ImageWidth, ImageHeigth);
        int size = ImageWidth * ImageHeigth * 3;
        pixels = new byte[size];
        Array.Fill( pixels, (byte)0);

        _ = UpdateImage();

        TimeLabel.SetText("Time: --");
    }

    private (byte r, byte g, byte b) GetColor(MandelbrotPoint p)
    {
        if (p.iters >= 100)
        {
            return (255, 255, 255);
        }

        // Цветовая палитра
        double t = (double)p.iters / p.maxIters;
        
        byte r = (byte)(9 * (1 - t) * t * t * t * 255);
        byte g = (byte)(15 * (1 - t) * (1 - t) * t * t * 255);
        byte b = (byte)(8.5 * (1 - t) * (1 - t) * (1 - t) * t * 255);
        
        return (r, g, b);
    }
    private async Task GenerateFractalDefault()
    {
        sw.Restart();
        for (int y=0; y<ImageHeigth; y++)
        {
            for(int x=0; x<ImageWidth; x++)
            {
                // Нужно привести x и y к подходящему интервалу
                // x : [-2 ; 1]
                double x_ = ((double)x / ImageWidth) * 3 - 2.2;
                // y : [-1; 1]
                double y_ = ((double)y / ImageHeigth) * 3 - 1.5;

                // Получаем точку
                MandelbrotPoint p = MandelbrotFractal.NextPoint(x_, y_);
                SetPixel(x, y, GetColor(p));
            }
            if (y % 10 == 0)
            {
                await UpdateImage();
            }
        }
        sw.Stop();
        TimeLabel.SetText($"Time: {sw.ElapsedMilliseconds} ms (default)");

        await UpdateImage();
        Console.WriteLine("✅ Done");
        //var arr = pixels?.Where(p => p!=0).Select((i, p) => i).ToArray();
        //foreach (var e in arr) Console.WriteLine(e);
    }

    private async Task GenerateFractalParallel()
    {   
        sw.Restart();
        Parallel.For(0, ImageHeigth, async y =>
        {
            Parallel.For(0, ImageWidth, x => 
            {
                // Нужно привести x и y к подходящему интервалу
                // x : [-2 ; 1]
                double x_ = ((double)x / ImageWidth) * 3 - 2.2;
                // y : [-1; 1]
                double y_ = ((double)y / ImageHeigth) * 3 - 1.5;

                // Получаем точку
                MandelbrotPoint p = MandelbrotFractal.NextPoint(x_, y_);
                SetPixel(x, y, GetColor(p));
            });
            if (y % 10 == 0)
            {
                await UpdateImage();
            }
        });

        sw.Stop();
        TimeLabel.SetText($"Time: {sw.ElapsedMilliseconds} ms (parallel)");

        await UpdateImage();
        Console.WriteLine("✅ Done");
        //var arr = pixels?.Where(p => p!=0).Select((i, p) => i).ToArray();
        //foreach (var e in arr) Console.WriteLine(e);
    }

    private void SetPixel(double x, double y, (byte, byte, byte) rgb)
    {
        if (pixels == null) return;
        //if (rgb != (0, 0, 0)) Console.WriteLine(rgb);
        int channels = 3; // число каналов, 3 для RGB 
        int rowstride = ImageWidth * channels; // Длина строки в байтовом представлении изображения

        // Позиция тройки байт пикселя 
        int offset = (int)(y * rowstride + x * channels);
        if (offset + 2 < pixels.Length && offset >= 0)
        {
            var (r, g, b) = rgb;
            pixels[offset] = r;
            pixels[offset+1] = g;
            pixels[offset+2] = b;
        }
    }

    private async Task UpdateImage()
    {
        var bytes = GLib.Bytes.New(pixels);

        GLib.Functions.IdleAdd(0, () =>
        {
            var stride = (nuint)(ImageWidth*3);
            var texture = Gdk.MemoryTexture.New(ImageWidth, ImageHeigth, Gdk.MemoryFormat.R8g8b8, bytes, stride);
            image.Paintable = texture;

            return false;
        });
        await Task.Delay(1);
    }
}