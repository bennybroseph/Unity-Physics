using System;

using Utility.Vector;

public static class BoidFactory
{
    private static Random s_Random = new Random();

    public static Boid Create(Vector3 minPosition, Vector3 maxPosition)
    {
        var position =
            new Vector3(
                s_Random.Next((int)minPosition.x, (int)maxPosition.x),
                s_Random.Next((int)minPosition.y, (int)maxPosition.y),
                s_Random.Next((int)minPosition.z, (int)maxPosition.z));

        return new Boid(position);
    }
}
