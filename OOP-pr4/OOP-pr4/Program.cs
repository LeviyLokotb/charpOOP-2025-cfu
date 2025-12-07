
using MyMath;

// Complex n = Complex.CreateExpComplex(3.6, 0.982);

// Console.WriteLine(n);
// Console.WriteLine(n.ToString(true));

// Complex k = new(3, 4);

// Console.WriteLine(k);
// Console.WriteLine(k.Abs);

// Условие
double m = 70;
double v = 7;
double fi_degrees = 44.95;
double fi = Double.DegreesToRadians(fi_degrees);
double Om = 2 * Math.PI / 86400;

Console.WriteLine(
$"""
-------===[ Вычисление силы Кориолиса ] ===-------
F = -2m (v x om)

========== Условие ==========
Масса студента (m): {m} кг -- Скаляр
Скорость студента (v): {v} м/с (на юг) -- Вектор
Угловая скорость земли (Om): {Om*1e5}e-5 рад/с -- Вектор
Широта Симферополя (fi): {fi_degrees}∘ == {fi} рад -- Скаляр

""");

Console.Write("[ press Return to start ] ");
Console.ReadLine();


// Решение
double om = Om * Math.Cos(fi);
Vector3 vec_om = new(0, 0, om);
Vector3 vec_v = new(0, -v, 0);

Vector3 F = Functions.CariolisForce(m, vec_v, vec_om);

Console.WriteLine(
$"""

========== Решение ==========
om = Om * cos(fi) = {om*1e5}e-5 рад/с 

Переходим к векторам:
Пусть 
    x -- перпендикулярно вверх от поверхности Земли 
    y -- направление на Север    (-y -- Юг)
    z -- направление на Запад (-z -- Восток)
Тогда
    om = {vec_om}
    v  = {vec_v}

F = -2m (v x om) = {F} H

Модуль силы Кариолиса: {F.Abs} Н
Направление: +x (верно) 

""");
