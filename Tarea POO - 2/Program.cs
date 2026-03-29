using System;

public class Motor
{
    private int litros_de_aceite;
    private int potencia;

    public Motor(int potencia)
    {
        this.potencia = potencia;
        this.litros_de_aceite = 0;
    }

    public int GetLitrosDeAceite() { return litros_de_aceite; }
    public int GetPotencia() { return potencia; }

    public void SetLitrosDeAceite(int litros) { this.litros_de_aceite = litros; }
    public void SetPotencia(int potencia) { this.potencia = potencia; }
}

public class Coche
{
    private Motor motor;
    private string marca;
    private string modelo;
    private double precioAverias;

    public Coche(string marca, string modelo)
    {
        this.marca = marca;
        this.modelo = modelo;
        this.precioAverias = 0.0;
        this.motor = new Motor(100);
    }

    public Motor GetMotor() { return motor; }
    public string GetMarca() { return marca; }
    public string GetModelo() { return modelo; }
    public double GetPrecioAverias() { return precioAverias; }

    public void AcumularAveria(double importe)
    {
        this.precioAverias += importe;
    }
}

public class Garaje
{
    private Coche cocheActual;
    private string averiaAsociada;
    private int numeroCochesAtendidos;

    public Garaje()
    {
        this.cocheActual = null;
        this.numeroCochesAtendidos = 0;
    }

    public bool AceptarCoche(Coche coche, string averia)
    {
        if (this.cocheActual != null)
        {
            return false;
        }

        this.cocheActual = coche;
        this.averiaAsociada = averia;
        this.numeroCochesAtendidos++;
        return true;
    }

    public void DevolverCoche()
    {
        this.cocheActual = null;
    }
}

class PracticaPOO
{
    static void Main(string[] args)
    {
        Garaje miGaraje = new Garaje();
        Coche coche1 = new Coche("Toyota", "Corolla");
        Coche coche2 = new Coche("Honda", "Civic");
        Random generadorAleatorio = new Random();

        // --- PRIMERA ENTRADA ---
        AtenderCocheEnGaraje(miGaraje, coche1, "aceite", generadorAleatorio);
        AtenderCocheEnGaraje(miGaraje, coche2, "frenos", generadorAleatorio);

        // --- SEGUNDA ENTRADA ---
        AtenderCocheEnGaraje(miGaraje, coche1, "motor", generadorAleatorio);
        AtenderCocheEnGaraje(miGaraje, coche2, "aceite", generadorAleatorio);

        Console.WriteLine("--- RESULTADOS FINALES ---");
        MostrarInfoCoche(coche1);
        MostrarInfoCoche(coche2);

        Console.ReadLine();
    }

    static void AtenderCocheEnGaraje(Garaje garaje, Coche coche, string tipoAveria, Random rand)
    {
        if (garaje.AceptarCoche(coche, tipoAveria))
        {
            Console.WriteLine($"El garaje ha aceptado el coche {coche.GetMarca()} {coche.GetModelo()} con avería de '{tipoAveria}'.");

            double importeAveria = rand.NextDouble() * 500;
            coche.AcumularAveria(importeAveria);

            if (tipoAveria.ToLower() == "aceite")
            {
                int litrosActuales = coche.GetMotor().GetLitrosDeAceite();
                coche.GetMotor().SetLitrosDeAceite(litrosActuales + 10);
            }

            garaje.DevolverCoche();
            Console.WriteLine($"Coche devuelto. Total averías: ${coche.GetPrecioAverias():F2}");
        }
        else
        {
            Console.WriteLine("El garaje está ocupado. No puede aceptar otro coche.");
        }
    }

    static void MostrarInfoCoche(Coche coche)
    {
        Console.WriteLine($"Coche: {coche.GetMarca()} {coche.GetModelo()}");
        Console.WriteLine($"- Gasto total en averías: ${coche.GetPrecioAverias():F2}");
        Console.WriteLine($"- Litros de aceite: {coche.GetMotor().GetLitrosDeAceite()}L");
        Console.WriteLine($"- Potencia: {coche.GetMotor().GetPotencia()} CV");
    }
}