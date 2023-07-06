using System.Drawing;
using Unity.VisualScripting;

public static class Globals
{
    public static int MaxDecalCount { get { return 1024; } }
    public static int PhysicalLayers { get { return 1 | (1 << 7); } }
    public static int InteractiveLayer { get { return 1 << 7; } }
}
