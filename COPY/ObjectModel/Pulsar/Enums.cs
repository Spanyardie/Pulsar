using System;

namespace Pulsar
{
    [Flags]
    public enum PropertyType
    {
        String = 0,
        Boolean = 1,
        Float = 2,
        Single = 4,
        Double = 8,
        Int = 16,
        Long = 32,
        DateTime = 64,
        Vector3 = 128,
        Point = 256,
        Colour = 512,
        Complex = 1024,
        Object = 2048
    }

    [Flags]
    public enum SceneObjectType
    {
        Camera = 0,
        Light = 1,
        Plane = 2,
        Node = 3
    }
}
