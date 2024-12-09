namespace MyML.Functions
{
    public interface IActFunction
    {
        double ValueAtX(double x);

        double DerivativeAtX(double x);
    }
}
