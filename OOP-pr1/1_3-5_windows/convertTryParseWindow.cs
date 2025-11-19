using Gtk;
public class convertTryParseWindow : templateWindow
{
    public convertTryParseWindow() : base()
    {
        controlBox.Append(Label.New("Convert, Parse, TryParse для преобразования строки в число"));
        // Заголовок
        var MyTitle = new Box()
        {
            Homogeneous = true
        };
        MyTitle.SetMarginTop(5);
        MyTitle.SetMarginBottom(5);
        MyTitle.SetMarginStart(5);
        MyTitle.SetMarginEnd(5);
        MyTitle.SetSpacing(20);
        controlBox.Append(MyTitle);

        // Кнопка свеедний
        var helpButton = new Button();
        helpButton.SetLabel("?  Сведения");
        helpButton.OnClicked += (sender, e) =>
        {
            Log("""
            =========================================
            TryParse - принимает out параметр 
            и записывает значение в него. 
            Если не вышло запишет null.
            Возвращает bool (вышло ли преобразование?) 
            Пример: 
             isOK = Int32.TryParse("123", out value); 
             // value == 123, isOk == true
            """);
            Log("""
            =========================================
            Parse - возвращает преобразованное значение.
            В случае неудачи кинет исключение 
            (FormatException, OverflowException, 
            ArgumentNullException) 
            Пример:
             try
             {
                 value = Int32.Parse("Hello"); // Исключение
             }
             catch
             {
                 ...
             }
            """);
            Log("""
            =========================================
            Convert - класс содержащий множество 
            преобразований. Возможности Convert 
            обычно шире чем у других преобразований.
            В случае неудачи кинет исключение 
            (FormatException, OverflowException) 
            Пример:
             try
             {
                 value = Int32.Parse("Hello"); // Исключение
             }
             catch
             {
                 ...
             }
            """);
        };
        MyTitle.Append(helpButton);

        // Кнопка очистки
        MyTitle.Append(clearButton);

        // Кнопка закрытия
        MyTitle.Append(closeButton);


        // Поле ввода
        controlBox.Append(Label.New("Введите строковый литреал для конвертации:"));
        TextView entry = new TextView()
        {
            Monospace = true,
            MarginStart = 10,
            MarginEnd = 10,
            HeightRequest = 40,
            LeftMargin = 10,
            RightMargin = 10,
            TopMargin = 10,
        };
        SetFocus(entry);
        controlBox.Append(entry);
        TextBuffer entry_buff = entry.Buffer!;

        // Кнопка
        var convButton = new Button();
        convButton.SetLabel("Конвертировать");
        convButton.OnClicked += (sender, e) =>
        {
            Log("========================================");
            string text = entry_buff.Text ?? "";
            Int32 i32;
            Double d;
            Decimal m;

            // Convert
            Log("// Convert");
            try
            {
                i32 = Convert.ToInt32(text);
                Log($"Результат Convert.ToInt32:\n {i32}");
            }
            catch (Exception err)
            {
                Log($"Преобразование Convert.ToInt32 не удалось.\nИсключение: {err.Message}");
            }

            try
            {
                d = Convert.ToDouble(text);
                Log($"Результат Convert.ToDouble:\n {d}");
            }
            catch (Exception err)
            {
                Log($"Преобразование Convert.ToDouble не удалось.\nИсключение: {err.Message}");
            }

            try
            {
                m = Convert.ToDecimal(text);
                Log($"Результат Convert.ToDecimal:\n {m}");
            }
            catch (Exception err)
            {
                Log($"Преобразование Convert.ToDecimal не удалось.\nИсключение: {err.Message}");
            }

            // Parse
            Log("// Parse");
            try
            {
                i32 = Int32.Parse(text);
                Log($"Результат Int32.Parse:\n {i32}");
            }
            catch (Exception err)
            {
                Log($"Преобразование Int32.Parse не удалось.\nИсключение: {err.Message}");
            }

            try
            {
                d = Double.Parse(text);
                Log($"Результат Double.Parse:\n {d}");
            }
            catch (Exception err)
            {
                Log($"Преобразование Double.Parse не удалось.\nИсключение: {err.Message}");
            }

            try
            {
                m = Decimal.Parse(text);
                Log($"Результат Decimal.Parse:\n {m}");
            }
            catch (Exception err)
            {
                Log($"Преобразование Decimal.Parse не удалось.\nИсключение: {err.Message}");
            }


            // TryParse
            Log("// TryParse");
            if (Int32.TryParse(text, out i32)) Log($"Результат Int32.TryParse:\n {i32}");
            else Log($"Преобразование Int32.TryParse не удалось.");

            if (Double.TryParse(text, out d)) Log($"Результат Double.TryParse:\n {d}");
            else Log($"Преобразование Double.TryParse не удалось.");

            if (Decimal.TryParse(text, out m)) Log($"Результат Decimal.TryParse:\n {m}");
            else Log($"Преобразование Decimal.TryParse не удалось.");
            Log("========================================");
        };
        controlBox.Append(convButton);
    }
}