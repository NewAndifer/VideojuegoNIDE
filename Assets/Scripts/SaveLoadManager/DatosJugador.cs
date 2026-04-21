[System.Serializable]
public class DatosJugador
{
    public int id;
    public string correo;
    public string nombre;
    public int monedas;
    public string dificultad; 
    public Enemigo[] enemigosDerrotados;
}

[System.Serializable]
public class Enemigo
{
    public int id;       
    public bool derrotado; 
}
