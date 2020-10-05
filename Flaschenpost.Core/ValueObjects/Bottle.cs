using Flaschenpost.Shared;

namespace Flaschenpost.Core.ValueObjects
{
    public class Bottle
    {
        public double Volume { get; set; }
        public string Material { get; set; }

        public override bool Equals(object obj)
        {
            var value = obj as Bottle;

            if (value is null)
                return false;

            return
                value.Volume == Volume
                && Material.IsEqualTo(value.Material);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Volume.GetHashCode();
                hash = hash * 23 + Material?.GetHashCode() ?? 0;
                return hash;
            }
        }
    }
}
