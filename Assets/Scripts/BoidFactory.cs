using System;

using Utility;

public static class BoidFactory
{
    private static Random random = new Random();

    public static Boid Create(Vector minPosition, Vector maxPosition)
    {
        var position =
            new Vector(
                random.Next((int)minPosition.x, (int)maxPosition.x),
                random.Next((int)minPosition.y, (int)maxPosition.y),
                random.Next((int)minPosition.z, (int)maxPosition.z));

        return new Boid(position);
    }
}
