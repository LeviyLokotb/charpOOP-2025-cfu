using Gtk;
using static WindowTools;
class ExceptionMatrixWindow : baseWindow
{
    private Matrix A;
    private Matrix B;
    private Matrix C;
    private Grid matrixA;
    private Grid matrixB;
    private Grid matrixC;
    private Label warningsLabel = Label.New("--");

    public ExceptionMatrixWindow() : base()
    {
        Box scrollBox = new();
        ConfigureBox(scrollBox, Orientation.Vertical);
        scrollBox.Append(Label.New(""));
        ScrolledWindow scrolledWindow = new()
        {
            MinContentHeight = 600,
        };
        scrolledWindow.SetChild(scrollBox);
        mainBox.Append(scrolledWindow);

        // Создаём матрицы и их визуальные отображения
        A = new(2, 3, randomValues: true);
        matrixA = CreateMatrixGrid(A, 1);
        //Box boxA = new();
        //boxA.Append(matrixA);

        B = new(3, 2, randomValues: true);
        matrixB = CreateMatrixGrid(B, 3);
        //Box boxB = new();
        //boxB.Append(matrixB);

        //C = new(3, 3, randomValues: false);
        C = TryMultiply(A, B);
        matrixC = CreateMatrixGrid(C, 5);
        //Box boxC = new();
        //boxC.Append(matrixC);

        // Добавляем элементы на экран
        scrollBox.Append(matrixA);
        scrollBox.Append(Label.New(" x "));
        scrollBox.Append(matrixB);
        AddButton(" = ", scrollBox, (sender, e) =>
        {
            C = TryMultiply(A, B);
            matrixC = CreateMatrixGrid(C, 5);
            ReplaceChild(scrollBox, 5, matrixC);
        });
        scrollBox.Append(matrixC);

        // Настройки -- Матрица 1
        Box settings1 = new(){Homogeneous = true};
        ConfigureBox(settings1, Orientation.Horizontal);
        settings1.Append(Label.New("Матрица 1: "));
        // ▬ ▮ 
        AddButton("- ▮", settings1, (sender, e) =>
        {
            A.RemoveCol();
            matrixA = CreateMatrixGrid(A, 1);
            ReplaceChild(scrollBox, 1, matrixA);
        });
        
        AddButton("+ ▮", settings1, (sender, e) =>
        {
            A.AddCol(randomValues: true);
            matrixA = CreateMatrixGrid(A, 1);
            ReplaceChild(scrollBox, 1, matrixA);
        });

        AddButton("- ▬", settings1, (sender, e) =>
        {
            A.RemoveRow();
            matrixA = CreateMatrixGrid(A, 1);
            ReplaceChild(scrollBox, 1, matrixA);
        });

        AddButton("+ ▬", settings1, (sender, e) =>
        {
            A.AddRow(randomValues: true);
            matrixA = CreateMatrixGrid(A, 1);
            ReplaceChild(scrollBox, 1, matrixA);
        });

        // Настройки -- Матрица 2
        Box settings2 = new(){Homogeneous = true};
        ConfigureBox(settings2, Orientation.Horizontal);
        settings2.Append(Label.New("Матрица 2: "));
        AddButton("- ▮", settings2, (sender, e) =>
        {
            B.RemoveCol();
            matrixB = CreateMatrixGrid(B, 3);
            ReplaceChild(scrollBox, 3, matrixB);
        });
        
        AddButton("+ ▮", settings2, (sender, e) =>
        {
            B.AddCol(randomValues: true);
            matrixB = CreateMatrixGrid(B, 3);
            ReplaceChild(scrollBox, 3, matrixB);
        });

        AddButton("- ▬", settings2, (sender, e) =>
        {
            B.RemoveRow();
            matrixB = CreateMatrixGrid(B, 3);
            ReplaceChild(scrollBox, 3, matrixB);
        });

        AddButton("+ ▬", settings2, (sender, e) =>
        {
            B.AddRow(randomValues: true);
            matrixB = CreateMatrixGrid(B, 3);
            ReplaceChild(scrollBox, 3, matrixB);
        });

        mainBox.Append(settings1);
        mainBox.Append(settings2);

        ScrolledWindow scrolled = new();

        scrolled.Child = warningsLabel;
        mainBox.Append(scrolled);

        // Контрольная панель
        AddButton("? Пример", controlPanel, (sender, e) =>
        {
            var (Ar, Ac) = (A.Rows, A.Cols);
            A = new(Ar, Ac, randomValues: true);
            matrixA = CreateMatrixGrid(A, 1);
            ReplaceChild(scrollBox, 1, matrixA);

            var (Br, Bc) = (B.Rows, B.Cols);
            B = new(Br, Bc, randomValues: true);
            matrixB = CreateMatrixGrid(B, 3);
            ReplaceChild(scrollBox, 3, matrixB);

            C = TryMultiply(A, B);
            matrixC = CreateMatrixGrid(C, 5);
            ReplaceChild(scrollBox, 5, matrixC);
        });

        controlPanel.Append(Label.New("Умножение матриц:"));
        controlPanel.Append(closeButton);
    }

    public Matrix TryMultiply(Matrix a, Matrix b)
    {
        Matrix c;
        try
        {
            c = Matrix.MatrixMultipy(a, b);
            warningsLabel.SetText("--");
        }
        catch (Exception e)
        {
            c = new Matrix(3, 3, randomValues: false);
            string prev = warningsLabel.GetText();
            warningsLabel.SetText($"{e.Message}\n");
        }
        return c;
    }

    public Grid CreateMatrixGrid(Matrix M, int index)
    {
        // Grid -- контейнер-сетка
        Grid grid = new();
        grid.Halign = Align.Center;
        // Создаём ячейки
        foreach (var (i, j, value) in M)
        {
            // Текстовое поле с валидацией
            TextView cell = AddDisplay();
            EventControllerKey controller = new();

            cell.SetSizeRequest(100, 60);
            cell.Buffer!.Text = $"{value}";

            controller.OnKeyReleased += (sender, e) =>
            {
                if (double.TryParse(cell.Buffer.Text ?? "0", out double val))
                {
                    Matrix N = index switch
                    {
                        1 => A,
                        3 => B,
                        5 => C,
                        _ => null!,
                    };
                    N.SetElement(i, j, val);
                    warningsLabel.SetText($"--");
                }
                else
                {
                    string sM = index switch { 1 => "A", 3 => "B", 5 => "C", _ => "--"};
                    warningsLabel.SetText($"An invalid value in matrix {sM} cell [{i}][{j}]: {cell.Buffer.Text}");
                }
                //Console.WriteLine($"[{i} : {j}] = {val}");
            };
            controller.OnKeyPressed += CreateValidationController(cell, "-1234567890,");

            cell.AddController(controller);

            // Рамка
            Frame frame = new()
            {
                Child = cell,
                MarginBottom = 2,
                MarginTop = 2,
                MarginStart = 2,
                MarginEnd = 2,
            };

            // Добавляем всё в grid
            grid.Attach(frame, j, i, 1, 1);
        }
        return grid;
    }
}