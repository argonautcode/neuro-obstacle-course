using System;

public static class BrainUtil
{
    public static double FastTanh(double x)
    {
        return 2 / (1 + FastExp(-2 * x)) - 1;
    }

    // https://stackoverflow.com/a/412988
    // https://nic.schraudolph.org/pubs/Schraudolph99.pdf
    public static double FastExp(double x)
    {
        long tmp = (long)(1512775 * x + 1072632447);
        return BitConverter.Int64BitsToDouble(tmp << 32);
    }
}