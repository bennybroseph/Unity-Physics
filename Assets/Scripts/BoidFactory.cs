using System;

using Utility.Vector;

public static class BoidFactory
{
    private static Random random = new Random();

    public static Boid Create(Vector3 minPosition, Vector3 maxPosition)
    {
        var position =
            new Vector3(
                random.Next((int)minPosition.x, (int)maxPosition.x),
                random.Next((int)minPosition.y, (int)maxPosition.y),
                random.Next((int)minPosition.z, (int)maxPosition.z));

        return new Boid(position);
    }
}
