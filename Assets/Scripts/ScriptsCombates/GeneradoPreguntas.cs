
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public static class GeneradorPreguntas
{
    public struct DatosPregunta
    {
        public string textoOperacion;
        public int resultado;
        public List<int> numeros;
    }

    public static DatosPregunta Generar(int dificultad, string op)
    {
        op = (op ?? "suma").Trim().ToLower();

        List<int> nums = ObtenerNumerosAleatorios(dificultad, op);
        int resultadoCorrecto = CalcularResultado(op, nums);
        string texto = FormatearOperacion(op, nums);

        return new DatosPregunta
        {
            numeros = nums,
            resultado = resultadoCorrecto,
            textoOperacion = texto
        };

    }

    public static List<int> ObtenerNumerosAleatorios(int dificultad, string op)
    {
        List<int> lista = new List<int>();
        int rango = 10;

        switch (op)
        {
            case "suma":
                rango = (dificultad == 1) ? 10 : (dificultad == 2) ? 100 : (dificultad == 3) ? 500 : 1000;
                break;
            case "resta":
                rango = (dificultad == 1) ? 10 : (dificultad == 2) ? 100 : (dificultad == 3) ? 500 : 1000;;
                break;
            case "multiplicacion":
                rango = (dificultad == 1) ? 10 : (dificultad == 2) ? 50 : (dificultad == 3) ? 100 : 200;
                break;
            case "division":
                rango = (dificultad == 1) ? 10 : (dificultad == 2) ? 20 : (dificultad == 3) ? 50 : 100;
                break;
            default:
                rango = (dificultad == 1) ? 10 : (dificultad == 2) ? 100 : 1000;
                break;
        }

        for (int i = 0; i < 2; i++)
        {
            lista.Add(Random.Range(1, rango + 1));
        }

        if (op == "division")
        {
            lista.Sort();
            lista.Reverse();
            int dividendo = 1;
            for (int i = 0; i < lista.Count; i++)
            {
                dividendo *= lista[i];
            }

            lista[0] = dividendo;
        }

        if (op == "resta")
        {
            lista.Sort();
            lista.Reverse();
        }

        return lista;
    }

    public static int CalcularResultado(string op, List<int> nums)
    {
        switch (op)
        {
            case "suma": return nums[0] + nums[1];
            case "resta": return nums[0] - nums[1];
            case "multiplicacion": return nums[0] * nums[1];
            case "division": return nums[0] / nums[1];
            default: return 0;
        }
    }

    private static string FormatearOperacion(string op, List<int> nums)
    {
        char simbolo = op switch
        {
            "suma" => '+',
            "resta" => '-',
            "multiplicacion" => 'x',
            _ => '/'
        };
        return $"{nums[0]} {simbolo} {nums[1]}";
    }





}