public class SafeFailure
{
    /// <summary>
    /// Объект класса, предоставляющего разные этапы
    /// отключения реактора и соответствующие исключения
    /// </summary>
    private Switch switch_ = new();
    Action<string> Print;
    /// <summary>Класс отвечающий за эмуляцию остановки реактора</summary>
    /// <param name="Print">Метод для вывода информации</param>
    public SafeFailure(Action<string> Print) => this.Print = Print;
    public void PerformShutdown()
    {
        Print("=== НАЧАЛО ПРОЦЕДУРЫ ОТКЛЮЧЕНИЯ РЕАКТОРА ===");
        
        // Шаг 1: Отключение от генератора питания
        try
        {
            Print("Шаг 1 - отключение от генератора питания...");
            var result = switch_.DisconnectPowerGenerator();
            Print($"> {result}\n");
        }
        catch (PowerGeneratorCommsException e)
        {
            Print($"[x] Исключение на шаге 1: {e.Message}\n");
        }
        
        // Шаг 2: Проверка системы первичного охлаждения
        try
        {
            Print("Шаг 2 - проверка системы первичного охлаждения...");
            var status = switch_.VerifyPrimaryCoolantSystem();
            Print($"> {status}\n");
        }
        catch (CoolantTemperatureReadException e)
        {
            Print($"[x] Исключение на шаге 2: {e.Message}\n");
        }
        catch (CoolantPressureReadException e)
        {
            Print($"[x] Исключение на шаге 2: {e.Message}\n");
        }
        
        // Шаг 3: Проверка системы резервного охлаждения
        try
        {
            Print("Шаг 3 - проверка системы резервного охлаждения...");
            var status = switch_.VerifyBackupCoolantSystem();
            Print($"> {status}\n");
        }
        catch (CoolantTemperatureReadException e)
        {
            Print($"[x] Исключение на шаге 3: {e.Message}\n");
        }
        catch (CoolantPressureReadException e)
        {
            Print($"[x] Исключение на шаге 3: {e.Message}\n");
        }
        
        // Шаг 4: Запись температуры активной зоны до отключения
        try
        {
            Print("Шаг 4 - запись температуры активной зоны до отключения...");
            var temperature = switch_.GetCoreTemperature();
            Print($"> {temperature:F2}°C\n");
        }
        catch (CoreTemperatureReadException e)
        {
            Print($"[x] Исключение на шаге 4: {e.Message}\n");
        }
        
        // Шаг 5: Введение регулирующих стержней в реактор
        try
        {
            Print("Шаг 5 - введение регулирующих стержней в реактор...");
            var result = switch_.InsertRodCluster();
            Print($"> {result}\n");
        }
        catch (RodClusterReleaseException e)
        {
            Print($"[x] Исключение на шаге 5: {e.Message}\n");
        }
        
        // Шаг 6: Запись температуры активной зоны после отключения
        try
        {
            Print("Шаг 6 - запись температуры активной зоны после отключения...");
            var temperature = switch_.GetCoreTemperature();
            Print($"> {temperature:F2}°C\n");
        }
        catch (CoreTemperatureReadException e)
        {
            Print($"[x] Исключение на шаге 6: {e.Message}\n");
        }
        
        // Шаг 7: Запись уровня радиации после отключения
        try
        {
            Print("Шаг 7 - запись уровня радиации после отключения...");
            var radiation = switch_.GetRadiationLevel();
            Print($"> {radiation:F2} мЗв/ч\n");
        }
        catch (CoreRadiationLevelReadException e)
        {
            Print($"[x] Исключение на шаге 7: {e.Message}\n");
        }
        
        // Шаг 8: Трансляция сообщения "Отключение завершено"
        try
        {
            Print("Шаг 8 - трансляция сообщения 'Отключение завершено'...");
            switch_.SignalShutdownComplete();
            Print("> OK\n");
        }
        catch (SignallingException e)
        {
            Print($"[x] Исключение на шаге 8: {e.Message}\n");
        }
        
        Print("=== ПРОЦЕДУРА ОТКЛЮЧЕНИЯ ЗАВЕРШЕНА ===");
    }

    
}