namespace Threax.AspNetCore.Tracking
{
    public interface ITimestamper
    {
        void SetCreated(ICreated i);
        void SetModified(IModified i);
    }
}