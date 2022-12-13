public static class Ease
{
    public static float InQuint(float x) => x*x*x*x*x;
    public static float OutQuint(float x) => -((1-x)*(1-x)*(1-x)*(1-x)*(1-x)) + 1;
    public static float InOutQuint(float x) => x < 0.5 ? 8 * x * x * x * x * x : 1 - 
        ((-2 * x + 2)*(-2 * x + 2)*(-2 * x + 2)*(-2 * x + 2)*(-2 * x + 2)) / 2;
    public static float OutBackQuint(float x) => -(x-1)*(x-1)*(x-1)*(x-1) 
        + 5*(x-1)*(x-1)*(x-1) + 5*(x-1)*(x-1) + 1;


    public static float InQuart(float x) => x*x*x*x;
    public static float OutQuart(float x) => -((1-x)*(1-x)*(1-x)*(1-x)) + 1;
    public static float InOutQuart(float x) => x < 0.5 ? 8 * x * x * x * x : 1 - 
        ((-2 * x + 2)*(-2 * x + 2)*(-2 * x + 2)*(-2 * x + 2)) / 2;
    public static float OutBackQuart(float x) => -(x-1)*(x-1)*(x-1)*(x-1) 
        + 4*(x-1)*(x-1)*(x-1) + 4*(x-1)*(x-1) + 1;
    

    public static float InCubic(float x) => x*x*x;
    public static float OutCubic(float x) => -((1-x)*(1-x)*(1-x)) + 1;
    public static float InOutCubic(float x) => x < 0.5 ? 4 * x * x * x : 1 - 
        ((-2 * x + 2)*(-2 * x + 2)*(-2 * x + 2)) / 2;
    public static float OutBackCubic(float x) => -(x-1)*(x-1)*(x-1)*(x-1) 
        + 3*(x-1)*(x-1)*(x-1) + 3*(x-1)*(x-1) + 1;
    

    public static float InQuad(float x) => x*x;
    public static float OutQuad(float x) => -((1-x)*(1-x)) + 1;
    public static float InOutQuad(float x) => x < 0.5 ? 2 * x * x : 1 - 
        ((-2 * x + 2)*(-2 * x + 2)) / 2;
    public static float OutBackQuad(float x) => -(x-1)*(x-1)*(x-1)*(x-1) 
        + 2*(x-1)*(x-1)*(x-1) + 2*(x-1)*(x-1) + 1;

    public static float Linear(float x) => x;
    public static float OutBackLinear(float x) => -(x-1)*(x-1)*(x-1)*(x-1) 
        + (x-1)*(x-1)*(x-1) + (x-1)*(x-1) + 1;
    

    public static float OutPowBack(float x, float p) => -(x-1)*(x-1)*(x-1)*(x-1) 
        + p*(x-1)*(x-1)*(x-1) + p*(x-1)*(x-1) + 1;
    public static float OutBack(float x) => 1 + 2.70158f*(x-1)*(x-1)*(x-1) 
        + 1.70158f*(x-1)*(x-1);


    public enum Type
    {
        In, Out, InOut, OutBack
    } 
    public enum Power
    {
        Linear, Quad, Cubic, Quart, Quint
    }
    
    public delegate float Function(float x);
    public static Function GetEase(Type type, Power power)
    {
        if(power == Power.Linear)         return Linear;
        else if(power == Power.Quad)
        {
            if     (type == Type.In)      return      InQuad;
            else if(type == Type.In)      return      InQuad;
            else if(type == Type.Out)     return     OutQuad;
            else if(type == Type.InOut)   return   InOutQuad;
            else if(type == Type.OutBack) return OutBackQuad;
        }
        else if(power == Power.Cubic)
        {
            if     (type == Type.In)      return      InCubic;
            else if(type == Type.In)      return      InCubic;
            else if(type == Type.Out)     return     OutCubic;
            else if(type == Type.InOut)   return   InOutCubic;
            else if(type == Type.OutBack) return OutBackCubic;
        }
        else if(power == Power.Quart)
        {
            if     (type == Type.In)      return      InQuart;
            else if(type == Type.In)      return      InQuart;
            else if(type == Type.Out)     return     OutQuart;
            else if(type == Type.InOut)   return   InOutQuart;
            else if(type == Type.OutBack) return OutBackQuart;
        }
        else if(power == Power.Quint)
        {
            if     (type == Type.In)      return      InQuint;
            else if(type == Type.In)      return      InQuint;
            else if(type == Type.Out)     return     OutQuint;
            else if(type == Type.InOut)   return   InOutQuint;
            else if(type == Type.OutBack) return OutBackQuint;
        }


        return Linear;
    }
}

