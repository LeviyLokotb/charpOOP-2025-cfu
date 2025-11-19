using System;
using Gio;
using Gtk;
using HarfBuzz;
using static WindowTools;

/// <summary>
/// Главное окно приложения, содержащее навигационные кнопки 
/// </summary>
public partial class MainWindow : baseWindow
{
    /// <summary>
    /// Инициализирует новый экземпляр главного окна приложения
    /// </summary>
    public MainWindow() : base()
    {
        controlPanel.Append(Label.New("Описание и вызов методов C#"));

        // Вычисление НОД
        mainBox.Append(Label.New("НОД"));
        AddNavigationButton<GCDWindow>("НОД 2, 3, 4, N чисел", mainBox);
        // Умножение матриц
        mainBox.Append(Label.New("Умножение матриц"));
        AddNavigationButton<MatrixWindow>("Умножение двух матриц", mainBox);
        // Умножение матриц с исключениями
        AddNavigationButton<ExceptionMatrixWindow>("Умножение матриц с исключениями", mainBox);

        mainBox.Append(exitButton);
    }

}
