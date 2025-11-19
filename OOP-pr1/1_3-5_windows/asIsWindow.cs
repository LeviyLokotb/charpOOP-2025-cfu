using Gtk;
using static Converter;
public class asIsWindow : templateWindow
{
    private ComboBoxText comboBoxFrom;
    private ComboBoxText comboBoxTo;
    public asIsWindow() : base()
    {
        controlBox.Append(Label.New("Преобразование с помощью as и is"));
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
        helpButton.SetLabel("≣ Сведения");
        helpButton.OnClicked += (sender, e) =>
        {
            Log("""
            * Player
            |
            * 
            |\
            | Mage -- уникальный метод Fireball()
            * 
            |\
            | Fighter -- уникальный метод Dismember()
            * 
             \
              Dragonborn -- уникальный метод DragonBreath()
            """);
        };
        MyTitle.Append(helpButton);

        // Кнопка очистки
        MyTitle.Append(clearButton);
        
        // Кнопка закрытия
        MyTitle.Append(closeButton);

        // Выпадающие менюшки
        comboBoxFrom = new ComboBoxText();
        comboBoxTo = new ComboBoxText();
        foreach (RPGClass s in Enum.GetValues(typeof(RPGClass)))
        {
            comboBoxFrom.AppendText($"{s}");
            comboBoxTo.AppendText($"{s}");
        }
        comboBoxFrom.SetActive(0);
        comboBoxTo.SetActive(0);

        // Кнопка
        var checkButton = new Button();
        checkButton.SetLabel("Проверить");
        checkButton.OnClicked += OnCheckButtonClicked;

        // Добавляем
        controlBox.Append(Label.New("Из:"));
        controlBox.Append(comboBoxFrom);
        controlBox.Append(Label.New("В:"));
        controlBox.Append(comboBoxTo);
        controlBox.Append(checkButton);
    }
    private void OnCheckButtonClicked(Button sender, EventArgs e)
    {
        string selected1 = comboBoxFrom.GetActiveText() ?? "Не выбрано";
        string selected2 = comboBoxTo.GetActiveText() ?? "Не выбрано";
        Log($"""
        ========================================
        Проверка: {selected1} -> {selected2}...
        ========================================
        """);
        

        dynamic p1 = selected1 switch
        {
            "Mage" => new Mage("Антон Городецкий"),
            "Fighter" => new Fighter("Дон Румата"),
            "Dragonborn" => new Dragonborn("Довакин"),
            _ => new Player("Пайтон Шарпович"),
        };
        Log($"{selected1} p = new {selected1}(\"{p1.Name}\")");

        switch (selected2)
        {
            case "Mage":
                // is
                Log("""
                // is
                if (p1 is Mage mage1)
                {
                    mage1.Attack();
                    mage1.Fireball();
                }
                """);
                Log("Проверка:");
                if (p1 is Mage mage1)
                {
                    Log($"{mage1.Attack()}\n{mage1.Fireball()}");
                }
                else
                {
                    Log("Получен null, приведение не удалось!");
                }

                // as
                Log(""" 
                // as
                Mage? mage2 = p as Mage;
                if (mage2 != null)
                {
                    mage2.Attack();
                    mage2.Fireball();
                }
                """);
                Mage? mage2 = p1 as Mage;
                Log("Проверка:");
                if (mage2 != null)
                {
                    Log($"{mage2.Attack()}\n{mage2.Fireball()}");
                }
                else
                {
                    Log("Получен null, приведение не удалось!");
                }
            
                break;
            case "Fighter":
                // is
                Log("""
                // is
                if (p is Fighter fighter1)
                {
                    fighter1.Attack();
                    fighter1.Dismember();
                }
                """);
                Log("Проверка:");
                if (p1 is Fighter fighter1)
                {
                    Log($"{fighter1.Attack()}\n{fighter1.Dismember()}");
                }
                else
                {
                    Log("Получен null, приведение не удалось!");
                }

                // as
                Log(""" 
                // as
                Fighter? fighter2 = p as Fighter;
                if (fighter2 != null)
                {
                    fighter2.Attack();
                    fighter2.Dismember();
                }
                """);
                Fighter? fighter2 = p1 as Fighter;
                Log("Проверка:");
                if (fighter2 != null)
                {
                    Log($"{fighter2.Attack()}\n{fighter2.Dismember()}");
                }
                else
                {
                    Log("Получен null, приведение не удалось!");
                }
                break;
            case "Dragonborn":
                // is
                Log("""
                // is
                if (p is Dragonborn dragon1)
                {
                    dragon1.Attack();
                    dragon1.DragonBreath();
                }
                """);
                Log("Проверка:");
                if (p1 is Dragonborn dragon1)
                {
                    Log($"{dragon1.Attack()}\n{dragon1.DragonBreath()}");
                }
                else
                {
                    Log("Получен null, приведение не удалось!");
                }

                // as
                Log(""" 
                // as
                Dragonborn? dragon2 = p1 as Dragonborn;
                if (dragon2 != null)
                {
                    dragon2.Attack();
                    dragon2.DragonBreath();
                }
                """);
                Dragonborn? dragon2 = p1 as Dragonborn;
                Log("Проверка:");
                if (dragon2 != null)
                {
                    Log($"{dragon2.Attack()}\n{dragon2.DragonBreath()}");
                }
                else
                {
                    Log("Получен null, приведение не удалось!");
                }
                break;
            default:
                // is
                Log("""
                // is
                if (p is Player player1)
                {
                    player1.Attack()
                }
                """);
                Log("Проверка:");
                if (p1 is Player player1)
                {
                    Log(player1.Attack());
                }
                else
                {
                    Log("Получен null, приведение не удалось!");
                }

                // as
                Log(""" 
                // as
                Player? player2 = p as Player;
                if (player2 != null)
                {
                    player2.Attack()
                }
                """);
                Player? player2 = p1 as Player;
                Log("Проверка:");
                if (player2 != null)
                {
                    Log(player2.Attack());
                }
                else
                {
                    Log("Получен null, приведение не удалось!");
                }
                break;
        } 

               
    }
}