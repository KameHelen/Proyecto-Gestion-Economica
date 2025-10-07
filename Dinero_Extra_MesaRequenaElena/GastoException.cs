using System;

public class GastoException : Exception
{
    public GastoException() : base("Saldo insuficiente para realizar el gasto") { }

    public GastoException(string message) : base(message) { }
}
