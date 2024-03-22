using System.Diagnostics.PerformanceData;

namespace LED5X7;


public  struct character
{
    public string c;
    public bool[,] led;

}
public static class GolobalValue
{
    public static int col = 5;
    public static int row = 7;
    public static character[] led;
    public static character[] multichar;
    public static int index; 
    public static int  MaxNumCharactoer;
    public static int MaxNumMulChar;


}