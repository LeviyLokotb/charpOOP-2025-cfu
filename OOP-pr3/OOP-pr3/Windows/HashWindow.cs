using Gtk;
using System.Reflection;
using System.Security.Cryptography;

class HashWindow : baseWindow
{
    private TextView entry;
    private TextView result;
    private ComboBoxText comboBox;

    public HashWindow() : base()
    {
        // Widgets
        entry = WindowTools.AddEntry();
        entry.Buffer!.Text = "Sakharov,Lev,Fedorovich";

        // Получаем список алгоритмов 
        //string[] algorithms = FindAllHashAlgorithms();
        // переносим SHA256 в начало
        //algorithms = algorithms.Where(name => name!="SHA256").ToArray();
        //algorithms = ["SHA256", .. algorithms];

        comboBox = WindowTools.AddComboBox(["SHA256", "MD5"]);

        result = WindowTools.AddDisplay();

        Button encryptButton = WindowTools.AddButton("⇌", (sender, e) =>
        {
            /*
            SHA256 Sha256 = SHA256.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(entry!.Buffer!.Text ?? "");
            var hash_bytes = Sha256.ComputeHash(bytes);
            string hash_string = Convert.ToHexString(hash_bytes);
            result.Buffer!.Text = hash_string;
            */
            string text = comboBox.GetActiveText() ?? "SHA256";

            try 
            {
                // Тут указываем также название сборки -- это часть типа
                Type type = Type.GetType($"System.Security.Cryptography.{text}, System.Security.Cryptography.Algorithms")!;
                if (type == null)  throw new ArgumentException("Неверное название алгоритма");
                var hashMethod = typeof(HashAlg).GetMethod("Hash", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                if (hashMethod == null) throw new ArgumentException("Не удалось найти метод Hash");
                var genericHashMethod = hashMethod.MakeGenericMethod(type) ?? throw new ArgumentException("Не удалось создать дженерик");

                result.Buffer!.Text = (string)( genericHashMethod.Invoke(null, [entry!.Buffer!.Text ?? ""]) ?? "" );
            }
            catch {}
            
            //result.Buffer!.Text = HashAlg.Hash<SHA256>(entry!.Buffer!.Text ?? "");
        });

        // mainBox
        mainBox.Append(entry);
        mainBox.Append(comboBox);
        mainBox.Append(encryptButton);
        mainBox.Append(result);

        // controlPanel
        WindowTools.AddButton("∅ Очистить", controlPanel, (sender, e) =>
        {
            entry!.Buffer!.Text = "";
            result!.Buffer!.Text = "";
        });        
        controlPanel.Append(Label.New("Хэширование строки (SHA256)"));
        controlPanel.Append(exitButton);
    }

    private string[] FindAllHashAlgorithms()
    {
        string[] algorithms = [];
        // Сборки в котороых находятся алгоритмы
        var assemblys = new[]{Assembly.Load("System.Security.Cryptography")};
        try
        {
            // Ищем во всех сборках
            foreach(var assembly in assemblys)
            {
                foreach(var type in assembly.GetTypes()){ 
                    if(/*type.BaseType == typeof(HashAlgorithm) &&*/
                        type.GetMethod("Create", 
                            System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static, null, 
                            Type.EmptyTypes, null) != null
                    ) algorithms = [.. algorithms, type.Name];
                }
            }
        }
        catch {throw;}
        return algorithms;
    }
}