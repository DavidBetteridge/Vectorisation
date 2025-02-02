using System.Numerics;

namespace From0to10000;

public class SIMD {
    
    public int Add()
    {
        return 1 + 2 + 3 + 4 + 5 + 6 + 7 + DateTime.Now.Year;
    }
    
    public int Vector2()
    {
        var v1 = new Vector<int>([1, 2, 3, 4]);
        var v2 = new Vector<int>([5, 6, 7, DateTime.Now.Year]);
        var v3 = v1 + v2;
        return v3[0] + v3[1] + v3[2] + v3[3];
    }
    
}